using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Excel = Microsoft.Office.Interop.Excel;


namespace Employee
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public EmployeeData employeeData = new EmployeeData();
        
        
        public MainWindow()
        {
            InitializeComponent();

            employeeData.fetchData(DBConnect.getEmployee());
            DataContext = employeeData;
            dgridEmployee.ItemsSource = employeeData.Employees;
            
        }

        public class ExportToExcel<T, U>
        where T : class
        where U : ObservableCollection<T>
        {
            public ObservableCollection<T> dataToPrint;
            // Excel object references.
            private Excel.Application _excelApp = null;
            private Excel.Workbooks _books = null;
            private Excel._Workbook _book = null;
            private Excel.Sheets _sheets = null;
            private Excel._Worksheet _sheet = null;
            private Excel.Range _range = null;
            private Excel.Font _font = null;
            // Optional argument variable
            private object _optionalValue = Missing.Value;

            /// <summary>
            /// Generate report and sub functions
            /// </summary>
            public void GenerateReport()
            {
                try
                {
                    if (dataToPrint != null)
                    {
                        if (dataToPrint.Count != 0)
                        {
                            Mouse.SetCursor(Cursors.Wait);
                            CreateExcelRef();
                            FillSheet();
                            OpenReport();
                            Mouse.SetCursor(Cursors.Arrow);
                        }
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error while generating Excel report");
                }
                finally
                {
                    ReleaseObject(_sheet);
                    ReleaseObject(_sheets);
                    ReleaseObject(_book);
                    ReleaseObject(_books);
                    ReleaseObject(_excelApp);
                }
            }
            /// <summary>
            /// Make MS Excel application visible
            /// </summary>
            private void OpenReport()
            {
                _excelApp.Visible = true;
            }
            /// <summary>
            /// Populate the Excel sheet
            /// </summary>
            private void FillSheet()
            {
                object[] header = CreateHeader();
                WriteData(header);
            }
            /// <summary>
            /// Write data into the Excel sheet
            /// </summary>
            /// <param name="header"></param>
            private void WriteData(object[] header)
            {
                object[,] objData = new object[dataToPrint.Count, header.Length];

                for (int j = 0; j < dataToPrint.Count; j++)
                {
                    var item = dataToPrint[j];
                    for (int i = 0; i < header.Length; i++)
                    {
                        var y = typeof(T).InvokeMember(header[i].ToString(),
                        BindingFlags.GetProperty, null, item, null);
                        objData[j, i] = (y == null) ? "" : y.ToString();
                    }
                }
                AddExcelRows("A2", dataToPrint.Count, header.Length, objData);
                AutoFitColumns("A1", dataToPrint.Count + 1, header.Length);
            }
            /// <summary>
            /// Method to make columns auto fit according to data
            /// </summary>
            /// <param name="startRange"></param>
            /// <param name="rowCount"></param>
            /// <param name="colCount"></param>
            private void AutoFitColumns(string startRange, int rowCount, int colCount)
            {
                _range = _sheet.get_Range(startRange, _optionalValue);
                _range = _range.get_Resize(rowCount, colCount);
                _range.Columns.AutoFit();
            }
            /// <summary>
            /// Create header from the properties
            /// </summary>
            /// <returns></returns>
            private object[] CreateHeader()
            {
                PropertyInfo[] headerInfo = typeof(T).GetProperties();

                // Create an array for the headers and add it to the
                // worksheet starting at cell A1.
                List<object> objHeaders = new List<object>();
                for (int n = 0; n < headerInfo.Length; n++)
                {
                    objHeaders.Add(headerInfo[n].Name);
                }

                var headerToAdd = objHeaders.ToArray();
                AddExcelRows("A1", 1, headerToAdd.Length, headerToAdd);
                SetHeaderStyle();

                return headerToAdd;
            }
            /// <summary>
            /// Set Header style as bold
            /// </summary>
            private void SetHeaderStyle()
            {
                _font = _range.Font;
                _font.Bold = true;
            }
            /// <summary>
            /// Method to add an excel rows
            /// </summary>
            /// <param name="startRange"></param>
            /// <param name="rowCount"></param>
            /// <param name="colCount"></param>
            /// <param name="values"></param>
            private void AddExcelRows(string startRange, int rowCount,
            int colCount, object values)
            {
                _range = _sheet.get_Range(startRange, _optionalValue);
                _range = _range.get_Resize(rowCount, colCount);
                _range.set_Value(_optionalValue, values);
            }
            /// <summary>
            /// Create Excel application parameters instances
            /// </summary>
            private void CreateExcelRef()
            {
                _excelApp = new Excel.Application();
                _books = (Excel.Workbooks)_excelApp.Workbooks;
                _book = (Excel._Workbook)(_books.Add(_optionalValue));
                _sheets = (Excel.Sheets)_book.Worksheets;
                _sheet = (Excel._Worksheet)(_sheets.get_Item(1));
            }
            /// <summary>
            /// Release unused COM objects
            /// </summary>
            /// <param name="obj"></param>
            private void ReleaseObject(object obj)
            {
                try
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                    obj = null;
                }
                catch (Exception ex)
                {
                    obj = null;
                    MessageBox.Show(ex.Message.ToString());
                }
                finally
                {
                    GC.Collect();
                }
            }
        }
        //public class dtaCollection : ObservableCollection<EmployeeMPM> { }
        private void ExportExcel(object sender, RoutedEventArgs e)
        {
            //dtaCollection data = new dtaCollection();
            //data.Add(new EmployeeMPM {NIK = "14110110142",Name = "Christofer Lauren", Supervisor = "Ciel", Depo = "Tangerang", Branch ="HO Alam Sutera",Employee_type="ADMIN", Category="Intern",Join_date="12/12/2018",Status="ACTIVE",Resign_date="", Reason = "" });
            ExportToExcel<EmployeeMPM,ObservableCollection<EmployeeMPM> > s = new ExportToExcel<EmployeeMPM, ObservableCollection<EmployeeMPM>>();
            ICollectionView view = CollectionViewSource.GetDefaultView(dgridEmployee.ItemsSource);
            s.dataToPrint = (ObservableCollection<EmployeeMPM>)view.SourceCollection;
            s.GenerateReport();
            //DataTable excelDT = new DataTable();
            //excelDT = ((DataView)dgridEmployee.ItemsSource).ToTable();

            /*Microsoft.Office.Interop.Excel.Application excel = null;
            Microsoft.Office.Interop.Excel.Workbook wb = null;

            object missing = Type.Missing;
            Microsoft.Office.Interop.Excel.Worksheet ws = null;
            Microsoft.Office.Interop.Excel.Range rng = null;

            try
            {
                excel = new Microsoft.Office.Interop.Excel.Application();
                wb = excel.Workbooks.Add();
                ws = (Microsoft.Office.Interop.Excel.Worksheet)wb.ActiveSheet;

                for (int Idx = 0; Idx < excelDT.Columns.Count; Idx++)
                {
                    ws.Range["A1"].Offset[0, Idx].Value = excelDT.Columns[Idx].ColumnName;
                }

                for (int Idx = 0; Idx < excelDT.Rows.Count; Idx++)
                {  // <small>hey! I did not invent this line of code, 
                   // I found it somewhere on CodeProject.</small> 
                   // <small>It works to add the whole row at once, pretty cool huh?</small>
                    ws.Range["A2"].Offset[Idx].Resize[1, excelDT.Columns.Count].Value =
                    excelDT.Rows[Idx].ItemArray;
                }

                excel.Visible = true;
                wb.Activate();
            }
            catch (COMException ex)
            {
                MessageBox.Show("Error accessing Excel: " + ex.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString());
            }
            */
            /* Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
             Microsoft.Office.Interop.Excel._Workbook workBook = app.Workbooks.Add(Type.Missing);
             Microsoft.Office.Interop.Excel._Worksheet workSheet = null;
             workSheet = workBook.Sheets["Sheet1"];
             workSheet = workBook.ActiveSheet;

             DataTable excelDT = new DataTable();
             excelDT = ((DataView)dgridEmployee.ItemsSource).ToTable();

             for (int i = 1; i < dgridEmployee.Columns.Count + 1; i++)
             {
                 workSheet.Cells[1, i] = dgridEmployee.Columns[i - 1].Header;
             }

             //workSheet.Cells[i + 2,j+1] = dgridEmployee.Columns[j].GetCellContent(dgridEmployee.Items[i]);
             for (int idx = 0; idx < excelDT.Rows.Count; idx++)
             {
                 for (int idx2 = 0; idx2 <= dgridEmployee.Columns.Count; idx2++)
                 {

                 }
                 idx++;
             }*/

            //   }
            //}

            /*var saveFileToExcel = new SaveFileDialog();
            saveFileToExcel.FileName = "*";
            saveFileToExcel.DefaultExt = "xlsx";
            saveFileToExcel.Filter =  "Excel Files|*.xls;*.xlsx;*.xlsm";
            if (saveFileToExcel.ShowDialog() == true)

            {
                workBook.SaveAs(saveFileToExcel.FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            }
            app.Quit();*/
        }

      

        private void UpdateEmployee(object sender, RoutedEventArgs e)
        {
            if (dgridEmployee.SelectedItem != null)
            {
                UpdateEmployee updateWindow = new UpdateEmployee();
                updateWindow.DataContext = employeeData;
                //MessageBox.Show(employeeData.SelectedEmployee.NIK);
                updateWindow.Show();
            }
            else MessageBox.Show("No item selected..");
        }

        private void Add_Employee(object sender, RoutedEventArgs e)
        {
            AddEmployee addWindow = new AddEmployee();
            addWindow.ShowDialog();
            employeeData = new EmployeeData();
            employeeData.fetchData(DBConnect.getEmployee());
            DataContext = employeeData;
        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            dgridEmployee.Items.Refresh();
        }

        private void tbx_search_TextChanged(object sender, TextChangedEventArgs e)
        {
            var _itemSourceList = new CollectionViewSource() { Source = employeeData.Employees };

            // ICollectionView the View/UI part 
            ICollectionView Itemlist = _itemSourceList.View;

            // your Filter
            var yourCostumFilter = new Predicate<object>(item => ((EmployeeMPM)item).Name.ToLower().Contains(tbx_search.Text.ToLower().ToString()));

            //now we add our Filter
            Itemlist.Filter = yourCostumFilter;

            dgridEmployee.ItemsSource = Itemlist;
        }
    }
    
}
