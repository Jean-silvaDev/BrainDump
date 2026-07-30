using System.Threading.Channels;

namespace BrainDump.Infrastructure.BackgroundServices;

/// <summary>
/// Fila assíncrona em memória baseada em Channel para processamento de gravações de voz.
/// </summary>
public class VoiceProcessingQueue
{
    private readonly Channel<Guid> _queue;

    public VoiceProcessingQueue()
    {
        var options = new UnboundedChannelOptions
        {
            SingleReader = true
        };
        _queue = Channel.CreateUnbounded<Guid>(options);
    }

    public async ValueTask EnqueueAsync(Guid voiceEntryId, CancellationToken cancellationToken = default)
    {
        await _queue.Writer.WriteAsync(voiceEntryId, cancellationToken);
    }

    public async ValueTask<Guid> DequeueAsync(CancellationToken cancellationToken = default)
    {
        return await _queue.Reader.ReadAsync(cancellationToken);
    }
}
