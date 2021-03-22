using Microsoft.EntityFrameworkCore;
using Shukhlyada.Domain.Models;
using Shukhlyada.Infrastructure.Abstractions;

namespace Shukhlyada.Infrastructure
{
    public class AppDbContext: DbContext, IUnitOfWork
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Channel> Channels { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Report> Reports { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //Accounts
            builder.Entity<Account>(a =>
            {
                a.HasKey(a => a.Id);

                a.Property(a => a.Username)
                .HasMaxLength(25)
                .IsRequired();

                a.Property(a => a.Password)
                .IsRequired();

                a.Property(a => a.Email)
                .HasMaxLength(300)
                .IsRequired();

                a.Property(a => a.Type)
                .HasDefaultValue(AccountType.User)
                .IsRequired();

                a.Property(a => a.ProfilePictureId)
                .HasDefaultValue(0);

                a.Property(a => a.RegisterDate)
                .HasDefaultValueSql("getdate()");

                a.Property(a => a.Salt)
                .IsRequired();
            });


            //Channels
            builder.Entity<Channel>(c =>
            {
                c.HasKey(c => c.Id);

                c.Property(c => c.Name)
                .HasMaxLength(100)
                .IsRequired();

                c.Property(c => c.Description)
                .IsRequired();

                c.Property(c => c.DateOfCreation)
                .HasDefaultValueSql("getdate()");
            });

            //Subscriptions
            builder.Entity<Account>()
                .HasMany(a => a.Subscriptions)
                .WithMany(c => c.Subscribers)
                .UsingEntity(x => x.ToTable("Subscriptions"));

            //Moderation
            builder.Entity<Account>()
                .HasMany(a => a.ChannelsWithPermissions)
                .WithMany(c => c.UsersWithPermissions)
                .UsingEntity<AccessLevel>(
                    al => al
                        .HasOne(al => al.Channel)
                        .WithMany(c => c.UsersPermissions)
                        .HasForeignKey(al => al.ChannelId),
                    al => al
                        .HasOne(al => al.Account)
                        .WithMany(a => a.PermissionsInChannels)
                        .HasForeignKey(m => m.AccountId),
                    al =>
                    {
                        //Moderation settings
                        al.HasKey(al => new { al.AccountId, al.ChannelId });

                        al.Property(al => al.Permissions)
                        .IsRequired();
                    });

            //Post
            builder.Entity<Account>()
                .HasMany(a => a.PostedInChannels)
                .WithMany(c => c.UsersThatPosted)
                .UsingEntity<Post>(
                    p => p
                        .HasOne(p => p.Channel)
                        .WithMany(c => c.Posts)
                        .HasForeignKey(p => p.ChannelId)
                        .OnDelete(DeleteBehavior.Cascade),
                    p => p
                        .HasOne(p => p.Account)
                        .WithMany(a => a.CreatedPosts)
                        .HasForeignKey(p => p.AccountId)
                        .OnDelete(DeleteBehavior.NoAction),
                    p =>
                    {
                        //Post settings
                        p.HasKey(p => p.Id);

                        p.Property(p => p.ChannelId)
                        .IsRequired();

                        p.Property(p => p.AccountId)
                        .IsRequired();

                        p.Property(p => p.PublishedDate)
                        .HasDefaultValueSql("getdate()");

                        p.Property(p => p.Title)
                        .HasMaxLength(50)
                        .IsRequired();

                        p.Property(p => p.Content)
                        .IsRequired();
                    });

            //Comment
            builder.Entity<Account>()
                .HasMany(a => a.CommentedOn)
                .WithMany(p => p.UsersCommented)
                .UsingEntity<Comment>(
                    c => c
                        .HasOne(c => c.Post)
                        .WithMany(p => p.Comments)
                        .HasForeignKey(c => c.PostId),
                    c => c
                        .HasOne(c => c.Account)
                        .WithMany(a => a.CreatedComments)
                        .HasForeignKey(c => c.AccountId)
                        .OnDelete(DeleteBehavior.NoAction),
                    c =>
                    {
                        //Comment settings
                        c.HasKey(c => new { c.Id, c.PostId });

                        c.Property(c => c.AccountId)
                        .IsRequired();

                        c.Property(c => c.Content) //обмеження?
                        .IsRequired();


                    });

            //reports
            builder.Entity<Report>(r =>
            {
                r.HasKey(r => r.Id);

                r.HasOne(r => r.ReportedChannel)
                .WithMany(c => c.Reports)
                .HasForeignKey(r => r.ChannelId);

                r.HasOne(r => r.ReportedPost)
                .WithMany(p => p.Reports)
                .HasForeignKey(r => r.PostId);

                r.Property(r => r.Reason)
                .IsRequired();
            });

            //PostLikes
            builder.Entity<Account>()
                .HasMany(a => a.LikedPosts)
                .WithMany(p => p.UsersLiked)
                .UsingEntity(x => x.ToTable("PostLikes"));

            //CommentLikes
            builder.Entity<Account>()
                .HasMany(a => a.LikedComments)
                .WithMany(c => c.UsersLiked)
                .UsingEntity(x => x.ToTable("CommentLikes"));

        }
    }
}
