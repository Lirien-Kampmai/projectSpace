using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter
{
    /// <summary>
    /// The script is responsible for the ability to deal damage.
    /// The scripts is not attached to entities, but interacts with other scripts.
    /// </summary>
    public class Destructible : Entity
    {
        #region Destructible
        // ignore damage
        [SerializeField] private bool isIndestructible;
        // start hitpoint
        [SerializeField] private int startHitPoints;
        // current hitpoint
        private int currentHitPoints;

        [SerializeField] private float deBoostDuration;

        [SerializeField] private GameObject explosionPrefab;

        public bool IsIndestructible => isIndestructible;
        public int HitPoints => currentHitPoints;

        private void Awake() { currentHitPoints = startHitPoints; }

        protected virtual void Start()
        {
            currentHitPoints = startHitPoints;
        }

        // set damage to object
        public void ApplyDamage(int damage)
        {
            if (isIndestructible) return;
            currentHitPoints -= damage;
            if (currentHitPoints <= 0) Kill();
        }

        public void AddIndestructible(bool add) { isIndestructible = add; }

        // redefining the event when the object is destroyed
        protected virtual void Kill()
        {
            if (explosionPrefab != null) Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
            eventOnDeath?.Invoke();
        }

        [SerializeField] private UnityEvent eventOnDeath;
        public UnityEvent EventOnDeath => eventOnDeath;

        // лист со всеми уничтожаемыми обьектами
        private static HashSet<Destructible> allDestructible;
        public static IReadOnlyCollection<Destructible> AllDestructible => allDestructible;

        protected virtual void OnEnable()
        {
            if (allDestructible == null)
                allDestructible = new HashSet<Destructible>();

            allDestructible.Add(this);
        }

        protected virtual void OnDestroy() { allDestructible.Remove(this); }
        #endregion

        #region TeamId
        public const int TeamIdNeutral = 0;

        [SerializeField] private int teamId;
        public int TeamId => teamId;
        #endregion

        #region Score
        [SerializeField] private int scoreValue;
        public int ScoreValue => scoreValue;
        #endregion
    }
}


