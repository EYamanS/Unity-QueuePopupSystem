using System;
using System.Collections.Generic;

namespace Q2
{
    public class TaskQueueActions
    {
        private Queue<Action> _taskQueue;

        public TaskQueueActions()
        {
            _taskQueue = new Queue<Action>();
        }

        public void Enqueue(Action task)
        {
            _taskQueue.Enqueue(task);
        }

        public void Execute()
        {
            if (_taskQueue.Count > 0)
            {
                Action nextTask = _taskQueue.Dequeue();
                nextTask.Invoke();
            }
        }

        public void ExecuteAll()
        {
            while (_taskQueue.Count > 0)
            {
                Execute();
            }
        }
    }
}
