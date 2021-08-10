using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Image_Compression.Models
{
    public class ImageModel
    {
        public List<string> images { get; set; }
        public List<string> imageByteArray { get; set; }
        public string watermarkpath { get; set; }
        public float widthFraction { get; set; }
        public float heightFraction { get; set; }
        public string watermarkText { get; set; }
    }
}

