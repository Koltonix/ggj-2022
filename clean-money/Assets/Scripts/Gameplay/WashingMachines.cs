using System;
using UnityEngine;

namespace cm.gameplay
{
    public enum WashingState
    {
        INACTIVE = 0,
        REPARING = 1,
        WASHING = 2,
        COLLECT = 3
    }

    [Serializable]
    public struct StateTime
    {
        public StateTime(WashingState state, float delay)
        {
            this.state = state;
            this.delay = delay;
        }

        public WashingState state;
        public float delay;
    }

    public class WashingMachines : MonoBehaviour
    {
        public WashingState state = WashingState.INACTIVE;
        private Customer currentCustomer = null;

        [Serializable]
        private StateTime[] times = 
        {
            new StateTime(WashingState.INACTIVE, .0f),

        };


        private void FixedUpdate()
        {

        }

        public void CustomerJoin(Customer customer)
        {
            //customer.GoToWashingMachine...
        }

        private void OnTriggerEnter(Collider col)
        {
            Customer customer = col.gameObject.GetComponent<Customer>();
            if (customer && state == WashingState.INACTIVE)
            {
                CustomerJoin(customer);
            }
        }
    }
}