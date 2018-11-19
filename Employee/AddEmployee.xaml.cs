using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for AddEmployee.xaml
    /// </summary>
    public partial class AddEmployee : Window
    {
        private EmployeeData _employee = new EmployeeData();
        private EmployeeMPM addNewEmployee = new EmployeeMPM();
        
        public AddEmployee()
        {
            InitializeComponent();
            dp_join.SelectedDate = DateTime.Now;
            btn_add.IsEnabled = false;
        }

        public void CloseBtn(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void AddBtn(object sender, RoutedEventArgs e)
        {
            addNewEmployee.NIK = tbx_nik.Text.ToString();
            addNewEmployee.Name = tbx_nama.Text.ToString();
            addNewEmployee.Supervisor = tbx_spv.Text.ToString();
            addNewEmployee.Depo = tbx_depo.Text.ToString();
            addNewEmployee.Branch = tbx_branch.Text.ToString();
            addNewEmployee.Employee_type = tbx_employeeType.Text.ToString();
            addNewEmployee.Category = tbx_category.Text.ToString();
            addNewEmployee.Join_date = dp_join.Text.ToString();
            
                if (tbx_nik.Text != "")
                {
                    if (tbx_nama.Text != "")
                    {
                        if (tbx_spv.Text != "")
                        {
                            if (tbx_depo.Text != "")
                            {
                                if (tbx_branch.Text != "")
                                {
                                    if (tbx_employeeType.Text != "")
                                    {
                                        if (tbx_category.Text != "")
                                        {
                                            if (dp_join.Text.ToString() != "")
                                            {
                                                if (MessageBoxResult.Yes == MessageBox.Show("Add '" + addNewEmployee.Name + "'?", "Confirmation", MessageBoxButton.YesNo))
                                                {
                                                    DBConnect.insertData(addNewEmployee);
                                                    MainWindow main = new MainWindow();
                                                    main.dgridEmployee.ItemsSource = null;
                                                    this.Close();
                                                }
                                            }
                                            else { MessageBox.Show("Please select Join date.", "Information"); }
                                        }
                                        else{ MessageBox.Show("Please fill the Category.", "Information"); }
                                    }
                                    else { MessageBox.Show("Please fill the Employee Type.", "Information"); }
                                }
                                else { MessageBox.Show("Please fill the Branch.", "Information"); }
                            }
                            else { MessageBox.Show("Please fill the Depo.", "Information"); }
                        }
                        else { MessageBox.Show("Please fill the Supervisor.", "Information"); }
                    }
                    else { MessageBox.Show("Please fill the Name.", "Information"); }
                }
                else { MessageBox.Show("Please fill the NIK.", "Information"); }
        
        }

        private void tbx_nik_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbx_nik.Text != null)
            {
                btn_add.IsEnabled = true;
            }
            else
            {
                btn_add.IsEnabled = false;
            }
        }
    }
}
