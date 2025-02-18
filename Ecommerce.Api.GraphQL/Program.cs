using System.Reflection;
using System.Text.Json;
using Ecommerce.Api.GraphQL.Mutations;
using Ecommerce.Api.GraphQL.Queries;
using Ecommerce.Services.Utilities.Extensions.Generic;
using Serilog;
using ServiceAppStartup = Ecommerce.Services.AppStartup;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ServiceAppStartup.ConfigureServices(builder.Services);
ServiceAppStartup.InitializeLogging();

// Setup GraphQL API Services
var graphQlServer = builder.Services.AddGraphQLServer();

Log.Debug("Query types in this assembly: {types}", Assembly.GetExecutingAssembly().GetTypes().Where(type => type.Name.Contains("Queries"))
        .Select(type => type.Name).ToJson());

Log.Debug("Mutation types in this assembly: {types}", Assembly.GetExecutingAssembly().GetTypes().Where(type => type.Name.Contains("Mutations"))
        .Select(type => type.Name).ToJson());

// Add Queries
graphQlServer.AddQueryType<Query>().AddTypeExtension<CategoryQueries>();

// Add Mutations
graphQlServer.AddMutationType<Mutation>().AddTypeExtension<CategoryMutations>();

// Add Exception Handling
graphQlServer.AddErrorFilter(error => error.WithMessage(error.Exception?.Message ?? "Unexpected Server Error"));

var app = builder.Build();

app.MapGraphQL();

app.Run();