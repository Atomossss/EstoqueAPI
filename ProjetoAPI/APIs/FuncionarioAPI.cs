
using Microsoft.EntityFrameworkCore;

public static class FuncionarioApi
{

    public static void MapFuncionarioApi(this WebApplication app)
    {

        var group = app.MapGroup("/funcionario");


        group.MapGet("/", async (BancoDeDados db) =>
            await db.Funcionario.ToListAsync()
        );

        group.MapPost("/", async (Funcionario funcionario, BancoDeDados db) =>
        {
            db.Funcionario.Add(funcionario);
            await db.SaveChangesAsync();

            return Results.Created($"/funcionario/{funcionario.Id}", funcionario);
        }
        );

        group.MapPut("/{id}", async (int id, Funcionario funcionarioAlterado, BancoDeDados db) =>
        {
            var funcionario = await db.Funcionario.FindAsync(id);
            if (funcionario is null)
            {
                return Results.NotFound();
            }
            funcionario.Id = funcionarioAlterado.Id;
            funcionario.Nome = funcionarioAlterado.Nome;
            funcionario.Cargo = funcionarioAlterado.Cargo;

            await db.SaveChangesAsync();

            return Results.NoContent();
        }
        );

        group.MapDelete("/{id}", async (int id, BancoDeDados db) =>
        {
            if (await db.Funcionario.FindAsync(id) is Funcionario funcionario)
            {
                db.Funcionario.Remove(funcionario);
                await db.SaveChangesAsync();
                return Results.NoContent();
            }
            return Results.NotFound();
        }
        );
    }
}