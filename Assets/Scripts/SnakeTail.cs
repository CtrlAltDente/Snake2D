using UnityEngine;
using UnityEngine.UI;
using Managers;

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

        private float _distanceToContact = 0.1f;

        private void Start()
        {
            _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            _tailLenght = GameObject.Find("Lenght").GetComponent<Text>();
            _trail = GetComponent<TrailRenderer>();
        }

        public void GrowingUp(float value)
        {
            _trail.time += value;
        }

        private void Update()
        {
            GetPointsPositions();
            CheckToLose();
            CalculateLenght();
        }

        private void CheckToLose()
        {
            for (int i = 0; i < _trailPointsPosition.Length; i++)
            {
                for (int j = 0; j < _mouth.Length; j++)
                {

                    if (i > 3 && Vector3.Distance(_trailPointsPosition[i], _mouth[j].transform.position) <_distanceToContact)
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
            if (_trail.positionCount > 0)
            {
                Vector3[] pos = new Vector3[_trail.positionCount];
                _trail.GetPositions(pos);
                _trailPointsPosition = pos;
            }
        }

        private void CalculateLenght()
		{
            _tailLenght.text = "Lenght:" + (_trail.time / 0.25f).ToString("0");
		}
    }
}