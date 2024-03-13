using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// Script sinchronize the position of the object with the position of the target.
    /// </summary>
    public class SyncTransform : MonoBehaviour
    {
        [SerializeField] private Transform target;
        private void FixedUpdate() { transform.position = new Vector3(target.position.x, target.position.y, transform.position.z); }
    }
}