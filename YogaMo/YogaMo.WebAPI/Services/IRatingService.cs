using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YogaMo.Model.Requests;

namespace YogaMo.WebAPI.Services
{
    public interface IRatingService
    {
        List<Model.Rating> Get();
        Model.Rating Get(int id);
        Model.Rating GetByProductAndClient(int productId, int clientId);
        // public decimat GetAverageRating(int productId)

        Model.Rating Update(int id, RatingInsertRequest request);
        Model.Rating Insert(RatingInsertRequest request);
        void Delete(int id);


    }
}
