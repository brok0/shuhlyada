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

        public ChannelService(IChannelRepository channelRepository,IPostRepository postRepository)
        {
            _channelRepository = channelRepository;
            _postRepository = postRepository;
        }
        // -----------CHANNEL------------

        public async Task<Channel> CreateChannelAsync(Channel channel, Guid creatorId)
        {
            if (await _channelRepository.GetByNameAsync(channel.Name) != null)
            {
                throw new ChannelAlreadyExistException();
            }

            channel.UsersPermissions = new List<AccessLevel> { new AccessLevel { AccountId=creatorId, Permissions="creator"} };

            var newChannel = _channelRepository.Insert(channel);
            await _channelRepository.UnitOfWork.SaveChangesAsync();
            return newChannel;
        }

        public async Task<Channel> GetChannelByNameAsync(string name)
        {
            return await _channelRepository.GetByNameAsync(name);
        }

        // --------------POSTS-------------- // untested
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

        public async Task<List<Post>> GetAllPostsForChannel(Guid channelId) 
        {
            var posts = new PostsInChannelSpecification(channelId);
            var postList = await _postRepository.GetAsync(posts);
            return postList.ToList();
        }

        public async Task<Post> GetPostByIdAsync(Guid id)
        {
            return await _postRepository.GetByIdAsync(id);
        }
    }
}
