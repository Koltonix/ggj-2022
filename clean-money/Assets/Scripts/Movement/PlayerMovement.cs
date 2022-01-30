using UnityEngine;
using cm.events;

namespace cm.movement
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Input")]
        private Camera mainCamera = null;
        [SerializeField]
        private string horizontalKey = "MOVEX";
        [SerializeField]
        private string verticalKey = "MOVEZ";

        [SerializeField]
        private Vector3 inputDir = Vector3.zero;

        [Header("Movement")]
        [SerializeField]
        private float moveSpeed = 15.0f;
        [SerializeField]
        private float aimSpeed = 1.25f;
        [SerializeField]
        private new Rigidbody rigidbody = null;

        [SerializeField]
        private float richochetForce = 15.0f;

        [SerializeField]
        private GameEvent onDeath = null;

        private void Awake() => mainCamera = Camera.main;

        private void Update()
        {
            inputDir = GetInputDirection();
        }

        private void FixedUpdate()
        {
            Vector3 relDir = GetInputRelToCamera(inputDir);

            AimCharacter(relDir);
            MoveCharacter(relDir);
        }

        private Vector3 GetInputRelToCamera(Vector3 dir)
        {
            Vector3 cameraForward = new Vector3(mainCamera.transform.forward.x, 0.0f, mainCamera.transform.forward.z).normalized;
            Vector3 cameraRight = new Vector3(mainCamera.transform.forward.z, 0.0f, mainCamera.transform.right.z).normalized;

            return cameraForward * inputDir.z + cameraRight * inputDir.x;
        }

        private Vector3 GetInputDirection()
        {
            return new Vector3(Input.GetAxisRaw(horizontalKey), .0f, Input.GetAxisRaw(verticalKey));
        }

        private void AimCharacter(Vector3 dir)
        {
            rigidbody.transform.forward = Vector3.Slerp(rigidbody.transform.forward, dir, Time.deltaTime * aimSpeed);
        }

        private void MoveCharacter(Vector3 dir)
        {
            if (dir.normalized.magnitude < 0.25f)
                return;

            rigidbody.velocity += rigidbody.transform.forward * moveSpeed * Time.deltaTime;
        }
        
        private void OnCollisionEnter(Collision col)
        {
            Vector3 oppositeDir = (this.transform.position - col.GetContact(0).point).normalized;
            oppositeDir.y = 0.0f;
            rigidbody.AddForce(oppositeDir * richochetForce, ForceMode.Impulse);
        }

        private void OnDestroy()
        {
            onDeath.Raise();
        }
    }
}