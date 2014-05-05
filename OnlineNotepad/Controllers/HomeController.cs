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

                        return View();
                    }

                    ViewBag.Title = notepad.Title;

                    return View(notepad);
                }
            }

            public JsonResult Save(Guid alias, string content)
            {
                using (NotepadEntities entities = new NotepadEntities())
                {
                    var q = from n in entities.Notepads
                        where n.NotepadID == alias
                        select n;

                    var notepad = q.SingleOrDefault();

                    if (notepad != null)
                    {
                        notepad.Content = content;
                        entities.SaveChanges();
                    }
                }

                return Json(new { Content = content, Result = "Success" });
            }
        }
}
