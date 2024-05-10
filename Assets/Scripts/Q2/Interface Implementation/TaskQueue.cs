using System.Collections;
using System.Collections.Generic;
using Q2.Interface_Implementation.Common;
using UnityEngine;

namespace Q2.Interface_Implementation
{
    public class TaskQueue : ITaskQueue
    {
        private Queue<ITask> taskQueue;

        public TaskQueue()
        {
            taskQueue = new Queue<ITask>();
        }
        
        public void Enqueue(ITask task)
        {
            taskQueue.Enqueue(task);
        }

        public void ExecuteNext()
        {
            var nextTask = taskQueue.Dequeue();
            nextTask.Execute();   
        }

        public void ExecuteAll()
        {
            while (taskQueue.Count > 0)
            {
                ExecuteNext();
            }
        }
    }
}
