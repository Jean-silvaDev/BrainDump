using BrainDump.Application.UseCases.Tasks.ToggleTaskCompletion;
using BrainDump.Domain.Entities;
using BrainDump.Domain.Repositories;
using FluentAssertions;
using Moq;
using Xunit;

namespace BrainDump.Application.Tests.UseCases;

public class ToggleTaskCompletionUseCaseTests
{
    private readonly Mock<ITaskItemRepository> _taskItemRepositoryMock;
    private readonly ToggleTaskCompletionUseCase _useCase;

    public ToggleTaskCompletionUseCaseTests()
    {
        _taskItemRepositoryMock = new Mock<ITaskItemRepository>();
        _useCase = new ToggleTaskCompletionUseCase(_taskItemRepositoryMock.Object);
    }

    [Fact]
    public async Task ExecuteAsync_WhenTaskIsOpen_ShouldMarkAsCompleted()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var task = TaskItem.Create(userId, "Caminhada matinal", Category.Health, Priority.Medium, null);

        _taskItemRepositoryMock
            .Setup(r => r.GetByIdAsync(task.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(task);

        // Act
        var response = await _useCase.ExecuteAsync(task.Id, userId);

        // Assert
        response.IsCompleted.Should().BeTrue();
        task.IsCompleted.Should().BeTrue();
        _taskItemRepositoryMock.Verify(r => r.UpdateAsync(task, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task ExecuteAsync_WhenTaskIsCompleted_ShouldReopenTask()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var task = TaskItem.Create(userId, "Caminhada matinal", Category.Health, Priority.Medium, null);
        task.MarkAsCompleted();

        _taskItemRepositoryMock
            .Setup(r => r.GetByIdAsync(task.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(task);

        // Act
        var response = await _useCase.ExecuteAsync(task.Id, userId);

        // Assert
        response.IsCompleted.Should().BeFalse();
        task.IsCompleted.Should().BeFalse();
    }
}
