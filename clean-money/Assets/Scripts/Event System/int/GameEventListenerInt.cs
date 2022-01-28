using UnityEngine;

namespace cm.Events
{
    public class GameEventListenerInt : MonoBehaviour
    {
        public GameEventInt Event;
        public IntEvent Reponse;

        private void OnEnable() { Event.RegisterListener(this); }
        private void OnDisable() { Event.UnregisterListener(this); }
        public void OnEventRaise(int value) { Reponse.Invoke(value); }
    }
}