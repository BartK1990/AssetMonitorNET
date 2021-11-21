using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspMVC_Monitor.Models.Helpers
{
    public static class ExecuteHelper
    {
        public static bool ExecuteWithTimeLimit(TimeSpan timeSpan, Action codeBlock)
        {
            try
            {
                Task task = Task.Factory.StartNew(() => codeBlock());
                task.Wait(timeSpan);
                return task.IsCompleted;
            }
            catch (AggregateException ae)
            {
                throw ae.InnerExceptions[0];
            }
        }
    }
}
