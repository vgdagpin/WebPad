using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnlineNotepad.EF;
using OnlineNotepad.EF.Model;

namespace OnlineNotepad.UnitTest
{
    [TestClass]
    public class NotepadEntitiesTesting
    {
        [TestMethod]
        public void CanInitializeDatabase()
        {
            using (NotepadEntities entities = new NotepadEntities())
            {
                entities.Users.Add(new User
                {
                    Email = "vincsent.dagpin@gmail.com",
                    Password = "k4m0t3sdfsdfsfsfsdfsdfsdfs"
                });

                entities.SaveChanges();
            }
        }
    }
}
