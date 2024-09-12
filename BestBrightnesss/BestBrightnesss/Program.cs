using BestBrightnesss.BusinessLogic.LogicInterface;
using BestBrightnesss.BusinessLogic.Managers.InventoryMananger;
using BestBrightnesss.BusinessLogic.Managers.ProfileMananger;
using BestBrightnesss.BusinessLogic.Managers.ReportMananger;
using BestBrightnesss.BusinessLogic.Managers.SalesManager;
using BestBrightnesss.DataLogic;
using BestBrightnesss.Repositories.InventoryReposetory;
using BestBrightnesss.Repositories.ProfileRepository;
using BestBrightnesss.Repositories.ReportReposetory;
using BestBrightnesss.Repositories.RepositoryInterfaces;
using BestBrightnesss.Repositories.SalesRepository;
using BestBrightnesss.Services;
using DinkToPdf.Contracts;
using DinkToPdf;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Configure DbContext with your connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DefaultContext>(options =>
    options.UseSqlServer(connectionString));

// Add Identity services
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
})
.AddEntityFrameworkStores<DefaultContext>();

// Add authentication services and configure Azure AD options
builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAd"))
    .EnableTokenAcquisitionToCallDownstreamApi()
    .AddInMemoryTokenCaches();

// Add controllers with views for authentication UI
builder.Services.AddControllersWithViews().AddMicrosoftIdentityUI();

// Register business logic and repository services
builder.Services.AddTransient<ISalesLogic, RecordSalesMananger>();
builder.Services.AddTransient<iRecordSales, SalesRepository>();

builder.Services.AddTransient<IInventoryLogic, InvetoryMananger>();
builder.Services.AddTransient<iInventory, InventoryReposetory>();

builder.Services.AddTransient<IProfileLogic, RegisterMananger>();
builder.Services.AddTransient<iProfile, ProfileRepository>();

builder.Services.AddTransient<IReportLogic, ReportMananger>();
builder.Services.AddTransient<iReports, ReportRepository>();

// Register DinkToPdf converter
builder.Services.AddSingleton<IConverter>(provider =>
{
    return new SynchronizedConverter(new PdfTools());
});
builder.Services.AddTransient<PdfService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

app.Run();
