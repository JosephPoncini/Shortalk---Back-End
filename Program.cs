using Shortalk___Back_End.Services;
using Shortalk___Back_End.Services.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<LobbyService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<PasswordService>();

//this is how we’re connecting our database to API
var connectionString = builder.Configuration.GetConnectionString("ShortalkString");


//configures entity framework core to use SQL server as the database provider for  a datacontext DbContext in our project
builder.Services.AddDbContext<DataContext>(Options => Options.UseSqlServer(connectionString));


builder.Services.AddCors(options => options.AddPolicy("ShortalkPolicy", 
builder => {
    builder.WithOrigins("http://localhost:5151","http://localhost:3000","https://shortalk-front-end.vercel.app/")
    .AllowAnyHeader()
    .AllowAnyMethod();
}
));



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

app.UseHttpsRedirection();
app.UseCors("ShortalkPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
