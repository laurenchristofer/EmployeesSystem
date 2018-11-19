using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Employee
{
    public class EmployeeData : INotifyPropertyChanged
    {
        private ObservableCollection<EmployeeMPM> _employee = new ObservableCollection<EmployeeMPM>();
        private EmployeeMPM _selectedEmployee = null;
        private string _filter;

        public string Filter
        {
            get
            {
                return _filter;
            }
            set
            {
                if (_filter != value)
                {
                    _filter = value;
                    OnPropertyChanged(nameof(Filter));
                }
            }
        }

        public void fetchData(ObservableCollection<EmployeeMPM> employeeList)
        {
            this._employee = employeeList;
            OnPropertyChanged(nameof(Employees));
        }

        public ObservableCollection<EmployeeMPM> Employees
        {
            get
            {
                return _employee;
            }

            set
            {
                if (_employee != value)
                {
                    _employee = value;
                    OnPropertyChanged(nameof(Employees));
                }
            }
        }


        public EmployeeMPM SelectedEmployee
        {
            get
            {
                return _selectedEmployee;
            }

            set
            {
                if (_selectedEmployee != value)
                {
                    _selectedEmployee = value;
                    OnPropertyChanged(nameof(SelectedEmployee));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
