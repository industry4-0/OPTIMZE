using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vimachem.Hackathon
{
    public struct RecipeParameter
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
        public string Tolerance { get; set; }
    }
}
