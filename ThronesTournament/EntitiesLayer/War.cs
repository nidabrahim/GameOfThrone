using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer
{
    public class War : EntityObject
    {
        private List<Fight> _fights;


        public List<Fight> Fights
        {
            get { return _fights; }
            set { _fights = value; }
        }


        public War() { }

    }
}
