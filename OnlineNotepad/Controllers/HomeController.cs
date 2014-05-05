using OnlineNotepad.EF;
using OnlineNotepad.EF.Model;
using System;
using System.Linq;
using System.Web.Mvc;

namespace OnlineNotepad.Controllers
{
        public class HomeController : Controller
        {
            public ActionResult Index(string id)
            {
                using (NotepadEntities entities = new NotepadEntities())
                {
                    string tempAlias = (string.IsNullOrEmpty(id)) 
                        ? Guid.NewGuid().ToString("N").Substring(0, 8) 
                        : id;

                    var q = from n in entities.Notepads
                        where n.Alias == tempAlias
                        select n;

                    var notepad = q.SingleOrDefault();

                    if (notepad == null)
                    {
                        Notepad newNote = new Notepad
                        {
                            Title = "Untitled Notepad",
                            Alias = tempAlias
                        };

                        entities.Notepads.Add(newNote);

                        entities.SaveChanges();

                        Response.Redirect(tempAlias);
                    }

                    ViewBag.Title = notepad.Title;

                    return View(notepad);
                }
            }
        }
}
