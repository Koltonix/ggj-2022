using UnityEngine;

namespace cm.movement
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Input")]
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

        private void Update()
        {
            inputDir = GetInputDirection();
        }

        private void FixedUpdate()
        {
            AimCharacter(inputDir);
            MoveCharacter(inputDir);
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

            rigidbody.velocity = rigidbody.transform.forward * moveSpeed * Time.deltaTime;
        }
    }
}