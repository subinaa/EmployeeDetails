using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace EmployeeDetails
{
    public class ApiService
    {
        private readonly string baseUrl = "https://gorest.co.in/public/v2/";
        private readonly string apiToken = "0bf7fb56e6a27cbcadc402fc2fce8e3aa9ac2b40d4190698eb4e8df9284e2023";
        private readonly HttpClient httpClient;

        public ApiService()
        {
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiToken}");
        }

        public async Task<List<Employee>> GetEmployeesAsync()
        {
            var response = await httpClient.GetAsync($"{baseUrl}users");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Employee>>(content);
        }

        public async Task<Employee> CreateEmployeeAsync(Employee employee)
        {
            var content = JsonSerializer.Serialize(employee);
            var response = await httpClient.PostAsync($"{baseUrl}users", new StringContent(content));
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<Employee>();
        }
        // New method to get employee details by ID
        public async Task<Employee> GetEmployeeByIdAsync(int empId)
        {
            var response = await httpClient.GetAsync($"{baseUrl}users/{empId}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<Employee>();
        }
        // New method to update the email of an employee
        public async Task<Employee> UpdateEmailAsync(int empId, string newEmail)
        {
            var updateData = new { email = newEmail };
            var content = JsonSerializer.Serialize(updateData);
            var response = await httpClient.PatchAsync($"{baseUrl}users/{empId}", new StringContent(content, System.Text.Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<Employee>();

        }
    }
}



