using UnityEngine;
using UnityEngine.Events;
using cm.movement;

namespace cm.gameplay
{
    public class MachineState : MonoBehaviour
    {
        [SerializeField]
        protected bool canOccur  = false;
        [SerializeField]
        private bool playerNearby = false;

        [SerializeField]
        private KeyCode interactKey = KeyCode.E;

        public Vector2 timerRange = new Vector2(2.5f, 15.0f);
        [SerializeField]
        private float _timer = 0.0f;

        [SerializeField]
        protected UnityEvent onTimerEnd = null;

        [SerializeField]
        protected UnityEvent onStateEnter = null;
        [SerializeField]
        protected UnityEvent onStateExit = null;

        protected virtual void Start()
        {
            _timer = GetValueInRange();
        }

        protected virtual void Update()
        {
            if (Input.GetKeyDown(interactKey) && playerNearby && canOccur)
                PlayerInteract();
        }

        private void FixedUpdate()
        {
            if (canOccur)
                CountDown();
        }

        private void OnTriggerEnter(Collider col)
        {
            PlayerMovement player = col.gameObject.GetComponent<PlayerMovement>();
            playerNearby = player != null;
        }

        private void OnTriggerExit(Collider col)
        {
            PlayerMovement player = col.gameObject.GetComponent<PlayerMovement>();
            playerNearby = !(player != null);
        }

        protected virtual void PlayerInteract()
        {

        }

        public virtual void OnStateEnter()
        {
            onStateEnter.Invoke();
            canOccur = true;
        }

        public virtual void OnStateExit()
        {
            onStateExit.Invoke();
            canOccur = false;
        }

        protected virtual void CountDown()
        {
            _timer -= Time.deltaTime;
            if (_timer < 0)
            {
                onTimerEnd.Invoke();
                _timer = GetValueInRange();
            }
        }

        private float GetValueInRange()
        {
            return Random.Range(timerRange.x, timerRange.y);
        }
    }
}
