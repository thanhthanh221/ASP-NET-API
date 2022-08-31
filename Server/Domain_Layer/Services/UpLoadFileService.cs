using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace Domain_Layer.Services
{
    public static class UpLoadFileService
    {
        public static String Dir = System.IO.Directory.GetCurrentDirectory();
        public static async Task<String> SaveImage(IFormFile file, String locationStorage)
        {
            if (file.Length > 0)
            {
                try
                {
                    if (!Directory.Exists(Dir + "\\Images\\" + $"\\{locationStorage}\\"))
                    // Kiểm tra xem đã tồn tại thư mục chưa
                    {
                        Directory.CreateDirectory(Dir + "\\Images\\" + $"\\{locationStorage}\\");
                    }
                    using (FileStream fileStream = System.IO.File.Create(Dir + "\\Images\\" + $"\\{locationStorage}\\" + file.FileName))
                    {
                        await file.CopyToAsync(fileStream);
                        await fileStream.FlushAsync(); // giải phóng bộ đệm

                        MemoryStream ms = new MemoryStream();
                        file.CopyTo(ms);
                        byte[] fileBytes = ms.ToArray();
                        string imgToBase64 = Convert.ToBase64String(fileBytes);
                        // act on the Base64 data

                        return imgToBase64;
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
            var imagePath = Path.Combine(Dir + "\\Images\\" + $"\\{locationStorage}\\" + imageName);
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }

        }

    }
}