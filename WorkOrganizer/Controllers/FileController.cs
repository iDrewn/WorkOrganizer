using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WorkOrganizer.Data;

namespace WorkOrganizer.Controllers
{
    [Authorize]
    public class FileUploadController : Controller
    {
        private readonly ApplicationDbContext context;
        private IHostingEnvironment hostingEnvironment;
        private IFileService fileService;

        public FileUploadController(ApplicationDbContext context,
                                    IFileService fileService,
                                    IHostingEnvironment hostingEnvironment)
        {
            this.fileService = fileService;
            this.context = context;
            this.hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Show(int id)
        {
            var pp = context.Project.Include(x => x.Files).FirstOrDefault(x => x.Id == id);

            return View(pp);

        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(int projectId, IFormFile file)
        {
            if (file == null || file.Length == 0)
                return Content("file not selected");

            string path_Root = hostingEnvironment.WebRootPath;

            string pathToFile = path_Root + "//Files//" + file.FileName;

            using (var stream = new FileStream(pathToFile, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var newFile = await fileService.CreateFileAsync(file.FileName, "/Files/" + file.FileName, projectId);

            return RedirectToAction("Show", new{ id = projectId });
        }

        public class UploadFileModel
        {
            public IFormFile FileToUpload { get; set; }
            public IFormFile Name { get; set; }
            public IFormFile Uri { get; set; }
        }

        [HttpGet]           //browser/web-delen stödjer bara get och post. För API-delen så kan man även nyttja [HttpDelete]
        public async Task<IActionResult> DeleteFile(int projectId, int fileId)
        {
            var fileToRemove = await context.File.FirstOrDefaultAsync(m => m.Id == fileId);

            if (fileToRemove != null)
            {
                context.File.Remove(fileToRemove);
                await context.SaveChangesAsync();
            }

            string path_Root = hostingEnvironment.WebRootPath;

            string pathToFile = path_Root + fileToRemove.Uri;
            
            if (System.IO.File.Exists(pathToFile))
            {
                System.IO.File.Delete(pathToFile);
            }

            return RedirectToAction("Show", new { Id = projectId });
        }







        //public async Task<IActionResult> DeleteRequest(int id)
        //{
        //    var fileRemove = await context.File.FirstOrDefaultAsync(m => m.Id == id);
        //    if (fileRemove != null)
        //    {
        //        context.File.Remove(fileRemove);
        //        await context.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    return RedirectToAction("Show");
        //}

    }

    public interface IFileService
    {
        Task<Domain.Entities.File> CreateFileAsync(string name, string filePath, int projectId);
    }

    public class FileService : IFileService
    {
        private readonly ApplicationDbContext context;

        public FileService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Domain.Entities.File> CreateFileAsync(string name, string filePath, int projectId)
        {
            var newFile = new Domain.Entities.File
            {
                Name = name,
                Uri = filePath,
                ProjectId = projectId
            };

            context.File.Add(newFile);

            await context.SaveChangesAsync();

            return newFile;
        }
    }
}