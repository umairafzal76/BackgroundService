using WebAPIService.Shared.Model;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace WebAPIService
{
    public class JobSort : IJobSort
    {
        private const int MaxMessagesInChannel = 100;

        private readonly Channel<JobItem> _channel;
        private readonly ILogger _logger;
        private readonly IAppDataStorage _appDataStorage;

        public JobSort(ILogger logger, IAppDataStorage appDataStorage)
        {
            _logger = logger;
            _appDataStorage = appDataStorage;

            var options = new BoundedChannelOptions(MaxMessagesInChannel)
            {
                SingleWriter = false,
                SingleReader = true
            };
            _channel = Channel.CreateBounded<JobItem>(options);
        }

        public async Task<bool> AddItemAsync(JobItem jobItem, CancellationToken ct = default)
        {
            while (await _channel.Writer.WaitToWriteAsync(ct) && !ct.IsCancellationRequested)
            {
                ChannelMessageWriter(LogEventMap.SortService_ListReceived, jobItem.Id, "Writing to channel.");

                if (_channel.Writer.TryWrite(jobItem))
                {
                    ChannelMessageWriter(LogEventMap.SortService_ListReceivedSent, jobItem.Id, "Written to channel successfully.");
                    return true;
                }
            }
            return false;
        }

        public IEnumerable<JobItem> GetAllJobsList()
        {
            return _appDataStorage.GetAllItems()?.Select(a => new JobItem(a));
        }

        public JobItem GetJobById(string id)
        {
            return _appDataStorage.GetItem(id);
        }


        

        public IAsyncEnumerable<JobItem> ReadAllAsync(CancellationToken ct = default) =>
            _channel.Reader.ReadAllAsync(ct);

        private void ChannelMessageWriter(LogEventMap events, string id, string message)
        {
            _logger.Information($"{events} JobItemId {id} {message}");
        }


    }

}