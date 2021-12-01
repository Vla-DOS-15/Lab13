using System;
using System.Windows.Forms;

namespace Lab13
{
    public partial class fCar : Form
    {
        public Car TheCar;
        public fCar()
        {
            InitializeComponent();
        }

        public fCar(Car t)
        {
            TheCar = t;
            InitializeComponent();
        }
        private void btnOk_Click(object sender, EventArgs e)
        {
            TheCar.Make = tbMake.Text.Trim();
            TheCar.Model = tbModel.Text.Trim();
            TheCar.Color = tbColor.Text.Trim(); 
            TheCar.YearOfCreation = uint.Parse(tbYearOfCreation.Text.Trim()); 
            TheCar.Price = int.Parse(tbPrice.Text.Trim());
            TheCar.KeyWorkingOrder = chbHasWorkingOrder.Checked;

            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
