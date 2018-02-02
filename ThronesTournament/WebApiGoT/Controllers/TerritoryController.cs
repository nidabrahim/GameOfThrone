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
    public class TerritoryController : ApiController
    {
        ThronesTournamentManager businessManager = new ThronesTournamentManager();


        public List<TerritoryDTO> GetAllTerritories()
        {
            List<TerritoryDTO> listTerritory = new List<TerritoryDTO>();

            foreach (var territory in businessManager.ListTerritories())
            {
                listTerritory.Add(new TerritoryDTO(territory));
            }

            return listTerritory;
        }
    }
}
