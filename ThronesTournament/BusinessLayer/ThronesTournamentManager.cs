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
            List<String> res = new List<String>();
            dal.GetAllHousesSup200Unit().ForEach(h => res.Add(h.ToString()));

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

    }
}
