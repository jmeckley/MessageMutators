namespace Sample.Core.Implementation
{
    public class MappingEngine 
        : IMappingEngine
    {
        readonly IContainerAdaptor _container;

        public MappingEngine(IContainerAdaptor container)
        {
            _container = container;
        }

        public TOut Map<TIn, TOut>(TIn item)
        {
            return _container.Resolve<IMapper<TIn, TOut>>().Map(item);
        }
    }

    public interface IMappingEngine
    {
        TOut Map<TIn, TOut>(TIn item);
    }
}
