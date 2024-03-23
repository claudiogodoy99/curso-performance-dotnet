var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

var app = builder.Build();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Adicionando um Middleware  no final do pipeline para modificar todas respostas
app.Use(async (context, next) =>
{
    context.Response.StatusCode = 499;
    context.Response.Headers.Add("Header-do-Mid", "Oi do Mid");
    await next.Invoke();
});

app.Run();
