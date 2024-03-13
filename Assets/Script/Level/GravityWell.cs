using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// Script, responsible for the strength and radius of gravity of an entity.
    /// The script is attached to the entity, which should have its own gravity.
    /// </summary>
    [RequireComponent(typeof(CircleCollider2D))]
    public class GravityWell : MonoBehaviour
    {
        // strength gravity
        [SerializeField] private float force;
        // radius gravity
        [SerializeField] private float radius;

        private void OnTriggerStay2D(Collider2D collision)
        { 
            // check for null
            if (collision.attachedRigidbody == null) return;

            // distance from collision to transform
            Vector2 dir = transform.position - collision.transform.position;

            // set distance
            float dist = dir.magnitude;

            if (dist < radius)
            {
                // set the force of attraction, the closer the stronger it is (dist / m_Radius)
                Vector2 forceVector = dir.normalized * force * (dist / radius);
                // applying gravity to a body
                collision.attachedRigidbody.AddForce(forceVector, ForceMode2D.Force);
            }
        }

        // it is better not to include in the build because of possible errors
#if UNITY_EDITOR
        private void OnValidate() { GetComponent<CircleCollider2D>().radius = radius; }
#endif
    }
}