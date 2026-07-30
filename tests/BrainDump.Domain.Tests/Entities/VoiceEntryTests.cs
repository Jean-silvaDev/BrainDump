using BrainDump.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace BrainDump.Domain.Tests.Entities;

public class VoiceEntryTests
{
    [Fact]
    public void Create_WithValidParameters_ShouldInstantiateVoiceEntryWithPendingStatus()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var audioFilePath = "uploads/voice/audio123.m4a";
        var audioFormat = "m4a";
        var duration = 45;
        var byteSize = 102400L;

        // Act
        var entry = VoiceEntry.Create(userId, audioFilePath, audioFormat, duration, byteSize);

        // Assert
        entry.Should().NotBeNull();
        entry.Id.Should().NotBeEmpty();
        entry.UserId.Should().Be(userId);
        entry.AudioFilePath.Should().Be(audioFilePath);
        entry.AudioFormat.Should().Be("m4a");
        entry.DurationSeconds.Should().Be(duration);
        entry.ByteSize.Should().Be(byteSize);
        entry.Status.Should().Be(VoiceEntryStatus.PendingTranscription);
        entry.TranscribedText.Should().BeNull();
    }

    [Fact]
    public void Create_WithEmptyUserId_ShouldThrowArgumentException()
    {
        // Act
        Action act = () => VoiceEntry.Create(Guid.Empty, "path", "mp3", 10, 100);

        // Assert
        act.Should().Throw<ArgumentException>()
           .WithMessage("*ID do usuário*");
    }

    [Fact]
    public void MarkAsCompleted_WhenPending_ShouldUpdateStatusAndText()
    {
        // Arrange
        var entry = VoiceEntry.Create(Guid.NewGuid(), "path", "mp3", 10, 100);

        // Act
        entry.MarkAsCompleted("Comprar leite no mercado amanhã");

        // Assert
        entry.Status.Should().Be(VoiceEntryStatus.Completed);
        entry.TranscribedText.Should().Be("Comprar leite no mercado amanhã");
    }
}
