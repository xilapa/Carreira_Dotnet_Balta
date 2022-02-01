using System;

namespace DataAccess_Dapper.Models;

public class CareerItem
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public Course Course { get; set; }
}