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
    public partial class ExamForm : Form
    {
        private List<Subject> subjects;

        public ExamForm()
        {
            InitializeComponent();
            LoadSubjects();
            LoadExams();
        }

        private void LoadSubjects()
        {
            subjects = SubjectController.GetAllSubjects();
            cmbSubject.DataSource = subjects;
            cmbSubject.DisplayMember = "SubjectName";
            cmbSubject.ValueMember = "SubjectID";
        }

        private void LoadExams()
        {
            dgvExams.DataSource = ExamController.GetAllExams();
            dgvExams.Columns["ExamID"].Visible = false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtExamName.Text)) return;
            int subjectId = (int)cmbSubject.SelectedValue;
            ExamController.AddExam(txtExamName.Text.Trim(), subjectId);
            LoadExams();
            txtExamName.Clear();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvExams.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvExams.SelectedRows[0].Cells["ExamID"].Value);
                string name = txtExamName.Text.Trim();
                int subjectId = (int)cmbSubject.SelectedValue;
                ExamController.UpdateExam(id, name, subjectId);
                LoadExams();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvExams.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvExams.SelectedRows[0].Cells["ExamID"].Value);
                ExamController.DeleteExam(id);
                LoadExams();
            }
        }

        private void dgvExams_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtExamName.Text = dgvExams.Rows[e.RowIndex].Cells["ExamName"].Value.ToString();
                cmbSubject.SelectedValue = Convert.ToInt32(dgvExams.Rows[e.RowIndex].Cells["SubjectID"].Value);
            }
        }


        private void ExamForm_Load(object sender, EventArgs e)
        {

        }

        private void txtExamName_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvExams_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
