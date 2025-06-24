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
    public partial class StudentForm : Form
    {
        private List<Course> courses;

        public StudentForm()
        {
            InitializeComponent();
            LoadCourses();
            LoadStudents();
        }

        private void LoadCourses()
        {
            courses = CourseController.GetAllCourses();
            cmbCourse.DataSource = courses;
            cmbCourse.DisplayMember = "CourseName";
            cmbCourse.ValueMember = "CourseID";
        }

        private void LoadStudents()
        {
            dgvStudents.DataSource = StudentController.GetAllStudents();
            dgvStudents.Columns["StudentID"].Visible = false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtStudentName.Text))
            {
                return;
            }

            int courseId = (int)cmbCourse.SelectedValue;
            StudentController.AddStudent(txtStudentName.Text.Trim(), courseId);
            LoadStudents();
            txtStudentName.Clear();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvStudents.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvStudents.SelectedRows[0].Cells["StudentID"].Value);
                string name = txtStudentName.Text.Trim();
                int courseId = (int)cmbCourse.SelectedValue;
                StudentController.UpdateStudent(id, name, courseId);
                LoadStudents();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvStudents.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvStudents.SelectedRows[0].Cells["StudentID"].Value);
                StudentController.DeleteStudent(id);
                LoadStudents();
            }
        }

        private void dgvStudents_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtStudentName.Text = dgvStudents.Rows[e.RowIndex].Cells["Name"].Value.ToString();
                cmbCourse.SelectedValue = Convert.ToInt32(dgvStudents.Rows[e.RowIndex].Cells["CourseID"].Value);
            }
        }


        private void StudentForm_Load(object sender, EventArgs e)
        {

        }
    }
}
