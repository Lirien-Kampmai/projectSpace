using UnityEngine;

namespace SpaceShooter
{
    public class Lifetime : MonoBehaviour
    {
        [SerializeField] private float lifeTime;
        private float timer;

        void Update()
        {
            if (timer < lifeTime)
                timer += Time.deltaTime;
            else
                Destroy(gameObject);
        }
    }
}