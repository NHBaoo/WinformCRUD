using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSVGV
{
    class CbbItem
    {
        public string key { get; set; }
        public string value { get; set; }

        public CbbItem(string key, string value)
        {
            this.key = key;
            this.value = value;
        }

        public override string ToString()
        {
            return this.key;
        }
    }
}
