using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PranicAhmedbad.Lib.Common;
using PranicAhmedbad.Lib.Repository.Account;
using PranicAhmedbad.Lib.Repository.ModuleErrorLog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IAccountRepository, AccountRepository>();
builder.Services.AddSingleton<IModuleErrorLogRepository, ModuleErrorLogRepository>();
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

