using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AlfaLogger.Repository.Extensions;
using ContextEf;
using Data;
using Data.Entities;
using Data.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Repository.Commands;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Repository.Services;

public interface IGetItems<T> where T : IEntity, new()
{
    IQueryable<T> Get(
        RepositoryCommand<T> command,
        bool tracked = true);
}

public class Items<T> : IGetItems<T> where T : Entity, new()
{
    private readonly DbSet<T> _Set;
    public Items(AppDbContext DB)
    {
        _Set = DB.Set<T>();
    }
    public IQueryable<T> Get(RepositoryCommand<T> command, bool tracked = true)
    {
        var query = tracked ? _Set.AsQueryable() : _Set.AsQueryable().AsNoTracking();
        return query
            .ApplyFilters(command.Filters)
            .ApplyInclude(command.Includes)
            .OrderByDesc()
            .Page(command.ZeroStart, command.Size);
    }
}


