using BrainDump.Application.Common.Interfaces;
using BrainDump.Application.DTOs.AI;
using BrainDump.Application.UseCases.VoiceEntries.ProcessVoiceEntryTranscription;
using BrainDump.Domain.Entities;
using BrainDump.Domain.Repositories;
using FluentAssertions;
using Moq;
using Xunit;

namespace BrainDump.Application.Tests.UseCases;

public class ProcessVoiceEntryTranscriptionUseCaseTests
{
    private readonly Mock<IVoiceEntryRepository> _voiceEntryRepositoryMock;
    private readonly Mock<IParsedTaskItemRepository> _parsedTaskItemRepositoryMock;
    private readonly Mock<IAudioStorageService> _audioStorageServiceMock;
    private readonly Mock<ITranscriptionService> _transcriptionServiceMock;
    private readonly Mock<IItemClassifierService> _itemClassifierServiceMock;
    private readonly ProcessVoiceEntryTranscriptionUseCase _useCase;

    public ProcessVoiceEntryTranscriptionUseCaseTests()
    {
        _voiceEntryRepositoryMock = new Mock<IVoiceEntryRepository>();
        _parsedTaskItemRepositoryMock = new Mock<IParsedTaskItemRepository>();
        _audioStorageServiceMock = new Mock<IAudioStorageService>();
        _transcriptionServiceMock = new Mock<ITranscriptionService>();
        _itemClassifierServiceMock = new Mock<IItemClassifierService>();

        _useCase = new ProcessVoiceEntryTranscriptionUseCase(
            _voiceEntryRepositoryMock.Object,
            _parsedTaskItemRepositoryMock.Object,
            _audioStorageServiceMock.Object,
            _transcriptionServiceMock.Object,
            _itemClassifierServiceMock.Object);
    }

    [Fact]
    public async Task ExecuteAsync_WithValidVoiceEntry_ShouldTranscribeAndCreateParsedItems()
    {
        // Arrange
        var voiceEntry = VoiceEntry.Create(Guid.NewGuid(), "uploads/test.m4a", "m4a", 15, 1024);
        _voiceEntryRepositoryMock
            .Setup(r => r.GetByIdAsync(voiceEntry.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(voiceEntry);

        _transcriptionServiceMock
            .Setup(t => t.TranscribeAudioAsync(It.IsAny<Stream>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new TranscriptionResult("Comprar pão amanhã", 0.95f, "pt-BR"));

        _itemClassifierServiceMock
            .Setup(c => c.ClassifyAndExtractItemsAsync(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new[]
            {
                new ParsedTaskDraft("Comprar pão", Category.Shopping, Priority.Low, DateTime.UtcNow.AddDays(1), 0.9f)
            });

        // Act
        await _useCase.ExecuteAsync(voiceEntry.Id);

        // Assert
        voiceEntry.Status.Should().Be(VoiceEntryStatus.Completed);
        voiceEntry.TranscribedText.Should().Be("Comprar pão amanhã");

        _parsedTaskItemRepositoryMock.Verify(r => r.AddRangeAsync(
            It.Is<IEnumerable<ParsedTaskItem>>(items => items.Count() == 1 && items.First().Title == "Comprar pão"),
            It.IsAny<CancellationToken>()), Times.Once);
    }
}
