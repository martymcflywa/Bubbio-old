using System;
using System.Collections.Generic;
using AutoMapper;
using Bubbio.Core.Events;

namespace Bubbio.Persist.Mongo.Maps
{
    public static class MapperEx
    {
        static MapperEx()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<IEvent, IMongoClassMap>()
                    .Include<Sleep, SleepMap>()
                    .Include<BottleFeed, BottleFeedMap>()
                    .Include<TummyTime, TummyTimeMap>()
                    .Include<WeightUpdate, WeightUpdateMap>()
                    .Include<HeightUpdate, HeightUpdateMap>()
                    .ConstructUsing(ClassMapConstructor);
            });
        }

        private static IMongoClassMap ClassMapConstructor(IEvent @event)
        {
            switch (@event)
            {
                case Sleep _:
                    return Mapper.Map<SleepMap>(@event);
                case BottleFeed _:
                    return Mapper.Map<BottleFeedMap>(@event);
                case TummyTime _:
                    return Mapper.Map<TummyTimeMap>(@event);
                case WeightUpdate _:
                    return Mapper.Map<WeightUpdateMap>(@event);
                case HeightUpdate _:
                    return Mapper.Map<HeightUpdateMap>(@event);
                default:
                    throw new InvalidOperationException($"{@event.GetType()} not supported");
            }
        }

        public static IEnumerable<IMongoClassMap> ToClassMap(this IEnumerable<IEvent> events)
        {
            return Mapper.Map<IEnumerable<IEvent>, IEnumerable<IMongoClassMap>>(events);
        }

        public static IMongoClassMap ToClassMap(this IEvent @event)
        {
            return Mapper.Map<IMongoClassMap>(@event);
        }
    }
}