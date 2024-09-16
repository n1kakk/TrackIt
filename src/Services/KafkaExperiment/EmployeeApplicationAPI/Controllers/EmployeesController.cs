using Confluent.Kafka;
using EmployeeApplicationAPI.Database;
using EmployeeApplicationAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace EmployeeApplicationAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly ILogger<EmployeesController> _logger;
        private readonly EmployeeDbContext _dbContext;

        public EmployeesController(ILogger<EmployeesController> logger, EmployeeDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            _logger.LogInformation("Requesting all employees");
            return await _dbContext.Employees.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> CreateEmployee(string name, string surname)
        {
            var employee = new Employee(Guid.NewGuid(), name, surname);

            _dbContext.Employees.Add(employee);
            await _dbContext.SaveChangesAsync();

            var message = new Message<string, string>()
            {
                Key = employee.Id.ToString(),
                Value = JsonSerializer.Serialize(employee)
            };
            Console.WriteLine(message.Value);

            //client
            var producerConfig = new ProducerConfig()
            {
                BootstrapServers = "localhost:29092",
                Acks = Acks.All,
                Debug = "all"
            };

            using (var producer = new ProducerBuilder<string, string>(producerConfig).Build())
            {
                await producer.ProduceAsync("employeeTopic", message);
            }

            return CreatedAtAction(nameof(CreateEmployee), new {id = employee.Id}, employee);
        }
    }
}
