using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// Script responsible for the behavior of the ship, its shooting and indicators
    /// </summary>
    [RequireComponent (typeof (Rigidbody2D))]
    public class SpaseShip : Destructible
    {
        #region Properties
        // mass rigidbody
        [Header("Space Ship")]
        [SerializeField] private float mass;
        // forward thrust
        [SerializeField] private float thrust;
        // torque force
        [SerializeField] private float mobility;
        // max linear speed
        [SerializeField] private float maxLinearVelocity;
        public float MaxLinearVelocity => maxLinearVelocity;
        public float currentLinearVelocity;

        // max rotation speed. degrees per second
        [SerializeField] private float maxAngularVelocity;
        public float MaxAngularVelocity => maxAngularVelocity;
        [SerializeField] private Sprite previewImage;
        public Sprite PreviewImage => previewImage;
        [SerializeField] private Turret[] turrets;
        [SerializeField] private float boostDuration;
        private float timer;
        private Rigidbody2D rigidbody2D;
        #endregion

        #region API
        // linear thrust control -1.0 to 1.0
        public float ThrustControl { get; set; }
        // rotation control -1.0 to 1.0
        public float TorqueControl { get; set; }
        #endregion

        #region Event
        protected override void Start()
        {
            base.Start();
            rigidbody2D = GetComponent<Rigidbody2D>();
            rigidbody2D.mass = mass;
            rigidbody2D.inertia = 1;
            InitOffensive();
        }

        private void FixedUpdate()
        {
            UpdateRigitbody();
            UpdateEnergy();
            if (timer < boostDuration)
                timer += Time.fixedDeltaTime;
            else
            {
                currentLinearVelocity = maxLinearVelocity;
                AddIndestructible(false);
                timer = 0;
            }
        }

        private void UpdateRigitbody()
        {
            rigidbody2D.AddForce(thrust * ThrustControl * transform.up * Time.fixedDeltaTime, ForceMode2D.Force);
            rigidbody2D.AddForce(-rigidbody2D.velocity * (thrust / currentLinearVelocity) * Time.fixedDeltaTime, ForceMode2D.Force);

            rigidbody2D.AddTorque(TorqueControl * mobility * Time.fixedDeltaTime, ForceMode2D.Force);
            rigidbody2D.AddTorque(-rigidbody2D.angularVelocity * (mobility / maxAngularVelocity) * Time.fixedDeltaTime, ForceMode2D.Force);
        }
        #endregion

        // method responsible for firing from an array of turrets
        public void Fire(TurretMode mode)
        {
            for (int i = 0; i < turrets.Length; i++)
                if (turrets[i].Mode == mode) turrets[i].Fire();
        }

        #region ammo and energy value
        [SerializeField] private int maxEnergy;
        [SerializeField] private int maxAmmo;
        [SerializeField] private int energyRegenPerSec;
        #endregion

        private float currentEnergy;
        private int   currentAmmo;

        public void AddEnergy(int energy)     { currentEnergy = Mathf.Clamp(currentEnergy + energy, 0, maxEnergy); }
        public void AddAmmo  (int ammo)       { currentAmmo = Mathf.Clamp(currentAmmo + ammo, 0, maxAmmo); }
        public void AddSpeed (float velocity) { currentLinearVelocity = Mathf.Clamp(currentLinearVelocity + velocity, 0, float.MaxValue); }

        // initialization at start to the maximum ammo, energy and thrust value
        private void InitOffensive()
        {
            currentEnergy = maxEnergy;
            currentAmmo = maxAmmo;
            currentLinearVelocity = maxLinearVelocity;
        }

        // regeneration energy
        private void UpdateEnergy()
        {
            currentEnergy += (float) energyRegenPerSec * Time.fixedDeltaTime;
            currentEnergy = Mathf.Clamp(currentEnergy, 0, maxEnergy);
        }

        // consumes energy
        public bool DrawEnergy(int count)
        {
            if (count == 0)
                return true;

            if (currentEnergy >= count)
            {
                currentEnergy -= count;
                return true;
            }
            return false;
        }

        // consumes ammo
        public bool DrawAmmo(int count)
        {
            if (count == 0)
                return true;

            if (currentAmmo >= count)
            {
                currentAmmo -= count;
                return true;
            }
            return false;
        }

        // add a turret by working on an array of turrets
        public void AssightWeapon(TurretProperties properties)
        {
            for (int i = 0; i < turrets.Length; i++)
                turrets[i].AssightLoadout(properties);
        }
    }
}