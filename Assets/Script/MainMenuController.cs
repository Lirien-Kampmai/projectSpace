using UnityEngine;

namespace SpaceShooter
{
    public class MainMenuController : SingletonBase<MainMenuController>
    {
        [SerializeField] private SpaseShip       defaultSpaseShip;
        [SerializeField] private GameObject      episodeSelect;
        [SerializeField] private GameObject      shipSelect;
        [SerializeField] private PlayerStatistic playerStatistic;

        private void Start() { LevelSequenceController.PlayerShip = defaultSpaseShip; }

        public void OnButtonStartNew()
        {
            episodeSelect.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }

        public void OnSelectShip()
        {
            shipSelect.SetActive(true);
            gameObject.SetActive(false);
        }

        public void OnButtonExit() { Application.Quit(); }
    }
}