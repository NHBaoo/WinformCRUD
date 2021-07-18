using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSVGV
{
    class CSDL
    {
        public DataTable dtCoSo { get; set; }
        public DataTable dtGV { get; set; }

        private static CSDL _Instance;

        public static CSDL Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new CSDL();
                }

                return _Instance;
            }

            set
            {

            }
        }

        private CSDL()
        {
            // create table
            this.dtCoSo = new DataTable();
            this.dtGV = new DataTable();

            dtCoSo.Columns.AddRange(new DataColumn[]
            {
                // max length 5
                new DataColumn("MaCoSo", typeof(string)),
                new DataColumn("Ten", typeof(string)),
                new DataColumn("SLGV", typeof(int))
            });

            this.dtGV.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("MaGV", typeof(int)),
                new DataColumn("Ten", typeof(string)),
                new DataColumn("SDT", typeof(string)),
                new DataColumn("NgaySinh", typeof(DateTime)),
                new DataColumn("MaCoSo", typeof(string))
            });

            // Add data into table
            DataRow cs1 = this.dtCoSo.NewRow();
            cs1["MaCoSo"] = "00001";
            cs1["Ten"] = "Co So 1";
            cs1["SLGV"] = 5;

            DataRow cs2 = this.dtCoSo.NewRow();
            cs2["MaCoSo"] = "00002";
            cs2["Ten"] = "Co So 2";
            cs2["SLGV"] = 2;

            this.dtCoSo.Rows.Add(cs1);
            this.dtCoSo.Rows.Add(cs2);

            DataRow gv1 = this.dtGV.NewRow();
            gv1["MaGV"] = 1;
            gv1["Ten"] = "NVA";
            gv1["SDT"] = "0123456781";
            gv1["NgaySinh"] = DateTime.Now;
            gv1["MaCoSo"] = "00001";

            DataRow gv2 = this.dtGV.NewRow();
            gv2["MaGV"] = 2;
            gv2["Ten"] = "NVB";
            gv2["SDT"] = "0123456782";
            gv2["NgaySinh"] = DateTime.Now;
            gv2["MaCoSo"] = "00001";

            DataRow gv3 = this.dtGV.NewRow();
            gv3["MaGV"] = 3;
            gv3["Ten"] = "NVC";
            gv3["SDT"] = "0123456783";
            gv3["NgaySinh"] = DateTime.Now;
            gv3["MaCoSo"] = "00002";


            DataRow gv4 = this.dtGV.NewRow();
            gv4["MaGV"] = 4;
            gv4["Ten"] = "NVD";
            gv4["SDT"] = "0123456784";
            gv4["NgaySinh"] = DateTime.Now;
            gv4["MaCoSo"] = "00002";

            this.dtGV.Rows.Add(gv1);
            this.dtGV.Rows.Add(gv2);
            this.dtGV.Rows.Add(gv3);
            this.dtGV.Rows.Add(gv4);
        }

        public bool SaveGV(DataRow dr)
        {
            foreach (DataRow item in this.dtGV.Rows)
            {
                if (item["MaGV"].Equals(dr["MaGV"]))
                {

                    item["Ten"] = dr["Ten"];
                    item["SDT"] = dr["SDT"];
                    item["NgaySinh"] = dr["NgaySinh"];
                    item["MaCoSo"] = dr["MaCoSo"];

                    return true;
                }
            }

            this.dtGV.Rows.Add(dr);
            return true;
        }
    }
}
