using WebAPIService.Shared.Model;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace WebAPIService
{
    public interface IJobSort
    {
        public Task<bool> AddItemAsync(JobItem jobItem, CancellationToken ct = default);

        public IAsyncEnumerable<JobItem> ReadAllAsync(CancellationToken ct = default);

        public IEnumerable<JobItem> GetAllJobsList();
        public JobItem GetJobById(string id);

        
    }
}
