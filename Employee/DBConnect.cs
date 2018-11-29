using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Collections.ObjectModel;
using System.Windows;

namespace Employee
{
    class DBConnect
    {
        private static String connectionString = @"Server=localhost;
                                                   Port=3306;
                                                   Database=karyawanmpm;
                                                   Uid=root;
                                                   Pwd=";
                                                   
        private static MySqlConnection conn = new MySqlConnection(connectionString);

        public static void connect()
        {
            try
            {
                conn.Open();
                Console.WriteLine("Success");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine("Error");
            }
        }

        public static void disconnect()
        {
            try
            {
                if (conn != null)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }

        public static void updateData(EmployeeMPM updateEmployee)
        {
            try
            {
                connect();
                string updateQuery = "UPDATE employee SET name = '"+ updateEmployee.Name.ToString() + "', supervisor = '"+ updateEmployee.Supervisor.ToString() + "', depo = '"+ updateEmployee.Depo.ToString() + "',branch = '"+ updateEmployee.Branch.ToString() + "',employee_type = '"+ updateEmployee.Employee_type.ToString() + "',category = '"+ updateEmployee.Category.ToString() + "',join_date = '"+ updateEmployee.Join_date.ToString() + "',status='"+ updateEmployee.Status.ToString() + "', resign_date ='"+ updateEmployee.Resign_date.ToString() + "', reason = '"+ updateEmployee.Reason.ToString() + "' WHERE nik = '"+ updateEmployee.NIK.ToString() + "'";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = updateQuery;
                cmd.Connection = conn;
                
                int updates = cmd.ExecuteNonQuery();
                if (updates == 1)
                {
                    MessageBox.Show("Employee "+updateEmployee.Name+ " has been updated");
                }

                disconnect();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                disconnect();
            }
        }

        public static void insertData(EmployeeMPM newEmployee)
        {
            try
            {
                connect();
                string insertQuery = "INSERT into employee (nik,name,supervisor,depo,branch,employee_type,category,join_date,status) VALUES (@nik,@name,@supervisor,@depo,@branch,@employee_type,@category,@join_date,@status)";
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = insertQuery;
                cmd.Parameters.AddWithValue("@nik",newEmployee.NIK);
                cmd.Parameters.AddWithValue("@name", newEmployee.Name);
                cmd.Parameters.AddWithValue("@supervisor", newEmployee.Supervisor);
                cmd.Parameters.AddWithValue("@depo", newEmployee.Depo);
                cmd.Parameters.AddWithValue("@branch", newEmployee.Branch);
                cmd.Parameters.AddWithValue("@employee_type", newEmployee.Employee_type);
                cmd.Parameters.AddWithValue("@category", newEmployee.Category);
                cmd.Parameters.AddWithValue("@join_date", newEmployee.Join_date);
                cmd.Parameters.AddWithValue("@status", "ACTIVE");
                cmd.Connection = conn;
                int ins = cmd.ExecuteNonQuery();
                if (ins == 1)
                {
                    MessageBox.Show(newEmployee.Name+" has been added");
                }
                disconnect();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : "+ex.StackTrace);
                disconnect();
            }
        }
        
        public static ObservableCollection<EmployeeMPM> getEmployee()
        {
            ObservableCollection<EmployeeMPM> EmployeeList = new ObservableCollection<EmployeeMPM>();
            try
            {
                connect();
                string query = "SELECT id,nik,name,supervisor,depo,branch,employee_type,category,join_date,status,resign_date,reason FROM employee WHERE employee_type LIKE 'MU%' OR employee_type LIKE 'DSS%' ORDER BY nik";

                MySqlCommand command = new MySqlCommand(query, conn);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    EmployeeMPM employee = new EmployeeMPM();
                    employee.Id = reader.GetInt32(0);
                    employee.NIK = reader.GetString(1);
                    employee.Name = reader.GetString(2);
                    employee.Supervisor = reader.GetString(3);
                    employee.Depo = reader.GetString(4);
                    employee.Branch = reader.GetString(5);
                    employee.Employee_type = reader.GetString(6);
                    employee.Category = reader.GetString(7);
                    employee.Join_date = reader.GetString(8);
                    employee.Status = reader.GetString(9);
                    employee.Resign_date = reader.GetString(10);
                    employee.Reason = reader.GetString(11); 
                    EmployeeList.Add(employee);
                }
                disconnect();
                return EmployeeList;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                disconnect();
            }
            return null;
        }

