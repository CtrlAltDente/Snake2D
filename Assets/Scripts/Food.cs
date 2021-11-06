using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;

namespace Foods
{
    public class Food : MonoBehaviour
    {
        [SerializeField]
        private GameManager _gameManager;

        private enum TypeOfFood { Apple = 1, Bonus };

        [SerializeField]
        private int _score;

        [SerializeField]
        private TypeOfFood _type;

        private void Start()
        {
            _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
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
    }
}