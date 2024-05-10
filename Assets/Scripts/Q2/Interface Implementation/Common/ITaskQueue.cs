
namespace Q2.Interface_Implementation.Common
{
    public interface ITaskQueue
    {
        public abstract void Enqueue(ITask task);
        public abstract void ExecuteNext();
    }
}
