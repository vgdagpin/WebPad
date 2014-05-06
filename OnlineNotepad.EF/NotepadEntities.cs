using OnlineNotepad.EF.Contract;
using OnlineNotepad.EF.Model;
using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;

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
            try
            {
                foreach (var item in ChangeTracker.Entries<IAuditedEntity>())
                    item.Entity.CreatedOn = DateTime.Now;

                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var errors = ex.EntityValidationErrors
                    .Cast<DbEntityValidationResult>();

                var xx = string.Empty;

                throw;

            }
            catch (Exception ex)
            {

                throw;
            }
            
        }

        public class DBInitializer : DropCreateDatabaseAlways<NotepadEntities>
        {
            protected override void Seed(NotepadEntities context)
            {
                context.Database.ExecuteSqlCommand("ALTER TABLE tbl_Users ADD CONSTRAINT UQ_Email UNIQUE (Email)");
                context.Database.ExecuteSqlCommand("ALTER TABLE tbl_Notepads ADD CONSTRAINT UQ_Alias UNIQUE (Alias)");
            }
        }
    }
}