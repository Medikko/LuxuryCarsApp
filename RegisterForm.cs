using LuxuryCarsApp.Data;
using LuxuryCarsApp.Data.Models.UserModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LuxuryCarsApp
{
    public partial class RegisterForm : Form
    {
        private AppDbContext context;
        public RegisterForm()
        {
            InitializeComponent();
           
        }

        private void RegisterForm_Load(object sender, EventArgs e)
        {
            ClearForm();
            for (int i = 18; i <= 100; i++)
            {
                cbYears.Items.Add(i);
            }
            using (context = new AppDbContext())
            {
                foreach (var city in context.Cities.ToList())
                {
                    cbCity.Items.Add(city.Name);
                }
            }
           tbPassword.UseSystemPasswordChar = true;
        }
        private void btnRegister_Click(object sender, EventArgs e)
        {
            bool isValidEmail = false;
            bool isValidBalance = false;
            bool isValidAge = false;
            MessageBoxIcon icon = MessageBoxIcon.Error;
            MessageBoxButtons messageBoxButtons = MessageBoxButtons.OK;
            string messageBoxTextCaption = "Error";



            if (tbUsername.Text.Length < 4)
            {
                MessageBox.Show("Username must be atleast 4 characters", messageBoxTextCaption, messageBoxButtons, icon);
            }
            else if (string.IsNullOrWhiteSpace(tbPassword.Text))
            {
                MessageBox.Show("Password can't be white space or empty!", messageBoxTextCaption, messageBoxButtons, icon);
            }
            else if (string.IsNullOrWhiteSpace(tbFirstName.Text))
            {
                MessageBox.Show("First Name can't be white space or empty!", messageBoxTextCaption, messageBoxButtons, icon);
            }
            else if (string.IsNullOrWhiteSpace(tbLastName.Text))
            {
                MessageBox.Show("Last Name can't be white space or empty!", messageBoxTextCaption, messageBoxButtons, icon);
            } 
            else if (string.IsNullOrWhiteSpace(tbEmail.Text))
            {
                MessageBox.Show("Email can't be white space or empty!", messageBoxTextCaption, messageBoxButtons, icon);
            }
            else if (string.IsNullOrWhiteSpace(cbCity.Text))
            {
                MessageBox.Show("City can't be white space or empty!", messageBoxTextCaption, messageBoxButtons, icon);
            }
            else if (tbPassword.Text.Length < 8)
            {
                MessageBox.Show("Password must be atleast 8 characters", messageBoxTextCaption, messageBoxButtons, icon);
            }
            else
            {
                try
                {
                    string emailAddress = this.tbEmail.Text;
                    MailAddress address = new MailAddress(emailAddress);
                    isValidEmail = (address.Address == emailAddress);
                }
                catch (FormatException)
                {
                    MessageBox.Show("Invalid Email", messageBoxTextCaption, messageBoxButtons, icon);
                }
                try
                {
                    double balance = double.Parse(tbBalance.Text);
                    double maxBalance = 1000000000;
                    if (balance >= 0 || balance <= maxBalance)
                    {
                        isValidBalance = true;
                    }
                }
                catch (FormatException)
                {
                    MessageBox.Show("Invalid Balance", messageBoxTextCaption, messageBoxButtons, icon);
                }
                int years = 1;
                try
                {
                    years = int.Parse(cbYears.Text);
                    if (years < 100 || years >= 18)
                    {
                        isValidAge = true;
                    }

                }
                catch (FormatException)
                {
                    MessageBox.Show("Invalid age", messageBoxTextCaption, messageBoxButtons, icon);
                }
                if (!isValidEmail)
                {
                    MessageBox.Show("Invalid Email", messageBoxTextCaption, messageBoxButtons, icon);
                }
                else if (!isValidBalance)
                {
                    MessageBox.Show("Invalid Balance", messageBoxTextCaption, messageBoxButtons, icon);
                }
                else if (!isValidAge)
                {
                    MessageBox.Show("Invalid Age", messageBoxTextCaption, messageBoxButtons, icon);
                }
                else
                {
                    using (context = new AppDbContext())
                    {
                        City city = context.Cities.FirstOrDefault(c => c.Name == cbCity.Text);
                        if (city == null)
                        {
                            city = new City() { Name = cbCity.Text };
                        }
                        User user = context.Users.FirstOrDefault(u => u.Role == Role.Boss);
                        if (user == null)
                        {
                            string password = Hash(tbPassword.Text);
                            user = new User()
                            {
                                UserName = tbUsername.Text,
                                Email = tbEmail.Text,
                                Password = password,
                                FirstName = tbFirstName.Text,
                                LastName = tbLastName.Text,
                                Balance = decimal.Parse(tbBalance.Text),
                                Years = int.Parse(cbYears.Text),
                                City = city,
                                Role = Role.Boss
                            };
                            context.Users.Add(user);
                            context.SaveChanges();
                        }
                        else
                        {
                            user = context.Users.FirstOrDefault(u => u.Email == tbEmail.Text);
                            if (user == null)
                            {
                                user = context.Users.FirstOrDefault(u => u.UserName == tbEmail.Text);
                                if (user == null)
                                {
                                    string password = Hash(tbPassword.Text);

                                    user = new User()
                                    {
                                        UserName = tbUsername.Text,
                                        Email = tbEmail.Text,
                                        Password = password,
                                        FirstName = tbFirstName.Text,
                                        LastName = tbLastName.Text,
                                        Balance = decimal.Parse(tbBalance.Text),
                                        Years = int.Parse(cbYears.Text),
                                        City = city,
                                        Role = Role.User
                                    };
                                    context.Users.Add(user);
                                    context.SaveChanges();
                                    MessageBox.Show("You have register seccesfull!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    MainForm mainForm = new MainForm();
                                    if (mainForm != null)
                                    {
                                        mainForm.Show();
                                    }
                                    ClearForm();
                                    this.Close();
                                }
                                else
                                {
                                    MessageBox.Show("User with this username already exist!", messageBoxTextCaption, messageBoxButtons, icon);
                                }

                            }
                            else
                            {
                                MessageBox.Show("User with this email already exist!", messageBoxTextCaption, messageBoxButtons, icon);
                            }
                        }
                    }
                }
                       
                
            }


        }
        public static string Hash(string input)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
                var sb = new StringBuilder(hash.Length*2);

                foreach (byte b in hash)
                {
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            MainForm mainForm = Application.OpenForms.OfType<MainForm>().FirstOrDefault();
            if (mainForm != null)
            {
                mainForm.Show();
            }
            this.Close();
        }
        private void ClearForm()
        {
            tbUsername.Text = "";
            tbPassword.Text = "";
            tbFirstName.Text = "";
            tbLastName.Text = "";
            tbBalance.Text = "";
            cbCity.Text = "Sofia";
            cbYears.Text = "18";
        }

        private void ckPassword_Click(object sender, EventArgs e)
        {
            if (ckPassword.Checked==true)
            {
                ckPassword.Checked = false;
                tbPassword.UseSystemPasswordChar = true;

            }
            else
            {
                ckPassword.Checked = true;
                tbPassword.UseSystemPasswordChar = false;
            }
        }
    }
}
