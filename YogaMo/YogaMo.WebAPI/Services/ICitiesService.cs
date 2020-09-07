using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YogaMo.Model.Requests;

namespace YogaMo.WebAPI.Services
{
    public interface ICitiesService
    {
        List<Model.Cities> Get();
        Model.Cities GetById(int id);
        Model.Cities Insert(CitiesInsertRequest request);
    }
}
