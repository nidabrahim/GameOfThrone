using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLayer;
using WebApiGoT.Models;
using System.Web.Http.Cors;

namespace WebApiGoT.Controllers
{
    [RoutePrefix("api/war")]
    [EnableCors("*", "*", "*")]
    public class WarController : ApiController
    {
        ThronesTournamentManager businessManager = new ThronesTournamentManager();

        [Route("GetAllWars")]
        public List<WarDTO> GetAllWars()
        {
            List<WarDTO> listWar= new List<WarDTO>();

            foreach (var war in businessManager.ListWars())
            {
                listWar.Add(new WarDTO() {
                    Fights = war.Fights.Select(fight => new FightDTO()
                    {
                        HouseChalleging = fight.HouseChalleging.Name,
                        HouseChalleged = fight.HouseChalleged.Name,
                        WinningHouse = fight.WinningHouse.Name,
                        Territory = new TerritoryDTO()
                        {
                            Owner = fight.Territory.Owner.FirstName + " " + fight.Territory.Owner.LastName,
                            TerritoryType = fight.Territory.TerritoryType.ToString()
                        }
                    }).ToList<FightDTO>()
                });
            }

            return listWar;
        }

        [Route("GetWarById/{id:int}")]
        public WarDTO GetWarById(int id)
        {
            var war = businessManager.GetWar(id);

            WarDTO warDTO = new WarDTO() {
                Fights = war.Fights.Select(fight => new FightDTO()
                {
                    HouseChalleging = fight.HouseChalleging.Name,
                    HouseChalleged = fight.HouseChalleged.Name,
                    WinningHouse = fight.WinningHouse.Name,
                    Territory = new TerritoryDTO()
                    {
                        Owner = fight.Territory.Owner.FirstName + " " + fight.Territory.Owner.LastName,
                        TerritoryType = fight.Territory.TerritoryType.ToString()
                    }
                }).ToList<FightDTO>()
            };

            return warDTO;
        }

        [Route("GetWar")]
        public int GetWar()
        {
            int result = businessManager.AddWar();
            
            return result;
        }

        [Route("GetLastWar")]
        public WarDTO GetLastWar()
        {
            var war = businessManager.GetLastWar();

            WarDTO warDTO = new WarDTO()
            {
                Fights = war.Fights.Select(fight => new FightDTO()
                {
                    HouseChalleging = fight.HouseChalleging.Name,
                    HouseChalleged = fight.HouseChalleged.Name,
                    WinningHouse = fight.WinningHouse.Name,
                    Territory = new TerritoryDTO()
                    {
                        Owner = fight.Territory.Owner.FirstName + " " + fight.Territory.Owner.LastName,
                        TerritoryType = fight.Territory.TerritoryType.ToString()
                    }
                }).ToList<FightDTO>()
            };

            return warDTO;
        }
    }
}
