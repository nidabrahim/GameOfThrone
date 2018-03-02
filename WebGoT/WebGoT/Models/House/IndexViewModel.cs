using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Character = WebGoT.Models.Character;

namespace WebGoT.Models.House
{
    public class IndexViewModel
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public int NumberOfUnities { get; set; }
        public List<Character.IndexViewModel> Housers { get; set; }

        public IndexViewModel(int id, String name, int numberOfUnities, List<Character.IndexViewModel> Housers)
        {
            this.Id = id;
            this.Name = name;
            this.NumberOfUnities = numberOfUnities;
            this.Housers = new List<Character.IndexViewModel>();
            this.Housers = Housers;
        }
    }
}