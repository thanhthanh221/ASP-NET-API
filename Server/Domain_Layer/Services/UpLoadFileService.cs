using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Domain_Layer.Services
{
     public static class UpLoadFileService{
        public static async Task<String> SaveImage(IFormFile file, String locationStorage)
        {
            if(file.Length > 0)
            {
                try
                {
                    if(!Directory.Exists(Environment.CurrentDirectory + $"\\{locationStorage}\\"))
                    // Kiểm tra xem đã tồn tại thư mục chưa
                    {
                        Directory.CreateDirectory(Environment.CurrentDirectory + $"\\{locationStorage}\\");
                    }
                    using (FileStream fileStream = System.IO.File.Create(Environment.CurrentDirectory +  $"\\{locationStorage}\\" + file.FileName))
                    {
                        await file.CopyToAsync(fileStream);
                        await fileStream.FlushAsync(); // giải phóng bộ đệm
                        
                        return file.FileName;
                    }
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
            }   
            else
            {
                return "Không Up được file";
            }
        }
        public static void DeleteImage(String imageName, String locationStorage)
        {
            var imagePath = Path.Combine(Environment.CurrentDirectory + $"\\{locationStorage}\\" + imageName);
            if(System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }

        }
        
    }
}