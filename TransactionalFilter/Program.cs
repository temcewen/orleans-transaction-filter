using HotChocolate.AspNetCore;
using Orleans.Configuration;

using TransactionalFilter.Api;
using TransactionalFilter.Backend;


var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddGraphQLServer()
    .AllowIntrospection(builder.Environment.IsDevelopment())
    .AddGlobalObjectIdentification()
    .AddQueryFieldToMutationPayloads()
    .AddMutationConventions()
    .AddMutationType<Mutation>()
    .AddQueryType<Query>();

if (builder.Environment.IsDevelopment())
{
    builder.Host.UseOrleans(builder =>
    {
        builder
        .UseLocalhostClustering()
        .AddMemoryGrainStorageAsDefault()
        .UseInMemoryReminderService()
        .UseTransactions()
        .AddMemoryStreams("StreamProvider")
        .AddMemoryGrainStorage("PubSubStore")
        .AddIncomingGrainCallFilter<LoggingCallFilter>();
    });
}
else
{
    builder.Host.UseOrleans(builder =>
    {
        var connectionString = "";
        builder
        .UseAzureStorageClustering(options => options.ConfigureTableServiceClient(connectionString))
        .AddAzureTableGrainStorageAsDefault(options => options.ConfigureTableServiceClient(connectionString))
        .AddAzureTableTransactionalStateStorageAsDefault(options => options.ConfigureTableServiceClient(connectionString))
        .UseAzureTableReminderService(options => options.ConfigureTableServiceClient(connectionString))
        .UseTransactions()
        .AddAzureTableGrainStorage("PubSubStore", options => options.ConfigureTableServiceClient(connectionString))
        .AddEventHubStreams("StreamProvider", b => { })
        .AddIncomingGrainCallFilter<LoggingCallFilter>()
        .Configure<ClusterOptions>(options =>
        {
            options.ClusterId = "cluster0";
            options.ServiceId = "transactionalfilter";
        });
    });
}

var app = builder.Build();

app
    .MapGraphQL()
    .WithOptions(new GraphQLServerOptions
    {
        Tool =
        {
            Enable = app.Environment.IsDevelopment()
        }
    });

app.Run();
