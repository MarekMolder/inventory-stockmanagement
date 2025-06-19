﻿using App.DAL.DTO;
using Base.Contracts;

namespace App.DAL.EF.Mappers;

public class PersonUOWMapper : IMapper<App.DAL.DTO.Person, App.Domain.Logic.Person>
{
    private readonly ActionEntityUOWMapper _actionEntityUOWMapper = new();
    private readonly StockAuditUOWMapper _stockAuditUOWMapper = new();

    public Person? Map(Domain.Logic.Person? entity)
    {
        if (entity == null) return null;

        var res = new Person()
        {
            Id = entity.Id,
            PersonName = entity.PersonName,
            Actions = entity.Actions?.Select(t => _actionEntityUOWMapper.Map(t)).ToList()!,
            StockAudits = entity.StockAudits?.Select(t => _stockAuditUOWMapper.Map(t)).ToList()!,
        };
        return res;
    }

    public Domain.Logic.Person? Map(Person? entity)
    {
        if (entity == null) return null;
        var res = new Domain.Logic.Person()
        {
            Id = entity.Id,
            PersonName = entity.PersonName,
            Actions = entity.Actions?.Select(t => _actionEntityUOWMapper.Map(t)).ToList()!,
            StockAudits = entity.StockAudits?.Select(t => _stockAuditUOWMapper.Map(t)).ToList()!,
        };
        return res;
    }
    
    public static Person? MapSimple(Domain.Logic.Person? entity)
    {
        if (entity == null) return null;

        return new Person()
        {
            Id = entity.Id,
            PersonName = entity.PersonName,
        };
    }

    public static Domain.Logic.Person? MapSimple(Person? entity)
    {
        if (entity == null) return null;

        return new Domain.Logic.Person()
        {
            Id = entity.Id,
            PersonName = entity.PersonName,
        };
    }
}
