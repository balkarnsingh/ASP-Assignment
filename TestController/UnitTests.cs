using ASP_Assignment.Controllers;
using ASP_Assignment.Data;
using Assignment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace TestController
{
    public class UnitTests
    {

        [Fact]
        public void Index()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Index")
                .Options;
            MoviesController controller = new MoviesController(new ApplicationDbContext(options));

            using (var context = new ApplicationDbContext(options))
            {
                context.Movies.Add(new Movie { Description = "Movie Description 1", Genre = Genre.Action, Rating = 5.4, Title = "Movie Title 1", Year = 2019 });
                context.Movies.Add(new Movie { Description = "Movie Description 2", Genre = Genre.Action, Rating = 5.4, Title = "Movie Title 2", Year = 2019 });
                context.Movies.Add(new Movie { Description = "Movie Description 3", Genre = Genre.Action, Rating = 5.4, Title = "Movie Title 3", Year = 2019 });
                context.SaveChanges();
            }

            ActionResult result = controller.Index();

            Assert.IsType<ViewResult>(result);
            if (result.GetType() == typeof(ViewResult))
            {
                var model = ((ViewResult)result).Model;
                Assert.IsType<List<Movie>>(model);
                if (model.GetType() == typeof(List<Movie>))
                {
                    Assert.Equal(3, ((List<Movie>)model).Count);
                }
            }
        }


        [Fact]
        public void AddMovie_ValidData()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "AddMovie_ValidData")
                .Options;
            MoviesController controller = new MoviesController(new ApplicationDbContext(options));

            var movie = new Movie
            {
                Description = "Movie Description",
                Genre = Genre.Action,
                Rating = 5.4,
                Title = "Movie Title",
                Year = 2019
            };

            ActionResult result = controller.Create(movie);


            using (var context = new ApplicationDbContext(options))
            {
                Assert.Equal(1, context.Movies.Count());
                Assert.Equal("Movie Title", context.Movies.Single().Title);
                Assert.IsType<RedirectToActionResult>(result);
            }
        }


        [Fact]
        public void AddMovie_InvalidData()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "AddMovie_InvalidData")
                .Options;
            MoviesController controller = new MoviesController(new ApplicationDbContext(options));

            Movie movie = null;

            ActionResult result = controller.Create(movie);

            using (var context = new ApplicationDbContext(options))
            {
                Assert.Equal(0, context.Movies.Count());
            }
        }

    }
}
