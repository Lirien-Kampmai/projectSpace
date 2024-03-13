using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class AIPointerPatrol : MonoBehaviour
    {
        [SerializeField] private float radius;
        public float Radius => radius;

        private static readonly Color GizmosColor = Color.magenta;

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = GizmosColor;
            Gizmos.DrawSphere(transform.position, radius);
        }

        // лист со всеми обьектами
        private static HashSet<AIPointerPatrol> allAIPointerPatrol;
        public static IReadOnlyCollection<AIPointerPatrol> AllAIPointerPatrol => allAIPointerPatrol;
    }
}