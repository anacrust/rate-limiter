using RateLimiter;
using RateLimiter.Models.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddInMemoryRateLimiting();
builder.Services.AddControllers();

builder.Services.Configure<ActiveProcessorsOptions>(builder.Configuration.GetSection(ActiveProcessorsOptions.Position));
builder.Services.Configure<LastCallTimeSpanOptions>(builder.Configuration.GetSection(LastCallTimeSpanOptions.Position));
builder.Services.Configure<RequestRateOptions>(builder.Configuration.GetSection(RequestRateOptions.Position));

var app = builder.Build();

app.UseClientRateLimiting();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
