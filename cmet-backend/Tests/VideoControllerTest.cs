using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using cmet_backend.Controllers;
using cmet_backend.Video;
using cmet_backend.Common;
using Microsoft.Extensions.Logging.Abstractions;

public class VideoControllerTests
{
    private readonly Mock<IVideoRepository> _mockRepo;
    private readonly Mock<ILogger<VideoController>> _mockLogger;
    private readonly VideoController _controller;

    public VideoControllerTests()
    {
        _mockRepo = new Mock<IVideoRepository>();
        _mockLogger = new Mock<ILogger<VideoController>>();
        _controller = new VideoController(_mockRepo.Object, _mockLogger.Object);
    }

    [Fact]
    public async Task Get_ReturnsVideo_WhenExists()
    {
        // Arrange
        var expectedVideo = new VideoMaterialEntity
        {
            Id = "video123",
            Description = "Test",
            Link = "http://example.com",
            CreatedAt = DateTime.Parse("2025-05-30T01:12:46.7929923Z")
        };

        _mockRepo.Setup(r => r.GetByIdAsync("video123")).ReturnsAsync(expectedVideo);

        // Act
        var actionResult = await _controller.Get("video123") as ObjectResult;
        Assert.NotNull(actionResult);

        // Приводим к ApiResponse
        var apiResponse = actionResult.Value as ApiResponse;
        Assert.NotNull(apiResponse);
        Assert.True(apiResponse.Success);
        Assert.Null(apiResponse.Error);

        // Извлекаем VideoMaterialEntity из apiResponse.Data
        var actualVideo = Assert.IsType<VideoMaterialEntity>(apiResponse.Data);

        // Assert: сравнение по полям
        Assert.Equal(expectedVideo.Id, actualVideo.Id);
        Assert.Equal(expectedVideo.Description, actualVideo.Description);
        Assert.Equal(expectedVideo.Link, actualVideo.Link);
        Assert.Equal(expectedVideo.CreatedAt, actualVideo.CreatedAt);
    }

    [Fact]
    public async Task Get_ReturnsNotFound_WhenMissing()
    {
        // Arrange
        var id = "missingId";
        _mockRepo.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((VideoMaterialEntity)null);

        // Act
        var result = await _controller.Get(id) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(404, result.StatusCode);
    }

    [Fact]
    public async Task GetAll_ReturnsAllVideos()
    {
        // Arrange
        var expected = new List<VideoMaterialEntity>
    {
        new VideoMaterialEntity { Id = "1", Description = "Vid1", CreatedAt = DateTime.UtcNow, Link = "link1" },
        new VideoMaterialEntity { Id = "2", Description = "Vid2", CreatedAt = DateTime.UtcNow, Link = "link2" }
    };

        var mockRepo = new Mock<IVideoRepository>();
        mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(expected);

        var controller = new VideoController(mockRepo.Object, NullLogger<VideoController>.Instance);

        // Act
        var result = await controller.GetAll();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var apiResponse = Assert.IsType<ApiResponse>(okResult.Value);

        var actual = Assert.IsAssignableFrom<IEnumerable<VideoMaterialEntity>>(apiResponse.Data);

        Assert.Equal(expected.Count, actual.Count());

        foreach (var expectedItem in expected)
        {
            Assert.Contains(actual, a => a.Id == expectedItem.Id &&
                                         a.Description == expectedItem.Description &&
                                         a.Link == expectedItem.Link);
        }
    }

    [Fact]
    public async Task Add_AddsNewVideo()
    {
        // Arrange
        var data = new VideoMaterialData
        {
            Description = "New Vid",
            Link = "link",
            CreatedAt = DateTime.UtcNow
        };

        _mockRepo.Setup(r => r.AddAsync(It.IsAny<VideoMaterialEntity>())).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.Add(data) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
    }

    [Fact]
    public async Task Patch_UpdatesVideo_WhenExists()
    {
        // Arrange
        var id = "123";
        var data = new VideoMaterialData { Description = "Updated", Link = "updatedLink", CreatedAt = DateTime.UtcNow };

        _mockRepo.Setup(r => r.ExistsById(id)).ReturnsAsync(true);
        _mockRepo.Setup(r => r.UpdateAsync(It.IsAny<VideoMaterialEntity>())).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.Patch(id, data) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
    }

    [Fact]
    public async Task Patch_ReturnsNotFound_WhenMissing()
    {
        // Arrange
        var id = "missing";
        var data = new VideoMaterialData { Description = "Updated", Link = "link", CreatedAt = DateTime.UtcNow };

        _mockRepo.Setup(r => r.ExistsById(id)).ReturnsAsync(false);

        // Act
        var result = await _controller.Patch(id, data) as ObjectResult;

        // Assert
        Assert.Equal(404, result.StatusCode);
    }

    [Fact]
    public async Task Delete_Deletes_WhenExists()
    {
        // Arrange
        var id = "toDelete";
        _mockRepo.Setup(r => r.ExistsById(id)).ReturnsAsync(true);
        _mockRepo.Setup(r => r.DeleteAsync(id)).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.Delete(id) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
    }

    [Fact]
    public async Task Delete_ReturnsNotFound_WhenMissing()
    {
        // Arrange
        var id = "notExists";
        _mockRepo.Setup(r => r.ExistsById(id)).ReturnsAsync(false);

        // Act
        var result = await _controller.Delete(id) as ObjectResult;

        // Assert
        Assert.Equal(404, result.StatusCode);
    }
}
