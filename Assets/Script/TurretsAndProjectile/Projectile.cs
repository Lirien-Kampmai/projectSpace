using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// Script responsible for projectile behavior.
    /// The scripts is attached to projectile entities.
    /// </summary>
    public class Projectile : MonoBehaviour
    {
        // speed projectile
        [SerializeField] private float velocityProjectile;
        // damage projectile
        [SerializeField] private int   damageProjectile;
        // lifetime projectile
        [SerializeField] private float lifetime;
        [SerializeField] private Lifetime lifetimePrefab;

        private float timer;
        private Destructible parent;

        private void Update()
        {
            #region formula for calculating the direction of the projectile
            float stepLenght = Time.deltaTime * velocityProjectile;
            Vector2 step = transform.up * stepLenght;
            #endregion

            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, stepLenght);
            if (hit)
            {
                Destructible destr = hit.collider.transform.root.GetComponent<Destructible>();
                if(destr != null && destr != parent)
                {
                    destr.ApplyDamage(damageProjectile);

                    // checking belonging to the player ship
                    if(parent == Player.Instance.ActiveShip)
                    {
                        // add a point if the enemy ship is destroyed
                        Player.Instance.AddScore(destr.ScoreValue);
                        Player.Instance.AddKill();
                    }
                }
                // method is called when a raycast hits
                OnProjectileLifeEnd(hit.collider, hit.point);
            }

            #region self destroy timer
            timer += Time.deltaTime;
            if (timer > lifetime)
                Destroy(gameObject);
            #endregion

            // projectile direction movement
            transform.position += new Vector3(step.x, step.y, 0);
        }

        private void OnProjectileLifeEnd(Collider2D collider, Vector2 position) { Destroy(gameObject); }
        public void SetParentShooter(Destructible parent) { this.parent = parent; }
    }
}