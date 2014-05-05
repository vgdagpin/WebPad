using OnlineNotepad.EF.Contract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineNotepad.EF.Model
{
    [Table("tbl_Notepads")]
    public class Notepad : IAuditedEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid NotepadID { get; set; }

        public string Content { get; set; }

        [Required, MaxLength(255)]
        public string Alias { get; set; }

        [Required, MaxLength(100)]
        public string Title { get; set; }

        public Guid? UserID { get; set; }

        public Notepad()
        {
            NotepadRevisions = new HashSet<NotepadRevision>();
        }
        public ICollection<NotepadRevision> NotepadRevisions { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
