using UnityEngine;
using UnityEngine.Events;

namespace cm.gameplay
{
    public class WashingState : MachineState
    {
        [SerializeField]
        private Customer customer = null;
        [SerializeField]
        private GameObject customerPrefab = null;

        public bool customerArrived = false;

        public UnityEvent onCustomerNear = null;
        public UnityEvent onCustomerLeave = null;


        protected override void CountDown()
        {
            if (customer && Vector3.Distance(customer.transform.position, this.transform.position) < 2.5f)
            {
                base.CountDown();

                if (!customerArrived)
                {
                    customerArrived = true;
                    onCustomerNear.Invoke();
                }
            }

            // ONLY COUNTDOWN WHEN THE CUSTOMER IS THERE
            return;
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();

            customerArrived = false;

            SpawnCustomer();
        }

        public void ReleaseCustomer()
        {
            if (!customer)
                return;


            onCustomerLeave.Invoke();

             // Send the Customer on their way!
            customer.state = MoveState.LEAVE;
            customer = null;
        }

        public void SpawnCustomer()
        {
            Vector3 spawnPoint = AIPath.Instance.GetPoint(0).position;
            customer = Instantiate(customerPrefab, spawnPoint, Quaternion.identity).GetComponent<Customer>();
            customer.machine = this;
        }
    }
}
