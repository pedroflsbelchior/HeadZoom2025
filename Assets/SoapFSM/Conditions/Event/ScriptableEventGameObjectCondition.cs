using Obvious.Soap;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableEventCondition", menuName = "Soap/FSM/Conditions/Events/GameObject Event Trigger")] public class ScriptableEventGameObjectCondition : ScriptableEventTriggerCondition<GameObject> { public new ScriptableEventGameObject scriptableEvent; }
