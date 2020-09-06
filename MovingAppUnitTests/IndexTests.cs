using System;
using System.Linq;
using Xunit;
using System.Collections.Generic;
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
using MovingAppUnitTests.Utils;

namespace MovingAppUnitTests
{
    public class IndexTests : IClassFixture<MovingAppSeedDataFixture>
    {
        MovingAppSeedDataFixture _fixture;

        public IndexTests(MovingAppSeedDataFixture fixture) 
        {
            _fixture = fixture;
        }

        [Theory]
        [InlineData(4, false)]
        [InlineData(0, false)]
        [InlineData(-1, false)]
        [InlineData(1, true)]
        public void TaskExists_UnfoundValuesShouldReturnFalse(int x, bool expected)
        {
            var pageModel = new IndexModel(new NullLogger<IndexModel>(), _fixture.MovingAppContext);

            //Evaluate
            bool actual = pageModel.TaskExists(x);

            //Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public async void OnGetAsync_ShouldPopulateListOfTasks()
        {
            //Arrange
            var pageModel = new IndexModel(new NullLogger<IndexModel>(), _fixture.MovingAppContext);
            List<MovingTask> expectedMovingTasks = _fixture.MovingAppContext.Task.ToList<MovingTask>();

            //Act
            await pageModel.OnGetAsync(null, null);

            //Assert
            var actualMovingTasks = Assert.IsAssignableFrom<List<MovingTask>>(pageModel.Tasks);
            Assert.Equal(
                expectedMovingTasks.OrderBy(t => t.ID).Select(t => t.Title),
                actualMovingTasks.OrderBy(t => t.ID).Select(t => t.Title)
            );
        }
    }
}