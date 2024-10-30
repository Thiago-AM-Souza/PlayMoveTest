using Fornecedores.API.Config;
using Fornecedores.Application;
using Usuario.Application;
using Fornecedores.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddUsuarioApplicationServices()
    .AddFornecedorApplicationServices()
    .AddFornecedorInfrastructureServices(builder.Configuration)
    .AddFornecedorApiServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGroup("/api").MapCarter();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.Run();

