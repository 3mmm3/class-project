using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using _3M.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _3M.Controllers
{
    public class FileController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly NotificationManager _notificationManager;

        public FileController(IWebHostEnvironment env,
            NotificationManager notificationManager)
        {
            _env = env;
            _notificationManager = notificationManager;
        }

        public IActionResult Download(Guid id)
        {
            var savePath = Path.Combine(_env.WebRootPath, "uploaded");
            var saveFileName = Directory.GetFiles(savePath)
                .FirstOrDefault(i => Path.GetFileNameWithoutExtension(i) == id.ToString());

            var x = Path.GetFileName(saveFileName);

            return PhysicalFile(saveFileName, "image/png",
                Path.GetFileName(saveFileName));
        }

        [HttpGet]
        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            try
            {
                if (file != null)
                {
                    var saveFileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                    var savePath = Path.Combine(_env.WebRootPath, "uploaded", saveFileName);
                    using (var dest = System.IO.File.Open(savePath, FileMode.Create, FileAccess.Write, FileShare.None))
                        await file.CopyToAsync(dest);
                    _notificationManager.Set(new Notification("فایل با موفقیت ثبت شد", Notification.SuccessType));
                }
                else
                {
                    _notificationManager.Set(new Notification("هیچ فایلی انتخاب نشده است", Notification.WarningType));
                }
            }
            catch (Exception e)
            {
                _notificationManager.Set(new Notification(e.Message, Notification.WarningType));
            }
            return View();
        }
    }
}