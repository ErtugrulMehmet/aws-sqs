using Amazon.Runtime;
using Amazon.SQS;
using Amazon.SQS.Model;

namespace aws_sqs
{
    public class WeatherForecastProcessor : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("Starting Background processor");
            var credentials = new BasicAWSCredentials("AKIA33V5NB3Q3KXH4NQJ", "Uk8UCakM6nTGZ2r3HsH8tcChX5HeD16o/8L83tcg");
            var client = new AmazonSQSClient(credentials, Amazon.RegionEndpoint.EUCentral1);


            while (!stoppingToken.IsCancellationRequested)
            {
                var request = new ReceiveMessageRequest()
                {
                    QueueUrl = "https://sqs.eu-central-1.amazonaws.com/815367261921/mehmet-demo",
                    WaitTimeSeconds=10
                };
                var response = await client.ReceiveMessageAsync(request);

                foreach (var message in response.Messages)
                {
                    Console.WriteLine(message.Body);
                }
            }
        }
    }
}
