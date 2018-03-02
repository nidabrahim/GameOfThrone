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


        public List<House> ListRandomHouses()
        {
            List<House> res = dal.GetAllHouses();
            List<House> randomHouses = new List<House>();
           
            res.Shuffle();
            for (int i=0; i<8; i++)
            {
                randomHouses.Add(res.ElementAt(i));
            }
            
            return res;
        }

        public List<String> ListHousesNames()
        {
            List<String> housesName = new List<string>();
            ListHouses().ForEach(h => housesName.Add(h.Name));

            return housesName;
        }

        public void AddHouse(string name, int numberOfUnities)
        {

            House house = new House();
            house.Name = name;
            house.NumberOfUnities = numberOfUnities;

            dal.SaveHouse(house);
        }

        public House GetHouse(int idHouse)
        {
            return dal.GetHouseById(idHouse);
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

        public void UpdateHouse(int idHouse, string name, int numberOfUnities)
        {
            House house = dal.GetHouseById(idHouse);
            house.Name = name;
            house.NumberOfUnities = numberOfUnities;

            dal.UpdateHouse(house);
        }

        public void DeleteHouse(int idHouse)
        {
            dal.DeleteHouse(dal.GetHouseById(idHouse));
        }



        public List<War> ListWars()
        {
            List<War> res = new List<War>();
            dal.GetAllWars().ForEach(h => res.Add(h));

            return res;
        }

        public War GetWar(int idWar)
        {
            return dal.GetWarById(idWar);
        }

        public War GetLastWar()
        {
            int idLastWar = dal.GetLastId("War");
            return dal.GetWarById(idLastWar);
        }

        public int AddWar()
        {
            dal.SaveWar();
            return 1;
        }
        


        public List<Fight> ListFights()
        {
            List<Fight> res = new List<Fight>();
            dal.GetAllFights().ForEach(h => res.Add(h));

            return res;
        }

        public Fight GetFight(int idFight)
        {
            return dal.GetFightById(idFight);
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

        public void DeleteFight(int idFight)
        {
            dal.DeleteFight(dal.GetFightById(idFight));
        }



        public List<Character> ListCharacters()
        {
            List<Character> res = new List<Character>();
            dal.GetAllCharacters().ForEach(c => res.Add(c));

            return res;
        }

        public Character GetCharacter(int idCharacter)
        {
            return dal.GetCharacterById(idCharacter);
        }

        public void AddCharacter(string FirstName, string LastName, int Bravoury, int Crazyness, int Pv, int IdType, int IdHouse)
        {

            Character character = new Character();
            character.FirstName = FirstName;
            character.LastName = LastName;
            character.Bravoury = Bravoury;
            character.Crazyness = Crazyness;
            character.Pv = Pv;
            character.Type = dal.GetCharacterTypeById(IdType);
            character.House = dal.GetHouseById(IdHouse);

            dal.SaveCharacter(character);
        }

        public void UpdateCharacter(int idCharacter, string FirstName, string LastName, int Bravoury, int Crazyness, int Pv, int IdType, int IdHouse)
        {
            Character character = dal.GetCharacterById(idCharacter);
            character.FirstName = FirstName;
            character.LastName = LastName;
            character.Bravoury = Bravoury;
            character.Crazyness = Crazyness;
            character.Pv = Pv;
            character.Type = dal.GetCharacterTypeById(IdType);
            character.House = dal.GetHouseById(IdHouse);

            dal.UpdateCharacter(character);
        }

        public void DeleteCharacter(int idCharacter)
        {
            dal.DeleteCharacter(dal.GetCharacterById(idCharacter));
        }



        public List<Territory> ListTerritories()
        {
            List<Territory> res = new List<Territory>();
            dal.GetAllTerritories().ForEach(t => res.Add(t));

            return res;
        }

        public Territory GetTerritory(int idTerritory)
        {
            return dal.GetTerritoryById(idTerritory);
        }

        public void AddTerritory(int idTerritoryType, int idOwner)
        {
            Territory territory = new Territory();
            territory.TerritoryType = dal.GetTerritoryTypeById(idTerritoryType);
            territory.Owner = dal.GetCharacterById(idOwner);

            dal.SaveTerritory(territory);
        }

        public void UpdateTerritory(int idTerritory, int idTerritoryType, int idOwner)
        {
            Territory territory = dal.GetTerritoryById(idTerritory);
            territory.TerritoryType = dal.GetTerritoryTypeById(idTerritoryType);
            territory.Owner = dal.GetCharacterById(idOwner);

            dal.UpdateTerritory(territory);
        }

        public void DeleteTerritory(int idTerritory)
        {
            dal.DeleteTerritory(dal.GetTerritoryById(idTerritory));
        }



        public List<TerritoryType> ListTerritoryType()
        {
            List<TerritoryType> res = dal.GetAllTerritoryTypes();

            return res;
        }

        public TerritoryType GetTerritoryType(int id)
        {
            return dal.GetTerritoryTypeById(id);
        }

        public void AddTerritoryType(String name)
        {
            TerritoryType territoryType = new TerritoryType();
            territoryType.Name = name;

            dal.SaveTerritoryType(territoryType);
        }



        public List<CharacterType> ListCharacterType()
        {
            List<CharacterType> res = dal.GetAllCharacterTypes();

            return res;
        }

        public CharacterType GetCharacterType(int id)
        {
            return dal.GetCharacterTypeById(id);
        }

        public void AddCharacterType(String name)
        {
            CharacterType characterType = new CharacterType();
            characterType.Name = name;

            dal.SaveCharacterType(characterType);
        }



        public List<RelationType> ListRelationType()
        {
            List<RelationType> res = dal.GetAllRelationTypes();

            return res;
        }

        public RelationType GetRelationType(int id)
        {
            return dal.GetRelationTypeById(id);
        }

        public void AddRelationType(String name)
        {
            RelationType relationType = new RelationType();
            relationType.Name = name;

            dal.SaveRelationType(relationType);
        }


        
        public House Combat(int idHouseChalleging, int idHouseChalleged, int idTerritory)
        {
            double scoreH1, scoreH2;
            House houseChalleging = dal.GetHouseById(idHouseChalleging);
            House houseChalleged = dal.GetHouseById(idHouseChalleged);
            Territory territory = dal.GetTerritoryById(idTerritory);
            Random rand = new Random();

            //Unité
            scoreH1 = houseChalleging.NumberOfUnities;
            scoreH2 = houseChalleged.NumberOfUnities;

            //Territory
            if (TerritoryOwner(houseChalleging, territory)) scoreH1 *= 10;
            if (TerritoryOwner(houseChalleged, territory)) scoreH2 *= 10;

            //Character
            scoreH1 += CharacterScore(houseChalleging);
            scoreH2 += CharacterScore(houseChalleged);

            //House Moral
            scoreH1 *= GetHouseMoral(houseChalleging.idEntityObject);
            scoreH2 *= GetHouseMoral(houseChalleged.idEntityObject);

            //Character Warrior Witch
            if (houseChalleging.isHouseContain(new CharacterType(CharaterTypeEnum.WARRIOR)) ||
                houseChalleging.isHouseContain(new CharacterType(CharaterTypeEnum.WITCH)) ) scoreH1 *= rand.Next(2,11);
            if (houseChalleged.isHouseContain(new CharacterType(CharaterTypeEnum.WARRIOR)) ||
                houseChalleged.isHouseContain(new CharacterType(CharaterTypeEnum.WITCH))) scoreH2 *= rand.Next(2, 11);

            //Character Loser
            if (houseChalleging.isHouseContain(new CharacterType(CharaterTypeEnum.LOSER))) scoreH1 -= rand.Next(1,101);
            if (houseChalleged.isHouseContain(new CharacterType(CharaterTypeEnum.LOSER))) scoreH2 -= rand.Next(1, 101);

            //Character Tactician Leader
            if (houseChalleging.isHouseContain(new CharacterType(CharaterTypeEnum.TACTICIAN)) ||
                houseChalleging.isHouseContain(new CharacterType(CharaterTypeEnum.LEADER))) scoreH1 += rand.Next(2, 6);
            if (houseChalleged.isHouseContain(new CharacterType(CharaterTypeEnum.TACTICIAN)) ||
                houseChalleged.isHouseContain(new CharacterType(CharaterTypeEnum.LEADER))) scoreH2 += rand.Next(2, 6);

            
            //Winning House
            House winning = (scoreH1 > scoreH2) ? houseChalleging : houseChalleged;

            AddFight(houseChalleging, houseChalleged, winning, territory);

            return winning;
        }

        private Double GetHouseMoral(int idHouse)
        {
            Double moral = 1;
            List<Fight> fights = dal.GetFightsByIdHouse(idHouse);
            if (fights.Count != 0)
            {
                Fight lastFight = fights.Last<Fight>();
                if (lastFight.WinningHouse.idEntityObject == idHouse)
                    moral = 1.2;
                else moral = 0.8;
            }

            return moral;
        }

        private Boolean TerritoryOwner(House house, Territory territory)
        {
            Boolean isOwner = false;
            foreach(Character character in house.Housers) {
                if (character.idEntityObject == territory.Owner.idEntityObject)
                    isOwner = true;
            }

            return isOwner;
        }

        private int CharacterScore(House house)
        {
            int score = 0;
            foreach (Character character in house.Housers)
                score += character.Bravoury + character.Crazyness + character.Pv;

            return score;
        }

        
    }
    static class MyExtensions
    {
        private static Random rng = new Random();
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
