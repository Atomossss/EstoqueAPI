
using Microsoft.EntityFrameworkCore;

public static class ProdutoApi
{

    public static void MapProdutoApi(this WebApplication app)
    {

        var group = app.MapGroup("/produto");


        group.MapGet("/", async (BancoDeDados db) =>
            await db.Produto.ToListAsync()
        );

        group.MapPost("/", async (Produto produto, BancoDeDados db) =>
        {
            db.Produto.Add(produto);
            await db.SaveChangesAsync();

            return Results.Created($"/produto/{produto.Id}", produto);
        }
        );

        group.MapPut("/{id}", async (int id, Produto produtoAlterado, BancoDeDados db) =>
        {
            var produto = await db.Produto.FindAsync(id);
            if (produto is null)
            {
                return Results.NotFound();
            }
            produto.Id = produtoAlterado.Id;
            produto.Nome = produtoAlterado.Nome;
            produto.Descricao = produtoAlterado.Descricao;

            await db.SaveChangesAsync();

            return Results.NoContent();
        }
        );

        group.MapDelete("/{id}", async (int id, BancoDeDados db) =>
        {
            if (await db.Produto.FindAsync(id) is Produto produto)
            {
                db.Produto.Remove(produto);
                await db.SaveChangesAsync();
                return Results.NoContent();
            }
            return Results.NotFound();
        }
        );
    }
}