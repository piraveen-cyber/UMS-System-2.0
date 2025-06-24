using System;
using System.Windows.Forms;
using UMS_System_2._0.Controllers;
using UMS_System_2._0.Views;

namespace UnicomTICManagementSystem.Views
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            string role = LoginController.CheckLogin(username, password);

            if (role != null)
            {
                MessageBox.Show($"Login successful! Role: {role}");
                this.Hide();
                MainForm mainForm = new MainForm(role);
                mainForm.Show();
            }
            else
            {
                MessageBox.Show("Wrong username or password.");
            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
