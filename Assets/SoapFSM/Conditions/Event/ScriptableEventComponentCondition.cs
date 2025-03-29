using Obvious.Soap;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableEventCondition", menuName = "Soap/FSM/Conditions/Events/Component Event Trigger")] public class ScriptableEventComponentCondition : ScriptableEventTriggerCondition<Component> { public new ScriptableEventComponent scriptableEvent; }
