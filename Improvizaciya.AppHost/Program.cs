var builder = DistributedApplication.CreateBuilder(args);

//не работает на dotnet 9
//var reverseProxy = builder.AddProject<Projects.Improvizaciya_ReversedProxy>("reverse-proxy");

var apiService = builder.AddProject<Projects.Improvizaciya_ApiService>("apiservice");

var front = builder.AddProject<Projects.Improvizaciya_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService)
    .WaitFor(apiService);

//Не работает на dotnet 9
//reverseProxy.WithReference(front);

var executableapp = builder.AddSpringApp(
    "executableapp", 
    "../Improvizaciya.JwtService/jwt-service", 
    new JavaAppExecutableResourceOptions
    {
        ApplicationName = "target/jwt-service-0.0.1-SNAPSHOT.jar",
        OtelAgentPath = "/home/wave/repos/Improvizaciya/agents"
    });

builder.Build().Run();
