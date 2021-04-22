namespace BreakableLime.DockerBackgroundService.models
{
    public interface IDockerReturnStore<T>
    {
        public T ReturnedValue { get; set; }
        public bool IsFinished(out T result);
    }
}