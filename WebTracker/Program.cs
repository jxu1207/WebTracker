using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Quartz;
using System;
using WebTracker.Job;

namespace WebTracker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();

        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
           Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var envVar = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                    //config.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Prod"}.json", optional: true);
                })
               .ConfigureServices((hostContext, services) =>
               {
                   // Add the required Quartz.NET services
                   services.AddQuartz(q =>
                   {
                       // Use a Scoped container to create jobs. I'll touch on this later
                       q.UseMicrosoftDependencyInjectionScopedJobFactory();
                       var jobKey = new JobKey("HelloWorldJob");

                       // Register the job with the DI container
                       q.AddJob<HelloWord>(opts => opts.WithIdentity(jobKey));

                       // Create a trigger for the job
                       q.AddTrigger(opts => opts
                           .ForJob(jobKey) // link to the HelloWorldJob
                           .WithIdentity("HelloWorldJob-trigger") // give the trigger a unique name
                           .WithCronSchedule("0/5 * * * * ?")); // run every 5 seconds

                   });

                   // Add the Quartz.NET hosted service

                   services.AddQuartzHostedService(
                       q => q.WaitForJobsToComplete = true);

                   // other config
               });
    }
}
