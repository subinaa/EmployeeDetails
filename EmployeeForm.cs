using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;


namespace EmployeeDetails
{
    public partial class MainForm : Form
    {
        private readonly ApiService apiService = new ApiService();
        private List<Employee> employees = new List<Employee>();

      

        private async void LoadEmployeesAsync()
        {
            employees = await apiService.GetEmployeesAsync();
            dataGridViewEmployees.DataSource = employees;
        }

        private async void buttonAdd_Click(object sender, EventArgs e)
        {
            var detailsForm = new MainForm();
            var employee = new Employee { name = "abd" };
            if (detailsForm.ShowDialog() == DialogResult.OK)
            {
                var newEmployee = await apiService.CreateEmployeeAsync(employee);
                employees.Add(newEmployee);
                dataGridViewEmployees.DataSource = null;
                dataGridViewEmployees.DataSource = employees;
            }
        }
    }
}




