using BrainDump.Application.DTOs.Review;
using BrainDump.Application.UseCases.Review.ConfirmParsedTasks;
using BrainDump.Domain.Entities;
using BrainDump.Domain.Repositories;
using FluentAssertions;
using Moq;
using Xunit;

namespace BrainDump.Application.Tests.UseCases;

public class ConfirmParsedTasksUseCaseTests
{
    private readonly Mock<IParsedTaskItemRepository> _parsedTaskItemRepositoryMock;
    private readonly Mock<ITaskItemRepository> _taskItemRepositoryMock;
    private readonly ConfirmParsedTasksUseCase _useCase;

    public ConfirmParsedTasksUseCaseTests()
    {
        _parsedTaskItemRepositoryMock = new Mock<IParsedTaskItemRepository>();
        _taskItemRepositoryMock = new Mock<ITaskItemRepository>();
        _useCase = new ConfirmParsedTasksUseCase(
            _parsedTaskItemRepositoryMock.Object,
            _taskItemRepositoryMock.Object);
    }

    [Fact]
    public async Task ExecuteAsync_WithPendingItems_ShouldApproveDraftsAndCreateOfficialTasks()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var voiceEntryId = Guid.NewGuid();

        var pendingParsed1 = ParsedTaskItem.Create(voiceEntryId, userId, "Comprar pão", Category.Shopping, Priority.Low, null);
        var pendingParsed2 = ParsedTaskItem.Create(voiceEntryId, userId, "Pagar conta de luz", Category.Finance, Priority.Urgent, DateTime.UtcNow.AddDays(1));

        _parsedTaskItemRepositoryMock
            .Setup(r => r.GetPendingByUserIdAsync(userId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new[] { pendingParsed1, pendingParsed2 });

        var request = new ConfirmTasksRequest(null); // Confirma todos

        // Act
        var result = await _useCase.ExecuteAsync(userId, request);

        // Assert
        result.Should().HaveCount(2);
        pendingParsed1.Status.Should().Be(ParsedTaskStatus.Approved);
        pendingParsed2.Status.Should().Be(ParsedTaskStatus.Approved);

        _taskItemRepositoryMock.Verify(r => r.AddRangeAsync(
            It.Is<IEnumerable<TaskItem>>(tasks => tasks.Count() == 2),
            It.IsAny<CancellationToken>()), Times.Once);
    }
}
