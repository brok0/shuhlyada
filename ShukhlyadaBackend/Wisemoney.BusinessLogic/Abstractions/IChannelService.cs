using Shukhlyada.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shukhlyada.BusinessLogic.Abstractions
{
    public interface IChannelService
    {
        public Task<Channel> CreateChannelAsync(Channel channel, Guid creatorId);
        public Task<Channel> GetChannelByNameAsync(string name);

        public Task<List<Post>> GetAllPostsForChannel(Guid channelId);

        public Task<Post> CreatePostAsync(Post post);

        public Task<Post> GetPostByIdAsync(Guid id);

        public Task<string> DeletePost(Guid id); // returning string to show user that post with its name was deleted



    }
}
