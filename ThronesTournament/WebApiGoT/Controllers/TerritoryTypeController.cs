using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLayer;
using WebApiGoT.Models;


namespace WebApiGoT.Controllers
{
    [RoutePrefix("api/TerritoryType")]
    public class TerritoryTypeController : ApiController
    {
        ThronesTournamentManager businessManager = new ThronesTournamentManager();


        [Route("GetAllTerritoryType")]
        public List<TerritoryTypeDTO> GetAllTerritoryType()
        {
            List<TerritoryTypeDTO> listTerritoryType = new List<TerritoryTypeDTO>();

            foreach (var territoryType in businessManager.ListTerritoryType())
            {
                listTerritoryType.Add(new TerritoryTypeDTO()
                {
                    id = territoryType.IdTerritoryType,
                    name = territoryType.Name

                });
            }

            return listTerritoryType;
        }


        [Route("GetTerritoryTypeById/{id:int}")]
        public TerritoryTypeDTO GetTerritoryTypeById(int id)
        {
            var territoryType = businessManager.GetTerritoryType(id);

            TerritoryTypeDTO territoryTypeDTO = new TerritoryTypeDTO() {
                id = territoryType.IdTerritoryType,
                name = territoryType.Name
            };

            return territoryTypeDTO;
        }
    }
}
