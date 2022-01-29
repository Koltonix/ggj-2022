using UnityEngine;
using UnityEngine.Events;
using cm.events;
using cm.movement;

namespace cm.gameplay
{
    public class MoneyState : MachineState
    {   
        [SerializeField]
        private KeyCode collectKey = KeyCode.E;

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

        private void OnTriggerStay(Collider col)
        {
            PlayerMovement player = col.gameObject.GetComponent<PlayerMovement>();
            if (Input.GetKeyDown(collectKey) && player)
                onCollect?.Invoke();
        }
    }
}
