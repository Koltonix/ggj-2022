using UnityEngine;
using UnityEngine.Events;
using cm.events;
using cm.movement;

namespace cm.gameplay
{
    public class RepairState : MachineState
    {   
        [SerializeField]
        private int hitsRequired = 5;
        [SerializeField]
        private int hitsLeft = 0;

        [SerializeField]
        private IntEvent onHitValue = null;

        public override void OnStateEnter()
        {
            base.OnStateEnter();

            hitsLeft = hitsRequired;
            onHitValue?.Invoke(hitsLeft);
        }

        protected override void CountDown() 
        {
            return;
        }
        
        protected override void PlayerInteract()
        {
            hitsLeft--;
            onHitValue?.Invoke(hitsLeft);
            
            if (hitsLeft <= 0)
                onTimerEnd?.Invoke();
        }
    }
}
