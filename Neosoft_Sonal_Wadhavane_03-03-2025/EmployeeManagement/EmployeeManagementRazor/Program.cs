using EmployeeManagementRazor.Services;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();

// Add services to the container.
builder.Services.AddRazorPages();

// ? Register HttpClient for API calls
builder.Services.AddHttpClient<EmployeeService>();
builder.Services.AddScoped<CountryService>();
builder.Services.AddScoped<StateService>();
builder.Services.AddScoped<CityService>();


var app = builder.Build();

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

app.MapRazorPages();

app.Run();
