using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee
{
    public class EmployeeMPM
    {
        private int _id;
        private string _nik;
        private string _name;
        private string _supervisor;
        private string _depo;
        private string _branch;
        private string _employee_type;
        private string _category;
        private string _join_date;
        private string _status;
        private string _resign_date;
        private string _reason;

        public int Id
        {
            get
            {
                return _id;
            }

            set
            {
                _id = value;
            }
        }

        public string NIK
        {
            get
            {
                return _nik;
            }

            set
            {
                _nik = value;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
            }
        }

        public string Supervisor
        {
            get
            {
                return _supervisor;
            }

            set
            {
                _supervisor = value;
            }
        }

        public string Depo
        {
            get
            {
                return _depo;
            }

            set
            {
                _depo = value;
            }
        }

        public string Branch
        {
            get
            {
                return _branch;
            }

            set
            {
                _branch = value;
            }
        }

        public string Employee_type
        {
            get
            {
                return _employee_type;
            }

            set
            {
                _employee_type = value;
            }
        }

        public string Category
        {
            get
            {
                return _category;
            }

            set
            {
                _category = value;
            }
        }

        public string Join_date
        {
            get
            {
                return _join_date;
            }

            set
            {
                _join_date = value;
            }
        }

        public string Status
        {
            get
            {
                return _status;
            }

            set
            {
                _status = value;
            }
        }

        public string Resign_date
        {
            get
            {
                return _resign_date;
            }

            set
            {
                _resign_date = value;
            }
        }

        public string Reason
        {
            get
            {
                return _reason;
            }

            set
            {
                _reason = value;
            }
        }
    }
}
