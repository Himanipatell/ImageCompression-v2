using Image_Compression.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Image_Compression.Models;
using System.Net.Http;
using System.Collections.Generic;

namespace Image_Compression.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImageController : Controller
    {       

        //For multiple images
        [HttpPost]
        public async Task<IActionResult> ImageCompress([FromBody] ImageModel imageobj)
        {
            byte[] compressedBytes = null;
            var tasks = new List<Task>();
            ImageCompresser imageCompresser = new ImageCompresser();

            foreach (string path in imageobj.images)
            {
               // byte[] compressedBytes = null;
                Task t = Task.Run(() =>
                    {
                        compressedBytes = imageCompresser.compress(path).Result;
                    });
                tasks.Add(t);
            }

           /* byte[] fileBytes = System.Convert.FromBase64String(imageobj.imageByteArray);

            Task task = Task.Run(() =>
            {
                compressedBytes = imageCompresser.compress(fileBytes, imageobj.watermarkpath).Result;
            });
            tasks.Add(task);*/
            Task.WaitAll(tasks.ToArray());
            //return Ok("images compressed"); ;
            return Ok(new { compressedBytes = compressedBytes }); ;


        }

        [HttpPost("cropcompress")]
        public async Task<IActionResult> ImageCropCompress([FromBody] ImageModel imageobj)
        {
            byte[] compressedBytes = null;
            var tasks = new List<Task>();
            ImageCompresser imageCompresser = new ImageCompresser();

            foreach (string path in imageobj.images)
            {
                // byte[] compressedBytes = null;
                Task t = Task.Run(() =>
                {
                    compressedBytes = imageCompresser.compressWithCrop(path, imageobj.widthFraction,imageobj.heightFraction).Result;
                });
                tasks.Add(t);
            }

            /* byte[] fileBytes = System.Convert.FromBase64String(imageobj.imageByteArray);

             Task task = Task.Run(() =>
             {
                 compressedBytes = imageCompresser.compress(fileBytes, imageobj.watermarkpath).Result;
             });
             tasks.Add(task);*/
            Task.WaitAll(tasks.ToArray());
            //return Ok("images compressed"); ;
            return Ok(new { compressedBytes = compressedBytes }); ;


        }

        [HttpPost("imagecompress")]
        public async Task<IActionResult> ImageCompressImageWatermark([FromBody] ImageModel imageobj)
        {
            byte[] compressedBytes = null;
            var tasks = new List<Task>();
            ImageCompresser imageCompresser = new ImageCompresser();

            foreach (string path in imageobj.images)
            {
                // byte[] compressedBytes = null;
                Task t = Task.Run(() =>
                {
                    compressedBytes = imageCompresser.compressWithImageWatermark(path,imageobj.widthFraction,imageobj.heightFraction, imageobj.watermarkpath).Result;
                });
                tasks.Add(t);
            }

            /* byte[] fileBytes = System.Convert.FromBase64String(imageobj.imageByteArray);

             Task task = Task.Run(() =>
             {
                 compressedBytes = imageCompresser.compress(fileBytes, imageobj.watermarkpath).Result;
             });
             tasks.Add(task);*/
            Task.WaitAll(tasks.ToArray());
            //return Ok("images compressed"); ;
            return Ok(new { compressedBytes = compressedBytes }); ;


        }

        [HttpPost("textcompress")]
        public async Task<IActionResult> ImageCompressTextWatermark([FromBody] ImageModel imageobj)
        {
            byte[] compressedBytes = null;
            var tasks = new List<Task>();
            ImageCompresser imageCompresser = new ImageCompresser();

            foreach (string path in imageobj.images)
            {
                // byte[] compressedBytes = null;
                Task t = Task.Run(() =>
                {
                    compressedBytes = imageCompresser.compressWithTextWatermark(path,imageobj.widthFraction,imageobj.heightFraction,imageobj.watermarkText).Result;
                });
                tasks.Add(t);
            }

            /* byte[] fileBytes = System.Convert.FromBase64String(imageobj.imageByteArray);

             Task task = Task.Run(() =>
             {
                 compressedBytes = imageCompresser.compress(fileBytes, imageobj.watermarkpath).Result;
             });
             tasks.Add(task);*/
            Task.WaitAll(tasks.ToArray());
            //return Ok("images compressed"); ;
            return Ok(new { compressedBytes = compressedBytes }); ;


        }
    }
}