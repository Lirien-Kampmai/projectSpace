using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// Script responsible for creating new debris object.
    /// The script is attached to the spawn-entity
    /// </summary>
    [RequireComponent(typeof(DrawCircleArea))]
    public class SpawnerDebris : MonoBehaviour
    {
        // array prefab of the created debtis object
        [SerializeField] private Destructible[] debtisPrefab;
        // zone of spawn
        private DrawCircleArea area;
        // number of objects debris created
        [SerializeField] private int numberOfCreatedDebris;
        // debris flight speed
        private float randomSpeed;

        private void Start()
        {
            for(int i = 0; i < numberOfCreatedDebris; i++)
                SpawnDebris();
        }

        private void OnValidate() { area = GetComponent<DrawCircleArea>(); }

        private void SpawnDebris()
        {
            // picking a random value in an array 
            int index = Random.Range(0, debtisPrefab.Length);

            // command to spawn selected random value in an array
            GameObject debris = Instantiate(debtisPrefab[index].gameObject);

            debris.transform.position = area.GetRandonInsideZone();

            // when trash is destroyed, we subscribe to an event that the trash spawner restart
            debris.GetComponent<Destructible>().EventOnDeath.AddListener(OnDebrisDead);

            // we get the rigidbody2D and set the direction of movement and speed
            Rigidbody2D rigidbody2D = debris.GetComponent<Rigidbody2D>();
            randomSpeed = Random.Range(0, 3);
            if (rigidbody2D != null && randomSpeed > 0)
                rigidbody2D.velocity = UnityEngine.Random.insideUnitCircle * randomSpeed;
        }

        private void OnDebrisDead() { SpawnDebris(); }
    }
}