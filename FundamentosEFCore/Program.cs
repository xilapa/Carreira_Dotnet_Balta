using System;
using FundamentosEFCore.Data;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello, World!");

var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
optionsBuilder.UseSqlServer("Server=localhost,1433;Database=testDev;User ID=sa;Password=senha_123;TrustServerCertificate=True");
var context = new DataContext(optionsBuilder.Options);
var categories = await context.Categories.AsNoTracking().ToListAsync();

foreach(var category in categories)
    Console.WriteLine(category.Title);

