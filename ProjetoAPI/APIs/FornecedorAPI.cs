using Microsoft.EntityFrameworkCore;

public static class FornecedorApi
{

    public static void MapFornecedorApi(this WebApplication app)
    {

        var group = app.MapGroup("/fornecedor");


        group.MapGet("/", async (BancoDeDados db) =>
            await db.Fornecedor.ToListAsync()
        );

        group.MapPost("/", async (Fornecedor fornecedor, BancoDeDados db) =>
        {
            db.Fornecedor.Add(fornecedor);
            await db.SaveChangesAsync();

            return Results.Created($"/fornecedor/{fornecedor.Id}", fornecedor);
        }
        );

        group.MapPut("/{id}", async (int id, Fornecedor fornecedorAlterado, BancoDeDados db) =>
        {
            var fornecedor = await db.Fornecedor.FindAsync(id);
            if (fornecedor is null)
            {
                return Results.NotFound();
            }
            fornecedor.Id = fornecedorAlterado.Id;
            fornecedor.Nome = fornecedorAlterado.Nome;
            fornecedor.ProdutoFornecido = fornecedorAlterado.ProdutoFornecido;

            await db.SaveChangesAsync();

            return Results.NoContent();
        }
        );

        group.MapDelete("/{id}", async (int id, BancoDeDados db) =>
        {
            if (await db.Fornecedor.FindAsync(id) is Fornecedor fornecedor)
            {
                db.Fornecedor.Remove(fornecedor);
                await db.SaveChangesAsync();
                return Results.NoContent();
            }
            return Results.NotFound();
        }
        );
    }
}