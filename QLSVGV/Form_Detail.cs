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
    public partial class Form_Detail : Form
    {
        private int MaGV;
        public delegate void PointToForm_MNG();
        public PointToForm_MNG reloadForm_MNG;
        public Form_Detail(int MaGV = -1)
        {
            InitializeComponent();
            this.MaGV = MaGV;
            CreateCbbCoSo();
            UpdateView();
        }
        private void UpdateView()
        {
            GiangVien gv = CSDL_OOP.Instance.FindGVByMaGV(this.MaGV);

            if (gv != null)
            {
                this.tb_MaGiangVien.Text = gv.MaGV.ToString();
                this.tb_HoTen.Text = gv.Ten;
                this.tb_Sodienthoai.Text = gv.SDT;

                foreach (CbbItem item in this.cbb_CoSo.Items)
                {
                    if (item.value == gv.MaCoSo)
                    {
                        this.cbb_CoSo.SelectedItem = item;
                    }
                }

                this.dtp.Value = gv.NgaySinh;

            }
        }

        private void CreateCbbCoSo()
        {
            foreach (CoSo coSo in CSDL_OOP.Instance.getAllCoSo())
            {
                this.cbb_CoSo.Items.Add(new CbbItem(coSo.Ten, coSo.MaCoSo));
            }
            this.cbb_CoSo.SelectedIndex = 0;
        }
        private static bool isInValidSDT(string SDT)
        {
            if (SDT.Length != 10)
            {
                return true;
            }

            foreach (char ch in SDT)
            {
                if (ch < 48 || ch > 57)
                {
                    return true;
                }
            }

            return false;
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            int maGV;
            // add mode

            if (this.MaGV == -1)
            {
                maGV = CSDL_OOP.GenerateId();

            }
            else
            {
                // edit mode
                maGV = this.MaGV;
            }

            string ten = this.tb_HoTen.Text.Trim();
            string SDT = this.tb_Sodienthoai.Text.Trim();
            DateTime ngaySinh = this.dtp.Value;
            string maCoSo = ((CbbItem)this.cbb_CoSo.SelectedItem).value;

            if (ten.Length == 0)
            {
                MessageBox.Show("Ten khong duoc de trong!");
                return;
            }

            if (isInValidSDT(SDT))
            {
                MessageBox.Show("SDT phai co dung 10 ki tu so");
                return;
            }

            GiangVien gv = new GiangVien(maGV, ten, SDT, ngaySinh, maCoSo);

            CSDL_OOP.Instance.SaveGV(gv);
            this.reloadForm_MNG();
            this.Dispose();
        }
    }
}
