
using OnlineNotepad.EF.Contract;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineNotepad.EF.Model
{
    [Table("tbl_NotepadRevisions")]
    public class NotepadRevision : IAuditedEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NotepadRevisionID { get; set; }

        [Required]
        public Guid NotepadID { get; set; }

        [Required]
        public Notepad Notepad { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }
    }
}
