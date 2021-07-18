using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSVGV
{
    class GiangVien
    {
        public int MaGV { get; private set; }
        public string Ten { get; private set; }
        public string SDT { get; private set; }
        public DateTime NgaySinh { get; private set; }
        public string MaCoSo { get; private set; }

        public GiangVien(int maGV, string ten, string sdt, DateTime ngaySinh, string maCoSo)
        {
            this.MaGV = maGV;
            this.Ten = ten;
            this.SDT = sdt;
            this.NgaySinh = ngaySinh;
            this.MaCoSo = maCoSo;
        }

        public GiangVien(GiangVien other)
        {
            this.MaGV = other.MaGV;
            this.Ten = other.Ten;
            this.SDT = other.SDT;
            this.NgaySinh = other.NgaySinh;
            this.MaCoSo = other.MaCoSo;
        }
    }
}
