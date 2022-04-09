namespace LuxuryCarsApp
{
    using LuxuryCarsApp.Data;
    using LuxuryCarsApp.Data.Models.UserModels;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Security.Cryptography;
    using System.Windows.Forms;
    using LuxuryCarsApp.Constants;
    using LuxuryCarsApp.Models;
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
                    if (GlobalConstant.buttonLogin=="Add")
                    {
                        if (user.Role == Role.Admin || user.Role == Role.Boss)
                        {
                            MessageBox.Show($"Welcome back {user.UserName}");
                            AddLuxuryCar addLuxuryCar = new AddLuxuryCar();
                            if (addLuxuryCar != null)
                            {
                                addLuxuryCar.Show();
                            }
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("You login seccessful but still have permission for that opeartion!", "Information", MessageBoxButtons.OK);
                            MainForm mainForm = Application.OpenForms.OfType<MainForm>().FirstOrDefault();
                            if (mainForm != null)
                            {
                                mainForm.Show();
                            }
                            this.Close();
                        }
                    }
                    else if(GlobalConstant.buttonLogin == "Buy")
                    {
                        List<Car> cars = context.Cars.ToList();
                        if (cars.Any())
                        {
                            if (user.Balance>=cars[GlobalConstant.item].Price)
                            {
                                user.Balance -= cars[GlobalConstant.item].Price;
                                string carBrand = cars[GlobalConstant.item].Brand;
                                string carModel = cars[GlobalConstant.item].Name;
                                decimal carPrice = cars[GlobalConstant.item].Price;
                                DateTime order = DateTime.UtcNow.AddDays(30);
                                context.Cars.Remove(cars[GlobalConstant.item]);
                                context.SaveChanges();
                                cars.Remove(cars[GlobalConstant.item]);
                                MessageBox.Show($"You have seccesfully purchase {carBrand} {carModel} for {carPrice:f2}","Info",MessageBoxButtons.OK);
                                MessageBox.Show($"Your order is processing and will dilevery on {order.Date}");
                            }
                            else
                            {
                                MessageBox.Show($"You haven't enough balance to buy this car!", "Error", MessageBoxButtons.OK);
                            }
                        }
                        this.Close();
                        MainForm mainForm = new MainForm();
                        if (mainForm != null)
                        {
                            mainForm.Show();
                        }
                    }
                   
                }
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
            RegisterForm registerForm = new RegisterForm();
            registerForm.Show();
            this.Close();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            tbUsername.Text = "";
            tbPassword.Text = "";
            tbPassword.UseSystemPasswordChar = true;

        }

        private void ckPassword_Click(object sender, EventArgs e)
        {
            if (ckPassword.Checked == true)
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
