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
        private float moveSpeed = 2.5f;

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

            this.transform.position = Vector3.Lerp(this.transform.position, pos, Time.deltaTime * moveSpeed);
            
            if (Vector3.Distance(this.transform.position, pos) < 0.5f)
            {
                pointIndex++;
                if (AIPath.Instance.GetPoint(pointIndex) == null)
                    Destroy(this.gameObject);
            }

            if (Vector3.Distance(this.transform.position, machine.transform.position) < 2.5f && state != MoveState.LEAVE)
                state = MoveState.GOTOMACHINE;
        }

        private void GoToMachine()
        {
            Vector3 targetPos = machine.transform.position + (machine.transform.forward * 1.5f);
            this.transform.position = Vector3.Lerp(this.transform.position, targetPos , Time.deltaTime * moveSpeed);

            if (Vector3.Distance(this.transform.position, targetPos) < 0.5f)
                state = MoveState.WAITATMACHINE;
        }
    }
}