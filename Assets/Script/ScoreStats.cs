using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class ScoreStats : MonoBehaviour
    {
        [SerializeField] private Text text;

        private int lastScore;

        private void Update() { UpdateScore(); }

        private void UpdateScore()
        {
            // player presence check 
            if(Player.Instance != null)
            {
                int currentScore = Player.Instance.Score;
                
                if (lastScore != currentScore)
                {
                    lastScore = currentScore;
                    text.text = "Score: " + lastScore.ToString();
                }    
            }
        }
    }
}