using Zadanie9.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Zadanie9.Forms;
using Zadanie9;
using Zadanie9.Forms;

namespace Zadanie9.Forms
{
    public partial class fmLogin : Form
    {
        private bool passwordVisible = false;
        public fmLogin()
        {
            InitializeComponent();
            textBoxPassword.UseSystemPasswordChar = true;
        }

        private void btnOk_Click(object sender, System.EventArgs e)
        {
            string username = textBoxLogin.Text;
            string password = textBoxPassword.Text;
            try
            {
                List<User> users = Program.context.Users.ToList();
                User u = users.FirstOrDefault(p => p.UserName == username && p.Password == password);
                if (u != null)
                {
                    MainWindow mainWindow = new MainWindow(u.Role, u.UserName);
                    mainWindow.Owner = this;
                    this.Hide();
                    textBoxPassword.Clear();
                    mainWindow.Show();
                }
                else
                    MessageBox.Show("Incorrect login", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void fmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult x = MessageBox.Show("Do you want to close the app?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (x == DialogResult.No)
                e.Cancel = true;
            else
            {
                e.Cancel = true;
                Environment.Exit(0);
            }
        }

        private void pictureBoxUnVisible_Click(object sender, EventArgs e)
        {
            ShowPasswordCharacters(false);
        }

        private void pictureBoxVisible_Click(object sender, EventArgs e)
        {
            ShowPasswordCharacters(true);
        }

        private void ShowPasswordCharacters(bool val)
        {
            passwordVisible = !val;
            textBoxPassword.UseSystemPasswordChar = val;
            passwordPicVisible.Visible = !val;
            passwordPicUnvisible.Visible = val;
        }

        private void txtLogin_KeyPress(object sender, KeyPressEventArgs e)
        {
            PressLoginButton(sender, e);
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            PressLoginButton(sender, e);
        }

        private void PressLoginButton(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnOk_Click(sender, e);
                e.Handled = true;
            }
        }
    }
}