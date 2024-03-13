using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// Script is responsible for add ammo and energy when lifting powerup.
    /// The script is attached to the powerup entity.
    /// </summary>
    public class PowerUpStats : PowerUp
    {
        public enum EffectType
        {
            AddAmmo,
            AddEnergy,
            AddSpeed,
            AddIncreasedVulnerability,
        }

        [SerializeField] private EffectType effectType;
        [SerializeField] private float value;

        protected override void OnPicketUp(SpaseShip ship)
        {
            if (effectType == EffectType.AddAmmo)   ship.AddAmmo  ((int)value);
            if (effectType == EffectType.AddEnergy) ship.AddEnergy((int)value);
            if (effectType == EffectType.AddSpeed)  ship.AddSpeed      (value);
            if (effectType == EffectType.AddIncreasedVulnerability) ship.AddIndestructible(true);
        }
    }
}