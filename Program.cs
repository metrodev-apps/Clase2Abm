var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();



// Team Router
app.MapTeam();


// Run App
app.Run();
