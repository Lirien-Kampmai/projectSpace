using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// Script responsible for firing turrets and changes weapons.
    /// The scripts is attached to turret-entities.
    /// Works in conjunction with a script "TurretProperties".
    /// </summary>
    public class Turret : MonoBehaviour
    {
        // selected turret mode
        [SerializeField] TurretMode mode;
        public TurretMode Mode => mode;

        // link to scriptableobject asset
        [SerializeField] private TurretProperties turretProperties;

        private float refireTime;

        public bool CanFire => refireTime <= 0;

        private SpaseShip ship;

        private void Start()  { ship = transform.root.GetComponent<SpaseShip>(); }

        private void Update() { if (refireTime > 0) refireTime -= Time.deltaTime; }

        public void Fire()
        {
            #region possible fire check
            if (ship.DrawEnergy(turretProperties.EnegyUseWeapon) == false)   return;
            if (ship.DrawAmmo(turretProperties.AmmoUseWeapon)    == false)   return;
            if (turretProperties == null)                                    return;
            if (refireTime > 0)                                              return;
            #endregion

            // create projectile
            Projectile projectile = Instantiate(turretProperties.ProjectilePrefab).GetComponent<Projectile>();
            //set position
            projectile.transform.position = transform.position;
            //set rotation
            projectile.transform.up = transform.up;
            // set parent
            projectile.SetParentShooter(ship);
            // set rate of fire 
            refireTime = turretProperties.RateOfFire;

            // SFX

        }

        // method that changes weapons on pickup
        public void AssightLoadout(TurretProperties props)
        {
            if (mode != props.Mode) return;
            refireTime = 0;
            turretProperties = props;
        }
    }
}