﻿using System;
using Bubbio.Core.Events.Enums;

namespace Bubbio.Core.Exceptions
{
    public class TransitionEventException : Exception
    {
        public TransitionEventException(EventType eventType, Transition transition)
            : base($"{eventType.ToString()} requires an existing event opposite to {transition.ToString()}")
        {

        }
    }
}