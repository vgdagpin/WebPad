﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnlineNotepad.EF;

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
                entities.SaveChanges();
            }
        }
    }
}
