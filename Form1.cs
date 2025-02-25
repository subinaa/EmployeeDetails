using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmployeeDetails
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadEmployeesAsync();
        }

        private async void btnFInd_ClickAsync(object sender, EventArgs e)
        {
            if (int.TryParse(txtEmpId.Text, out int empId))
            {
                try
                {
                    var employeeDetails = await apiService.GetEmployeeByIdAsync(empId);
                    txtName.Text = employeeDetails.Name;
                    txtGender.Text = employeeDetails.Gender;
                    txtEmail.Text = employeeDetails.Email;
                    MessageBox.Show($"Name: {employeeDetails.Name}\nEmail: {employeeDetails.Email}\nGender: {employeeDetails.Gender}\nStatus: {employeeDetails.Status}", "Employee Details");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Employee not found or an error occurred: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Invalid Employee ID.");
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtName.Text, out int empId) && !string.IsNullOrEmpty(txtEmail.Text))
            {
                try
                {
                    var updatedEmployee = await apiService.UpdateEmailAsync(empId, txtEmail.Text);
                    MessageBox.Show($"Updated Email for {updatedEmployee.Name}: {updatedEmployee.Email}", "Update Successful");
                    LoadEmployeesAsync(); // Refresh the employee list
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while updating the email: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Invalid Employee ID or Email.");
            }
        }
    }

  
}
