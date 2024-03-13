using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// Script is responsible for add weapon when lifting powerup.
    /// The script is attached to the powerup entity.
    /// </summary>
    public class PowerUpWeapon : PowerUp
    {
        [SerializeField] private TurretProperties properties;

        protected override void OnPicketUp(SpaseShip ship) { ship.AssightWeapon(properties); }
    }
}