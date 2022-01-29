using UnityEngine;
using UnityEngine.Events;
using cm.events;
using cm.movement;

namespace cm.gameplay
{
    public class MoneyState : MachineState
    {   
        [SerializeField]
        private int amount = 50;

        [SerializeField]
        private UnityEvent onCollect = null;

        [SerializeField]
        private GameEventInt onMoneyIncrease = null;

        protected override void Start()
        {
            base.Start();

            onCollect.AddListener(delegate{onMoneyIncrease.Raise(amount);});
        }

        protected override void CountDown() 
        {
            return;
        }

        protected override void PlayerInteract()
        {
            base.PlayerInteract();
             
            onCollect?.Invoke();     
        }

        private void OnTriggerStay(Collider col)
        {
           
        }
    }
}
