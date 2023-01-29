using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StartLine_social_network.Controllers;
using StartLine_social_network.Data.Interfaces;
using StartLine_social_network.Models;
using StartLine_social_network.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace StartLine_social_network.Tests
{
    public class ClubControllerTests
    {
        private readonly ClubController _controller;
        private readonly Mock<IClubService> _clubServiceMock;
        private readonly Mock<IPhotoService> _photoServiceMock;
        private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;

        public ClubControllerTests()
        {
            _clubServiceMock = new Mock<IClubService>();
            _photoServiceMock = new Mock<IPhotoService>();
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            _controller = new ClubController(_clubServiceMock.Object, _photoServiceMock.Object, _httpContextAccessorMock.Object);
        }

        [Fact]
        public async Task Index_ShouldReturnViewResultWithClubs()
        {
            // Arrange
            var clubs = new List<Club> { new Club { Id = 1 }, new Club { Id = 2 } };
            _clubServiceMock.Setup(c => c.GetAllElements()).ReturnsAsync(clubs);

            // Act
            var result = await _controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Club>>(viewResult.ViewData.Model);
            Assert.Equal(clubs, model);
        }

        [Fact]
        public async Task Detail_ShouldReturnViewResultWithClub()
        {
            // Arrange
            var club = new Club { Id = 1, Title = "Club 1" };
            _clubServiceMock.Setup(c => c.GetByIdAsync(1)).ReturnsAsync(club);

            // Act
            var result = await _controller.Detail(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Club>(viewResult.ViewData.Model);
            Assert.Equal(club, model);
        }
    }
}