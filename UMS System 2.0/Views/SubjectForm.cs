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
    public partial class SubjectForm : Form
    {

        private List<Course> courses;

        public SubjectForm()
        {
            InitializeComponent();
            LoadCourses();
            LoadSubjects();
        }

        private void LoadCourses()
        {
            courses = CourseController.GetAllCourses();
            cmbCourse.DataSource = courses;
            cmbCourse.DisplayMember = "CourseName";
            cmbCourse.ValueMember = "CourseID";
        }

        private void LoadSubjects()
        {
            dgvSubjects.DataSource = SubjectController.GetAllSubjects();
            dgvSubjects.Columns["SubjectID"].Visible = false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSubjectName.Text)) return;
            int courseId = (int)cmbCourse.SelectedValue;
            SubjectController.AddSubject(txtSubjectName.Text.Trim(), courseId);
            LoadSubjects();
            txtSubjectName.Clear();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvSubjects.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvSubjects.SelectedRows[0].Cells["SubjectID"].Value);
                string name = txtSubjectName.Text.Trim();
                int courseId = (int)cmbCourse.SelectedValue;
                SubjectController.UpdateSubject(id, name, courseId);
                LoadSubjects();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvSubjects.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvSubjects.SelectedRows[0].Cells["SubjectID"].Value);
                SubjectController.DeleteSubject(id);
                LoadSubjects();
            }
        }

        private void dgvSubjects_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtSubjectName.Text = dgvSubjects.Rows[e.RowIndex].Cells["SubjectName"].Value.ToString();
                int courseId = Convert.ToInt32(dgvSubjects.Rows[e.RowIndex].Cells["CourseID"].Value);
                cmbCourse.SelectedValue = courseId;
            }
        }
        //public SubjectForm()
        //{
        //    InitializeComponent();
        //}

        private void SubjectForm_Load(object sender, EventArgs e)
        {

        }

        private void cmbCourse_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
