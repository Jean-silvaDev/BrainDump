using BrainDump.Application.DTOs.Tasks;
using BrainDump.Application.UseCases.Tasks.GetTasks;
using BrainDump.Domain.Entities;
using BrainDump.Domain.Repositories;
using FluentAssertions;
using Moq;
using Xunit;

namespace BrainDump.Application.Tests.UseCases;

public class GetTasksUseCaseTests
{
    private readonly Mock<ITaskItemRepository> _taskItemRepositoryMock;
    private readonly GetTasksUseCase _useCase;

    public GetTasksUseCaseTests()
    {
        _taskItemRepositoryMock = new Mock<ITaskItemRepository>();
        _useCase = new GetTasksUseCase(_taskItemRepositoryMock.Object);
    }

    [Fact]
    public async Task ExecuteAsync_WithValidUserId_ShouldReturnMappedTaskResponses()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var task1 = TaskItem.Create(userId, "Estudar C#", Category.Study, Priority.High, null);
        var task2 = TaskItem.Create(userId, "Comprar café", Category.Shopping, Priority.Low, null);

        _taskItemRepositoryMock
            .Setup(r => r.GetFilteredAsync(userId, It.IsAny<Category?>(), It.IsAny<Priority?>(), It.IsAny<bool?>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new[] { task1, task2 });

        var query = new GetTasksQuery();

        // Act
        var result = await _useCase.ExecuteAsync(userId, query);

        // Assert
        result.Should().HaveCount(2);
        result.Select(t => t.Title).Should().Contain(new[] { "Estudar C#", "Comprar café" });
    }
}
