using System.ComponentModel.DataAnnotations;
using Basket.DataBase;
using Microsoft.EntityFrameworkCore;

namespace Basket.Controller;

public class Nba
{
    private readonly IDbContextFactory<DbConnection> _dbContextFactory;

    public Nba(IDbContextFactory<DbConnection> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task AddEntityAsync<T>(T entity) where T : class
    {
        Validate(entity);

        await using var context = await _dbContextFactory.CreateDbContextAsync();
        await context.Set<T>().AddAsync(entity);
        await context.SaveChangesAsync();
    }

    public async Task<List<T>> GetAllEntitiesAsync<T>() where T : class
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync();
        return await context.Set<T>().ToListAsync();
    }

    private void Validate<T>(T entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        var context = new ValidationContext(entity);
        var results = new List<ValidationResult>();

        if (!Validator.TryValidateObject(entity, context, results, true))
        {
            throw new ValidationException($"Validation failed for {typeof(T).Name}: " +
                                          string.Join(", ", results.Select(r => r.ErrorMessage)));
        }
    }
}