using ContextEf;
using Data;
using Data.Entities.Base;
using Microsoft.EntityFrameworkCore;
using StatusGeneric;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Repository.Repositoryeis;

public interface IItems<out T> where T : class, IEntity, new()
{
    IStatusGeneric<IQueryable<T>> GetItems();
}
public class Items<T> : IItems<T> where T : Entity, new()
{
    private readonly AppDbContext _context;
    private readonly DbSet<T> _set;

    public Items(AppDbContext context)
    {
        _context = context;
        _set = _context.Set<T>();
    }
    public IStatusGeneric<IQueryable<T>> GetItems()
    {
        var status = new StatusGenericHandler<IQueryable<T>>();

        var s = _set.AsQueryable();

        return status.SetResult(s);
    }
}