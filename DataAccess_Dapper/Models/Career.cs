using System;
using System.Collections.Generic;

namespace DataAccess_Dapper.Models;

public class Career
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public List<CareerItem> Items { get; set; } = new List<CareerItem>();
}