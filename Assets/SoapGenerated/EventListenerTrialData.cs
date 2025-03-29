using UnityEngine;
using UnityEngine.Events;
using Obvious.Soap;

[AddComponentMenu("Soap/EventListeners/EventListener"+nameof(TrialData))]
public class EventListenerTrialData : EventListenerGeneric<TrialData>
{
    [SerializeField] private EventResponse[] _eventResponses = null;
    protected override EventResponse<TrialData>[] EventResponses => _eventResponses;

    [System.Serializable]
    public class EventResponse : EventResponse<TrialData>
    {
        [SerializeField] private ScriptableEventTrialData _scriptableEvent = null;
        public override ScriptableEvent<TrialData> ScriptableEvent => _scriptableEvent;

        [SerializeField] private TrialDataUnityEvent _response = null;
        public override UnityEvent<TrialData> Response => _response;
    }

    [System.Serializable]
    public class TrialDataUnityEvent : UnityEvent<TrialData>
    {
        
    }
}
