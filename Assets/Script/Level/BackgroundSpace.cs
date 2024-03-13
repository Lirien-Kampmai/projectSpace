using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// The script is responsible for adjusting the background parallax effect.
    /// The script is attached to the background entity.
    /// </summary>

    [RequireComponent (typeof(MeshRenderer))]
    public class BackgroundSpace : MonoBehaviour
    {
        // power parallax effect
        [Range(0.0f, 4.0f)]
        [SerializeField] private float parallaxPower;
        // scale texture background
        [SerializeField] private float textureScale;

        // link to material
        private Material starsMaterial;
        // initialization point
        private Vector2  initialOffset;

        private void Start()
        {
            // get link to material
            starsMaterial = GetComponent<MeshRenderer>().material;
            // set random point inside or on a circle with radius 1.0f
            initialOffset = UnityEngine.Random.insideUnitCircle;
            // main texture scale
            starsMaterial.mainTextureScale = Vector2.one * textureScale;
        }

        private void Update()
        {
            Vector2 offset = initialOffset;

            offset.x += transform.position.x / transform.localScale.x / parallaxPower;
            offset.y += transform.position.y / transform.localScale.y / parallaxPower;

            starsMaterial.mainTextureOffset = offset;
        }
    }
}