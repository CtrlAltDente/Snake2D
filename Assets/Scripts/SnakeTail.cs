using UnityEngine;
using UnityEngine.UI;
using Manager;

namespace Snake
{
    public class SnakeTail : MonoBehaviour
    {
        private GameManager _gameManager;

        [SerializeField]
        private Text _tailLenght;

        private TrailRenderer _trail;

        [SerializeField]
        private GameObject[] _mouth;

        [SerializeField]
        private Vector3[] _trailPointsPosition;

        private bool _collisionFinded = false;

        private void Start()
        {
            _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            _tailLenght = GameObject.Find("Lenght").GetComponent<Text>();
            _trail = GetComponent<TrailRenderer>();
        }

        private void Update()
        {
            CheckToLose();
            GetPointsPositions();
            CalculateLenght();
        }

        private void CheckToLose()
        {
            for (int i = 0; i < _trailPointsPosition.Length; i++)
            {
                for (int j = 0; j < _mouth.Length; j++)
                {

                    if (i > 3 && Vector3.Distance(_trailPointsPosition[i], _mouth[j].transform.position) < 0.1f)
                    {
                        _collisionFinded = true;
                        _gameManager.GameOver();
                        break;
                    }
                    if(_collisionFinded)
					{
                        break;
					}
                }
                if (_collisionFinded)
                {
                    break;
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

        public void GrowingUp(float value)
        {
            _trail.time += value;
        }

        private void CalculateLenght()
		{
            _tailLenght.text = "Lenght:" + (_trail.time / 0.25f).ToString("0");
		}
    }
}