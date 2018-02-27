using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebGoT.Models.Character
{
    public class IndexViewModel
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public int bravoury { get; set; }
        public int crazyness { get; set; }
        public int pv { get; set; }

        public IndexViewModel(string firstName, string lastName, int bravoury, int crazyness, int pv)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.bravoury = bravoury;
            this.crazyness = crazyness;
            this.pv = pv;
        }
    }
}