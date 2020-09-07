using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YogaMo.Model.Requests;

namespace YogaMo.WebAPI.Services
{
    public interface IClientService
    {
        // get all, get by name (city??)
        List<Model.Client> Get(ClientSearchRequest request);

        // get by id
        Model.Client Get(int id);

        // TODO?? get top 5 clients who bought the most products

        // insert (for registration?)
        Model.Client Insert(ClientInsertRequest request);

        // update
        Model.Client Update(int id, ClientUpdateRequest request);


        // delete

        // Authentication 
        Model.Client Authenticate(string username, string password);

        Model.Client MyProfile();

        Model.Client GetCurrentClient();
        void SetCurrentClient(Model.Client user);
    }
}
