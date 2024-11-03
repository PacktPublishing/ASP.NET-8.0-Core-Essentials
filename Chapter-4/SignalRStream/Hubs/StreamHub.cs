using System.Threading.Channels;
using Microsoft.AspNetCore.SignalR;

namespace SignalRStreamingApp.Hubs;

public class StreamHub : Hub
{
    public ChannelReader<int> Countdown(int count)
    {
        var channel = Channel.CreateUnbounded<int>();
        _ = WriteItemAsync(channel.Writer, count);
        return channel.Reader;
    }

    private async Task WriteItemAsync(ChannelWriter<int> writer, int count)
    {
        for (int i = count; i >= 0; i--)
        {
            await writer.WriteAsync(i);
            await Task.Delay(1000); // Simulating some dalay
        }
        
        writer.TryComplete();
    }
}