using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskSchedular
{
    public class SImpleSchedular : TaskScheduler
    {
        BlockingCollection<Task> tasks = new BlockingCollection<Task>();
        private Thread main;

        public SImpleSchedular()
        {
            main = new Thread(Execute);
            main.Start();
        }
        protected override IEnumerable<Task> GetScheduledTasks()
        {
            return tasks.GetConsumingEnumerable();
        }

        protected override void QueueTask(Task task)
        {
            tasks.Add(task);
            if (!main.IsAlive)
            {
                main = new Thread(Execute);
                main.Start();
            }
        }

        protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
        {
           return false;
        }

        private void Execute()
        {
            while (tasks.Count > 0)
            {
                var task = tasks.Take();
                TryExecuteTask(task);
            }
        }
    }
}
