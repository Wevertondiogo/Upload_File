using Microsoft.AspNetCore.Mvc;
using System.IO;
using Upload_File.Models;

namespace Upload_File.Controllers
{
    public class UploadController : Controller
    {
        public IActionResult Index()
        {
            var model = new SingleFileModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Upload(SingleFileModel model)
        {
            if (ModelState.IsValid)
            {
                model.IsResponse = true;
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files");

                if (!Directory.Exists(path)) Directory.CreateDirectory(path);

                var fileInfo = new FileInfo(model.File.FileName);
                var fileName = $"{model.FileName}{fileInfo.Extension}";
                var fileNameWithPath = Path.Combine(path, fileName);

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    model.File.CopyTo(stream);
                }

                model.IsSuccess = true;
                model.Message = "File upload successfully";
            }
            return View("Index", model);
        }

        public IActionResult MultiFile()
        {
            var model = new MultipleFilesModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult MultiUpload(MultipleFilesModel model)
        {
            if (ModelState.IsValid)
            {
                model.IsResponse = true;
                if (model.Files.Count > 0)
                {
                    foreach (var file in model.Files)
                    {
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files");
                        //create folder if not exist
                        if (!Directory.Exists(path))
                            Directory.CreateDirectory(path);

                        var fileNameWithPath = Path.Combine(path, file.FileName);
                        using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }
                    }
                    model.IsSuccess = true;
                    model.Message = "Files upload successfully";
                }
                else
                {
                    model.IsSuccess = false;
                    model.Message = "Please select files";
                }
            }
            return View("MultiFile", model);
        }
    }
}
