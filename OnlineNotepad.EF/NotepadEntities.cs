using OnlineNotepad.EF.Contract;
using OnlineNotepad.EF.Model;
using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;

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
                StringBuilder sb = new StringBuilder();
                
                ex.EntityValidationErrors.First()
                    .ValidationErrors
                    .ToList()
                    .ForEach(e =>
                    {
                        sb.AppendLine(e.PropertyName + ": " + e.ErrorMessage);
                    });

                throw new Exception(sb.ToString());
            }
        }

        public class DBInitializer : CreateDatabaseIfNotExists<NotepadEntities>
        {
            protected override void Seed(NotepadEntities context)
            {
                context.Database.ExecuteSqlCommand("ALTER TABLE tbl_Users ADD CONSTRAINT UQ_Email UNIQUE (Email)");
                context.Database.ExecuteSqlCommand("ALTER TABLE tbl_Notepads ADD CONSTRAINT UQ_Alias UNIQUE (Alias)");
            }
        }
    }
}