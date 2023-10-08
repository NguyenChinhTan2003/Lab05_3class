using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Lab05.BUS;
using Lab05.DAL.Entities;
namespace Lab05.GUI
{
    public partial class frmDKCN : Form
    {
        private readonly StudentService studentService = new StudentService();
        private readonly FacultyService facultyService = new FacultyService();
        private readonly MajorSevice majorSevice = new MajorSevice();
        public frmDKCN()
        {
            InitializeComponent();
        }

        private void frmDKCN_Load(object sender, EventArgs e)
        {
            var listFacultys = facultyService.GetAll();
           
            FillFacultyCombobox(listFacultys);
            
        }
        private void FillFacultyCombobox(List<Faculty> listFacultys)
        {
            this.cmbFaculty.DataSource = listFacultys;
            this.cmbFaculty.DisplayMember = "FacultyName";
            this.cmbFaculty.ValueMember = "FacultyID";
        }
        private void FillMajorCombobox(List<Major> listMajors)
        {
            this.cmbMajor.DataSource = listMajors;
            this.cmbMajor.DisplayMember = "Name";
            this.cmbMajor.ValueMember = "MajorID";
        }

        private void cmbFaculty_SelectedIndexChanged(object sender, EventArgs e)
        {
            Faculty selectedFaculty = cmbFaculty.SelectedItem as Faculty;
            if (selectedFaculty != null)
            {
                var listMajor = majorSevice.GetAllByFaculty(selectedFaculty.FacultyID);
                FillMajorCombobox(listMajor);
                var listStudents = studentService.GetAllHasNoMojor(selectedFaculty.FacultyID);


            }

        }
        private void BindGird(List<Student> listStudent)
        {
            dgvDKCN.Rows.Clear();
            foreach(var item in listStudent)
            {
                int index = dgvDKCN.Rows.Add();
                dgvDKCN.Rows[index].Cells[0].Value = item.StudentID;
                dgvDKCN.Rows[index].Cells[1].Value = item.FullName;
                if (item.Faculty != null)
                    dgvDKCN.Rows[index].Cells[3].Value = item.Faculty.FacultyName;
                dgvDKCN.Rows[index].Cells[4].Value = item.AverageScore + "";
                if (item.MajorID != null)
                    dgvDKCN.Rows[index].Cells[5].Value = item.MajorID;
            }
        }

        private void Showf1()
        {
            Form1 f1 = new Form1();
            f1.ShowDialog();

        }
        private void button1_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(new ThreadStart(Showf1)); // Khởi tạo luồng mới
            thread.Start();
            this.Close();
        }
    }
}
