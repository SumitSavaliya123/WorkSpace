using Common.Constants;
using WorkSpaceAPI.Extensions;
using WorkSpaceAPI.Middlewares;

var builder = WebApplication.CreateBuilder(args);

//Add Services to the container
builder.Services.AddHttpContextAccessor();

builder.Services.AddRouting(options => options.LowercaseUrls = true); //Lower case url in swagger

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConnectDatabase(builder.Configuration);
builder.Services.RegisterRepository();
builder.Services.RegisterServices();
builder.Services.ConfigureCors();
builder.Services.SetRequestBodySize();
builder.Services.RegisterMail(builder.Configuration);
builder.Services.AddSignalR();

var app = builder.Build();
app.UseCors(SystemConstants.CorsPolicy);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionMiddleware>();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
