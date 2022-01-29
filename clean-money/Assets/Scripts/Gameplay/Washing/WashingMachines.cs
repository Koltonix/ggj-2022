using UnityEngine;
using UnityEngine.Events;
using cm.events;

namespace cm.gameplay
{
    public class WashingMachines : MonoBehaviour
    {
        [SerializeField]
        private MachineState currentState = null;

        [SerializeField]
        private UnityEvent onStart = null;

        public GameEvent onNewState = null;
        public GameEvent onRemoveState = null;

        private void Start()
        {
            onStart?.Invoke();
        }

        private void FixedUpdate()
        {

        }

        public void SetNewState(MachineState state)
        {
            state.OnStateEnter();
            currentState = state;

            onNewState.Raise();
        }

        public void RemoveState()
        {
            currentState.OnStateExit();
            currentState = null;

            onNewState.Raise();
        }
    }
}