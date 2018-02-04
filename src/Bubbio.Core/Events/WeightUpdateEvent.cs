namespace Bubbio.Core.Events
{
    public class WeightUpdateEvent : BiometricUpdateEvent
    {
        public float Weight { get; set; }
    }
}