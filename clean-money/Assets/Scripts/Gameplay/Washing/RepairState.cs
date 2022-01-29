using UnityEngine;
using UnityEngine.Events;
using cm.events;
using cm.movement;

namespace cm.gameplay
{
    public class RepairState : MachineState
    {   

        [SerializeField]
        private KeyCode repairKey = KeyCode.E;

       protected override void CountDown() 
        {
            return;
        }

        private void OnTriggerStay(Collider col)
        {
            PlayerMovement player = col.gameObject.GetComponent<PlayerMovement>();
            if (Input.GetKeyDown(repairKey) && player && canOccur)
                onTimerEnd?.Invoke();
        }
    }
}
