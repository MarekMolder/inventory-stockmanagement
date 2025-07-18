﻿using System.ComponentModel.DataAnnotations;
using Base.Contracts;

namespace App.DTO.v1;

public class StorageRoomInInventoryCreate
{
    public DateTime? EndedAt { get; set; }
    
    public Guid InventoryId { get; set; }

    public Guid StorageRoomId { get; set; }
}