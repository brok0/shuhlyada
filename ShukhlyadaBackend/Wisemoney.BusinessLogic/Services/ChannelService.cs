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
using Shukhlyada.BusinessLogic.Exstensions;
namespace Shukhlyada.BusinessLogic.Services
{
    public class ChannelService : IChannelService
    {
        private readonly IChannelRepository _channelRepository;
        private readonly IPostRepository _postRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IReportRepository _reportRepository;


        public ChannelService(IChannelRepository channelRepository, IPostRepository postRepository, IAccountRepository accountRepository, IReportRepository reportRepository)
        {
            _channelRepository = channelRepository;
            _postRepository = postRepository;
            _accountRepository = accountRepository;
            _reportRepository = reportRepository;
        }
        // -----------CHANNEL------------

        public async Task<Channel> CreateChannelAsync(Channel channel, Guid creatorId)
        {
            if (await _channelRepository.GetByIdAsync(channel.Id) != null)
            {
                throw new ChannelAlreadyExistException();
            }

            channel.UsersPermissions = new List<AccessLevel> { new AccessLevel { AccountId = creatorId, Permissions = "creator;" } };

            var newChannel = _channelRepository.Insert(channel);
            await _channelRepository.UnitOfWork.SaveChangesAsync();
            await SubscribeToChannelAsync(creatorId, channel.Id);
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
            var userSpec = new UserWithPermissionsSpecification(UserId);
            var user = await _accountRepository.GetSingleAsync(userSpec);
            var channelSpec = new ChannelWithSubscribersSpecification(ChannelName);
            var channel = await _channelRepository.GetSingleAsync(channelSpec);



            if (channel.Subscribers == null)
            {
                channel.Subscribers = new List<Account>();
            }
            else if (channel.Subscribers.Contains(user)) // like "like post" works,on first request user is subscribed on second is unsubscribed
            {
                //throw new UserAlreadySubscribedException();
                channel.Subscribers.Remove(user);
                _channelRepository.Update(channel);
                await _channelRepository.UnitOfWork.SaveChangesAsync();
                return;
            }
            //else if(user.PermissionsInChannels.FirstOrDefault(x => x.ChannelId.Equals(ChannelName)).Permissions.FindPermission("creator"))  // unsubscribe
            //{
            //    throw new CreatorCantLeaveFromChannelException();
            //}
            channel.Subscribers.Add(user);

            _channelRepository.Update(channel);
            await _channelRepository.UnitOfWork.SaveChangesAsync();
        }

        // --------------POSTS-------------- 
        public async Task<Post> CreatePostAsync(Post post, Guid creatorId)
        {
            post.AccountId = creatorId;
            var channel = await _channelRepository.GetByIdAsync(post.ChannelId);

            if (channel.Posts == null)
                channel.Posts = new List<Post>() { post };

            channel.Posts.Add(post);

            _postRepository.Insert(post);
            await _postRepository.UnitOfWork.SaveChangesAsync();

            return post;
        }

        public async Task<string> DeletePostAsync(Guid id)
        {

            var postToDelete = await _postRepository.GetByIdAsync(id);
            if (postToDelete == null)
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
            var sortedPosts = channelWithPosts.Posts.OrderByDescending(x => x.PublishedDate).ToList();
            channelWithPosts.Posts = sortedPosts;
           
            return channelWithPosts;
        }

        public async Task<Post> GetPostByIdAsync(Guid id)
        {
            return await _postRepository.GetByIdAsync(id);
        }

        public async Task<int> LikePostAsync(Guid postId, Guid userId)
        {
            var post = await _postRepository.GetByIdAsync(postId);

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
        // ---------------COMMENTS-------------------
        public async Task<Comment> LeaveCommentAsync(Comment comment, Guid byUserId)
        {
            var post = await _postRepository.GetByIdAsync(comment.PostId);

            if (post.Comments == null) // getById gets post with comments / doesnt checks the condition
            {
                post.Comments = new List<Comment>();
            }

            comment.AccountId = byUserId;
            post.Comments.Add(comment);

            _postRepository.Update(post);
            await _postRepository.UnitOfWork.SaveChangesAsync();
            return comment;
        }


        // --------------REPORTS---------------------

        public async Task<Report> CreatePostReportAsync(Report report)
        {
            var post = await _postRepository.GetByIdAsync(report.PostId.Value);

            if (post == null)
            {
                throw new PostNotFoundException();
            }

            if (post.Reports == null)
            {
                post.Reports = new List<Report>();
            }

            //report.ChannelId = post.ChannelId;

            // var channel = await _channelRepository.GetByIdAsync(post.ChannelId);
            // report.ReportedChannel = channel;

            post.Reports.Add(report);

            _postRepository.Update(post);
            await _postRepository.UnitOfWork.SaveChangesAsync();
            return report;
        }


        public async Task<List<Report>> GetReportsForPostAsync(Guid PostId, Guid UserId)
        {
            var post = await _postRepository.GetByIdAsync(PostId);
            var user = await _accountRepository.GetByIdAsync(UserId);

            var spec = new ReportsForOnePostSpecification(PostId);
            var reportList = await _reportRepository.GetAsync(spec);

            if (reportList == null)
                throw new ReportsNotFoundException();
            //if (user.ChannelsWithPermissions.Contains(post.Channel))  user must have permission 
            // throw new UserDontHavePermission();
            var mapResult = new List<Report>();

            // foreach (var r in reportList)
            //     mapResult.Add(r);

            return reportList.ToList();
        }


        public async Task<List<Report>> GetReportsForChannelAsync(string ChannelId, Guid UserId)
        {
            var channel = await _channelRepository.GetByIdAsync(ChannelId);
            var user = await _accountRepository.GetByIdAsync(UserId);
            var reports = channel.Reports;


            return reports.ToList();

        }

        public async Task<List<Report>> GetReportsForPostsInChannelAsync(string ChannelId, Guid UserId)
        {
            //var user = await _accountRepository.GetByIdAsync(UserId);

            var spec = new ReportsForPostsInChannelSpecification(ChannelId);
            var postWithReport = await _postRepository.GetAsync(spec);


            if (postWithReport == null)
                throw new ReportsNotFoundException();

           /* var userWithPermissions = new PermissionInChannelSpecification(UserId);
            var allPermissions = await _accountRepository.GetSingleAsync(userWithPermissions);

            var permissionInChannel = allPermissions.PermissionsInChannels.FirstOrDefault(x => x.ChannelId == ChannelId);  /// under construction

            if (permissionInChannel.Permissions.FindPermission(""))  //user must have permission 
             throw new UserDontHavePermissionException();
           */
            var reportList = new List<Report>();

            foreach (var p in postWithReport)
            {
                var report = p.Reports;
                foreach (var r in report)
                {
                    reportList.Add(r);

                }

            }

            return reportList.ToList();

        }
    }
}
