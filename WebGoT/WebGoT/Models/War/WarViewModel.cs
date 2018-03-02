using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebGoT.Models.War
{
    public class WarViewModel
    {
        public List<FightViewModel> Fights { get; set; }

        public WarViewModel(List<FightViewModel> fights)
        {
            Fights = fights;
        }
    }
}