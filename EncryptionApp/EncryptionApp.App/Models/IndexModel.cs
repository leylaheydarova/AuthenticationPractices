using EncryptionApp.App.Views.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EncryptionApp.App.Models
{
    public class IndexModel : PageModel
    {
        private readonly IWebHostEnvironment _env;
        private readonly EncryptionService _encryptionService;

        public IndexModel(IWebHostEnvironment env, IConfiguration configuration)
        {
            _env = env;
            _encryptionService = new EncryptionService(configuration);
        }

        [BindProperty]
        public IFormFile UploadedFile { get; set; }

        public string Message { get; set; }

        public async Task<IActionResult> OnPostAsync(string action)
        {
            if (UploadedFile == null || UploadedFile.Length == 0)
            {
                Message = "Please select a file.";
                return Page();
            }

            var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
            Directory.CreateDirectory(uploadsFolder);

            var inputFilePath = Path.Combine(uploadsFolder, UploadedFile.FileName);
            var fileNameWithoutExt = Path.GetFileNameWithoutExtension(UploadedFile.FileName);
            var extension = Path.GetExtension(UploadedFile.FileName);

            // Save uploaded file
            using (var fileStream = new FileStream(inputFilePath, FileMode.Create))
            {
                await UploadedFile.CopyToAsync(fileStream);
            }

            string outputFilePath;
            if (action == "encrypt")
            {
                outputFilePath = Path.Combine(uploadsFolder, fileNameWithoutExt + ".encrypted");
                _encryptionService.EncryptFile(inputFilePath, outputFilePath);
                Message = $"File encrypted: {outputFilePath}";
            }
            else if (action == "decrypt")
            {
                outputFilePath = Path.Combine(uploadsFolder, fileNameWithoutExt + ".decrypted" + extension);
                _encryptionService.DecryptFile(inputFilePath, outputFilePath);
                Message = $"File decrypted: {outputFilePath}";
            }
            else
            {
                Message = "Unknown action.";
            }

            return Page();
        }
    }
}
