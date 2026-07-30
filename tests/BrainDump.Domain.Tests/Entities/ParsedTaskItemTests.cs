using BrainDump.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace BrainDump.Domain.Tests.Entities;

public class ParsedTaskItemTests
{
    [Fact]
    public void Create_WithValidParameters_ShouldInstantiatePendingReviewItem()
    {
        // Arrange
        var voiceEntryId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var title = "Comprar café no mercado";
        var dueDate = DateTime.UtcNow.AddDays(1);

        // Act
        var item = ParsedTaskItem.Create(
            voiceEntryId,
            userId,
            title,
            Category.Shopping,
            Priority.High,
            dueDate,
            0.95f);

        // Assert
        item.Should().NotBeNull();
        item.Id.Should().NotBeEmpty();
        item.VoiceEntryId.Should().Be(voiceEntryId);
        item.UserId.Should().Be(userId);
        item.Title.Should().Be(title);
        item.Category.Should().Be(Category.Shopping);
        item.Priority.Should().Be(Priority.High);
        item.DueDate.Should().Be(dueDate);
        item.ConfidenceScore.Should().Be(0.95f);
        item.Status.Should().Be(ParsedTaskStatus.PendingReview);
    }

    [Fact]
    public void Approve_ShouldChangeStatusToApproved()
    {
        // Arrange
        var item = ParsedTaskItem.Create(Guid.NewGuid(), Guid.NewGuid(), "Estudar C#", Category.Study, Priority.Medium, null);

        // Act
        item.Approve();

        // Assert
        item.Status.Should().Be(ParsedTaskStatus.Approved);
    }

    [Fact]
    public void Discard_ShouldChangeStatusToDiscarded()
    {
        // Arrange
        var item = ParsedTaskItem.Create(Guid.NewGuid(), Guid.NewGuid(), "Lixo", Category.Other, Priority.Low, null);

        // Act
        item.Discard();

        // Assert
        item.Status.Should().Be(ParsedTaskStatus.Discarded);
    }
}
