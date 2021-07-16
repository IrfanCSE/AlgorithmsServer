using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AlgorithmsServer.Helper
{
    public class Download
    {
        // public async File DownloadImage(string filename)
        // {
        //     var path = Path.GetFullPath("./wwwroot/images/school-assets/" + filename);
        //     MemoryStream memory = new MemoryStream();
        //     using (FileStream stream = new FileStream(path, FileMode.Open))
        //     {
        //         await stream.CopyToAsync(memory);
        //     }
        //     memory.Position = 0;
        //     return File(memory, "image/png", Path.GetFileName(path));
        // }
    }
}