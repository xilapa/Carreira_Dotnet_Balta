using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FundamentosEFCore.Models;

[Table("Category")]
public class Category
{
    public Guid Id { get; set; }
    public string Title { get; set; }
}