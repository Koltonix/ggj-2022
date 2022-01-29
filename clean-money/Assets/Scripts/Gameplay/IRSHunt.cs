using UnityEngine;
using cm.movement;

namespace cm.gameplay
{
    [ExecuteInEditMode]
    public class IRSHunt : MonoBehaviour
    {
        public PlayerMovement player = null;

        [SerializeField]
        private float radius = 5.0f;

        [SerializeField]
        private GameObject cylinder = null;

        private void Start()
        {
            player = FindObjectOfType<PlayerMovement>();
        }

        private void Update()
        {
            cylinder.transform.localScale = new Vector3(radius * 2, -cylinder.transform.localScale.y, radius * 2);
        }

        private void FixedUpdate()
        {
            if (!Application.isEditor)
                LocatePlayer();
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
                        Debug.Log("PLAYER FOUND");
                }
            }
        }

        private void RunItDown()
        {
            
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(this.transform.position, radius);
        }
    }
}