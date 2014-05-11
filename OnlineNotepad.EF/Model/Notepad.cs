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
        private string lockPassword;
        private string mime;
        private string theme;

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid NotepadID { get; set; }

        public string Content { get; set; }

        [Required, MaxLength(255)]
        public string Alias { get; set; }

        [Required, MaxLength(100)]
        public string Title { get; set; }

        [Required]
        public bool ShowContent { get; set; }

        public DateTime? ModifiedOn { get; set; }

        [MaxLength(255)]
        public string LockPassword
        {
            get { return lockPassword; }
            set { lockPassword = Utility.GetMd5Hash(value); }
        }

        [Required, MaxLength(10)]
        public string Mime
        {
            get { return string.IsNullOrEmpty(mime) ? "text/plain" : mime; }
            set { mime = value; }
        }

        [Required, MaxLength(100)]
        public string Theme
        {
            get { return string.IsNullOrEmpty(theme) ? "ambiance" : theme; }
            set { theme = value; }
        }

        public Guid? UserID { get; set; }

        public Notepad()
        {
            NotepadRevisions = new HashSet<NotepadRevision>();
        }

        public ICollection<NotepadRevision> NotepadRevisions { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
