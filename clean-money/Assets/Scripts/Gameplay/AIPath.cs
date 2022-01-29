using UnityEngine;

namespace cm.gameplay
{
    public class AIPath : MonoBehaviour
    {
        private Transform[] points = null;

        public static AIPath Instance = null;
        private void Awake()
        {
            if (Instance)
                Destroy(this);

            else
                Instance = this;
        }

        private void Start()
        {
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