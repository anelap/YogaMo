using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YogaMo.Web.Helpers;
using YogaMo.WebAPI.Database;

namespace YogaMo.Web.Controllers
{
    public class PictureController : BaseController
    {
        public PictureController(_150222Context context, IMapper mapper) : base(context, mapper)
        {
        }


        [HttpGet]
        public IActionResult Instructor(int id)
        {
            SendExpiresInPastHeader();

            var user = _context.Instructor.Where(x => x.InstructorId == id).FirstOrDefault();
            if (user == null)
                return NotFound();

            if (user.ProfilePicture == null || user.ProfilePicture.Length == 0)
            {
                return File(ImageHelper.DefaultImageLocation, "image/png");
            }

            return new FileContentResult(user.ProfilePicture, "image/jpeg");
        }


        [HttpGet]
        public IActionResult Client(int id)
        {
            SendExpiresInPastHeader();

            var user = _context.Client.Where(x => x.ClientId == id).FirstOrDefault();
            if (user == null)
                return NotFound();

            if (user.ProfilePicture == null || user.ProfilePicture.Length == 0) {
                return File(ImageHelper.DefaultImageLocation, "image/png");
            }

            return new FileContentResult(user.ProfilePicture, "image/jpeg");
        }

        [HttpGet]
        public IActionResult Product(int id)
        {
            SendExpiresInFutureHeader();

            var product = _context.Product.Where(x => x.ProductId == id).FirstOrDefault();
            if (product == null)
                return NotFound();

            if (product.Photo == null || product.Photo.Length == 0) {
                return File(ImageHelper.DefaultProductImageLocation, "image/png");
            }

            return new FileContentResult(product.Photo, "image/jpeg");
        }

        private void SendExpiresInPastHeader()
        {
            string ExpireDate = DateTime.UtcNow.AddYears(-10).ToString("ddd, dd MMM yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            Response.Headers.Add("Expires", ExpireDate + " GMT");
        }
        private void SendExpiresInFutureHeader()
        {
            string ExpireDate = DateTime.UtcNow.AddMinutes(60).ToString("ddd, dd MMM yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            Response.Headers.Add("Expires", ExpireDate + " GMT");
        }
    }
}
