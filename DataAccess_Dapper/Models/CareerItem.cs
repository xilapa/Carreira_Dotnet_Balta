using System;

namespace DataAccess_Dapper.Models;

public class CareerItem
{
    public string Title { get; set; }
    public Course Course { get; set; }
    public Guid CareerId { get; set; }
}