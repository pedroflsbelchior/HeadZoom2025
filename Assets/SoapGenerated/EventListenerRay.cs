using UnityEngine;
using UnityEngine.Events;
using Obvious.Soap;

[AddComponentMenu("Soap/EventListeners/EventListener"+nameof(Ray))]
public class EventListenerRay : EventListenerGeneric<Ray>
{
    [SerializeField] private EventResponse[] _eventResponses = null;
    protected override EventResponse<Ray>[] EventResponses => _eventResponses;

    [System.Serializable]
    public class EventResponse : EventResponse<Ray>
    {
        [SerializeField] private ScriptableEventRay _scriptableEvent = null;
        public override ScriptableEvent<Ray> ScriptableEvent => _scriptableEvent;

        [SerializeField] private RayUnityEvent _response = null;
        public override UnityEvent<Ray> Response => _response;
    }

    [System.Serializable]
    public class RayUnityEvent : UnityEvent<Ray>
    {
        
    }
}
