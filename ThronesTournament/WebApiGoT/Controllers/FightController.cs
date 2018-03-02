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

   
    [RoutePrefix("api/fight")]
    [EnableCors("*","*","*")]
    public class FightController : ApiController
    {
        ThronesTournamentManager businessManager = new ThronesTournamentManager();

        [Route("GetAllFights")]
        public List<FightDTO> GetAllFights()
        {
            List<FightDTO> listFight = new List<FightDTO>();

            foreach (var fight in businessManager.ListFights())
            {
                listFight.Add(new FightDTO() {
                    HouseChalleging = fight.HouseChalleging.Name,
                    HouseChalleged = fight.HouseChalleged.Name,
                    WinningHouse = fight.WinningHouse.Name,
                    Territory = new TerritoryDTO()
                    {
                        Owner = fight.Territory.Owner.FirstName + " " + fight.Territory.Owner.LastName,
                        TerritoryType = fight.Territory.TerritoryType.ToString()
                    }
                });
            }

            return listFight;
        }


        [Route("GetFightById/{id:int}")]
        public FightDTO GetFightById(int id)
        {
            var fight = businessManager.GetFight(id);

            FightDTO fightDTO = new FightDTO() {
                HouseChalleging = fight.HouseChalleging.Name,
                HouseChalleged = fight.HouseChalleged.Name,
                WinningHouse = fight.WinningHouse.Name,
                Territory = new TerritoryDTO()
                {
                    Owner = fight.Territory.Owner.FirstName + " " + fight.Territory.Owner.LastName,
                    TerritoryType = fight.Territory.TerritoryType.ToString()
                }
            };

            return fightDTO;
        }

        [Route("DeleteFight/{fight_id}")]
        [HttpDelete]
        public void DeleteFight(int fight_id)
        {
            businessManager.DeleteFight(fight_id);

        }

        [Route("GetWinningHouse/{idHouseChalleging:int}/{idHouseChalleged:int}")]
        public int GetWinningHouse(int idHouseChalleging, int idHouseChalleged)
        {
            var house = businessManager.Combat(idHouseChalleging, idHouseChalleged,1);

            return house.idEntityObject;
        }
    }
}
