using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class ResultPanelController : SingletonBase<ResultPanelController>
    {
        [SerializeField] private Text numkill;
        [SerializeField] private Text score;
        [SerializeField] private Text gametime;
        [SerializeField] private Text result;
        [SerializeField] private Text buttonNextText;
        private bool isSuccess;

        private void Start() { gameObject.SetActive(false); }

        public void ShowResult(PlayerStatistic levelStatistics, bool success)
        {
            gameObject.SetActive(true);

            isSuccess = success;

            result.text         = success ? "Win" : "Lose";
            buttonNextText.text = success ? "Main menu" : "Restart";

            numkill.text  = "Kills: " + levelStatistics.Numkill. ToString();
            score.text    = "Score: " + levelStatistics.Score.   ToString();
            gametime.text = "Time: "  + levelStatistics.Gametime.ToString();
            
            Time.timeScale = 0;
        }

        public void OnButtonNextAction()
        {
            gameObject.SetActive(false);

            Time.timeScale = 1;

            if (isSuccess)
                LevelSequenceController.Instance.AdvanceLevel();
            else
                LevelSequenceController.Instance.RestartLevel();
        }
    }
}