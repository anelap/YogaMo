using AutoMapper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading.Tasks;
using YogaMo.Model;
using YogaMo.Model.Requests;
using YogaMo.WebAPI.Database;
using YogaMo.WebAPI.Exceptions;

namespace YogaMo.WebAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly _150222Context _context;
        private readonly IMapper _mapper;

        public ProductService(_150222Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public void Delete(int id)
        {
            var entity = _context.Product.Find(id);

            _context.Product.Remove(entity);
            _context.SaveChanges();
        }

        List<Model.Product> IProductService.GetAvailableProducts()
        {
            /* var query = _context.Product.AsQueryable();

             query = query.Where(x => x.Status == true);

             var list = query.ToList();*/

            List<Database.Product> list = _context.Product.Where(x => x.Status == true).ToList();

            foreach(var item in list)
            {
                item.Photo = null;
            }

            return _mapper.Map<List<Model.Product>>(list);
        }
        public List<Model.Product> Get(ProductSearchRequest request)
        {
            var query = _context.Product.AsQueryable();

            if (!string.IsNullOrWhiteSpace(request?.Name))
            {
                query = query.Where(x => x.Name.ToUpper().Contains(request.Name.ToUpper()));
            }
            if (request?.CategoryId != 0)
            {
                query = query.Where(x => x.CategoryId == request.CategoryId);
            }

            var list = query.ToList();

            foreach(var item in list)
            {
                item.Photo = null;
            }

            return _mapper.Map<List<Model.Product>>(list);
        }

        public Model.Product Get(int id)
        {
            var entity = _context.Product.Find(id);
            if (entity == null) throw new UserException("Product not found");

            entity.Photo = null;

            return _mapper.Map<Model.Product>(entity);
        }

        public Model.Product Insert(ProductInsertRequest request)
        {
            var entity = _mapper.Map<Database.Product>(request);

            _context.Product.Add(entity);
            _context.SaveChanges();

            return _mapper.Map<Model.Product>(entity);
        }

        public Model.Product Update(int id, ProductInsertRequest request)
        {
            var entity = _context.Product.Find(id);
            if (entity == null) throw new UserException("Product not found");

            Mapper.Map<ProductInsertRequest, Database.Product>(request, entity);

            _context.SaveChanges();

            return _mapper.Map<Model.Product>(entity);
        }


        #region Photos
        public static Image CropImage(Image img, Rectangle cropArea)
        {
            Bitmap bmpImage = new Bitmap(img);
            Bitmap bmpCrop = bmpImage.Clone(cropArea,
            bmpImage.PixelFormat);
            return (Image)(bmpCrop);
        }

        public static Image ResizeImage(Image imgToResize, Size size)
        {
            int sourceWidth = imgToResize.Width;
            int sourceHeight = imgToResize.Height;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = ((float)size.Width / (float)sourceWidth);
            nPercentH = ((float)size.Height / (float)sourceHeight);

            if (nPercentH < nPercentW)
                nPercent = nPercentH;
            else
                nPercent = nPercentW;

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();

            return (Image)b;
        }

        #endregion
    }
}
