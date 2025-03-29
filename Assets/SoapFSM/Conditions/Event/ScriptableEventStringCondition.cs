using Obvious.Soap;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableEventCondition", menuName = "Soap/FSM/Conditions/Events/String Event Trigger")] public class ScriptableEventStringCondition : ScriptableEventTriggerCondition<string> { public new ScriptableEventString scriptableEvent; }
