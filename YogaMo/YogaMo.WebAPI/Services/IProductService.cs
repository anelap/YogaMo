using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YogaMo.Model.Requests;

namespace YogaMo.WebAPI.Services
{
    public interface IProductService
    {
        // get all
        List<Model.Product> Get(ProductSearchRequest request);

        // get by id
        Model.Product Get(int id);

        // get available products 
        List<Model.Product> GetAvailableProducts();

        // ?? RecommenedProducts


        // update (int id, product product)
        Model.Product Update(int id, ProductInsertRequest request);

        // insert
        Model.Product Insert(ProductInsertRequest request);


        // delete (int id)
        void Delete(int id);


    }
}
