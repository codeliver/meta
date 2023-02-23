using MetaData.Data;
using MetaData.Methods;
using MetaData.Models;
using MetaData.Models.ViewModels;
using MetadataExtractor;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Net;
using Directory = MetadataExtractor.Directory;

namespace MetaData.Controllers
{
    public class MetaController : Controller
    {
        //Dependency Injection
        private readonly ApplicationDbContext _db;
        private readonly ErrorLogs _errorLogs;
        private readonly IWebHostEnvironment _hostEnvironment;

        public MetaController(
            ApplicationDbContext db,
            ErrorLogs errorLogs,
            IWebHostEnvironment hostEnvironment
            )
        {
            _db = db;
            _errorLogs = errorLogs;
            _hostEnvironment = hostEnvironment;
        }

        //Post Method for Generating meta data
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Generate(IFormFile? file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string wwwRootPath = _hostEnvironment.WebRootPath;

                    if (file != null)
                    {
                        string fileName = file.FileName;
                        var uploads = Path.Combine(wwwRootPath, @"upload\meta");
                        var extension = Path.GetExtension(file.FileName);

                        //Copy file from upload to the location
                        using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                        {
                            file.CopyTo(fileStreams);
                        }

                        string imagePath = wwwRootPath + @"\upload\meta\" + fileName + extension;
                        string imagePath2 = @"\upload\meta\" + fileName + extension;

                        //Generate Metadata from image file
                        IEnumerable<Directory> directories = ImageMetadataReader.ReadMetadata(imagePath);

                        ViewBag.AllDirectories = directories;

                        //New meta
                        Metadata meta = new Metadata();

                        meta.DateGenerated = DateTime.Now;
                        meta.ImageURL = imagePath2;
                        meta.IPAddress = HttpContext.Connection.RemoteIpAddress?.ToString();

                        if(directories.Any() != null)
                        {
                            meta.ResponseCode = "Successful";
                        }

                        _db.MetaDataTable.Add(meta);
                        _db.SaveChanges();

                        return RedirectToAction("Result");
                    }
                }
                else
                {
                    ModelState.AddModelError("Error", "Metadata not generated");

                    Metadata meta = new Metadata();
                    meta.DateGenerated = DateTime.Now;
                    meta.ImageURL = "";
                    meta.IPAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
                    meta.ResponseCode = "UnSuccessful";

                    _db.MetaDataTable.Add(meta);
                    _db.SaveChanges();
                    
                    return RedirectToAction("Index","Home");
                }
                return View();
            }
            catch(Exception ex)
            {
                _errorLogs.LogExceptions(ex, "Error Generating meta data from file");
                ModelState.AddModelError(string.Empty, ex.Message);
                return View();
            }
        }


        //GET Method for Response page
        public IActionResult Result()
        {
            try
            {
                return View();
            }
            catch(Exception ex)
            {
                _errorLogs.LogExceptions(ex, "Error showing meta data from file");
                ModelState.AddModelError(string.Empty, ex.Message);
                return View();
            }
        }
    }
}
