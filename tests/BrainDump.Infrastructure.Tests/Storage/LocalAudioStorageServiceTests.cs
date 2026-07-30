using System.Text;
using BrainDump.Infrastructure.Storage;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace BrainDump.Infrastructure.Tests.Storage;

public class LocalAudioStorageServiceTests
{
    private readonly string _tempDirectory;
    private readonly IConfiguration _configuration;
    private readonly LocalAudioStorageService _storageService;

    public LocalAudioStorageServiceTests()
    {
        _tempDirectory = Path.Combine(Path.GetTempPath(), "BrainDumpTests_" + Guid.NewGuid());
        var inMemorySettings = new Dictionary<string, string?>
        {
            {"AudioStorage:Path", _tempDirectory}
        };

        _configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        _storageService = new LocalAudioStorageService(_configuration);
    }

    [Fact]
    public async Task SaveAudioAsync_ShouldWriteFileToDiskAndReturnRelativePath()
    {
        // Arrange
        var content = "test audio stream content";
        using var stream = new MemoryStream(Encoding.UTF8.GetBytes(content));
        var fileName = "test_recording.m4a";

        // Act
        var relativePath = await _storageService.SaveAudioAsync(stream, fileName);

        // Assert
        relativePath.Should().Contain("test_recording.m4a");
        var fullPath = Path.Combine(_tempDirectory, fileName);
        File.Exists(fullPath).Should().BeTrue();

        var savedContent = await File.ReadAllTextAsync(fullPath);
        savedContent.Should().Be(content);

        // Cleanup
        if (Directory.Exists(_tempDirectory))
            Directory.Delete(_tempDirectory, recursive: true);
    }
}
