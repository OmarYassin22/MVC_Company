using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis;
using System;
using System.IO;
using System.Reflection.Metadata;

namespace Demo.Presentaion.Helpers
{
    public class DocumentSettings
    {
        public static string Upload(IFormFile file, string folderName)
        {

            var pathName = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\", folderName);

            var filename = $"{Guid.NewGuid()}{file.FileName}";

            var filepath = Path.Combine(pathName, filename);


            using var stream = new FileStream(filepath, FileMode.CreateNew);


            file.CopyTo(stream);


            return filename;


        }
        public static void DeleteFile(string fileName,string foldername)
        {
    
            var filepath = Path.Combine(Directory.GetCurrentDirectory(),@"wwwroot\", foldername,fileName);
            if (File.Exists(filepath))
            {
                File.Delete(filepath);
            }

        }
    }
}
