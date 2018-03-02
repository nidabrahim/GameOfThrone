using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebGoT.Models.War
{
    public class TerritoryViewModel
    {
        public string Owner { get; set; }
        public string TerritoryType { get; set; }

        public TerritoryViewModel(string owner, string territoryType)
        {
            this.Owner = owner;
            this.TerritoryType = territoryType;
        }
    }
}