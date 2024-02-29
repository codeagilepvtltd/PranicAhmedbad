using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PranicAhmedbad.Lib.Common;
using PranicAhmedbad.Lib.Repository.Account;
using PranicAhmedbad.Lib.Repository.General;
using PranicAhmedbad.Lib.Repository.ModuleErrorLog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IAccountRepository, AccountRepository>();
builder.Services.AddSingleton<IEventRepository, EventRepository>();
builder.Services.AddSingleton<IModuleErrorLogRepository, ModuleErrorLogRepository>();
builder.Services.AddSingleton<IMasterRepository, MasterRepository>();

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
    options.CheckConsentNeeded = context => true; // Set for Session Data : https://stackoverflow.com/questions/49770491/session-variable-value-is-getting-null-in-asp-net-core
    options.MinimumSameSitePolicy = SameSiteMode.None;
});
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
               .AddCookie(options =>
               {
                   options.AccessDeniedPath = "/Home/ErrorForbidden";
                   options.LoginPath = "/Account/Login";
               }
           );

/*For TempData required to register*/
builder.Services.Configure<CookieTempDataProviderOptions>(options =>
{
    options.Cookie.IsEssential = true;
});


builder.Services.AddHttpContextAccessor();
builder.Services.AddRouting();
builder.Services.AddMvc().AddSessionStateTempDataProvider();
builder.Services.AddSession();
var app = builder.Build();
SQLHelper.InitializeConfiguration(app.Configuration);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();

/*Http Context Accessor*/

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();

