using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Foods;
using Bonuses;

namespace Managers
{
    public class SpawnManager : MonoBehaviour
    {
        [SerializeField]
        private GameManager _gameManager;

        [SerializeField]
        private GameObject _applePrefab;
        private GameObject _appleObject;

        [SerializeField]
        private GameObject _slowTimePrefab;
        [SerializeField]
        private GameObject _growUpPrefab;
        [SerializeField]
        private GameObject _speedUpPrefab;

        private GameObject _bonusObject;

        [SerializeField]
        private SliderValueManager _delayBetweenBonusSlider;
        private float _delayBetweenBonuses;

        [SerializeField]
        private SliderValueManager _bonusScorePointsSlider;

        private float _timeToCreateBonus;

        private bool _startCreatingBonus = false;

        BonusTypes _bonusType = new BonusTypes();

        private float _minPosY = -4f, _minPosX = -9f, _maxPosY = 4f, _maxPosX = 9f;

        private enum Bonus : int { SlowTime = 1, GrowUp = 2, SpeedUp = 3 };

        private void Start()
        {
            _gameManager.ClearEvent += ClearAll;
        }

        private void Update()
        {
            SpawnBonus();
        }

        private void ClearAll()
        {
            Destroy(_appleObject);
            Destroy(_bonusObject);
        }

        public void StartGame()
        {
            SetDelayBetweenBonuses();
            SpawnApple();
        }

        private void SetDelayBetweenBonuses()
        {
            _delayBetweenBonuses = _delayBetweenBonusSlider.GetValue();
            _timeToCreateBonus = _delayBetweenBonuses;
        }

        private void SpawnApple()
        {
            if (_gameManager.IsGameActive())
            {
                Vector2 pos = new Vector2(Random.Range(-9f, 9f), Random.Range(-3f, 4f));
                _appleObject = Instantiate(_applePrefab, pos, Quaternion.identity);
                _appleObject.GetComponent<Food>().SpawnEvent += SpawnApple;
            }
        }

        private void SpawnBonus()
        {
            if (_gameManager.IsGameActive())
            {
                if (BonusDontFinded())
                {
                    if (_timeToCreateBonus >= 0)
                    {
                        _timeToCreateBonus -= Time.deltaTime / Time.timeScale;
                    }
                    else
                    {
                        Vector2 pos = new Vector2(Random.Range(_minPosX, _maxPosX), Random.Range(_minPosY, _maxPosY));
                        System.Random rnd = new System.Random();

                        _bonusType.SetBonusType(Random.Range(1, 4));

                        switch (_bonusType.GetBonusType())
                        {
                            case BonusTypes.Bonus.GrowUp:
                                {
                                    _bonusObject = Instantiate(_growUpPrefab, pos, Quaternion.identity);
                                    break;
                                }
                            case BonusTypes.Bonus.SlowTime:
                                {
                                    _bonusObject = Instantiate(_slowTimePrefab, pos, Quaternion.identity);
                                    break;
                                }
                            case BonusTypes.Bonus.SpeedUp:
                                {
                                    _bonusObject = Instantiate(_speedUpPrefab, pos, Quaternion.identity);
                                    break;
                                }
                        }
                        _bonusObject.GetComponent<Food>().SpawnEvent += SpawnBonus;
                        _bonusObject.GetComponent<Food>().SetBonusScore(_bonusScorePointsSlider.GetValue());
                        _startCreatingBonus = false;
                    }
                }
            }
        }

        private bool BonusDontFinded()
        {
            if (_bonusObject == null)
            {
                if (!_startCreatingBonus)
                {
                    SetDelayBetweenBonuses();
                    _startCreatingBonus = true;
                }
                return true;
            }
            return false;
        }
    }
}