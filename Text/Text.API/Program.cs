using Text.API;
using Text.API.Gql;

var builder = WebApplication.CreateBuilder(args);

builder
    .AddGraphQL()
    .AddGraphQLServer(disableDefaultSecurity: true)
    .AddQueryType<Query>(x => x.Name(GqlUtils.QueryObjectTypeName))
    .AddMutationType<Mutation>(x => x.Name(GqlUtils.MutationObjectTypeName))
    .AddFiltering()
    .AddSorting()
    .AddProjections();

builder.Services
    .AddServices()
    .AddWorkers();

var app = builder.Build();

app.MapGraphQL();

app.Run();