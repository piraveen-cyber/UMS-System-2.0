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

namespace UMS_System_2._0.Views
{
    public partial class CourseForm : Form
    {
        public CourseForm()
    {
        InitializeComponent();
        LoadCourses();  // Load course list when form opens

    }
    // Load all courses into the DataGridView
    private void LoadCourses()
    {
        dgvCourses.DataSource = CourseController.GetAllCourses();
        dgvCourses.Columns["CourseID"].Visible = false; // hide ID column
    }

    // Add a new course
    private void btnAdd_Click(object sender, EventArgs e)
    {
        string name = txtCourseName.Text.Trim();
        if (string.IsNullOrWhiteSpace(name))
        {
            MessageBox.Show("Course name cannot be empty.");
            return;
        }

        if (CourseController.CourseExists(name))
        {
            MessageBox.Show("This course already exists.");
            return;
        }

        CourseController.AddCourse(name);
        LoadCourses();
        txtCourseName.Clear();
    }

    // Update selected course
    private void btnUpdate_Click(object sender, EventArgs e)
    {
        if (dgvCourses.SelectedRows.Count == 0)
        {
            MessageBox.Show("Please select a course to update.");
            return;
        }

        string name = txtCourseName.Text.Trim();
        if (string.IsNullOrWhiteSpace(name))
        {
            MessageBox.Show("Course name cannot be empty.");
            return;
        }

        int courseId = Convert.ToInt32(dgvCourses.SelectedRows[0].Cells[0].Value);
        CourseController.UpdateCourse(courseId, name);
        LoadCourses();
        txtCourseName.Clear();
    }

    // Delete selected course
    private void btnDelete_Click(object sender, EventArgs e)
    {
        if (dgvCourses.SelectedRows.Count == 0)
        {
            MessageBox.Show("Please select a course to delete.");
            return;
        }

        int courseId = Convert.ToInt32(dgvCourses.SelectedRows[0].Cells[0].Value);

        DialogResult result = MessageBox.Show("Are you sure you want to delete this course?", "Confirm Delete", MessageBoxButtons.YesNo);
        if (result == DialogResult.Yes)
        {
            CourseController.DeleteCourse(courseId);
            LoadCourses();
            txtCourseName.Clear();
        }
    }

    // Fill textbox when a row is clicked
    private void dgvCourses_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex >= 0)
        {
            txtCourseName.Text = dgvCourses.Rows[e.RowIndex].Cells["CourseName"].Value.ToString();
        }
    }

    private void dgvCourses_CancelRowEdit(object sender, QuestionEventArgs e)
    {

    }

    //private void dgvCourses_CellClick(object sender, KeyPressEventArgs e)
    //{

    //}

    private void CourseForm_Load(object sender, EventArgs e)
    {

    }

    private void dgvCourses_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {

    }
}
}
