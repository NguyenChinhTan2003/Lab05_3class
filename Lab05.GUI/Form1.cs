using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lab05.BUS;
using Lab05.DAL.Entities;
namespace Lab05.GUI
{
    public partial class Form1 : Form
    {
        private readonly StudentService studentService = new StudentService();
        private readonly FacultyService facultyService = new FacultyService();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var listFacultys = facultyService.GetAll();
            var listStudents = studentService.GetALL();
            FillFacultyCombobox(listFacultys);
            BindGird(listStudents);
        }
        private void FillFacultyCombobox(List<Faculty> listFacultys)
        {
            listFacultys.Insert(0, new Faculty());
            this.cmbKhoa.DataSource = listFacultys;

            this.cmbKhoa.DisplayMember = "FacultyName";
            this.cmbKhoa.ValueMember = "FacultyID";
        }
        private void BindGird(List<Student> listStudents)
        {
            dgvQLSV.Rows.Clear();
            foreach (var item in listStudents)
            {
                int index = dgvQLSV.Rows.Add();
                dgvQLSV.Rows[index].Cells[0].Value =item.StudentID;
                dgvQLSV.Rows[index].Cells[1].Value = item.FullName;
                if (item.Faculty != null)
                    dgvQLSV.Rows[index].Cells[2].Value = item.Faculty.FacultyName;
                dgvQLSV.Rows[index].Cells[3].Value = item.AverageScore + "";
                if (item.MajorID != null)
                    dgvQLSV.Rows[index].Cells[4].Value = item.MajorID + "";
            }

        }
        /*private void ShowAvatar(string ImageName)
        {
            if (string.IsNullOrEmpty(ImageName))
            {
                pictureBox1.Image = null;
            }
            else
            {
                string parentDirectory = 
            }
        }*/

        private void setGridViewStyle(DataGridView dgview)
        {
            dgview.BorderStyle = BorderStyle.None;
            dgview.DefaultCellStyle.SelectionForeColor = Color.DarkTurquoise;
            dgview.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgview.BackgroundColor = Color.White;
            dgview.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void ckbCN_CheckedChanged(object sender, EventArgs e)
        {
            var listStudents = new List<Student>();
            if (this.ckbCN.Checked)
                listStudents = studentService.GetAllHasMoMajor();
            else
                listStudents = studentService.GetALL();
            BindGird(listStudents);
        }

        private void đăngKýChuyênNgànhToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDKCN f2 = new frmDKCN();
            f2.ShowDialog();
            this.Close();
        }
    }
}
