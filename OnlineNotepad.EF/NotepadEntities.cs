using OnlineNotepad.EF.Contract;
using OnlineNotepad.EF.Model;
using System;
using System.Data.Entity;

namespace OnlineNotepad.EF
{
    public class NotepadEntities : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Notepad> Notepads { get; set; }

        public NotepadEntities()
            : base("name=NotepadConnection")
        {
            Database.SetInitializer(new DBInitializer());
        }

        public override int SaveChanges()
        {
            foreach (var item in ChangeTracker.Entries<IAuditedEntity>())
                item.Entity.CreatedOn = DateTime.Now;

            return base.SaveChanges();
        }

        public class DBInitializer : CreateDatabaseIfNotExists<NotepadEntities>
        {
            protected override void Seed(NotepadEntities context)
            {
                context.Database.ExecuteSqlCommand("ALTER TABLE tbl_Users ADD CONSTRAINT UQ_Email UNIQUE (Email)");
            }
        }
    }
}