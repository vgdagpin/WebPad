using OnlineNotepad.EF.Contract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;

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
            set { email = GetMd5Hash(value); }
        }

        [Required]
        public string Password
        {
            get { return password; }
            set { password = GetMd5Hash(value); }
        }

        public User()
        {
            Notepads = new HashSet<Notepad>();
        }

        public ICollection<Notepad> Notepads { get; set; }

        private static string GetMd5Hash(string input)
        {
            string retVal = string.Empty;

            using (MD5 md5Hasher = MD5.Create())
            {
                byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));
                StringBuilder sBuilder = new StringBuilder();

                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                retVal = sBuilder.ToString();
            }

            return retVal;
        }

        public DateTime CreatedOn { get; set; }
    }
}
