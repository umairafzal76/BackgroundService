# Background Service

# The problem 
To simulate a realistic problem, you will be implementing a minimal job scheduling solution exposed via a web API. In short, your solution should make it possible for clients to enqueue an array of numbers to be sorted in the background and to query the state of any previously enqueued job. 

# Functional requirements 
You are asked to develop a web API that supports the following operations: 
The client can enqueue a new job, by providing an unsorted array of numbers as input The client can retrieve an overview of all jobs (both pending and completed) The client can retrieve a specific job by its ID (see below), including the output (sorted array) if the job has completed 
A job takes an unsorted array of numbers as input, sorts the input using an algorithm of your choice, and outputs the sorted array. Apart from the input and output arrays a job should include the following metadata: 
An ID - a unique ID assigned by the application A timestamp - when was the job enqueued? A duration - how much time did it take to execute the job? A status - for example "pending" or "completed".
 
# Your delivery should: 
Include a fully functional solution built using C#, ASP.NET Core, and .NET Core/5/6 Expose a web API and implement relevant background job processing. 
Contain logging functionality. Include the complete codebase needed to run the solution. 
If you use any third-party libraries, these must be referenced via Nuget.
Contain a README explaining how to build, test, and run your solution locally, plus any additional details (e.g., architectural decisions) you might find relevant for us to know. 


## Pre-Requisite:
1. Visual Studio 2022, .Net Framework / .Net Core 3.1 & nuget packages.

## Solution consists of following projects
1. WebAPIService : .Net Core 3.1 Web Api based project.
2. WebAPIService.Shared : Contains the common/shared models which can be used across the projects classes to retreivedata.