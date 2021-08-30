using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace YogaMo.Web.Helpers
{
    public static class ImageHelper
    {
        public static readonly string DefaultImageLocation = "/dist/img/default.png";
        public static readonly string DefaultProductImageLocation = "/dist/img/default_product.png";

        public static byte[] ConvertUploadToByteArray(IFormFile file)
        {
            using var stream = new MemoryStream();
            file.CopyTo(stream);
            return stream.ToArray();
        }
    }
}
