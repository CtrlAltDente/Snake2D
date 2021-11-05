using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;

public class Food : MonoBehaviour
{
    [SerializeField]
    private GameManager _gameManager;

    private enum TypeOfFood {Apple = 1,Bonus};

    [SerializeField]
    private int _appleScore;
    [SerializeField]
    private int _bonusScore;

    [SerializeField]
    private TypeOfFood _type;

    private void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnDestroy()
    {
        if (GameObject.Find("Snake(Clone)"))
        {
            switch (_type)
            {
                case TypeOfFood.Apple:
                    {
                        _gameManager.AddScore(_appleScore);
                        break;
                    }
                case TypeOfFood.Bonus:
                    {
                        _gameManager.AddScore(_bonusScore);
                        break;
                    }
            }
        }
    }
}
