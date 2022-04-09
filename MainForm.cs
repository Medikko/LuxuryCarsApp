namespace LuxuryCarsApp
{
    using LuxuryCarsApp.Constants;
    using LuxuryCarsApp.Data;
    using LuxuryCarsApp.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;

    public partial class MainForm : Form
    {
        AppDbContext context;
        private List<Car> cars;
        private List<Manufacture> manufactures;
        public MainForm()
        {
            InitializeComponent();
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            using (context = new AppDbContext())
            {
                cars = context.Cars.ToList();
            }
             LoadInformation(cars);
        }
        private void btnBuy_Click(object sender, EventArgs e)
        {
            LoginForm login = new LoginForm();
            GlobalConstant.buttonLogin = "Buy";
            if (login != null)
            {
                login.Show();
            }
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            LoginForm login = new LoginForm();
            GlobalConstant.buttonLogin = "Add";
            if (login != null)
            {
                login.Show();
            }
            this.Hide();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            using (context = new AppDbContext())
            {
                cars = context.Cars.ToList();
            }
            LoadInformation(cars);
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (GlobalConstant.item > 0)
            {
                GlobalConstant.item--;
            }
            LoadInformation(cars);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (GlobalConstant.item < (cars.Count - 1))
            {
                GlobalConstant.item++;
            }
            LoadInformation(cars);
        }
        private void LoadInformation(List<Car> c)
        {
            using(context = new AppDbContext())
            {
                cars = context.Cars.ToList();
                manufactures = context.Manufactures.ToList();
                
            }
            if (cars.Any())
            {
                int manafactureIndex = cars[GlobalConstant.item].ManufactureId-1;
                string[] datetimeArgs = cars[GlobalConstant.item].Year.ToString().Split('.');
                string year = datetimeArgs[2];
                pictureBox1.Load(cars[GlobalConstant.item].ImageUrl);
                lbModel.Text = cars[GlobalConstant.item].Name;
                lbBrand.Text = cars[GlobalConstant.item].Brand;
                lbPrice.Text = cars[GlobalConstant.item].Price.ToString();
                lbYear.Text = year;
                lbHorsePower.Text = cars[GlobalConstant.item].HorsePower.ToString();
                lbMileage.Text = cars[GlobalConstant.item].Mileage.ToString();
                lbEngine.Text = cars[GlobalConstant.item].Engine;
                lbFuel.Text = cars[GlobalConstant.item].FuelType.ToString();
                lbBodyType.Text = cars[GlobalConstant.item].BodyType.ToString();
                lbManafacture.Text = manufactures[manafactureIndex].Name;
            }
        }
        private void labelEngine_Click(object sender, EventArgs e)
        {

        }
        private void labelMileage_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
