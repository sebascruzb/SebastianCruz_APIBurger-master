using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using SebastianCruz_APIBurger.Data;
using SebastianCruz_APIBurger.Data.Models;
namespace SebastianCruz_APIBurger.Controllers;

public static class PromoEndpoints
{
    public static void MapPromoEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Promo").WithTags(nameof(Promo));

        group.MapGet("/", async (SebastianCruz_APIBurgerContext db) =>
        {
            return await db.Promo.ToListAsync();
        })
        .WithName("GetAllPromos")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Promo>, NotFound>> (int promoid, SebastianCruz_APIBurgerContext db) =>
        {
            return await db.Promo.AsNoTracking()
                .FirstOrDefaultAsync(model => model.PromoId == promoid)
                is Promo model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetPromoById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int promoid, Promo promo, SebastianCruz_APIBurgerContext db) =>
        {
            var affected = await db.Promo
                .Where(model => model.PromoId == promoid)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.PromoId, promo.PromoId)
                    .SetProperty(m => m.Descripcion, promo.Descripcion)
                    .SetProperty(m => m.FechaPromo, promo.FechaPromo)
                    .SetProperty(m => m.BurguerId, promo.BurguerId)
                    .SetProperty(m => m.BurgerId, promo.BurgerId)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdatePromo")
        .WithOpenApi();

        group.MapPost("/", async (Promo promo, SebastianCruz_APIBurgerContext db) =>
        {
            db.Promo.Add(promo);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Promo/{promo.PromoId}",promo);
        })
        .WithName("CreatePromo")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int promoid, SebastianCruz_APIBurgerContext db) =>
        {
            var affected = await db.Promo
                .Where(model => model.PromoId == promoid)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeletePromo")
        .WithOpenApi();
    }
}
