using System.Security.Cryptography;
using System.Text;

namespace OnlineNotepad.EF
{
    internal static class Utility
    {
        internal static string GetMd5Hash(string input)
        {
            string retVal = string.Empty;

            if (string.IsNullOrEmpty(input))
            {
                return null;
            }

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
    }
}
