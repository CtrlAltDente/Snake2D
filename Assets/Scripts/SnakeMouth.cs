using System.Collections;
using UnityEngine;

namespace Snake
{
    public class SnakeMouth : MonoBehaviour
    {
        [SerializeField]
        private float _growing;

        [SerializeField]
        private float BonusTime;

        private enum Bonus { SlowTime = 1, GrowUp, SpeedUp };

        [SerializeField]
        private SnakeTail _snakeTail;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log("Trigger!");

            switch (collision.gameObject.tag)
            {
                case "Border":
                    {
                        Debug.Log("Lose");
                        break;
                    }

                case "Apple":
                    {
                        Destroy(collision.gameObject);
                        EatApple();
                        break;
                    }
                case "Bonus_GrowUp":
                    {
                        Destroy(collision.gameObject);
                        EatBonus(Bonus.GrowUp);
                        break;
                    }
                case "Bonus_SpeedUp":
                    {
                        Destroy(collision.gameObject);
                        EatBonus(Bonus.SpeedUp);
                        break;
                    }
                case "Bonus_SlowTime":
                    {
                        Destroy(collision.gameObject);
                        EatBonus(Bonus.SlowTime);
                        break;
                    }
            }
        }

        public void EatApple()
        {
            _snakeTail.GrowingUp(_growing);
        }

        private void EatBonus(Bonus bonus)
        {
            switch (bonus)
            {
                case Bonus.GrowUp:
                    {
                        EatGrowUpBonus();
                        break;
                    }
                case Bonus.SlowTime:
                    {
                        EatSlowTimeBonus();
                        break;
                    }
                case Bonus.SpeedUp:
                    {
                        EatSpeedUpBonus();
                        break;
                    }
            }
        }

        private void EatGrowUpBonus()
        {
            _snakeTail.GrowingUp(_growing * 4);
        }

        private void EatSlowTimeBonus()
        {
            _snakeTail.GrowingUp(_growing);
            if (Time.timeScale > 0.6f)
            {
                Time.timeScale -= 0.2f;
            }
        }

        private void EatSpeedUpBonus()
        {
            _snakeTail.GrowingUp(_growing);
            Time.timeScale += 0.2f;
        }
    }
}