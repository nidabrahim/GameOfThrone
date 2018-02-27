using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EntitiesLayer;

namespace WebApiGoT.Models
{
    public class HouseDTO
    {
        public String Name { get; set; }
        public int NumberOfUnities { get; set; }
        public List<CharacterDTO> Housers;


        public HouseDTO(House house) {

            this.Name = house.Name;
            this.NumberOfUnities = house.NumberOfUnities;

            this.Housers = new List<CharacterDTO>();

            foreach(var character in house.Housers)
            {
                this.Housers.Add(new CharacterDTO(character));
            }
        }
    }
}