using Shukhlyada.BusinessLogic.Abstractions;
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
    public class ChannelService : IChannelService
    {
        private readonly IChannelRepository _channelRepository;
        private readonly IPostRepository _postRepository;
        private readonly IAccountRepository _accountRepository;
   

        public ChannelService(IChannelRepository channelRepository,IPostRepository postRepository,IAccountRepository accountRepository)
        {
            _channelRepository = channelRepository;
            _postRepository = postRepository;
            _accountRepository = accountRepository;
        }
        // -----------CHANNEL------------

        public async Task<Channel> CreateChannelAsync(Channel channel, Guid creatorId)
        {
            if (await _channelRepository.GetByIdAsync(channel.Id) != null)
            {
                throw new ChannelAlreadyExistException();
            }

            channel.UsersPermissions = new List<AccessLevel> { new AccessLevel { AccountId=creatorId, Permissions="creator"} };

            var newChannel = _channelRepository.Insert(channel);
            await _channelRepository.UnitOfWork.SaveChangesAsync();
            return newChannel;
        }

        public async Task<Channel> GetChannelAsync(string name)
        {
            return await _channelRepository.GetByIdAsync(name);
        }
        public async Task SubscribeToChannel(Guid UserId, string ChannelName)
        {
            var user = await _accountRepository.GetByIdAsync(UserId);
            var channel = await _channelRepository.GetByIdAsync(ChannelName);

            //channel cant have 0 subs

            if(channel.Subscribers.Contains(user))
            {
                throw new UserAlreadySubscribedException();
            }

            channel.Subscribers.Add(user);
            
        }

        // --------------POSTS-------------- 
        public async Task<Post> CreatePostAsync(Post post,Guid creatorId)
        {
            post.AccountId = creatorId;
            var channel = await _channelRepository.GetByIdAsync(post.ChannelId);

            if (channel.Posts == null)
                channel.Posts = new List<Post>(){post};

            channel.Posts.Add(post);

            _postRepository.Insert(post);
            await _postRepository.UnitOfWork.SaveChangesAsync();

            return post; 
        }

        public async Task<string> DeletePost(Guid id)
        {

            var postToDelete = await _postRepository.GetByIdAsync(id);
            if(postToDelete == null)
            {
                throw new PostDeletionException();
            }
              _postRepository.Delete(postToDelete);
            await _postRepository.UnitOfWork.SaveChangesAsync();
            return postToDelete.Title;
        }

        public async Task<List<Post>> GetAllPostsForChannel(string channelName) 
        {
            var posts = new ChannelWithPostsSpecification(channelName);
            var postList = await _postRepository.GetAsync(posts);
            return postList.ToList();
        }

        public async Task<Post> GetPostByIdAsync(Guid id)
        {
            return await _postRepository.GetByIdAsync(id);
        }

        public async Task<int> LikePost(Guid postId,Guid userId)
        {
            var post =  await _postRepository.GetByIdAsync(postId);

            var user = await _accountRepository.GetByIdAsync(userId);

            var isUserLiked = post.UsersLiked.Contains(user);

            if (post.UsersLiked == null)
            {
                post.UsersLiked = new List<Account>();
            }
            
            if (isUserLiked)
            {
                post.UsersLiked.Remove(user);  // if user likes second time his like automatically removes
            }
            else
            {
                post.UsersLiked.Add(user);
            }


            await _postRepository.UnitOfWork.SaveChangesAsync();

            return post.UsersLiked.Count();
            
        }

        public async Task<Comment> LeaveComment(Comment comment, Guid byUserId)
        {
            var post = await _postRepository.GetByIdAsync(comment.PostId);
           
            if(post.Comments == null)
            {
                post.Comments = new List<Comment>();
            }

            comment.AccountId = byUserId;
            post.Comments.Add(comment);

            await _postRepository.UnitOfWork.SaveChangesAsync();
            return comment;
        }

        
       
    }
}
