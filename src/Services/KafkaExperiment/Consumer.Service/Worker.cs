using Confluent.Kafka;
using Consumer.Database;
using Consumer.Database.Entities;
using Consumer.Shared;
using System.Text.Json;

namespace Consumer.Service;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly EmployeeReportDbContext _employeeReportDbContext;

    public Worker(ILogger<Worker> logger, EmployeeReportDbContext employeeReportDbContext)
    {
        _logger = logger;
        _employeeReportDbContext = employeeReportDbContext;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {

        var consumerConfig = new ConsumerConfig()
        {
            BootstrapServers = "localhost:29092",
            ClientId = "myconsumerclient",
            GroupId = "employeeConsumerGroup",
            AutoOffsetReset = AutoOffsetReset.Earliest,
        };

        using (var consumer = new ConsumerBuilder<string, string>(consumerConfig).Build())
        {
            consumer.Subscribe("employeeTopic");
            while (!stoppingToken.IsCancellationRequested)
            {
                var consumerData = consumer.Consume(TimeSpan.FromSeconds(3));

                if (consumerData != null)
                {
                    var employee = JsonSerializer.Deserialize<Employee>(consumerData.Message.Value);
                    _logger.LogInformation($"Consuming {employee}");

                    var employeeReport = new EmployeeReport(Guid.NewGuid(), employee.Id, employee.Name, employee.Surname);

                    _employeeReportDbContext.Reports.Add(employeeReport);
                    await _employeeReportDbContext.SaveChangesAsync();
                }
                else
                    _logger.LogInformation("Nothing found to consume");
            }

        }
    }
}
