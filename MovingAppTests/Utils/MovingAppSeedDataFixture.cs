using System;
using System.Linq;
using Xunit;
using Moq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.Extensions.Logging.Abstractions;
using MovingApp.Pages;
using MovingApp.Data;
using MovingApp.Models;

namespace MovingAppTests.Utils
{
    public class MovingAppSeedDataFixture : IDisposable
    {
        public MovingAppContext MovingAppContext { get; private set; }

        public MovingAppSeedDataFixture()
        {
            //Arrange
            var optionsBuilder = new DbContextOptionsBuilder<MovingAppContext>()
                .UseInMemoryDatabase("MovingAppTestDb")
                .Options;

            MovingAppContext = new MovingAppContext(optionsBuilder);

            #region snippet1
            MovingAppContext.Task.AddRange(
                new MovingTask
                {
                    ID = 1,
                    Title = "Hoover",
                    DueDate = DateTime.Parse("2020-08-25"),
                    Status = (int)StatusTypes.Incomplete
                },
                new MovingTask
                {
                    ID = 2,
                    Title = "Clean walls",
                    DueDate = DateTime.Parse("2020-08-25"),
                    Status = (int)StatusTypes.Incomplete
                },
                new MovingTask
                {
                    ID = 3,
                    Title = "Pack Toys",
                    DueDate = DateTime.Parse("2020-08-25"),
                    Status = (int)StatusTypes.Incomplete
                }
            );
            MovingAppContext.SaveChanges();
            #endregion
        }

        public void Dispose()
        {
            MovingAppContext.Database.EnsureDeleted();
            MovingAppContext.Dispose();
        }           
    }
}