        //MU
        public static ObservableCollection<EmployeeMPM> MUEmployee()
        {
            ObservableCollection<EmployeeMPM> EmployeeList = new ObservableCollection<EmployeeMPM>();
            try
            {
                connect();
                string query = "SELECT id,nik,name,supervisor,depo,branch,employee_type,category,join_date,status,resign_date,reason FROM employee WHERE employee_type LIKE 'MU%' ORDER BY nik ASC";

                MySqlCommand command = new MySqlCommand(query, conn);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    EmployeeMPM employee = new EmployeeMPM();
                    employee.Id = reader.GetInt32(0);
                    employee.NIK = reader.GetString(1);
                    employee.Name = reader.GetString(2);
                    employee.Supervisor = reader.GetString(3);
                    employee.Depo = reader.GetString(4);
                    employee.Branch = reader.GetString(5);
                    employee.Employee_type = reader.GetString(6);
                    employee.Category = reader.GetString(7);
                    employee.Join_date = reader.GetString(8);
                    employee.Status = reader.GetString(9);
                    employee.Resign_date = reader.GetString(10);
                    employee.Reason = reader.GetString(11);
                    EmployeeList.Add(employee);
                }
                disconnect();
                return EmployeeList;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                disconnect();
            }
            return null;
        }

        //DSS
        public static ObservableCollection<EmployeeMPM> DSSEmployee()
        {
            ObservableCollection<EmployeeMPM> EmployeeList = new ObservableCollection<EmployeeMPM>();
            try
            {
                connect();
                string query = "SELECT id,nik,name,supervisor,depo,branch,employee_type,category,join_date,status,resign_date,reason FROM employee WHERE employee_type LIKE 'DSS%' ORDER BY nik ASC";

                MySqlCommand command = new MySqlCommand(query, conn);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    EmployeeMPM employee = new EmployeeMPM();
                    employee.Id = reader.GetInt32(0);
                    employee.NIK = reader.GetString(1);
                    employee.Name = reader.GetString(2);
                    employee.Supervisor = reader.GetString(3);
                    employee.Depo = reader.GetString(4);
                    employee.Branch = reader.GetString(5);
                    employee.Employee_type = reader.GetString(6);
                    employee.Category = reader.GetString(7);
                    employee.Join_date = reader.GetString(8);
                    employee.Status = reader.GetString(9);
                    employee.Resign_date = reader.GetString(10);
                    employee.Reason = reader.GetString(11);
                    EmployeeList.Add(employee);
                }
                disconnect();
                return EmployeeList;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                disconnect();
            }
            return null;
        }

        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

        public static int authentication(string _username, string _password)
        {
            int count;
            try
            {
                string username = _username.ToString();
                string pass = CreateMD5(_password.ToString()).ToLower();
                connect();

                string authQuery = "SELECT COUNT(1) FROM `hr_user` WHERE username = '" + username + "' AND password = '" + pass + "'";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = authQuery;
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", pass);
                cmd.Connection = conn;
                count = Convert.ToInt32(cmd.ExecuteScalar());
                if (count == 1)
                {
                    MainWindow main = new MainWindow();
                    LoginWindow login = new LoginWindow();
                    main.Show();
                    login.Close();
                    //MessageBox.Show("Success");
                }
                else
                {
                    MessageBox.Show("Incorrect username or password");
                }
                disconnect();
                return count;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                disconnect();
            }
            return 0;
        }

    }
}
