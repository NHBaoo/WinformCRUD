using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSVGV
{
    class CoSo
    {
        public string MaCoSo { get; private set; } 
        public string Ten { get; private set; }
        public int SLGV { get; private set; } // So luong giang vien

        public CoSo(string MaCoSo, string Ten, int SLGV)
        {
            this.MaCoSo = MaCoSo;
            this.Ten = Ten;
            this.SLGV = SLGV;
        }
    }
}
