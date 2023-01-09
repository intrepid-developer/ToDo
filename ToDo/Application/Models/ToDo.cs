using System;
using MudBlazor;

namespace ToDoApp.Application.Models;

public class ToDo
{
    public Guid Id { get; set; } = Guid.NewGuid();
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
}
