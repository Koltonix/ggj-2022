using UnityEngine;
using UnityEngine.Events;

namespace cm.gameplay
{
    public class MachineState : MonoBehaviour
    {
        [SerializeField]
        protected bool canOccur  = false;

        public Vector2 timerRange = new Vector2(2.5f, 15.0f);
        [SerializeField]
        private float _timer = 0.0f;

        [SerializeField]
        protected UnityEvent onTimerEnd = null;

        [SerializeField]
        private UnityEvent onStateEnter = null;
        [SerializeField]
        private UnityEvent onStateExit = null;

        protected virtual void Start()
        {
            _timer = GetValueInRange();
        }

        private void FixedUpdate()
        {
            if (canOccur)
                CountDown();
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
