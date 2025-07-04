﻿using App.DAL.Contracts;
using App.DAL.EF.Mappers;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class PersonRepository :BaseRepository<App.DAL.DTO.Person, App.Domain.Logic.Person>, IPersonRepository
{
    public PersonRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new PersonUOWMapper())
    {
    }

    public override async  Task<IEnumerable<App.DAL.DTO.Person>> AllAsync(Guid userId = default)
    {
        var query = GetQuery(userId);
        query = query.Include(p => p.User);
        return (await query.ToListAsync()).Select(e => Mapper.Map(e)!);
    }
    
}