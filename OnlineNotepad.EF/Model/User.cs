using OnlineNotepad.EF.Contract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineNotepad.EF.Model
{
    [Table("tbl_Users")]
    public class User : IAuditedEntity
    {
        private string password;
        private string email;

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UserID { get; set; }

        [Required, MaxLength(255)]
        public string Email
        {
            get { return email; }
            set { email = Utility.GetMd5Hash(value); }
        }

        [Required]
        public string Password
        {
            get { return password; }
            set { password = Utility.GetMd5Hash(value); }
        }

        public User()
        {
            Notepads = new HashSet<Notepad>();
        }

        public ICollection<Notepad> Notepads { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
