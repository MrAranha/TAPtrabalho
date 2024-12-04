using microservcolegio.Secretaria.Entities;
using microservcolegio.Secretaria.Entities.Services;
using microservcolegio.Secretaria.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<RepositoryDbContext>();
builder.Services.AddScoped<IAlunoService, AlunoService>();
builder.Services.AddScoped<ISolicitacaoService, SolicitacaoService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();

