using System;
using MudBlazor;

namespace ToDo.Application.Models;

public class ToDo
{
    public string Name { get; set; } = string.Empty;

    public string Icon
    {
        get
        {
            if (IsComplete) return Icons.TwoTone.CheckBox;

            return Icons.Material.Filled.CheckBoxOutlineBlank;
        }
    }

    public bool IsComplete { get; set; }

    public void SetComplete()
    {
        IsComplete = !IsComplete;
    }
}
