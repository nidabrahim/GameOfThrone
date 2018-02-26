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
    [RoutePrefix("api/fight")]
    public class FightController : ApiController
    {
        ThronesTournamentManager businessManager = new ThronesTournamentManager();

        [Route("GetAllFights")]
        public List<FightDTO> GetAllFights()
        {
            List<FightDTO> listFight = new List<FightDTO>();

            foreach (var fight in businessManager.ListFights())
            {
                listFight.Add(new FightDTO(fight));
            }

            return listFight;
        }
    }
}
