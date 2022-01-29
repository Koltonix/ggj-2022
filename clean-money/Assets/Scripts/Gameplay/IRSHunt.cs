using UnityEngine;
using UnityEngine.AI;
using cm.movement;

namespace cm.gameplay
{
    [ExecuteInEditMode, RequireComponent(typeof(NavMeshAgent))]
    public class IRSHunt : MonoBehaviour
    {
        private NavMeshAgent agent = null;

        public PlayerMovement player = null;

        [SerializeField]
        private float moveSpeed = 2.5f;
        [SerializeField]
        private float aimSpeed = 1.25f;

        [SerializeField]
        private float radius = 5.0f;

        [SerializeField]
        private float ignoreDelay = 5.0f;
        private float _ignoreDelay = 0.0f;

        public bool chase = false;

        [SerializeField]
        private GameObject cylinder = null;

        private void Start()
        {
            player = FindObjectOfType<PlayerMovement>();
            agent = this.GetComponent<NavMeshAgent>();

            _ignoreDelay = ignoreDelay;
        }

        private void Update()
        {
            cylinder.transform.localScale = new Vector3(radius * 2, -cylinder.transform.localScale.y, radius * 2);
        }

        // Doesn't run in Edit mode anyway.
        private void FixedUpdate()
        {
            if (!player)
                return;

            LocatePlayer();

            if (chase)
                RunItDown();

            else
            {
                
            }

        }

        private void LocatePlayer()
        {
            RaycastHit[] hits = Physics.SphereCastAll(this.transform.position, radius, this.transform.forward);
            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.gameObject == player.gameObject)
                {
                    RaycastHit check;
                    Physics.Raycast(this.transform.position, player.transform.position - this.transform.position, out check, radius);

                    if (check.collider && check.collider.gameObject == player.gameObject)
                        {
                            chase = true;
                            _ignoreDelay = ignoreDelay;
                        }

                    else
                    {
                        _ignoreDelay -= Time.deltaTime;
                        if (_ignoreDelay <= 0)
                        {
                            chase = false;
                            agent.velocity = Vector3.zero;
                        }

                    }
                }
            }
        }

        private void RunItDown()
        {
            agent.SetDestination(player.transform.position);
            this.transform.forward = Vector3.Slerp(this.transform.forward, player.transform.position - this.transform.position, Time.deltaTime * aimSpeed);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(this.transform.position, radius);
        }

        private void OnTriggerEnter(Collider col)
        {
            if (col.gameObject == player.gameObject)
            {
                Destroy(player.gameObject);
                // END GAME
            }
        }
    }
}