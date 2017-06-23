using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Spire.Doc;
using Spire.Doc.Documents;
//using Spire.Pdf;

namespace Converter.Controllers
{
    public class FileTestController : Controller
    {
        // GET: FileTest
        public ActionResult Index()
        {
            foreach (string upload in Request.Files)
            {
                string path = AppDomain.CurrentDomain.BaseDirectory + "uploads/";
                string filename = Path.GetFileName(Request.Files[upload].FileName);
                string filePath = Path.Combine(path, filename);
                Request.Files[upload].SaveAs(filePath);
                Document document = new Document();
                document.LoadFromFile(filePath);

                document.SaveToFile(Path.ChangeExtension(filePath, "pdf"), FileFormat.PDF);
            }
            return View();
        }
    }
}