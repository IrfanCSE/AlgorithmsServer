using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace AlgorithmsServer.Helper
{
    public class Upload
    {
        public static string UploadedFile(IFormFile model, out string name)
        {
            string filePath = null;
            string uniqueFileName = model.FileName;

            if (model != null)
            {
                string uploadsFolder = "./Python/Images";
                if (model.FileName.Length < 50)
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.FileName;
                filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.CopyTo(fileStream);
                }
            }
            name = $"dif_{uniqueFileName}";
            return filePath;
        }
    }
}