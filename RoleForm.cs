namespace LuxuryCarsApp
{
    using LuxuryCarsApp.Data;
    using LuxuryCarsApp.Data.Models.UserModels;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;

    public partial class RoleForm : Form
    {
        AppDbContext context;
        public RoleForm()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            AddLuxuryCar luxuryCar = Application.OpenForms.OfType<AddLuxuryCar>().FirstOrDefault();
            if (luxuryCar != null)
            {
                luxuryCar.Show();
            }
            this.Close();
        }

        private void btnAddRole_Click(object sender, System.EventArgs e)
        {
            using(context = new AppDbContext())
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    var item = dataGridView1.SelectedRows[0].Cells;
                    var id = int.Parse(item[0].Value.ToString());
                    var user = context.Users.Find(id);
                    if (user!=null)
                    {
                        user.Role = Role.Admin;
                    }
                    context.Update(user);
                    context.SaveChanges();
                    ResetSelect();
                    UpdateGrid();
                }
            }
        }
        private List<User> GetAll()
        {
            using(context = new AppDbContext())
            {
                return context.Users.Where(u => u.Role==Role.User).ToList();
            }
        }
        private void UpdateGrid()
        {
            dataGridView1.DataSource = GetAll();
            dataGridView1.ReadOnly = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }
        private void ResetSelect()
        {
            dataGridView1.ClearSelection();
        }
        private void RoleForm_Load(object sender, System.EventArgs e)
        {
            ResetSelect();
            UpdateGrid();
        }
    }
}
