using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UMS_System_2._0.Controllers;
using UnicomTICManagementSystem.Models;

namespace UMS_System_2._0.Views
{
    public partial class MarkForm : Form
    {
        private List<Student> students;
        private List<Exam> exams;

        public MarkForm()
        {
            InitializeComponent();
            LoadStudents();
            LoadExams();
            LoadMarks();
        }

        private void LoadStudents()
        {
            students = StudentController.GetAllStudents();
            cmbStudent.DataSource = students;
            cmbStudent.DisplayMember = "Name";
            cmbStudent.ValueMember = "StudentID";
        }

        private void LoadExams()
        {
            exams = ExamController.GetAllExams();
            cmbExam.DataSource = exams;
            cmbExam.DisplayMember = "ExamName";
            cmbExam.ValueMember = "ExamID";
        }

        private void LoadMarks()
        {
            dgvMarks.DataSource = MarkController.GetAllMarks();
            dgvMarks.Columns["MarkID"].Visible = false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void dgvMarks_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                cmbStudent.SelectedValue = Convert.ToInt32(dgvMarks.Rows[e.RowIndex].Cells["StudentID"].Value);
                cmbExam.SelectedValue = Convert.ToInt32(dgvMarks.Rows[e.RowIndex].Cells["ExamID"].Value);
                txtScore.Text = dgvMarks.Rows[e.RowIndex].Cells["Score"].Value.ToString();
            }
        }


        private void MarkForm_Load(object sender, EventArgs e)
        {

        }
    }
}
