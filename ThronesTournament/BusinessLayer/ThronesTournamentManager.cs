using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using EntitiesLayer;

namespace BusinessLayer
{
    public class ThronesTournamentManager
    {
        private IDal dal;

        public ThronesTournamentManager()
        {
            dal = DalManager.Instance.Dal;
        }

        public List<House> ListHouses()
        {
            List<House> res = new List<House>();
            dal.GetAllHouses().ForEach(h => res.Add(h) );

            return res;
        }

        public List<House> GetAllHousesSup200Unit()
        {
            List<House> houses = ListHouses();
            foreach (House h in houses)
            {
                if (h.NumberOfUnities < 200) houses.Remove(h);
            }
            return houses;
        }

        public List<War> ListWars()
        {
            List<War> res = new List<War>();
            dal.GetAllWars().ForEach(h => res.Add(h));

            return res;
        }

        public List<Fight> ListFights()
        {
            List<Fight> res = new List<Fight>();
            dal.GetAllFights().ForEach(h => res.Add(h));

            return res;
        }

        public List<String> ListHousesSup200Unit()
        {
            List<House> houses = dal.GetAllHouses();
            List<String> res = new List<String>();

            foreach (House h in houses)
            {
                if (h.NumberOfUnities < 200) res.Add(h.ToString());
            }

            return res;

        }

        public List<Character> ListCharacters()
        {
            List<Character> res = new List<Character>();
            dal.GetAllCharacters().ForEach(c => res.Add(c));

            return res;
        }

        public List<Territory> ListTerritories()
        {
            List<Territory> res = new List<Territory>();
            dal.GetAllTerritories().ForEach(t => res.Add(t));

            return res;
        }

        public void AddHouse(string name, int numberOfUnities) {

            House house = new House();
            house.Name = name;
            house.NumberOfUnities = numberOfUnities;

            dal.SaveHouse(house);
        }

        public void AddCharacter(string FirstName, string LastName, int Bravoury,int Crazyness, int Pv, int IdType)
        {

            Character character = new Character();
            character.FirstName = FirstName;
            character.LastName = LastName;
            character.Bravoury = Bravoury;
            character.Crazyness = Crazyness;
            character.Pv = Pv;
            character.Type = dal.GetCharacterTypeById(IdType);
            
            dal.SaveCharacter(character);
        }

        public void AddFight(House HouseChalleging, House HouseChalleged, House WinningHouse, Territory territory)
        {
            Fight fight = new Fight();
            fight.HouseChalleging = HouseChalleging;
            fight.HouseChalleged = HouseChalleged;
            fight.WinningHouse = WinningHouse;
            fight.Territory = territory;
            fight.War = GetLastWar();

            dal.SaveFight(fight);
        }

        public void AddTerritory(int idTerritoryType, int idOwner)
        {
            Territory territory = new Territory();
            territory.TerritoryType = dal.GetTerritoryTypeById(idTerritoryType);
            territory.Owner = dal.GetCharacterById(idOwner);

            dal.SaveTerritory(territory);
        }

        public House Combat(int idHouseChalleging, int idHouseChalleged, int idTerritory)
        {
            double scoreH1, scoreH2;
            House houseChalleging = dal.GetHouseById(idHouseChalleging);
            House houseChalleged = dal.GetHouseById(idHouseChalleged);
            Territory territory = dal.GetTerritoryById(idTerritory);

            //Unité
            scoreH1 = houseChalleging.NumberOfUnities;
            scoreH2 = houseChalleged.NumberOfUnities;

            //House 1
            scoreH1 *= GetHouseMoral(houseChalleging.idEntityObject);
            //House 2
            scoreH2 *= GetHouseMoral(houseChalleged.idEntityObject);

            if (houseChalleging.isHouseContain(new CharacterType(CharaterTypeEnum.WARRIOR))){
                scoreH1 += 10;
            }

            if (houseChalleged.isHouseContain(new CharacterType(CharaterTypeEnum.WARRIOR))){
                scoreH2 += 10;
            }

            House winning = (scoreH1 > scoreH2) ? houseChalleging : houseChalleged;

            AddFight(houseChalleging, houseChalleged, winning, territory);

            return winning;
        }

        private Double GetHouseMoral(int idHouse)
        {
            Double moral = 1;
            List<Fight> fights = dal.GetFightsByIdHouse(idHouse);
            Fight lastFight = fights.Last<Fight>();
            if (lastFight.WinningHouse.idEntityObject == idHouse)
                moral = 1.2;
            else moral = 0.8;
           

            return moral;
        }

        public War GetLastWar()
        {
            int idLastWar = dal.GetLastId("War");
            return dal.GetWarById(idLastWar);
        }
       

    }
}
