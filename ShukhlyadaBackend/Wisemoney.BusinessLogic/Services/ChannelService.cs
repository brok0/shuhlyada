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

            channel.UsersPermissions = new List<AccessLevel> { new AccessLevel { AccountId=creatorId, Permissions="creator;"} };

            var newChannel = _channelRepository.Insert(channel);
            await _channelRepository.UnitOfWork.SaveChangesAsync();
            return newChannel;
        }

        public async Task<List<Channel>> GetAllChannelsAsync()
        {
            var channels = await _channelRepository.GetAllAsync();
            return channels.ToList();
        }

        public async Task<Channel> GetChannelAsync(string name)
        {
            return await _channelRepository.GetByIdAsync(name);
        }

        public async Task SubscribeToChannelAsync(Guid UserId, string ChannelName)
        {
            var spec = new UserWithPermissionsSpecification(UserId);
            var user = await _accountRepository.GetSingleAsync(spec);
            var channel = await _channelRepository.GetByIdAsync(ChannelName);



            if(channel.Subscribers == null)
            {
                channel.Subscribers = new List<Account>();
            }
            else if(channel.Subscribers.Contains(user))//channel cant have 0 subs
            {
                throw new UserAlreadySubscribedException();
            }
            else if(user.PermissionsInChannels.FirstOrDefault(x => x.ChannelId.Equals(ChannelName)).Permissions.FindPermission("creator"))
            {
                throw new CreatorCantLeaveFromChannelException();
            }
            channel.Subscribers.Add(user);

            _channelRepository.Update(channel);
            await _channelRepository.UnitOfWork.SaveChangesAsync();
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

        public async Task<string> DeletePostAsync(Guid id)
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

        public async Task<Channel> GetChannelWithPostsAsync(string channelName) 
        {
            var spec = new ChannelWithPostsSpecification(channelName);
            var channelWithPosts = await _channelRepository.GetSingleAsync(spec);
            return channelWithPosts;
        }

        public async Task<Post> GetPostByIdAsync(Guid id)
        {
            return await _postRepository.GetByIdAsync(id);
        }

        public async Task<int> LikePostAsync(Guid postId,Guid userId)
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

            _postRepository.Update(post);
            await _postRepository.UnitOfWork.SaveChangesAsync();

            return post.UsersLiked.Count();
            
        }

        public async Task<Comment> LeaveCommentAsync(Comment comment, Guid byUserId)
        {
            var post = await _postRepository.GetByIdAsync(comment.PostId);
           
            if(post.Comments == null)
            {
                post.Comments = new List<Comment>();
            }

            comment.AccountId = byUserId;
            post.Comments.Add(comment);

            _postRepository.Update(post);
            await _postRepository.UnitOfWork.SaveChangesAsync();
            return comment;
        }


        //public async Task<int> LikeComment(Guid commentId, Guid userId)
        //{
        //    var post = await _postRepository.GetByIdAsync(commentId);

        //    var user = await _accountRepository.GetByIdAsync(userId);

        //    var isUserLiked = post.UsersLiked.Contains(user);

        //    if (post.UsersLiked == null)
        //    {
        //        post.UsersLiked = new List<Account>();
        //    }

        //    if (isUserLiked)
        //    {
        //        post.UsersLiked.Remove(user);  // if user likes second time his like automatically removes
        //    }
        //    else
        //    {
        //        post.UsersLiked.Add(user);
        //    }


        //    await _postRepository.UnitOfWork.SaveChangesAsync();

        //    return post.UsersLiked.Count();

        //}



    }
}
