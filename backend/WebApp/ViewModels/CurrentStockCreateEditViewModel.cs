﻿using App.BLL.DTO;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels;

public class CurrentStockCreateEditViewModel
{
    public CurrentStock CurrentStock { get; set; } = default!;

    [ValidateNever]
    public SelectList ProductSelectList { get; set; } = default!;
    
    [ValidateNever]
    public SelectList StorageRoomSelectList { get; set; } = default!;
}