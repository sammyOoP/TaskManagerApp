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
using MovingAppTests.Utils;

namespace MovingAppTests
{
    public class IndexTests : IClassFixture<MovingAppSeedDataFixture>
    {
        [Theory]
        [InlineData(4, false)]
        [InlineData(0, false)]
        [InlineData(-1, false)]
        [InlineData(1, true)]
        public void TaskExists_UnfoundValuesShouldReturnFalse(int x, bool expected)
        {
            using (var fixture = new MovingAppSeedDataFixture())
            {
                var pageModel = new IndexModel(new NullLogger<IndexModel>(), fixture.MovingAppContext);

                //Evaluate
                bool actual = pageModel.TaskExists(x);

                //Assert
                Assert.Equal(actual, expected);
            }
        }
    }
}