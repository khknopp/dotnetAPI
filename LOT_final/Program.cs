// ############ Initial configuration ############
// Namespaces
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

// Builder definition and settings
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthorization();
builder.Services.AddAuthentication("Bearer").AddJwtBearer();
var dbString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ProductDb>(opt => 
      opt.UseSqlite(dbString));

// App definition and settings
var app = builder.Build();
app.UseAuthorization();

// ############ Routes ############
// GET all products
app.MapGet("/produkty", async (ProductDb db) =>
    await db.Products.ToListAsync()
    ).RequireAuthorization();

// GET a product with a specific id
app.MapGet("/produkty/{id}", async (int id, ProductDb db) =>
    await db.Products.FindAsync(id)
        is Product product
            ? Results.Ok(product)
            : Results.NotFound()
            ).RequireAuthorization();

// POST a product
app.MapPost("/produkty", async (Product product, ProductDb db) =>
{
    db.Products.Add(product);
    await db.SaveChangesAsync();

    return Results.Created($"/produkty/{product.ID}", product);
}).RequireAuthorization();

// PUT (change) a product
app.MapPut("/produkty/{id}", async (int id, Product inputProduct, ProductDb db) =>
{
    var product = await db.Products.FindAsync(id);

    if (product is null) return Results.NotFound();

    product.Name = inputProduct.Name;
    product.Description = inputProduct.Description;
    product.Price = inputProduct.Price;
    product.LastEdited = DateTime.Now;

    await db.SaveChangesAsync();

    return Results.NoContent();
}).RequireAuthorization();

// DELETE a product
app.MapDelete("/produkty/{id}", async (int id, ProductDb db) =>
{
    if (await db.Products.FindAsync(id) is Product product)
    {
        db.Products.Remove(product);
        await db.SaveChangesAsync();
        return Results.Ok(product);
    }

    return Results.NotFound();
}).RequireAuthorization();

// ############ Launch the app ############
app.Run();
