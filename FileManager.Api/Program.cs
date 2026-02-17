

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDependencies(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

//Use Static File To Access Images
//Before .Net 9
//app.UseStaticFiles();
// .Net 9
app.MapStaticAssets();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
