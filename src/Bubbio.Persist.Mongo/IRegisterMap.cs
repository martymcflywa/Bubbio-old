using Bubbio.Persist.Mongo.Maps;

namespace Bubbio.Persist.Mongo
{
    public interface IRegisterMap
    {
        void Register<TClassMap>() where TClassMap : IMongoClassMap;
    }
}