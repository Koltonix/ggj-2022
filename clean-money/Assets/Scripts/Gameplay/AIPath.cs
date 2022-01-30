using UnityEngine;

namespace cm.gameplay
{
    public class AIPath : MonoBehaviour
    {
        public Transform[] points = null;

        public static AIPath Instance = null;
        private void Awake()
        {
            if (Instance)
                Destroy(this);

            else
                Instance = this;

            points = GetPointsInChildren();
        }

        private Transform[] GetPointsInChildren()
        {
            int childCount = this.transform.childCount;
            Transform[] points = new Transform[childCount];

            for (int i = 0; i < childCount; i++)
                points[i] = this.transform.GetChild(i).transform;

                return points;
        }

        public Transform GetPoint(int index)
        {
            if (index < points.Length)
                return points[index];

            return null;
        }

        public int GetClosestPointIndex(Vector3 currentPos)
        {
            int index = 0;
            float distance = float.MaxValue;
            for (int i = 0; i < points.Length; i++)
            {
                float _distance = Vector3.Distance(points[i].position, currentPos);
                if (_distance < distance)
                {
                    index = i;
                    distance = _distance;
                }
            }

            return index;
        }

        public float GetSpeedAtPoint(int index, float speed)
        {
            float totalDistance = GetTotalDistance();
            float currentDistance = GetDistanceFromPoint(index);

            return (currentDistance / totalDistance) * speed;
        }

        public float GetTotalDistance()
        {
            float distance = .0f;
            for (int i = 0; i < points.Length - 1; i++)
                distance += GetDistanceFromPoint(i);

            return distance;
        }

        public float GetDistanceFromPoint(int index)
        {
            if (index >= points.Length)
                return .0f;

            return Vector3.Distance(points[index].position, points[index + 1].position);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;

            Transform[] points = GetPointsInChildren();
            for (int i = 0; i < points.Length; i++)
            {
                Vector3 pos = points[i].position;
                Gizmos.DrawSphere(pos, 0.5f);

                if (i < points.Length - 1)
                    Gizmos.DrawLine(pos, points[i + 1].position);
            }
        }
    }
}