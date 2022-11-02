using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WebTracker.Job
{
    internal class HelloWord:IJob
    {
        private readonly ILogger<HelloWord> _logger;
        public HelloWord(ILogger<HelloWord> logger)
        {
            _logger = logger;
        }

        public Task Execute(IJobExecutionContext context)
        {
            var client = new HttpClient();
            HttpResponseMessage response = client.GetAsync("https://www.24s.com/en-ca/medium-dior-book-tote-dior_DIOKBP2KBLUZZZZZ00?color=blue").Result;
            var result = response.Content.ReadAsStringAsync().Result;
            _logger.LogInformation("Hello world!");
            return Task.CompletedTask;
        }
    }
}
