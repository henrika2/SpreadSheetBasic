// <copyright file="Error.razor.cs" company="UofU-CS3500">
// Copyright (c) 2024 UofU-CS3500. All rights reserved.
// </copyright>

namespace GUI.Components.Pages;

using Microsoft.AspNetCore.Components;
using System.Diagnostics;

/// <summary>
///   Generated code from the Blazor startup project.
/// </summary>
public partial class Error
{
    [CascadingParameter]
    private HttpContext? HttpContext { get; set; }

    private string? RequestId { get; set; }

    private bool ShowRequestId => !string.IsNullOrEmpty(this.RequestId);

    /// <summary>
    ///  Generated code form the Blazor startup project.
    /// </summary>
    protected override void OnInitialized() =>
        this.RequestId = Activity.Current?.Id ?? this.HttpContext?.TraceIdentifier;
}
