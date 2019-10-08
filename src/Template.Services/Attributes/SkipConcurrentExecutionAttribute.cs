﻿using System;
using Hangfire.Common;
using Hangfire.Server;

namespace Template.Services.Attributes
{
    /// <summary>
    /// Attribute to skip a job execution if the same job is already running.
    /// Mostly taken from: http://discuss.hangfire.io/t/job-reentrancy-avoidance-proposal/607
    /// </summary>
    public class SkipConcurrentExecutionAttribute : JobFilterAttribute, IServerFilter
    {
        private readonly int _timeoutInSeconds;

        public SkipConcurrentExecutionAttribute(int timeoutInSeconds)
        {
            if (timeoutInSeconds < 0) throw new ArgumentException("Timeout argument value should be greater that zero.");

            _timeoutInSeconds = timeoutInSeconds;
        }


        public void OnPerforming(PerformingContext filterContext)
        {
            var resource = string.Format(
                "{0}.{1}",
                filterContext.BackgroundJob.Job.Type.FullName,
                filterContext.BackgroundJob.Job.Method.Name);

            var timeout = TimeSpan.FromSeconds(_timeoutInSeconds);

            try
            {
                var distributedLock = filterContext.Connection.AcquireDistributedLock(resource, timeout);
                filterContext.Items["DistributedLock"] = distributedLock;
            }
            catch (Exception)
            {
                filterContext.Canceled = true;
            }
        }

        public void OnPerformed(PerformedContext filterContext)
        {
            if (!filterContext.Items.ContainsKey("DistributedLock"))
            {
                throw new InvalidOperationException("Can not release a distributed lock: it was not acquired.");
            }

            var distributedLock = (IDisposable)filterContext.Items["DistributedLock"];
            distributedLock.Dispose();
        }
    }
}
