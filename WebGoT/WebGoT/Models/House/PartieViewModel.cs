using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebGoT.Models.House
{
    public class PartieViewModel
    {
        public IndexViewModel House { get; set; }
        public List<IndexViewModel> RandHouses { get; set; }

        public PartieViewModel()
        {
            this.RandHouses = new List<IndexViewModel>();
        }
    }
}