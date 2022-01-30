using UnityEngine;

namespace cm.gameplay
{
    public enum MoveState
    {
        FOLLOWPATH = 0,
        GOTOMACHINE = 1,
        WAITATMACHINE = 2,
        LEAVE = 3,
    }

    public class Customer : MonoBehaviour
    {
        public WashingState machine = null;
        public int pointIndex = 0;

        [SerializeField]
        private float aimSpeed = 2.5f;
        [SerializeField]
        private float moveSpeed = 2.5f;

        [SerializeField]
        private float distanceFromMachine = 1.0f;

        [SerializeField]
        private float distanceInFrontOfMachine = 1.25f;

        public MoveState state = MoveState.FOLLOWPATH;

        private void FixedUpdate()
        {
            switch(state)
            {
                case MoveState.FOLLOWPATH:
                    FollowPath();
                    break;
                
                case MoveState.GOTOMACHINE:
                    GoToMachine();
                    break;

                case MoveState.WAITATMACHINE:
                    Vector3 targetAim =  machine.transform.position - this.transform.position;
                    targetAim.y = 0.0f;
                    this.transform.forward = Vector3.Slerp(this.transform.forward, targetAim, Time.deltaTime * aimSpeed);
                    break; 

                case MoveState.LEAVE:
                    FollowPath();
                    break;
            }
        }

        private void FollowPath()
        {
            Vector3 pos = AIPath.Instance.GetPoint(pointIndex).position;
            pos.y = this.transform.position.y;

            this.transform.position += this.transform.forward * moveSpeed * Time.deltaTime;

            Vector3 targetAim = pos - this.transform.position;
            targetAim.y = 0.0f;

            this.transform.forward = Vector3.Slerp(this.transform.forward, targetAim.normalized, Time.deltaTime * aimSpeed);
            
            if (Vector3.Distance(this.transform.position, pos) < 0.5f)
            {
                pointIndex++;
                if (AIPath.Instance.GetPoint(pointIndex) == null)
                    Destroy(this.gameObject);
            }

            if (machine && Vector3.Distance(this.transform.position, machine.transform.position) < distanceFromMachine && state != MoveState.LEAVE)
                state = MoveState.GOTOMACHINE;
        }

        private void GoToMachine()
        {
            Vector3 targetPos = machine.transform.position + (machine.transform.forward * distanceInFrontOfMachine);
            Vector3 targetAim =  targetPos - this.transform.position;
            targetAim.y = 0.0f;

            this.transform.forward = Vector3.Slerp(this.transform.forward, targetAim, Time.deltaTime * aimSpeed);
            this.transform.position = Vector3.Lerp(this.transform.position, targetPos, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(this.transform.position, targetPos) < 0.1f)
                state = MoveState.WAITATMACHINE;
        }
    }
}