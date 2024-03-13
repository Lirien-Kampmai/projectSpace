using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// The base script of an entity, required for inheritance by other scripts.
    /// </summary>
    public abstract class Entity : MonoBehaviour
    {
        // object name for user
        [SerializeField] private string nickname;
        public string Nickname => nickname;
    }
}