using System;
using System.Collections.Generic;

namespace DataAccess_Dapper.Models;

public class Blog
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public IList<Tag> Tags { get; set; } = new List<Tag>();
}