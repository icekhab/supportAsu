
using System.Data.Entity;
using SupportAsu.Model;

namespace SupportAsu.EntityFramework
{
    public class SupportAsuContext : DbContext
    {
        public DbSet<Claim> Claim { get; set; }
        public DbSet<ClaimTask> ClaimTask { get; set; }
        //private DbSet<Comment> Comment { get; set; }
        public DbSet<CommentClaim> CommentClaim { get; set; }
        public DbSet<CommentTask> CommentTask { get; set; }
        public DbSet<Dictionary> Dictionary { get; set; }
        public DbSet<DictionaryValue> DictionaryValue { get; set; }
        public DbSet<Task> Task { get; set; }
        public DbSet<TaskExecutors> TaskExecutors { get; set; }
        public DbSet<ViewedClaim> ViewedClaim { get; set; }
        public DbSet<Equipment> Equipment { get; set; }
        public DbSet<ProjectorShedule> ProjectorShedule { get; set; }
        public DbSet<Prophylaxis> Prophylaxis { get; set; }
        public DbSet<Purchase> Purchase { get; set; }
        public DbSet<PurchaseDetail> PurchaseDetail { get; set; }
        public DbSet<User> User { get; set; }

        public SupportAsuContext()
            : base("SupportAsuConnetion")
        {

        }
    }
}
