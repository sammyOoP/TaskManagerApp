using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MovingApp.Data;
using System;
using System.Linq;

using MovingApp.Utils;

namespace MovingApp.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using(var context = new MovingAppContext(
                serviceProvider.GetRequiredService<
                DbContextOptions<MovingAppContext>>()))
            {
                context.Database.EnsureCreated();

                if(context.Task.Any())
                {
                    return; //DB has already been seeded
                }

                context.Task.AddRange(
                    new MovingTask
                    {
                        Title = "Hoover",
                        DueDate = DateTime.Parse("2020-08-25"),
                        Status = (int)StatusTypes.Incomplete
                    },
                    new MovingTask
                    {
                        Title = "Clean walls",
                        DueDate = DateTime.Parse("2020-08-25"),
                        Status = (int)StatusTypes.Incomplete
                    },
                    new MovingTask
                    {
                        Title = "Pack Toys",
                        DueDate = DateTime.Parse("2020-08-25"),
                        Status = (int)StatusTypes.Incomplete
                    }
                );
                context.SaveChanges();
            }
        }
    }
}