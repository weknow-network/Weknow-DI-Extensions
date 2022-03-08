// see issue: https://stackoverflow.com/questions/39174989/how-to-register-multiple-implementations-of-the-same-interface-in-asp-net-core
// see autofac: https://autofac.readthedocs.io/en/latest/advanced/keyed-services.html

using Bnaya.Samples;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddKeyedSingleton<IFunctionality, AFunctionality>("A");
builder.Services.AddKeyedSingleton<IFunctionality, BFunctionality>("B");
builder.Services.AddSingleton<IList<int>>(new List<int> { 1, 2, 3});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
