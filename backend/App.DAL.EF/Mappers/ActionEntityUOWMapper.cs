﻿using App.DAL.DTO;
using App.Resources.Domain;
using Base.Contracts;
using Base.DAL.Contracts;
using Product = App.DAL.DTO.Product;
using Reason = App.DAL.DTO.Reason;
using Supplier = App.DAL.DTO.Supplier;

namespace App.DAL.EF.Mappers;

public class ActionEntityUOWMapper : IMapper<App.DAL.DTO.ActionEntity, App.Domain.Logic.ActionEntity>
{
    public ActionEntity? Map(Domain.Logic.ActionEntity? entity)
    {
        if (entity == null) return null;

        var res = new ActionEntity()
        {
            Id = entity.Id,
            Quantity = entity.Quantity,
            Status = entity.Status,
            
            ActionTypeId = entity.ActionTypeId,
            ActionType = ActionTypeEntityUOWMapper.MapSimple(entity.ActionType),
            
            ReasonId = entity.ReasonId,
            Reason = ReasonUOWMapper.MapSimple(entity.Reason),
            
            SupplierId = entity.SupplierId,
            Supplier = SupplierUOWMapper.MapSimple(entity.Supplier),
            
            ProductId = entity.ProductId,
            Product = ProductUOWMapper.MapSimple(entity.Product),
            
            StockAuditId = entity.StockAuditId,
            StockAudit = StockAuditUOWMapper.MapSimple(entity.StockAudit),
            
            StorageRoomId = entity.StorageRoomId,
            StorageRoom = StorageRoomUOWMapper.MapSimple(entity.StorageRoom),
            
        };
        return res;
    }

    public Domain.Logic.ActionEntity? Map(ActionEntity? entity)
    {
        if (entity == null) return null;

        var res = new Domain.Logic.ActionEntity()
        {
            Id = entity.Id,
            Quantity = entity.Quantity,
            Status = entity.Status,
            
            ActionTypeId = entity.ActionTypeId,
            ActionType = ActionTypeEntityUOWMapper.MapSimple(entity.ActionType),
            
            ReasonId = entity.ReasonId,
            Reason = ReasonUOWMapper.MapSimple(entity.Reason),
            
            SupplierId = entity.SupplierId,
            Supplier = SupplierUOWMapper.MapSimple(entity.Supplier),
            
            ProductId = entity.ProductId,
            Product = ProductUOWMapper.MapSimple(entity.Product),
            
            StockAuditId = entity.StockAuditId,
            StockAudit = StockAuditUOWMapper.MapSimple(entity.StockAudit),
            
            StorageRoomId = entity.StorageRoomId,
            StorageRoom = StorageRoomUOWMapper.MapSimple(entity.StorageRoom),
        };
        return res;
    }
    
    public static ActionEntity? MapSimple(Domain.Logic.ActionEntity? entity)
    {
        if (entity == null) return null;

        return new ActionEntity()
        {
            Id = entity.Id,
            Quantity = entity.Quantity,
            Status = entity.Status,
            ActionTypeId = entity.ActionTypeId,
            ReasonId = entity.ReasonId,
            SupplierId = entity.SupplierId,
            ProductId = entity.ProductId,
            StockAuditId = entity.StockAuditId,
            StorageRoomId = entity.StorageRoomId,
        };
    }
    
    public static Domain.Logic.ActionEntity? MapSimple(ActionEntity? entity)
    {
        if (entity == null) return null;

        return new Domain.Logic.ActionEntity()
        {
            Id = entity.Id,
            Quantity = entity.Quantity,
            Status = entity.Status,
            ActionTypeId = entity.ActionTypeId,
            ReasonId = entity.ReasonId,
            SupplierId = entity.SupplierId,
            ProductId = entity.ProductId,
            StockAuditId = entity.StockAuditId,
            StorageRoomId = entity.StorageRoomId,
        };
    }
}
