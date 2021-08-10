using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using ImageMagick;
using NetVips;
using static NetVips.Enums;
using Image_Compression.Models;
using System.Collections.Generic;

namespace Image_Compression.Services
{
    public class ImageCompresser
    {
        static int count= 1;
        public async Task<List<byte[]>> compress(string path,bool isByte)
        {
           
            byte[] originalBytes = null;
            List<byte[]> compressedBytes = new List<byte[]>();
            
            try
            {
                if (isByte == false)
                {
                    string fileName = Path.GetFileName(path);
                    using var image = NetVips.Image.NewFromFile(path);
                    using (WebClient webClient = new WebClient())
                    {
                        originalBytes = webClient.DownloadData(path);
                    }
                }
                else
                {
                    originalBytes = System.Convert.FromBase64String(path);
                }
                NetVips.Image img = ByteToImg(originalBytes);
                
                compressedBytes.Add(img.TiffsaveBuffer(ForeignTiffCompression.Jpeg));
                img.Tiffsave("C:\\Users\\HIMANI\\Pictures\\Compress Image\\Compress\\"+"_compressonly_"+count+".jpg" , ForeignTiffCompression.Jpeg);
                count++;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception occured");
            }
            return compressedBytes;
        }
        public async Task<byte[]> compressWithCrop(string path,float width,float height)
        {
            byte[] originalBytes = null;
            byte[] compressedBytes = null;
            bool f = false;
            try
            {
                string fileName = Path.GetFileName(path);
                using var image = NetVips.Image.NewFromFile(path);
                using (WebClient webClient = new WebClient())
                {
                    originalBytes = webClient.DownloadData(path);
                }
                NetVips.Image img = ByteToImg(originalBytes);
                NetVips.Image cropimg = getCroppedImage(img,width,height);//img.Smartcrop((int)(img.Width * width), (int)(img.Height * height), Interesting.Centre);
                compressedBytes = cropimg.TiffsaveBuffer(ForeignTiffCompression.Jpeg);
                ByteToImg(compressedBytes).Tiffsave("C:\\Users\\HIMANI\\Pictures\\Compress Image\\Compress\\" + "_compresscrop_" + fileName, ForeignTiffCompression.Jpeg);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception occured");
            }
            return compressedBytes;
        }
        public async Task<byte[]> compressWithImageWatermark(string path,float width, float height,string watermarkImagePath)
        {
            byte[] originalBytes = null;
            byte[] compressedBytes = null;
            bool f = false;

            try
            {
                string fileName = Path.GetFileName(path);
                String writePath = "C:/Users/HIMANI/Pictures/Compress Image/Compress" + fileName;
                using var image = NetVips.Image.NewFromFile(path);
                using (WebClient webClient = new WebClient())
                {
                    originalBytes = webClient.DownloadData(path);
                }
                NetVips.Image img = ByteToImg(originalBytes);
                NetVips.Image cropimg = getCroppedImage(img, width, height);//img.Smartcrop((int)(img.Width * width), (int)(img.Height * height), Interesting.Centre);

                byte[] imageWithWatermarkBytes = insertImageWaterMark(cropimg.JpegsaveBuffer(), watermarkImagePath, writePath);
                NetVips.Image imageWithWatermark = ByteToImg(imageWithWatermarkBytes);
                compressedBytes = imageWithWatermark.TiffsaveBuffer(ForeignTiffCompression.Jpeg);
               ByteToImg(compressedBytes).Tiffsave("C:\\Users\\HIMANI\\Pictures\\Compress Image\\Compress\\" + "_imagewatermark_" + fileName, ForeignTiffCompression.Jpeg);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception occured");
            }
            return compressedBytes;
        }
        public async Task<byte[]> compressWithTextWatermark(string path, float width, float height,string watermarkText)
        {
            byte[] originalBytes = null;
            byte[] compressedBytes = null;
            bool f = false;

            try
            {
                string fileName = Path.GetFileName(path);
                String writePath = "C:/Users/HIMANI/Pictures/Compress Image/Compress" + fileName;
                using var image = NetVips.Image.NewFromFile(path);
                using (WebClient webClient = new WebClient())
                {
                    originalBytes = webClient.DownloadData(path);
                }
                NetVips.Image img = ByteToImg(originalBytes);
                NetVips.Image cropimg = getCroppedImage(img, width, height);//img.Smartcrop((int)(img.Width * width), (int)(img.Height * height), Interesting.Centre);

                byte[] imageWithWatermarkBytes = insertTextWatermark(cropimg.JpegsaveBuffer(), watermarkText);
                NetVips.Image imageWithWatermark = ByteToImg(imageWithWatermarkBytes);
                compressedBytes = imageWithWatermark.TiffsaveBuffer(ForeignTiffCompression.Jpeg);
                ByteToImg(compressedBytes).Tiffsave("C:\\Users\\HIMANI\\Pictures\\Compress Image\\Compress\\" + "_textwatermark_" + fileName, ForeignTiffCompression.Jpeg);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception occured");
            }
            return compressedBytes;
        }
             
      
        private NetVips.Image getCroppedImage(NetVips.Image img,float width,float height)
        {
           // NetVips.Image img = ByteToImg(imageBytes);
            NetVips.Image croppedImage = img.Smartcrop((int)(img.Width * width), (int)(img.Height * height), Interesting.Centre);
            return croppedImage;
        }       
        private byte[] insertImageWaterMark(byte[] compressedImage, string watermarkpath, string writePath)
        {
            byte[] output = null;
            NetVips.Image img = ByteToImg(compressedImage);

            using (var magicimage = new MagickImage(compressedImage))
            {
                using (var watermark = new MagickImage(watermarkpath))
                {
                    int width = (int)(img.Width * 0.8);
                    int height = (int)(img.Height * 0.8);
                    magicimage.Composite(watermark, width,height , CompositeOperator.Over);
                }
                magicimage.Write(writePath);
                output = magicimage.ToByteArray();
            }
            return output;
        }

        private byte[] insertTextWatermark(byte[] imageBytes, string watermarkText)
        {
            NetVips.Image img = ByteToImg(imageBytes);
            Stream stream = new MemoryStream(imageBytes);
            Bitmap bitmap = new Bitmap(stream);
            Bitmap tempBitMap = new Bitmap(bitmap, bitmap.Width, bitmap.Height);
            Graphics graphicsImage = Graphics.FromImage(tempBitMap);
            StringFormat stringformat1 = new StringFormat();
            stringformat1.Alignment = StringAlignment.Far;
            Color StringColor1 = ColorTranslator.FromHtml("#FFFFFF");
            int width = (int)(img.Width * 0.95);
            int height = (int)(img.Height * 0.95);
            graphicsImage.DrawString(watermarkText, new Font("arail", 40, FontStyle.Regular), new SolidBrush(StringColor1), new Point(width, height), stringformat1);
            // bitmap.Save(writePath);
            using (var ms = new MemoryStream())
            {
                tempBitMap.Save(ms, bitmap.RawFormat);
                return ms.ToArray();
            }

        }

        private byte[] getImageBytes(string path)
        {
            byte[] bytes;
            string fileName = Path.GetFileName(path);
            using var image = NetVips.Image.NewFromFile(path);
            using (WebClient webClient = new WebClient())
            {
                bytes = webClient.DownloadData(path);
            }
            return bytes;
        }

        
        private NetVips.Image ByteToImg(byte[] byteArr)
        {
            var ms = new MemoryStream(byteArr);
            return NetVips.Image.NewFromStream(ms);
        }             

    }
}