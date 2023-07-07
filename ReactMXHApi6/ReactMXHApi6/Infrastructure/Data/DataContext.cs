using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ReactMXHApi6.Core.Entities;

namespace ReactMXHApi6.Infrastructure.Data
{
    public class DataContext : IdentityDbContext<AppUser>
    {
        public DataContext(DbContextOptions options) : base(options) { }

        // Define as entidades DbSet para acesso aos conjuntos de dados correspondentes no banco de dados
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Connection> Connections { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<LastMessageChat> LastMessageChats { get; set; }
        public DbSet<PlayerIds> PlayerIds { get; set; }
        public DbSet<ImageOfPost> ImageOfPosts { get; set; }

        // Configura os relacionamentos e restrições de exclusão no modelo de banco de dados
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Define o relacionamento entre a entidade Message e a entidade AppUser como destinatário
            builder.Entity<Message>()
                .HasOne(u => u.Recipient)
                .WithMany(m => m.MessagesReceived)
                .HasForeignKey(u => u.RecipientId)
                .OnDelete(DeleteBehavior.Restrict);

            // Define o relacionamento entre a entidade Message e a entidade AppUser como remetente
            builder.Entity<Message>()
                .HasOne(u => u.Sender)
                .WithMany(m => m.MessagesSent)
                .HasForeignKey(u => u.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            // Define o relacionamento entre a entidade Post e a entidade AppUser
            builder.Entity<Post>()
                .HasOne(p => p.User)
                .WithMany(u => u.Posts)
                .HasForeignKey(p => p.UserId);

            // Define o relacionamento entre a entidade Comment e a entidade AppUser
            builder.Entity<Comment>()
                .HasOne(p => p.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(p => p.UserId);

            // Define o relacionamento entre a entidade Comment e a entidade Post
            builder.Entity<Comment>()
                .HasOne(p => p.Post)
                .WithMany(u => u.Comments)
                .HasForeignKey(p => p.PostId);

            // Define o relacionamento entre a entidade LastMessageChat e a entidade AppUser como destinatário
            builder.Entity<LastMessageChat>()
                .HasOne(u => u.Recipient)
                .WithMany(m => m.LastMessageChatsReceived)
                .HasForeignKey(u => u.RecipientId)
                .OnDelete(DeleteBehavior.Restrict);

            // Define o relacionamento entre a entidade LastMessageChat e a entidade AppUser como remetente
            builder.Entity<LastMessageChat>()
                .HasOne(u => u.Sender)
                .WithMany(m => m.LastMessageChatsSent)
                .HasForeignKey(u => u.SenderId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
