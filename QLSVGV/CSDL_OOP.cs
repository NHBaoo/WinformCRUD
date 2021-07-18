using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSVGV
{
    class CSDL_OOP
    {
        public delegate bool MyCompare(object sv1, object sv2);
        private static CSDL_OOP _Instance;
        public static CSDL_OOP Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new CSDL_OOP();
                }
                return _Instance;
            }
            set
            {

            }
        }
        private CSDL_OOP()
        {

        }

        public List<CoSo> getAllCoSo()
        {
            List<CoSo> listCoSo = new List<CoSo>();

            foreach (DataRow dr in CSDL.Instance.dtCoSo.Rows)
            {
                listCoSo.Add(ExtractCoSo(dr));
            }

            return listCoSo;
        }

        private static CoSo ExtractCoSo(DataRow cs)
        {
            string maCoSo = cs["MaCoSo"].ToString();
            string ten = cs["Ten"].ToString();
            int slGV = Convert.ToInt32(cs["SLGV"].ToString());

            return new CoSo(maCoSo, ten, slGV);
        }

        private static GiangVien ExtractGV(DataRow gv)
        {
            int maGV = Convert.ToInt32(gv["MaGV"].ToString());
            string ten = gv["Ten"].ToString();
            string sdt = gv["SDT"].ToString();
            DateTime ngaySinh = Convert.ToDateTime(gv["NgaySinh"].ToString());
            string maCoSo = gv["MaCoSo"].ToString();

            return new GiangVien(maGV, ten, sdt, ngaySinh, maCoSo);
        }

        public List<GiangVien> GetAllGV()
        {
            return FindGVsByMaCoSo("00000");
        }

        public List<GiangVien> FindGVsByMaCoSo(string maCoSo)
        {
            List<GiangVien> listGV = new List<GiangVien>();

            foreach (DataRow drSV in CSDL.Instance.dtGV.Rows)
            {
                GiangVien gv = ExtractGV(drSV);
                if (gv.MaCoSo.Equals(maCoSo) || maCoSo.Equals("00000"))
                {
                    listGV.Add(gv);
                }
            }

            return listGV;
        }

        public GiangVien FindGVByMaGV(int maGV)
        {
            foreach (DataRow drSV in CSDL.Instance.dtGV.Rows)
            {
                GiangVien gv = ExtractGV(drSV);
                if (gv.MaGV == maGV)
                {
                    return new GiangVien(gv);
                }
            }

            return null;
        }

        public static List<GiangVien> SortBy(MyCompare myCompare, List<GiangVien> listSV)
        {
            List<GiangVien> sortedList = CloneListGV(listSV);
            int n = sortedList.Count;

            for (int i = 0; i < n - 1; i++)
            {
                int minOrMaxIdx = i;
                for (int j = i + 1; j < n; j++)
                {
                    GiangVien gv1 = sortedList[j];
                    if (myCompare(sortedList[minOrMaxIdx], gv1))
                    {
                        minOrMaxIdx = j;
                    }
                }

                if (minOrMaxIdx != i)
                {
                    GiangVien tmp = sortedList[i];
                    sortedList[i] = sortedList[minOrMaxIdx];
                    sortedList[minOrMaxIdx] = tmp;
                }
            }

            return sortedList;
        }

        private static List<GiangVien> CloneListGV(List<GiangVien> listSV)
        {
            List<GiangVien> result = new List<GiangVien>();

            for (int i = 0; i < listSV.Count; i++)
            {
                result.Add(new GiangVien(listSV[i]));
            }

            return result;
        }

        public static bool CompareTenGVDESC(object sv1, object sv2)
        {
            return ((GiangVien)sv1).Ten.CompareTo(((GiangVien)sv2).Ten) < 0;
        }

        public static bool CompareMaGVDESC(object sv1, object sv2)
        {
            return ((GiangVien)sv1).MaGV < ((GiangVien)sv2).MaGV;
        }

        public static int GenerateId()
        {
            List<GiangVien> listGV = CSDL_OOP.Instance.GetAllGV();
            int maxId = 0;
            foreach (GiangVien gv in listGV)
            {
                if (gv.MaGV > maxId)
                {
                    maxId = gv.MaGV;
                }
            }

            return maxId + 1;
        }

        public bool SaveGV(GiangVien gv)
        {
            DataRow dr = CSDL.Instance.dtGV.NewRow();
            dr["MaGV"] = gv.MaGV;
            dr["Ten"] = gv.Ten;
            dr["SDT"] = gv.SDT;
            dr["NgaySinh"] = gv.NgaySinh;
            dr["MaCoSo"] = gv.MaCoSo;

            // update
            foreach (DataRow item in CSDL.Instance.dtGV.Rows)
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

            // add new
            CSDL.Instance.dtGV.Rows.Add(dr);
            return true;
        }

        public bool DeleteGVByMaGV(int maGV)
        {
            foreach (DataRow dr in CSDL.Instance.dtGV.Rows)
            {
                if (dr["MaGV"].Equals(maGV))
                {
                    // Khong duoc xoa Giao Vien khi chi con duy nhat 1 GV trong CoSo do
                    List<GiangVien> listGV = FindGVsByMaCoSo(dr["MaCoSo"].ToString());
                    if (listGV.Count == 1)
                    {
                        return false;
                    }

                    CSDL.Instance.dtGV.Rows.Remove(dr);
                    return true;
                }
            }

            return false;
        }
    }
}
