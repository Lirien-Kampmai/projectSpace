using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// Script responsible for controlling the camera and linking it to the player.
    /// The script is attached to the controller entity.
    /// </summary>
    public class CameraController : MonoBehaviour
    {
        // target camera
        [SerializeField] private Camera camera;
        // target entity
        [SerializeField] private Transform target;

        // linear interpolation
        [SerializeField] private float interpolationLinear;
        // angular interpolation
        [SerializeField] private float interpolationAngular;
        // set position camera by Z
        [SerializeField] private float cameraZOffset;
        // camera deviation from tracking target
        [SerializeField] private float forwardOffset;

        private void FixedUpdate()
        {
            // check for null
            if (camera == null || target == null) return;

            // start position camera
            Vector2 camPositipn = camera.transform.position;

            // finish position camera
            Vector2 targetPosition = target.position + (target.transform.up * forwardOffset);

            // calculate new position camera (interpolation)
            Vector2 newCamPosition = Vector2.Lerp(camPositipn, targetPosition, interpolationLinear * Time.deltaTime);

            // move new position camera
            camera.transform.position = new Vector3(newCamPosition.x, newCamPosition.y, cameraZOffset);

            // camera rotation
            if (interpolationAngular > 0)
                camera.transform.rotation = Quaternion.Slerp(camera.transform.rotation,
                                            target.rotation, interpolationAngular * Time.deltaTime);
        }

        // sets the target when the entity changes. Used in other scripts.
        public void SetTarget(Transform newTarget) { target = newTarget; }
    }
}