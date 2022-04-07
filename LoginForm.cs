using LuxuryCarsApp.Data;
using LuxuryCarsApp.Data.Models.UserModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace LuxuryCarsApp
{
    public partial class LoginForm : Form
    {
        AppDbContext context;
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            using (context = new AppDbContext())
            {
                string password = Hash(tbPassword.Text);
                User user = context.Users.FirstOrDefault(u => u.UserName==tbUsername.Text && u.Password==password);
                if (user==null)
                {
                    MessageBox.Show("Wrong password or username!");
                }
                else
                {
                    if (user.Role==Role.Admin || user.Role==Role.Boss)
                    {
                        MessageBox.Show($"Welcome back {user.UserName}");
                        AddLuxuryCar addLuxuryCar = Application.OpenForms.OfType<AddLuxuryCar>().FirstOrDefault();
                        if (addLuxuryCar!=null)
                        {
                            addLuxuryCar.Show();
                        }
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("You login seccessful but still have permission for that opeartion!","Information",MessageBoxButtons.OK);
                        MainForm mainForm = Application.OpenForms.OfType<MainForm>().FirstOrDefault();
                        if (mainForm!=null)
                        {
                            mainForm.Show();
                        }
                        this.Close();
                    }
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }
        public static string Hash(string input)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
                var sb = new StringBuilder(hash.Length * 2);

                foreach (byte b in hash)
                {
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();
            }
        }
        private void btnRegister_Click(object sender, EventArgs e)
        {

        }
    }
}
