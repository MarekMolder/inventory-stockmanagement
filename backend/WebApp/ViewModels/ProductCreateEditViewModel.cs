﻿using App.BLL.DTO;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels;

public class ProductCreateEditViewModel
{
    public Product Product { get; set; } = default!;

    [ValidateNever]
    public SelectList ProductCategorySelectList { get; set; } = default!;
}