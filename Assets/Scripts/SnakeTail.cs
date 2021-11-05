using UnityEngine;

namespace Snake
{
    public class SnakeTail : MonoBehaviour
    {
        private TrailRenderer _trail;

        [SerializeField]
        private GameObject[] _mouth;

        [SerializeField]
        private Vector3[] _trailPointsPosition;

        private void Start()
        {
            _trail = GetComponent<TrailRenderer>();
        }

        private void Update()
        {
            CheckToLose();
            GetPointsPositions();
        }

        public void GrowingUp(float value)
        {
            _trail.time += value;
        }

        private void CheckToLose()
        {
            for (int i = 0; i < _trailPointsPosition.Length; i++)
            {
                for (int j = 0; j < _mouth.Length; j++)
                {

                    if (i>3 && Vector3.Distance(_trailPointsPosition[i], _mouth[j].transform.position) < 0.1f)
                    {
                        Debug.Log("Lose");
                    }
                }
            }
        }

        private void GetPointsPositions()
        {
            if (_trail.positionCount > 2)
            {
                Vector3[] pos = new Vector3[_trail.positionCount];
                _trail.GetPositions(pos);
                _trailPointsPosition = pos;
            }
        }
    }
}