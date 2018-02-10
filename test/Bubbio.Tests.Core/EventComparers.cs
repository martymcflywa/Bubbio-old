using System;
using System.Collections.Generic;
using Bubbio.Core.Events;

namespace Bubbio.Tests.Core
{
    public class RootEventComparer : IEqualityComparer<IEvent>
    {
        public bool Equals(IEvent x, IEvent y)
        {
            if (x == null || y == null)
                return false;

            return x.SequenceId.Equals(y.SequenceId)
                   && x.EventId.ToString().Equals(y.EventId.ToString())
                   && x.BabyId.ToString().Equals(y.BabyId.ToString())
                   && x.EventType.Equals(y.EventType)
                   && x.Timestamp.Equals(y.Timestamp);
        }

        public int GetHashCode(IEvent obj)
        {
            throw new NotImplementedException();
        }
    }

    public class TransitionEventComparer : IEqualityComparer<ITransition>
    {
        public bool Equals(ITransition x, ITransition y)
        {
            return x.SequenceId.Equals(y.SequenceId)
                   && x.EventId.ToString().Equals(y.EventId.ToString())
                   && x.BabyId.ToString().Equals(y.BabyId.ToString())
                   && x.EventType.Equals(y.EventType)
                   && x.Timestamp.Equals(y.Timestamp)
                   && x.Transition.Equals(y.Transition);
        }

        public int GetHashCode(ITransition obj)
        {
            throw new NotImplementedException();
        }
    }
}