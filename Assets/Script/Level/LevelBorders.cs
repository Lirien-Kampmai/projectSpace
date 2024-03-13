using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// Singlton script responsible for determining the boundaries of the level.
    /// The script is attached to the borders entity.
    /// </summary>
    public class LevelBorders : SingletonBase<LevelBorders>
    {
       
        [SerializeField] private float radius;
        public float Radius => radius;

        public enum Mode
        {
            Limit,
            Teleport,
            Death,
        }

        [SerializeField] private Mode limitMode;
        public Mode LimitMode => limitMode;

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            UnityEditor.Handles.color = Color.red;
            UnityEditor.Handles.DrawWireDisc(transform.position, transform.forward, radius);
        }
#endif
    }
}