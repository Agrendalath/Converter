using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Spire.Doc;
using Spire.Doc.Documents;
using Microsoft.AspNet.Identity;

namespace Converter.Controllers
{
    [Authorize]
    public class ConvertController : Controller
    {
        public ActionResult Index()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "uploads/" + User.Identity.GetUserId().ToString();
            Directory.CreateDirectory(path);

            foreach (string upload in Request.Files)
            {
                if (Request.Files[upload].ContentLength == 0)
                    continue;

                string filename = Path.GetFileName(Request.Files[upload].FileName);
                string filePath = Path.Combine(path, filename);
                Request.Files[upload].SaveAs(filePath);
                Document document = new Document();
                document.LoadFromFile(filePath);

                document.SaveToFile(Path.ChangeExtension(filePath, "pdf"), FileFormat.PDF);                
            }

            ViewBag.Files = Directory.GetFiles(path).Select(Path.GetFileName).ToArray();
            return View();
        }

        public FileStreamResult Get(string fileName)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "uploads/" + User.Identity.GetUserId().ToString() + "/";
            try
            {
                return File(new FileStream(path + fileName, FileMode.Open), "text/plain", fileName);
            }
            catch (IOException)
            {
                throw new HttpException(404, "File not found.");
            }
        }

        public ActionResult Delete(string fileName)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "uploads/" + User.Identity.GetUserId().ToString() + "/" + fileName;
            try
            {
                System.IO.File.Delete(path);
            }
            catch (IOException)
            {
                throw new HttpException(404, "File not found.");
            }
            
            return RedirectToAction("Index");
        }
    }
}