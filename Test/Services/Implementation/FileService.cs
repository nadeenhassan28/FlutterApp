﻿namespace Test.Services.Implementation;
public class FileService
{
    private readonly IWebHostEnvironment _env;
    public FileService(IWebHostEnvironment env)
    {
        _env = env;
    }

    public async Task<Tuple<int, string>> SaveImage(IFormFile imageFile, string folder)
    {
        return await Task.Run(() =>
        {
            try
            {
                var contentPath = _env.WebRootPath;
                // path = "c://projects/productminiapi/uploads" ,not exactly something like that
                var path = Path.Combine(contentPath, "Uploads\\", folder);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                // Check the allowed extenstions
                var ext = Path.GetExtension(imageFile.FileName);
                var allowedExtensions = new string[] { ".jpg", ".png", ".jpeg" };
                if (!allowedExtensions.Contains(ext))
                {
                    string msg = string.Format("Only {0} extensions are allowed", string.Join(",", allowedExtensions));
                    return new Tuple<int, string>(0, msg);
                }
                string uniqueString = Guid.NewGuid().ToString();
                // we are trying to create a unique filename here
                var newFileName = uniqueString + ext;
                var fileWithPath = Path.Combine(path, newFileName);
                var stream = new FileStream(fileWithPath, FileMode.Create);
                imageFile.CopyTo(stream);
                stream.Close();
                return new Tuple<int, string>(1, newFileName);
            }
            catch (Exception ex)
            {
                return new Tuple<int, string>(0, "Error has occured");
            }
        });
    }

    public bool DeleteImage(string imageFileName, string folder)
    {
        var contentPath = _env.WebRootPath;
        var path = Path.Combine(contentPath, $"Uploads", folder, imageFileName);
        if (File.Exists(path))
        {
            File.Delete(path);
            return true;
        }
        return false;
    }
}

