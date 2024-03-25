using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Base
{
    public class AssociationRule
    {
        public double Support { get; set; }
        public Tuple<List<string>, List<string>> Rule { get; set; }
    }

}
