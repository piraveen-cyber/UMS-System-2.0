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
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            LoadRoles();
        }

        private void LoadRoles()
        {
            cmbRole.Items.Clear();
            cmbRole.Items.Add("Admin");
            cmbRole.Items.Add("Staff");
            cmbRole.Items.Add("Lecturer");
            cmbRole.Items.Add("Student");
            cmbRole.SelectedIndex = 0;
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            string selectedRole = cmbRole.SelectedItem?.ToString();

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter both username and password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string role = await LoginController.AuthenticateAsync(username, password);

            if (role == null)
            {
                MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (role != selectedRole)
            {
                MessageBox.Show($"Role mismatch. You are registered as '{role}', not '{selectedRole}'.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MessageBox.Show($"Welcome {role}!", "Login Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // TODO: Redirect to MainForm or dashboard
            // MainForm dashboard = new MainForm(role);
            // dashboard.Show();
            // this.Hide();
        }
    

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }
    }
}
