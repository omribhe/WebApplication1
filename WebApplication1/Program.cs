
using Microsoft.AspNetCore.SignalR;
using WebApplication1.Hubs;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<MyHub>(new MyHub());

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRazorPages();

builder.Services.AddCors(options =>
{ 
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.AllowAnyHeader().
                          AllowAnyMethod().
                          WithOrigins("http://localhost:3000")
                          .AllowCredentials();
                      }
                      );
});
builder.Services.AddSignalR();
var app = builder.Build();
//WebApplication1.Database.createDatabase();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(MyAllowSpecificOrigins);
app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.UseEndpoints(endpoints =>
    {
        endpoints.MapHub<MyHub>("/MyHub");
    });

app.Run();
