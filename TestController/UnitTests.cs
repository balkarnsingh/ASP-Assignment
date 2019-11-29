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


        [Fact]
        public void EditMovie_ValidData()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "EditMovie_ValidData")
                .Options;
            MoviesController controller = new MoviesController(new ApplicationDbContext(options));

            var tmp = new Movie
            {
                Description = "Movie Description",
                Genre = Genre.Action,
                Rating = 5.4,
                Title = "Movie Title",
                Year = 2019
            };

            using (var context = new ApplicationDbContext(options))
            {
                context.Movies.Add(tmp);
                context.SaveChanges();
            }


            var movie = new Movie
            {
                Id = 1,
                Description = "Movie Description",
                Genre = Genre.Action,
                Year = 2019,
                Title = "New",
                Rating = 5.4
            };


            controller.Edit(1, movie);

            using (var context = new ApplicationDbContext(options))
            {
                Assert.Equal("New", context.Movies.FirstOrDefault().Title);
            }
        }

        [Fact]
        public void EditMovie_InvalidData()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "EditMovie_InvalidData")
                .Options;
            MoviesController controller = new MoviesController(new ApplicationDbContext(options));

            var tmp = new Movie
            {
                Description = "Movie Description",
                Genre = Genre.Action,
                Rating = 5.4,
                Title = "Movie Title",
                Year = 2019
            };

            using (var context = new ApplicationDbContext(options))
            {
                context.Movies.Add(tmp);
                context.SaveChanges();
            }


            Movie movie = null;


            controller.Edit(1, movie);


            using (var context = new ApplicationDbContext(options))
            {
                Assert.NotEqual("New", context.Movies.FirstOrDefault().Title);
            }
        }


        [Fact]
        public void GetMovieDetails_InvalidData()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "GetMovieDetails_InvalidData")
                .Options;
            MoviesController controller = new MoviesController(new ApplicationDbContext(options));


            var tmp = new Movie
            {
                Description = "Movie Description",
                Genre = Genre.Action,
                Rating = 5.4,
                Title = "Movie Title",
                Year = 2019
            };

            using (var context = new ApplicationDbContext(options))
            {
                context.Movies.Add(tmp);
                context.SaveChanges();
            }

            ActionResult result = controller.Details(2);

            Assert.IsType<ViewResult>(result);
            if (result.GetType() == typeof(ViewResult))
            {
                var model = ((ViewResult)result).Model;
                Assert.Null(model);
            }
        }


        [Fact]
        public void GetMovieDetails_ValidData()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "GetMovieDetails_ValidData")
                .Options;
            MoviesController controller = new MoviesController(new ApplicationDbContext(options));

            var tmp = new Movie
            {
                Description = "Movie Description",
                Genre = Genre.Action,
                Rating = 5.4,
                Title = "Movie Title",
                Year = 2019
            };

            using (var context = new ApplicationDbContext(options))
            {
                context.Movies.Add(tmp);
                context.SaveChanges();
            }

            ActionResult result = controller.Details(1);

            Assert.IsType<ViewResult>(result);
            if (result.GetType() == typeof(ViewResult))
            {
                var model = ((ViewResult)result).Model;
                Assert.IsType<Movie>(model);
                if (model.GetType() == typeof(Movie))
                {
                    Assert.Equal(1, ((Movie)model).Id);
                }
            }


            using (var context = new ApplicationDbContext(options))
            {
                Assert.NotEqual(0, context.Movies.Count());
            }
        }

        [Fact]
        public void DeleteMovie_InvalidData()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "DeleteMovie_InvalidData")
                .Options;
            MoviesController controller = new MoviesController(new ApplicationDbContext(options));

            var tmp = new Movie
            {
                Description = "Movie Description",
                Genre = Genre.Action,
                Rating = 5.4,
                Title = "Movie Title",
                Year = 2019
            };

            using (var context = new ApplicationDbContext(options))
            {
                context.Movies.Add(tmp);
                context.SaveChanges();
            }


            Movie movie = null;

            controller.Delete(2, movie);

            using (var context = new ApplicationDbContext(options))
            {
                Assert.NotEqual(0, context.Movies.Count());
            }
        }

        [Fact]
        public void DeleteMovie_ValidData()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "DeleteMovie_ValidData")
                .Options;
            MoviesController controller = new MoviesController(new ApplicationDbContext(options));

            var tmp = new Movie
            {
                Description = "Movie Description",
                Genre = Genre.Action,
                Rating = 5.4,
                Title = "Movie Title",
                Year = 2019
            };

            using (var context = new ApplicationDbContext(options))
            {
                context.Movies.Add(tmp);
                context.SaveChanges();
            }


            Movie movie;
            using (var context = new ApplicationDbContext(options))
            {
                movie = context.Movies.FirstOrDefault(x => x.Id == 1);
            }

            controller.Delete(1, movie);

            using (var context = new ApplicationDbContext(options))
            {
                Assert.Equal(0, context.Movies.Count());
            }
        }

    }
}
