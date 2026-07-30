using BrainDump.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace BrainDump.Domain.Tests.Entities;

public class TaskItemTests
{
    [Fact]
    public void Create_WithValidParameters_ShouldInstantiateTaskItem()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var title = "Relatório mensal de vendas";
        var dueDate = DateTime.UtcNow.AddDays(3);

        // Act
        var task = TaskItem.Create(userId, title, Category.Work, Priority.High, dueDate);

        // Assert
        task.Should().NotBeNull();
        task.Id.Should().NotBeEmpty();
        task.UserId.Should().Be(userId);
        task.Title.Should().Be(title);
        task.Category.Should().Be(Category.Work);
        task.Priority.Should().Be(Priority.High);
        task.DueDate.Should().Be(dueDate);
        task.IsCompleted.Should().BeFalse();
        task.CompletedAt.Should().BeNull();
    }

    [Fact]
    public void MarkAsCompleted_ShouldSetIsCompletedTrueAndSetCompletedAt()
    {
        // Arrange
        var task = TaskItem.Create(Guid.NewGuid(), "Fazer caminhada", Category.Health, Priority.Medium, null);

        // Act
        task.MarkAsCompleted();

        // Assert
        task.IsCompleted.Should().BeTrue();
        task.CompletedAt.Should().NotBeNull();
    }

    [Fact]
    public void Reopen_ShouldSetIsCompletedFalseAndClearCompletedAt()
    {
        // Arrange
        var task = TaskItem.Create(Guid.NewGuid(), "Fazer caminhada", Category.Health, Priority.Medium, null);
        task.MarkAsCompleted();

        // Act
        task.Reopen();

        // Assert
        task.IsCompleted.Should().BeFalse();
        task.CompletedAt.Should().BeNull();
    }
}
