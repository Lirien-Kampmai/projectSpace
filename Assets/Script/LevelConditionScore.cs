using UnityEngine;

namespace SpaceShooter
{
    public class LevelConditionScore : MonoBehaviour, LevelCondition
    {
        [SerializeField] private int score;
        // переменная для установки достижения
        private bool riched;

        bool LevelCondition.IsComplited
        {
            get
            {
                if(Player.Instance != null && Player.Instance.ActiveShip != null)
                    if (Player.Instance.Score >= score)
                        riched = true;
                return riched;
            }
        }
    }
}