using EncryptionApp.App.Views.Pages; // S?nin EncryptionService namespace-i
using Microsoft.AspNetCore.Mvc;

namespace EncryptionApp.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly EncryptionService _encryptionService;

        public HomeController(IWebHostEnvironment env, IConfiguration configuration)
        {
            _env = env;
            _encryptionService = new EncryptionService(configuration);
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(IFormFile UploadedFile, string action)
        {
            if (UploadedFile == null || UploadedFile.Length == 0)
            {
                ViewBag.Message = "Please select a file.";
                return View();
            }

            var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
            Directory.CreateDirectory(uploadsFolder);

            var inputFilePath = Path.Combine(uploadsFolder, UploadedFile.FileName);
            var fileNameWithoutExt = Path.GetFileNameWithoutExtension(UploadedFile.FileName);
            var extension = Path.GetExtension(UploadedFile.FileName);

            using (var fileStream = new FileStream(inputFilePath, FileMode.Create))
            {
                await UploadedFile.CopyToAsync(fileStream);
            }

            string outputFilePath;
            if (action == "encrypt")
            {
                outputFilePath = Path.Combine(uploadsFolder, fileNameWithoutExt + ".encrypted");
                _encryptionService.EncryptFile(inputFilePath, outputFilePath);
                ViewBag.Message = $"File encrypted successfully: {Path.GetFileName(outputFilePath)}";
            }
            else if (action == "decrypt")
            {
                outputFilePath = Path.Combine(uploadsFolder, fileNameWithoutExt + ".decrypted" + extension);
                _encryptionService.DecryptFile(inputFilePath, outputFilePath);
                ViewBag.Message = $"File decrypted successfully: {Path.GetFileName(outputFilePath)}";
            }
            else
            {
                ViewBag.Message = "Unknown action.";
            }

            return View();
        }
    }
}
