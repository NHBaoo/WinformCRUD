using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLSVGV
{
    public partial class Form_MNG : Form
    {
        public Form_MNG()
        {
            InitializeComponent();
            Createcbb_CoSo();
            Createcbb_Sort();
            Show("00000");
        }
        private void Createcbb_CoSo()
        {
            this.cbb_CoSo.Items.Add(new CbbItem("ALL", "00000"));
            foreach (CoSo coSo in CSDL_OOP.Instance.getAllCoSo())
            {
                this.cbb_CoSo.Items.Add(new CbbItem(coSo.Ten, coSo.MaCoSo));
            }
            this.cbb_CoSo.SelectedIndex = 0;
        }

        private void Createcbb_Sort()
        {
            this.cbb_Sort.Items.Add(new CbbItem("Ten giam dan", "Ten"));
            this.cbb_Sort.Items.Add(new CbbItem("MaGV giam dan", "MaGV"));
        }
        private void Show(string maCoSo, string searchText = "", string sortBy = "")
        {
            List<GiangVien> tmpList = CSDL_OOP.Instance.FindGVsByMaCoSo(maCoSo);
            List<GiangVien> result = new List<GiangVien>();

            foreach (GiangVien gv in tmpList)
            {
                ////if (gv.Ten.ToLower().StartsWith(searchText.ToLower()))
                ////{
                ////    result.Add(gv);
                ////}

                if (gv.Ten.ToLower().Contains(searchText.ToLower()))
                {
                    result.Add(gv);
                }
            }

            if (sortBy.Equals("MaGV"))
            {
                result = CSDL_OOP.SortBy(CSDL_OOP.CompareMaGVDESC, result);
            }
            else if (sortBy.Equals("Ten"))
            {
                result = CSDL_OOP.SortBy(CSDL_OOP.CompareTenGVDESC, result);
            }

            this.dgv.DataSource = result;
        }

        public void ReloadView()
        {
            string maCoSo = GetSelectedMaCoSo();
            string searchText = GetSearchText();

            Show(maCoSo, searchText);
        }

        private string GetSelectedMaCoSo()
        {
            CbbItem map = (CbbItem)this.cbb_CoSo.SelectedItem;
            return map.value;
        }
        private string GetSearchText()
        {
            return this.tb_Search.Text;
        }
        private int GetSelectedMaGV()
        {
            if (this.dgv.SelectedRows.Count == 0)
            {
                return -1;
            }

            DataGridViewRow dgvRow = this.dgv.SelectedRows[0];
            int maGV = Convert.ToInt32(dgvRow.Cells["MaGV"].Value.ToString());

            return maGV;
        }

        private void btn_Show_Click(object sender, EventArgs e)
        {
            string maCoSo = GetSelectedMaCoSo();
            Show(maCoSo);
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            string maCoSo = GetSelectedMaCoSo();
            string searchText = GetSearchText();

            Show(maCoSo, searchText);
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            Form_Detail f = new Form_Detail();
            f.reloadForm_MNG = new Form_Detail.PointToForm_MNG(this.ReloadView);
            f.Show();
        }

        private void btn_Edit_Click(object sender, EventArgs e)
        {
            int MaGV = GetSelectedMaGV();

            if (MaGV == -1)
            {
                MessageBox.Show("Vui long chon SV can Edit");
                return;
            }

            Form_Detail f = new Form_Detail(MaGV);
            f.reloadForm_MNG = new Form_Detail.PointToForm_MNG(this.ReloadView);
            f.Show();
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            int MaGV = GetSelectedMaGV();

            if (MaGV == -1)
            {
                MessageBox.Show("Vui long chon SV can Xoa");
                return;
            }

            if (CSDL_OOP.Instance.DeleteGVByMaGV(MaGV))
            {
                ReloadView();
            }
            else
            {
                MessageBox.Show("Id khong ton tai hoac CoSo chi con dung duy nhat 1 GiaoVien.");
            }
        }

        private void btn_Sort_Click(object sender, EventArgs e)
        {
            CbbItem sortedBy = (CbbItem)this.cbb_Sort.SelectedItem;

            if (sortedBy == null)
            {
                MessageBox.Show("Vui long chon sortBy combobox");
                return;
            }

            string maCoSo = GetSelectedMaCoSo();
            string searchText = GetSearchText();

            Show(maCoSo, searchText, sortedBy.value);
        }
    }
}
