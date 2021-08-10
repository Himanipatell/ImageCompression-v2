# ImageCompression-v2

Image Compression Service

Objective: The main objective of this service is to compress the image. Other than that it will also crop the image in a particular manner and will add watermarks on the image.

System Process: Image compression service will have an input of image as bytes (or image url) formed in json structure. Then the service will crop the image and add the text or image as a watermark on a particular image, after that it will compress the image & return image(bytes).


Post End Points:

All API’S take input as bytes of an image & return bytes of a compressed image

/image/compress
{  
    "imagebytes": [“ ”]
}

This api compress the image bytes

/image/cropImage/compress
{  
    "imagebytes": [“ ”],
    "height": “”,
   "width": “”,
}

This api crop the image bytes in a particular manner and then compress the image

/image/cropImage/textWatermark/compress
{  
    "imagebytes": [“ ”]
    "height": “”,
    "width": “”,
    "watermarkImageText": “”,
}

This api compress the image bytes, crop the image & add text watermark on image

/image/cropImage/imageWatermark/compress
{  
    "imagebytes": [“ ”],
    "height": “”,
    "width": “”,
    "watermarkImagepath":” ”
}

This api compress the image bytes, crop the image & add image watermark on image

/image/cropImage/watermark/compress
{  
    "imagebytes": [“ ”],
    "height": “”,
    "width": “”,
    "watermarkImagepath":” ”
}

This api compress the image bytes, crop the image & add text & Image both watermark on image












