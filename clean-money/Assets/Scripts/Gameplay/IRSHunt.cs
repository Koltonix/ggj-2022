using System.Collections;
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
        private Coroutine aiCoroutine = null;

        [SerializeField]
        [Range(0.0f, 1.0f)]
        private float waitChance = 0.25f;
        [SerializeField]
        private Vector2 waitRange = new Vector2(0.5f, 5.0f);

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
            LocatePlayer();

            if (chase && player)
            {
                if (aiCoroutine != null)
                {
                    StopCoroutine(aiCoroutine);
                    aiCoroutine = null;
                }

                RunItDown();
            }

            else
            {
                if (aiCoroutine == null)
                    aiCoroutine = StartCoroutine(WalkNStuff());
            }

        }

        private void LocatePlayer()
        {
            RaycastHit[] hits = Physics.SphereCastAll(this.transform.position, radius, this.transform.forward);
            foreach (RaycastHit hit in hits)
            {
                if (player && hit.collider.gameObject == player.gameObject)
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
            if (player && col.gameObject == player.gameObject)
            {
                Destroy(player.gameObject);
                // END GAME
            }
        }

        private IEnumerator WalkNStuff()
        {
            int index = AIPath.Instance.GetClosestPointIndex(this.transform.position);
            
            Transform[] points = AIPath.Instance.points;
            for (int i = index; i < points.Length; i++)
            {
                if (!points[i])
                    continue;
                
                bool wait = Random.Range(0, 1.0f) < waitChance;

                if (wait)
                    yield return new WaitForSeconds(Random.Range(waitRange.x, waitRange.y));

                yield return WalkTo(points[i].position);
            }

            Destroy(this.gameObject);

            yield return null;
        }

        private IEnumerator WalkTo(Vector3 point)
        {
            while (Vector3.Distance(this.transform.position, point) > 0.5f)
            {
                agent.destination = point;
                this.transform.forward = Vector3.Slerp(this.transform.forward, point - this.transform.position, Time.deltaTime * aimSpeed);

                yield return new WaitForEndOfFrame();
            }

            yield return null;
        }
    }
}