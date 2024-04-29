using Microsoft.EntityFrameworkCore;

public static class ClienteApi
{

    public static void MapClienteApi(this WebApplication app)
    {

        var group = app.MapGroup("/cliente");


        group.MapGet("/", async (BancoDeDados db) =>
            await db.Cliente.ToListAsync()
        );

        group.MapPost("/", async (Cliente cliente, BancoDeDados db) =>
        {
            db.Cliente.Add(cliente);
            await db.SaveChangesAsync();

            return Results.Created($"/cliente/{cliente.Id}", cliente);
        }
        );

        group.MapPut("/{id}", async (int id, Cliente clienteAlterado, BancoDeDados db) =>
        {
            var cliente = await db.Cliente.FindAsync(id);
            if (cliente is null)
            {
                return Results.NotFound();
            }
            cliente.Id = clienteAlterado.Id;
            cliente.Nome = clienteAlterado.Nome;
            cliente.ProdutoComprado = clienteAlterado.ProdutoComprado;

            await db.SaveChangesAsync();

            return Results.NoContent();
        }
        );

        group.MapDelete("/{id}", async (int id, BancoDeDados db) =>
        {
            if (await db.Cliente.FindAsync(id) is Cliente cliente)
            {
                db.Cliente.Remove(cliente);
                await db.SaveChangesAsync();
                return Results.NoContent();
            }
            return Results.NotFound();
        }
        );
    }
}