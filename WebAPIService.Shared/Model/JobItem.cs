using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebAPIService.Shared.Model
{
    /// <summary>
    /// Number based job item
    /// </summary>
    public class JobItem
    {

        public string Id { get; set; }
        public DateTime? EnqueuedTimeStamp { get; set; }
        public DateTime? CompletedTimeStamp { get; set; }
        public TimeSpan? ExecuteDuration => CompletedTimeStamp - EnqueuedTimeStamp;
        public JobStatus Status { get; set; }
        public List<int> Items { get; set; }

        public JobItem()
        {
            Initialize();
        }

        public JobItem(JobItem jobItem)
        {
            Id = jobItem.Id;
            Status = jobItem.Status;
            EnqueuedTimeStamp = jobItem?.EnqueuedTimeStamp;
            CompletedTimeStamp = jobItem?.EnqueuedTimeStamp;
        }


        public void UpdateJobStatus(JobStatus jobStatus)
        {
            Status = jobStatus;

            if (jobStatus == JobStatus.Completed
                || jobStatus == JobStatus.Failed)
            {
                CompletedTimeStamp = DateTime.Now;
            }

        }


        /// <summary>
        /// Initialize with collections of array
        /// </summary>
        /// <param name="items">Array of string</param>
        public JobItem(int[] items)
        {
            Initialize();

            Items = new List<int>(items);

            if (Items?.Count() == 1)
            {
                UpdateJobStatus(JobStatus.Completed);
            }
        }

        private void Initialize()
        {

            Id = Guid.NewGuid().ToString();
            Status = JobStatus.Pending;
            EnqueuedTimeStamp = DateTime.Now;
        }

    }
}