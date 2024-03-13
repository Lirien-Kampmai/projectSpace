using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SpaceShooter
{
    /// <summary>
    /// Drawing a visual circle.
    /// Script is bound to any object to whitch you want to draw the circle.
    /// </summary>
    public class DrawCircleArea : MonoBehaviour
    {
        [SerializeField] private float radius;
        public float Radius => radius;

        public Vector2 GetRandonInsideZone() { return (Vector2)transform.position + (Vector2)UnityEngine.Random.insideUnitSphere * radius; }


#if UNITY_EDITOR
        // set color
        private static Color GizmosColor = Color.green;
        private void OnDrawGizmos()
        {
            Handles.color = GizmosColor;
            // draw gizmos to color
            Handles.DrawWireDisc(transform.position, transform.forward, radius);
        }
#endif
    }
} 