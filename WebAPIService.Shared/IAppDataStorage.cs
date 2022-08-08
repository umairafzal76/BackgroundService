
using System.Collections.Generic;
using WebAPIService.Shared.Model;

namespace WebAPIService
{
    public interface IAppDataStorage
    {
        public void Add(JobItem jobItem);
        public JobItem GetItem(string id);
        public List<JobItem> GetAllItems();
    }
}
