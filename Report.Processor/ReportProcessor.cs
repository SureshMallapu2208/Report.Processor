using Amazon.Runtime.Internal;
using Amazon.S3;
using Amazon.S3.Model;
using Report.Processor.S3;
using Report.Processor.Services;

namespace Report.Processor
{
    public class ReportProcessor : BackgroundService
    {
        private readonly ILogger<ReportProcessor> _logger;
        private readonly IUserService _userService;
        private readonly IS3Operations _s3Operations;


        public ReportProcessor(ILogger<ReportProcessor> logger, IUserService userService, IS3Operations s3Operations)
        {
            _logger = logger;
            _userService = userService;
            _s3Operations = s3Operations;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            var timer = new PeriodicTimer(TimeSpan.FromMinutes(5));
            while (await timer.WaitForNextTickAsync(stoppingToken))
            {
                string path = string.Format(Directory.GetCurrentDirectory() + "\\{0}", "Reports");
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                Array.ForEach(Directory.GetFiles(path), File.Delete);

                var users = await _userService.GetAllusers();

                string fileName = Path.Combine(path, $"s3File.txt");
                Write(users, fileName);

                 _s3Operations.PutObject();

            }
        }
        public void Write(List<User> requests, string fileName)
        {
            string message = GetData(requests);

            using (StreamWriter outputFile = new StreamWriter(fileName, true))
            {
                outputFile.WriteLine(message);
            }
        }

        private string GetData(List<User> requests)
        {
            string requestData = string.Empty;
            foreach (var request in requests)
            {
                requestData += string.Format("{0}|{1}{2}{3}", request.Name, request.Email, request.Mobile, Environment.NewLine);

            }
            return requestData;
        }
    }
}
