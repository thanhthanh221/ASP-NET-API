using System;
using System.Collections.Generic;
using System.IO;
using BackEnd.Dto;
using BackEnd.Entities;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Services {
    // Các trường hợp xử lý biến đổi
    public static class UpLoadFileService{
        public static async Task<String> SaveImage(IFormFile file, String locationStorage)
        {
            if(file.Length > 0)
            {
                try
                {
                    if(!Directory.Exists(Startup.ContentRootPath+ "\\Images\\" + $"\\{locationStorage}\\"))
                    // Kiểm tra xem đã tồn tại thư mục chưa
                    {
                        Directory.CreateDirectory(Startup.ContentRootPath + "\\Images\\" + $"\\{locationStorage}\\");
                    }
                    using (FileStream fileStream = System.IO.File.Create(Startup.ContentRootPath + "\\Images\\"+ $"\\{locationStorage}\\" + file.FileName))
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
            var imagePath = Path.Combine(Startup.ContentRootPath + "\\Images\\" + $"\\{locationStorage}\\" + imageName);
            if(System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }

        }
        
    }

}