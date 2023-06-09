using Example.BusinessLogic.Services;
using Example.BusinessLogic.Validators;
using Example.DataAccess;
using Example.DataAccess.Repository;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddDbContext<DatabaseContext> (
    options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("Default")
    ));
builder.Services.AddScoped(typeof(IGenericRepository<> ), typeof(GenericRepository<>));
builder.Services.AddScoped<IContactService ,ContactService>();
builder.Services.AddValidatorsFromAssemblyContaining<ContactDtoValidator>();
//builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();