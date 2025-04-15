using CasaMateriaisDeConstrucao.Data;
using Microsoft.EntityFrameworkCore; 

var builder = WebApplication.CreateBuilder(args);
var connectionString = "server=localhost;port=3306;database=produtosConstrucao;user=root;password=1234";
builder.Services.AddDbContext<AppDbContext>(options => options.UseMySQL(connectionString));
var app = builder.Build();

app.MapGet("/api/produtos", async (AppDbContext db) =>
{
    var produtos = await db.Produtos.ToListAsync();
    return Results.Ok(produtos);
});

app.Run();
