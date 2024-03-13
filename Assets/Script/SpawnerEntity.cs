using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// Script responsible for creating new object.
    /// The script is attached to the spawn-entity
    /// </summary>

    [RequireComponent(typeof(DrawCircleArea))]
    public class SpawnerEntity : MonoBehaviour
    {
        public enum SpawnMode
        {
            Start,
            Loop,
        }

        // array prefab of the created object
        [SerializeField] private AIController [] entityPrefab;
        // zone of spawn
        [SerializeField] private DrawCircleArea area;
        // link to spawn mode
        [SerializeField] private SpawnMode spawnMode;
        // number of objects created
        [SerializeField] private int   numberOfCreatedObject;
        // respawn time
        [SerializeField] private float respawnTime;

        private AIPointerPatrol[] pointPatroll;
        public AIPointerPatrol [] PointPatroll => pointPatroll;

        private float timer;

        private void Start()
        {
            if (spawnMode == SpawnMode.Start) SpawnEntity();
            pointPatroll = GetComponentsInChildren<AIPointerPatrol>();
            timer = respawnTime;
        }

        private void OnValidate() { area = GetComponent<DrawCircleArea>(); }

        private void Update()
        {
            if(timer > 0) timer -= Time.deltaTime;

            if (spawnMode == SpawnMode.Loop && timer < 0)
            {
                SpawnEntity();
                timer = respawnTime;
            }
        }

        private void SpawnEntity()
        {
            for (int i = 0; i < numberOfCreatedObject; i++)
            {
                // picking a random value in an array 
                int index = Random.Range(0, entityPrefab.Length);

                // command to spawn selected random value in an array
                GameObject entity = Instantiate(entityPrefab[index].gameObject, transform);

                // spawn in a random zone
                entity.transform.position = area.GetRandonInsideZone();
            }
        }
    }
}