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
                            Alias = tempAlias,
                        };

                        entities.Notepads.Add(newNote);

                        entities.SaveChanges();

                        Response.Redirect(tempAlias);

                        return View();
                    }

                    ViewBag.Title = notepad.Title;

                    //notepad.Content = GetDefaultNotepadContent(notepad.Content);

                    return View(notepad);
                }
            }

            public static string GetPlaceHolder()
            {
                string retVal = string.Empty;

                using (NotepadEntities entities = new NotepadEntities())
                {
                    var q = from n in entities.Notepads
                        where n.Alias == "DefaultContent"
                        select n;

                    var defaultNote = q.SingleOrDefault();

                    if (defaultNote != null)
                    {
                        retVal = defaultNote.Content;
                    }
                }

                return retVal;
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

            public JsonResult SaveWithConfig(Notepad notepad)
            {
                using (NotepadEntities entities = new NotepadEntities())
                {
                    var noteToUpdate = entities.Notepads.Find(notepad.NotepadID);

                    noteToUpdate.Content = notepad.Content;
                    noteToUpdate.LockPassword = notepad.LockPassword;
                    noteToUpdate.Mime = notepad.Mime;
                    noteToUpdate.ModifiedOn = DateTime.Now;
                    noteToUpdate.ShowContent = notepad.ShowContent;
                    noteToUpdate.Theme = notepad.Theme;
                    noteToUpdate.Title = notepad.Title;

                    entities.SaveChanges();

                    return Json(new
                    {
                        Result = "Sucess"
                    });
                }
            }
        }
}
