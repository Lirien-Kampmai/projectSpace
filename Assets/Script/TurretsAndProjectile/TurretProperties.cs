using UnityEngine;

namespace SpaceShooter
{
    // fire mode weapon
    public enum TurretMode
    {
        Primary,
        Secondary
    }

    /// <summary>
    /// Script is responsible for creating scripted asset for the turrets.
    /// </summary>
    [CreateAssetMenu]
    public sealed class TurretProperties : ScriptableObject
    {
        // select mod weapon
        [SerializeField] private TurretMode mode;
        public TurretMode Mode => mode;

        // link to prefab roket
        [SerializeField] private Projectile projectilePrefab;
        public Projectile ProjectilePrefab => projectilePrefab;

        // rate of fire turret
        [SerializeField] private float rateOfFire;
        public float RateOfFire => rateOfFire;

        // energy per sec for fire
        [SerializeField] private int enegyUseWeapon;
        public int EnegyUseWeapon => enegyUseWeapon;

        // ammo per sec for fire
        [SerializeField] private int ammoUseWeapon;
        public int AmmoUseWeapon => ammoUseWeapon;

        // sound fire
        [SerializeField] private AudioClip launchSFX;
        public AudioClip LaunchSFX => launchSFX;
    }
}