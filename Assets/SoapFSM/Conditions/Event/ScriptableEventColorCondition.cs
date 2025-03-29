using Obvious.Soap;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableEventCondition", menuName = "Soap/FSM/Conditions/Events/Color Event Trigger")] public class ScriptableEventColorCondition : ScriptableEventTriggerCondition<Color> { public new ScriptableEventColor scriptableEvent; }
