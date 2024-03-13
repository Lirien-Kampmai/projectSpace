using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// Singleton script responsible for the player entity.
    /// </summary>
    public class Player : SingletonBase<Player>
    {
        #region Player
        // amount life
        [SerializeField] private int amountLife;
        // link to ship
        [SerializeField] private SpaseShip ship;
        // prefab player ship
        [SerializeField] private GameObject prefabSpaseShip;
        public SpaseShip ActiveShip => ship;

        [SerializeField] private CameraController   cameraController;
        [SerializeField] private MovementController movementController;

        private Vector3 StartPosition;

        private void Start() { Respawn(); }

        private void OnShipDeath()
        {
            amountLife--;
            if (amountLife > 0)
                Respawn();
            else
                LevelSequenceController.Instance.FinishCurrentLevel(false);
        }

        private void Respawn()
        {
            if(LevelSequenceController.PlayerShip != null)
            {
                var newPlayerShip = Instantiate(LevelSequenceController.PlayerShip);
                ship = newPlayerShip.GetComponent<SpaseShip>();
                cameraController.SetTarget(ship.transform);
                movementController.SetTargetShip(ship);
                ship.EventOnDeath.AddListener(OnShipDeath); 
            }
        }
        #endregion
        #region Score
        public int Score   { get; private set; }
        public int NumKill { get; private set; }
        public void AddKill()           { NumKill++; }
        public void AddScore(int score) { Score += score; }
        #endregion
        #region
        protected override void Awake()
        {
            base.Awake();
            if (ship != null)
                Destroy(ship.gameObject);
        }
        #endregion
    }
}