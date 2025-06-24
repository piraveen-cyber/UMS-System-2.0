using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UMS_System_2._0.Views
{
    public partial class MainForm : Form
    {
        private string userRole;

        // Constructor receives role after login
        public MainForm(string role)
        {
            InitializeComponent();
            userRole = role;
            lblWelcome.Text = $"Welcome! Role: {role}";
            ApplyRoleAccess();
        }

        // Hide or show buttons based on user role
        private void ApplyRoleAccess()
        {
            // Only Admin sees these
            btnCourses.Visible = userRole == "Admin";
            btnSubjects.Visible = userRole == "Admin";
            btnStudents.Visible = userRole == "Admin";

            // Admin and Staff can see Exams
            btnExams.Visible = userRole == "Admin" || userRole == "Staff";

            // Admin, Staff, Lecturer, Student can see Marks
            btnMarks.Visible = userRole == "Admin" || userRole == "Staff" || userRole == "Lecturer" || userRole == "Student";

            // Everyone can see Timetables
            btnTimetables.Visible = true;
        }

        // Button clicks open respective forms

        private void btnCourses_Click(object sender, EventArgs e)
        {
            CourseForm courseForm = new CourseForm();
            courseForm.ShowDialog();
        }

        private void btnSubjects_Click(object sender, EventArgs e)
        {
            var form = new SubjectForm();
            form.ShowDialog();
        }

        private void btnStudents_Click(object sender, EventArgs e)
        {
            var form = new StudentForm();
            form.ShowDialog();
        }

        private void BtnExams_Click(object sender, EventArgs e)
        {
            var form = new ExamForms();
            form.ShowDialog();
        }

        private void btnMarks_Click(object sender, EventArgs e)
        {
            var form = new MarkForm();
            form.ShowDialog();
        }

        private void btnTimetables_Click(object sender, EventArgs e)
        {
            var form = new TimetableForm();
            form.ShowDialog();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            // Close main form and restart application (back to login)
            this.Close();
            Application.Restart();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void MainForm_Load_1(object sender, EventArgs e)
        {

        }

        private void btnExams_Click(object sender, EventArgs e)
        {

        }

        //private void btnSubjects_Click(object sender, EventArgs e)
        //{

        //}

        //private void btnStudents_Click(object sender, EventArgs e)
        //{
        //    StudentForm form = new StudentForm();
        //    form.ShowDialog();
        //}

        //private void btnExams_Click(object sender, EventArgs e)
        //{
        //    ExamForm form = new ExamForm();
        //    form.ShowDialog();
        //}

        //private void btnMarks_Click(object sender, EventArgs e)
        //{
        //    MarkForm form = new MarkForm();
        //    form.ShowDialog();
        //}

        //private void btnTimetables_Click(object sender, EventArgs e)
        //{
        //    TimetableForm form = new TimetableForm();
        //    form.ShowDialog();
        //}
    }
}
