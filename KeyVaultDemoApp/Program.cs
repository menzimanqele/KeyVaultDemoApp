using Azure.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var config = app.Services.GetService<IConfiguration>();
var vaultName = config.GetValue<string>("VaultName");
Uri vaultUri = new Uri($"https://{vaultName}.vault.azure.net/");
builder.Configuration.AddAzureKeyVault(vaultUri, new DefaultAzureCredential());

app.UseAuthorization();

app.MapControllers();

app.Run();