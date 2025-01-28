using Microsoft.AspNetCore.Mvc;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace RandomImageApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RandomImageController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetRandomImage(int width = 500, int height = 500)
        {
            const int maxDimension = 720;
            if (width > maxDimension || height > maxDimension)
            {
                return BadRequest($"Width and height must not exceed {maxDimension}.");
            }

            using (var bitmap = new Bitmap(width, height))
            {
                var random = new System.Random();
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        Color color = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
                        bitmap.SetPixel(x, y, color);
                    }
                }

                using (var ms = new MemoryStream())
                {
                    bitmap.Save(ms, ImageFormat.Png);
                    return File(ms.ToArray(), "image/png");
                }
            }
        }
    }
}