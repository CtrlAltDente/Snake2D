using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;

namespace Foods
{
    public class Food : MonoBehaviour
    {
        [SerializeField]
        private GameObject _snake;
        [SerializeField]
        private TrailRenderer _snakeTail;
        [SerializeField]
        private Vector3[] _snakeTailPointsPositions;

        [SerializeField]
        private GameManager _gameManager;

        private enum TypeOfFood { Apple = 1, Bonus };

        [SerializeField]
        private int _score;

        [SerializeField]
        private TypeOfFood _type;

        private void Start()
        {
            GetComponentsAndValues();
            CheckPosition();
        }

        public void FoodIsEaten()
		{
            switch (_type)
            {
                case TypeOfFood.Apple:
                    {
                        _gameManager.AddScore(_score);
                        Destroy(gameObject);
                        break;
                    }
                case TypeOfFood.Bonus:
                    {
                        _gameManager.AddScore(_score);
                        Destroy(gameObject);
                        break;
                    }
            }
        }

        private void GetComponentsAndValues()
		{
            _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

            _snake = GameObject.FindGameObjectWithTag("Player");
            _snakeTail = GameObject.FindGameObjectWithTag("SnakeTail").GetComponent<TrailRenderer>();

            Vector3[] pos = new Vector3[_snakeTail.positionCount];
            _snakeTail.GetPositions(pos);
            _snakeTailPointsPositions = pos;
        }
        private void CheckPosition()
		{
            if (_snakeTail.positionCount > 0)
            {
                for (int i = 0; i < _snakeTailPointsPositions.Length; i++)
                {
                    if (Vector3.Distance(transform.position, _snakeTailPointsPositions[i]) < 0.5f)
                    {
                        ResetPosition();
                    }
                }
            }
        }
		private void ResetPosition()
		{
            transform.position = new Vector2(Random.Range(-9f, 9f), Random.Range(-3f, 4f));
            CheckPosition();
        }
	}
}