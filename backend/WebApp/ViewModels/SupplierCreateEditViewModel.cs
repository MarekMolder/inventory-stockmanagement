﻿using App.BLL.DTO;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels;

public class SupplierCreateEditViewModel
{
    public Supplier Supplier { get; set; } = default!;

    [ValidateNever]
    public SelectList AddressSelectList { get; set; } = default!;
}