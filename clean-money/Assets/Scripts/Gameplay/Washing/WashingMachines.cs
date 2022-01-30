using UnityEngine;
using UnityEngine.Events;
using cm.events;

namespace cm.gameplay
{
    public class WashingMachines : MonoBehaviour
    {
        public MachineState currentState = null;

        [SerializeField]
        private MachineState[] randomStates = null;
        
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

        public void SetRandomState()
        {
            SetNewState(randomStates[Random.Range(0, randomStates.Length)]);
        }

        public void SetNewState(MachineState state)
        {
            if (!state)
                return;

            state.OnStateEnter();
            currentState = state;

            onNewState.Raise();
        }

        public void RemoveState()
        {
            if (!currentState)
                return;

            currentState.OnStateExit();
            currentState = null;

            onNewState.Raise();
        }
    }
}