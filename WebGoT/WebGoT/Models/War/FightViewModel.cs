using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebGoT.Models.War
{
    public class FightViewModel
    {
        public string HouseChalleging { get; set; }
        public string HouseChalleged { get; set; }
        public string WinningHouse { get; set; }
        public TerritoryViewModel Territory { get; set; }

        public FightViewModel(string houseChalleging, string houseChalleged, string winningHouse, TerritoryViewModel territory)
        {
            this.HouseChalleging = houseChalleging;
            this.HouseChalleged = houseChalleged;
            this.WinningHouse = winningHouse;
            this.Territory = territory;
        }
    }
}