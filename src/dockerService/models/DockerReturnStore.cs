using BreakableLime.DockerBackgroundService.models;

namespace BreakableLime.DockerBackgroundService
{
    public class DockerReturnStore<T> : IDockerReturnStore<T>
    {
        private T _returnedValue;
        public T ReturnedValue
        {
            get => _returnedValue;

            set
            {
                _hasCompleted = true;
                _returnedValue = value;
            }
        }

        public bool IsFinished(out T result)
        {
            result = _returnedValue;
            return _hasCompleted;
        }

        private bool _hasCompleted = false;
    }
}