namespace LuxuryCarsApp
{
    using LuxuryCarsApp.Data;
    using LuxuryCarsApp.Models;
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Windows.Forms;
    public partial class AddLuxuryCar : Form
    {
        AppDbContext context;

        public string DataTime { get; private set; }

        public AddLuxuryCar()
        {
            InitializeComponent();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            using(context = new AppDbContext())
            {
                if (string.IsNullOrWhiteSpace(tbModel.Text))
                {
                    MessageBox.Show("Model can't be null or whitespaces!");
                }
                else if (string.IsNullOrWhiteSpace(tbBrand.Text))
                {
                    MessageBox.Show("Brand can't be null or whitespaces!");
                }
                else if (string.IsNullOrWhiteSpace(tbPrice.Text))
                {
                    MessageBox.Show("Price can't be null or whitespaces!");
                }
                else if (string.IsNullOrWhiteSpace(cbYear.Text) || int.Parse(cbYear.Text)>=2022 || int.Parse(cbYear.Text) < 1950)
                {
                    MessageBox.Show("Select year from drop down list!");
                }
                else if (string.IsNullOrWhiteSpace(tbHorsePower.Text))
                {
                    MessageBox.Show("Horse power can't be null or whitespaces!");
                }
                else if (string.IsNullOrWhiteSpace(tbImage.Text))
                {
                    MessageBox.Show("Invalid Url!");
                }
                else if (string.IsNullOrWhiteSpace(tbEngine.Text))
                {
                    MessageBox.Show("Engine can't be null or whitespaces!");
                }
                else if (string.IsNullOrWhiteSpace(tbMileage.Text))
                {
                    MessageBox.Show("Mileage can't be null or whitespaces!");
                }
                else if (string.IsNullOrWhiteSpace(cbFuel.Text))
                {
                    MessageBox.Show("Select fuel type from drop down list!");
                }               
                else if (string.IsNullOrWhiteSpace(cbManafacture.Text))
                {
                    MessageBox.Show("Manafacture can't be null or whitespaces!");
                }
                else if (string.IsNullOrWhiteSpace(cbBodyType.Text))
                {
                    MessageBox.Show("Select body type from drop down list!");
                }
                else if (int.Parse(tbHorsePower.Text) >= 1300)
                {
                    MessageBox.Show("Car with that horsepower doesn't exist!");
                }
                else
                {
                    using(context = new AppDbContext())
                    {
                        Manufacture manafacture = context.Manufactures.FirstOrDefault(m => m.Name == cbManafacture.Text);
                        if (manafacture==null)
                        {
                            manafacture = new Manufacture() { Name = cbManafacture.Text };
                        }
                        CultureInfo cultures = CultureInfo.CurrentCulture;
                        Car car = new Car()
                        {
                            Name = tbModel.Text,
                            Brand= tbBrand.Text,
                            Price = decimal.Parse(tbPrice.Text),
                            Year =  DateTime.ParseExact(cbYear.Text,"yyyy",cultures),
                            HorsePower = int.Parse(tbHorsePower.Text),
                            ImageUrl = tbImage.Text,
                            Engine = tbEngine.Text,
                            Mileage = double.Parse(tbMileage.Text),
                            FuelType = (FuelType)Enum.Parse(typeof(FuelType),cbFuel.Text),
                            BodyType = (BodyType)Enum.Parse(typeof(BodyType),cbBodyType.Text),
                            Manufacture = manafacture
                        };
                        context.Add(car);
                        context.SaveChanges();
                        MessageBox.Show("You have seccesfull add a car!");
                        ClearForm();
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
            else
            {
                mainForm = new MainForm();
                mainForm.Show();
            }
            this.Close();
        }
        private void AddLuxuryCar_Load(object sender, EventArgs e)
        {
            cbFuel.Items.Add(FuelType.Gasoline);
            cbFuel.Items.Add(FuelType.LPG);
            cbFuel.Items.Add(FuelType.Diesel);
            cbFuel.Text = FuelType.Gasoline.ToString();
            cbYear.Text = DateTime.Now.Year.ToString();
            cbBodyType.Items.Add(BodyType.Limosine);
            cbBodyType.Items.Add(BodyType.Sedan);
            cbBodyType.Items.Add(BodyType.Liftback);
            cbBodyType.Items.Add(BodyType.Jeep);
            cbBodyType.Items.Add(BodyType.Coupe);
            cbBodyType.Items.Add(BodyType.Other);
            cbBodyType.Text = BodyType.Sedan.ToString();
            for (int i =1950; i <= 2022; i++)
            {
                cbYear.Items.Add(i);
            }
            using(context = new AppDbContext())
            {
                foreach (var item in context.Manufactures)
                {
                    cbManafacture.Items.Add(item.Name);
                    cbManafacture.Text = item.Name;
                }
            }
        }
        private void ClearForm()
        {
            tbImage.Text = "";
            tbBrand.Text = "";
            tbEngine.Text = "";
            tbHorsePower.Text = "";
            tbMileage.Text = "";
            tbPrice.Text = "";
            tbModel.Text = "";
            cbFuel.Text = FuelType.Gasoline.ToString();
            cbBodyType.Text = BodyType.Sedan.ToString();
            cbYear.Text = DateTime.Now.Year.ToString();
            cbManafacture.Text = "";
        }

        private void btnAdmin_Click(object sender, EventArgs e)
        {
            RoleForm role = new RoleForm();
            if (role!=null)
            {
                role.Show();
            }
            this.Hide();
        }
    }
}
