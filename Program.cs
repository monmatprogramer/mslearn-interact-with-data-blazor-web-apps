using BlazingPizza.Data;
/*
    * allows the app to use the new service.
*/
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
// stpe 5
// Add services to the container.
builder.Services.AddHttpClient();
/*
    * adds the http client to the container
    * allows the app to access HTTP commands
    * uses an HttpClient to get the JSON for pizza specials
*/
builder.Services.AddSqlite<PizzaStoreContext>("Data Source=myPizzaStore.db");
/*
    * adds the SQLite database to the container
    * registers the new PizzaStoreContext
    * provides the filename for the SQLite database.
*/

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();
app.UseRouting();

app.MapRazorPages();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
// step 8
app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
/*
    * adds a controller route to the app
    * the default route is Home/Index
    * the default route is the home page
*/
//---------------------------------------------------------------------------------------------
/* 
    * Step 4
    * Initialize the database
*/
var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
using (var scope = scopeFactory.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<PizzaStoreContext>();
    if (db.Database.EnsureCreated())
    {
        SeedData.Initialize(db);
        /*
            * If there isn't a database already created, it calls the SeedData static class to create one.

        */
    }

}
/*
    * creates a database scope with the PizzaStoreContext
*/

app.Run();