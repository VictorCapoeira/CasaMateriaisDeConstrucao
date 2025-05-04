using CasaMateriaisDeConstrucao.Data;
using Microsoft.EntityFrameworkCore; 

var builder = WebApplication.CreateBuilder(args);
var connectionString = "server=localhost;port=3306;database=produtosConstrucao;user=root;password=1234";
builder.Services.AddDbContext<AppDbContext>(options => options.UseMySQL(connectionString));
var app = builder.Build();
app.UseStaticFiles();
app.MapGet("/api/produtos", async (AppDbContext db) =>
{
    var produtos = await db.Produtos.ToListAsync();
    return Results.Ok(produtos);
});
app.MapPost("/api/candidaturas", async (HttpRequest request, AppDbContext db) =>
{
    var form = await request.ReadFormAsync();

    var vaga = form["vaga"];
    var nome = form["nome"];
    var email = form["email"];
    var telefone = form["telefone"];
    var arquivo = form.Files.GetFile("curriculo");

    if (arquivo == null || arquivo.Length == 0)
    {
        return Results.BadRequest("Arquivo inválido");
    }

    var uploadsFolder = Path.Combine("uploads", "curriculos");

    if (!Directory.Exists(uploadsFolder))
        Directory.CreateDirectory(uploadsFolder);

    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(arquivo.FileName);
    var filePath = Path.Combine(uploadsFolder, fileName);

    using (var stream = new FileStream(filePath, FileMode.Create))
    {
        await arquivo.CopyToAsync(stream);
    }

    var candidatura = new Candidatura
    {
        Vaga = vaga!,
        Nome = nome!,
        Email = email!,
        Telefone = telefone!,
        CurriculoPath = filePath
    };

    db.Candidaturas.Add(candidatura);
    await db.SaveChangesAsync();

    return Results.Ok("Candidatura enviada com sucesso!");
});

app.MapGet("/", context =>
{
    context.Response.Redirect("/index.html");
    return Task.CompletedTask;
});


app.Run();
