using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class PlayerShipSelectionController : MonoBehaviour
    {
        [SerializeField] private SpaseShip prefab;

        [SerializeField] private Text  shipname;
        [SerializeField] private Text  hitpoints;
        [SerializeField] private Text  speed;
        [SerializeField] private Text  agility;
        [SerializeField] private Image preview;
        [SerializeField] private GameObject spaceShipSelectPanel;

        private void Start()
        {
            if (prefab != null)
            {
                shipname.text  = prefab.Nickname;
                hitpoints.text = "HP : " +      prefab.HitPoints.ToString();
                speed.text     = "Speed : " +   prefab.MaxLinearVelocity.ToString();
                agility.text   = "Agility : " + prefab.MaxAngularVelocity.ToString();
                preview.sprite = prefab.PreviewImage;
            }
        }

        public void OnSelectShip()
        {
            LevelSequenceController.PlayerShip = prefab;
            MainMenuController.Instance.gameObject.SetActive(true);
            spaceShipSelectPanel.SetActive(false);
        }
    }
}