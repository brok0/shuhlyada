using Shukhlyada.BusinessLogic.Abstractions;
using Shukhlyada.BusinessLogic.Exstensions;
using Shukhlyada.BusinessLogic.Specifications;
using Shukhlyada.Domain.Exceptions;
using Shukhlyada.Domain.Models;
using Shukhlyada.Infrastructure.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shukhlyada.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IPostRepository _postRepository;
        private readonly IMailService _mailService;

        public UserService(IAccountRepository accountRepository, IMailService mailService, IPostRepository postRepository)
        {
            _accountRepository = accountRepository;
            _postRepository = postRepository;
            _mailService = mailService;
        }

        public async Task<Account> AuthenticateAsync(string email, string password)
        {
            var user = await _accountRepository.GetByEmailAsync(email);

            if (user == null)
            {
                throw new UserNotFoundException();
            }

            var hashedPassword = password.SHA2Hash(user.Salt);
            if (user.Password == hashedPassword)
            {
                return user;
            }

            throw new UserNotFoundException();

        }

        public async Task<Account> CreateAccountAsync(Account acc)
        {
            if ((await _accountRepository.GetByEmailAsync(acc.Email)) != null)
            {
                throw new UserAlreadyExistException();
            }

            var salt = hashExtension.GenerateSalt();

            acc.Password = acc.Password.SHA2Hash(salt);
            acc.Salt = salt;
            acc.Type = AccountType.User;

            
            Account insertedAccount = _accountRepository.Insert(acc);

            await _accountRepository.UnitOfWork.SaveChangesAsync();

            return insertedAccount;
        }

        public async Task SendChangePasswordMailAsync(string email)
        {
            if ((await _accountRepository.GetByEmailAsync(email)) != null)
            {
                throw new UserNotFoundException();
            }
            string subject = "Recover Password";
            string body = "Recover Password";
            await _mailService.SendMailAsync(email, subject, body, false);
        }

        public async Task<List<Channel>> GetSubscribedChannelsAsync(Guid userId)
        {
            var channelsSpec = new UserSubscribedChannelsSpecification(userId);
            var userchannels = await _accountRepository.GetSingleAsync(channelsSpec);


            var channels = userchannels.Subscriptions.ToList();

            if(channels.Count==0)
            { throw new UserNotSubscribedToAnyChannelException(); }

            return channels;
        }

        public async Task<List<Post>> GetCreatedPostsAsync(Guid userId)
        {
            var spec = new UserCreatedPostsSpecification(userId);
            var userWithPosts = await _accountRepository.GetSingleAsync(spec);

            var posts = userWithPosts.CreatedPosts;
            
            if(posts == null)
            { throw new PostNotFoundException(); }
            
            return posts.ToList();

        }

        public async Task ChangeAvatar(Guid userId,int imageId)
        {
            var user = await _accountRepository.GetByIdAsync(userId);

            user.ProfilePictureId = imageId;

            _accountRepository.Update(user);
            await _accountRepository.UnitOfWork.SaveChangesAsync();

        }

        public async Task<List<Post>> GetLikedPostsAsync(Guid userId)
        {
            var user = await _accountRepository.GetByIdAsync(userId);
            var spec = new UserLikedPostsSpecification(user);

            var userWithPosts = await _postRepository.GetAsync(spec);

            //var posts = userWithPosts.LikedPosts;

            if(userWithPosts == null)
            {
                throw new PostNotFoundException();
            }
            return userWithPosts.ToList();
        }
    }
}
