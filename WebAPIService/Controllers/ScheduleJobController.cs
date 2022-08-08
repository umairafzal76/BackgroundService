using WebAPIService;
using WebAPIService.Controllers;
using WebAPIService.Shared.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Collections.Generic;

namespace WebAPIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduledJobController : ControllerBaseEx
    {
        private readonly IAppDataStorage _appDataStorage;
        private readonly IJobSort _jobSort;

        public ScheduledJobController(ILogger logger, IAppDataStorage appDataStorage, IJobSort jobSort)
        : base((logger))
        {
            _appDataStorage = appDataStorage;
            _jobSort = jobSort;
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IEnumerable<JobItem> GetAllScheduledJob()
        {
            LogInfo(LogEventMap.WebApi_ScheduledJob_AllJobStatus, null, "Fetching all jobs start.");
            return _jobSort.GetAllJobsList();
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public JobItem GetScheduledJobById(string id)
        {

            var jobItem = _jobSort.GetJobById(id);

            if (jobItem == null)
            {
                LogInfo(LogEventMap.WebApi_ScheduledJob_SpecificJob_NotFound, id, $"Job not found.");
                return null;
            }

            if (jobItem?.Status == JobStatus.Completed)
            {
                return jobItem;
            }
            else
            {
                return new JobItem(jobItem);
            }
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public string AddScheduledJob(int[] items)
        {
            var jobItem = new JobItem(items);

            LogInfo(LogEventMap.WebApi_ScheduledJob_ItemReceived, jobItem.Id, $"Job Items received : {items?.Length}");

            if (jobItem.Status == JobStatus.Completed) //if index =  1
            {
                LogInfo(LogEventMap.WebApi_ScheduledJob_ItemReceived_Returned, jobItem.Id, $"Job Completed.");
                _jobSort.AddItemAsync(jobItem);
                return jobItem.Id;
            }

            _jobSort.AddItemAsync(jobItem);
            LogInfo(LogEventMap.WebApi_ScheduledJob_ItemReceived_Scheduled, jobItem.Id, $"Job addded to scheduled.");

            return jobItem.Id;
        }
    }
}