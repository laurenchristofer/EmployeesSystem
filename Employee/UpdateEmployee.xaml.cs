using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Employee
{
    /// <summary>
    /// Interaction logic for UpdateEmployee.xaml
    /// </summary>
    public partial class UpdateEmployee : Window
    {
        public EmployeeMPM upEmployee = new EmployeeMPM();
        private bool enabled = true;

        public UpdateEmployee()
        { 
            InitializeComponent();
            if (cbx_status.Text.ToString() == "Active")
            {
                cbx_status.SelectedIndex = 0;
            }
            else
            {
                cbx_status.SelectedIndex = 1;
            }
            //cbx_resignReason.IsEnabled = false;
        }

        private void UpdateBtn(object sender, RoutedEventArgs e)
        {
            upEmployee.NIK = tbx_nik.Text.ToString();
            upEmployee.Name = tbx_nama.Text.ToString();
            upEmployee.Supervisor = tbx_spv.Text.ToString();
            upEmployee.Depo = tbx_depo.Text.ToString();
            upEmployee.Branch = tbx_branch.Text.ToString();
            upEmployee.Employee_type = tbx_employeeType.Text.ToString();
            upEmployee.Category = tbx_category.Text.ToString();
            upEmployee.Join_date = dp_join.Text.ToString();
            upEmployee.Status = cbx_status.Text.ToString();
            upEmployee.Resign_date = dp_resignDate.Text.ToString();
            upEmployee.Reason = cbx_resignReason.Text.ToString();
            
            if (MessageBoxResult.Yes == MessageBox.Show("Do you want to update '"+upEmployee.Name+"' data?","Update Information",MessageBoxButton.YesNo))
            {
                BindingExpression Nama = tbx_nama.GetBindingExpression(TextBox.TextProperty);
                Nama.UpdateSource();
                BindingExpression Spv = tbx_spv.GetBindingExpression(TextBox.TextProperty);
                Spv.UpdateSource();
                BindingExpression Depo = tbx_depo.GetBindingExpression(TextBox.TextProperty);
                Depo.UpdateSource();
                BindingExpression Branch = tbx_branch.GetBindingExpression(TextBox.TextProperty);
                Branch.UpdateSource();
                BindingExpression Type = tbx_employeeType.GetBindingExpression(TextBox.TextProperty);
                Type.UpdateSource();
                BindingExpression Category = tbx_category.GetBindingExpression(TextBox.TextProperty);
                Category.UpdateSource();
                BindingExpression Join = dp_join.GetBindingExpression(DatePicker.SelectedDateProperty);
                Join.UpdateSource();
                BindingExpression Status = cbx_status.GetBindingExpression(ComboBox.TextProperty);
                Status.UpdateSource();
                BindingExpression reason = cbx_resignReason.GetBindingExpression(ComboBox.TextProperty);
                reason.UpdateSource();
                BindingExpression Resign = dp_resignDate.GetBindingExpression(DatePicker.SelectedDateProperty);
                Resign.UpdateSource();

                DBConnect.updateData(upEmployee);
                this.Close();
            }
            

        }

        private void CloseBtn(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cbx_status_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbx_status.SelectedIndex == 0)
            {
                cbx_resignReason.IsEnabled = !enabled;
                dp_resignDate.IsEnabled = !enabled;
                cbx_resignReason.Text = "";
                dp_resignDate.Text = "";
            }
            else
            {
                cbx_resignReason.IsEnabled = enabled;
                dp_resignDate.IsEnabled = enabled;
            }
        }
    }

   
}
