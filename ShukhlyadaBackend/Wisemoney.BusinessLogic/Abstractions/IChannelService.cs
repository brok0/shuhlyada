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

        public Task<Channel> GetChannelAsync(string name);

        public Task<List<Channel>> GetAllChannelsAsync();

        public Task<Channel> GetChannelWithPostsAsync(string channelName);

        public Task<Post> CreatePostAsync(Post post,Guid creatorId);

        public Task<Post> GetPostByIdAsync(Guid id);

        public Task<string> DeletePostAsync(Guid id); // returning string to show user that post with its name was deleted

        public Task<int> LikePostAsync(Guid postId,Guid userId);

        public Task<Comment> LeaveCommentAsync(Comment comment,Guid byUserId);

        public Task SubscribeToChannelAsync(Guid UserId, string ChannelId);

        public  Task<List<Report>> GetReportsForChannelAsync(string ChannelId, Guid UserId);

        public  Task<List<Report>> GetReportsForPostAsync(Guid PostId, Guid UserId);
        public  Task<Report> CreatePostReportAsync(Report report);
        public  Task<List<Report>> GetReportsForPostsInChannelAsync(string ChannelId, Guid UserId);

    }
}
