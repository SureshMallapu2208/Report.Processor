using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.S3;
using Microsoft.Extensions.DependencyInjection;
using Report.Processor;
using Report.Processor.S3;
using Report.Processor.Services;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        var options = hostContext.Configuration.GetAWSOptions();
        services.AddDefaultAWSOptions(options);
        services.AddAWSService<IAmazonS3>();
        services.AddAWSService<IAmazonDynamoDB>();
        services.AddSingleton<IDynamoDBContext, DynamoDBContext>();
        services.AddSingleton<IS3Operations, S3Operations>();
        services.AddSingleton<IUserService, UserService>();

        services.AddHostedService<ReportProcessor>();
    })
    .Build();

await host.RunAsync();
