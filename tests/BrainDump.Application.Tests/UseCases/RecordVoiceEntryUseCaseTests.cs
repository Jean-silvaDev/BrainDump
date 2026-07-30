using System.Text;
using BrainDump.Application.Common.Interfaces;
using BrainDump.Application.UseCases.VoiceEntries.RecordVoiceEntry;
using BrainDump.Domain.Entities;
using BrainDump.Domain.Repositories;
using FluentAssertions;
using Moq;
using Xunit;

namespace BrainDump.Application.Tests.UseCases;

public class RecordVoiceEntryUseCaseTests
{
    private readonly Mock<IAudioStorageService> _audioStorageServiceMock;
    private readonly Mock<IVoiceEntryRepository> _voiceEntryRepositoryMock;
    private readonly RecordVoiceEntryUseCase _useCase;

    public RecordVoiceEntryUseCaseTests()
    {
        _audioStorageServiceMock = new Mock<IAudioStorageService>();
        _voiceEntryRepositoryMock = new Mock<IVoiceEntryRepository>();
        _useCase = new RecordVoiceEntryUseCase(
            _audioStorageServiceMock.Object,
            _voiceEntryRepositoryMock.Object);
    }

    [Fact]
    public async Task ExecuteAsync_WithValidAudio_ShouldSaveAudioAndReturnResponse()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var audioBytes = Encoding.UTF8.GetBytes("fake audio data stream");
        using var stream = new MemoryStream(audioBytes);

        var command = new RecordVoiceEntryCommand(
            userId,
            stream,
            "sample.m4a",
            "audio/m4a",
            15,
            audioBytes.Length);

        _audioStorageServiceMock
            .Setup(s => s.SaveAudioAsync(It.IsAny<Stream>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync("uploads/voice/saved_sample.m4a");

        // Act
        var response = await _useCase.ExecuteAsync(command);

        // Assert
        response.Should().NotBeNull();
        response.UserId.Should().Be(userId);
        response.AudioFilePath.Should().Be("uploads/voice/saved_sample.m4a");
        response.Status.Should().Be(VoiceEntryStatus.PendingTranscription);

        _audioStorageServiceMock.Verify(s => s.SaveAudioAsync(It.IsAny<Stream>(), It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
        _voiceEntryRepositoryMock.Verify(r => r.AddAsync(It.IsAny<VoiceEntry>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task ExecuteAsync_WithExceededFileSize_ShouldThrowInvalidOperationException()
    {
        // Arrange
        var userId = Guid.NewGuid();
        using var stream = new MemoryStream(new byte[100]);
        var command = new RecordVoiceEntryCommand(
            userId,
            stream,
            "large.m4a",
            "audio/m4a",
            10,
            11 * 1024 * 1024); // 11 MB > 10 MB limit

        // Act
        Func<Task> act = async () => await _useCase.ExecuteAsync(command);

        // Assert
        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("*10 MB*");
    }
}
