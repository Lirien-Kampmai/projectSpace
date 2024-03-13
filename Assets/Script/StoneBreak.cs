using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(DrawCircleArea))]
    public class StoneBreak : MonoBehaviour
    {
        [SerializeField] private Destructible [] debtisPrefab;
        [SerializeField] private uint maxDebrisGenerate;
        [SerializeField] private uint minDebrisGenerate;
        private DrawCircleArea radius;
        private Vector2 velocity;

#if UNITY_EDITOR
        private void OnValidate() { radius = GetComponent<DrawCircleArea>(); }
#endif

        private void OnDestroy()
        {
            minDebrisGenerate = 0;
            int spanwnvalue = (int)Random.Range(minDebrisGenerate, maxDebrisGenerate);
            for(int i = 0; i < spanwnvalue; i++) SpawnStone();
        }

        private void SpawnStone()
        {
            int index = Random.Range(0, debtisPrefab.Length);
            
            GameObject debris = Instantiate(debtisPrefab[index].gameObject, transform.parent);
            debris.transform.position = radius.GetRandonInsideZone();
            debris.transform.Rotate(new Vector3(0, 0, Random.Range (-20, 20) ));
            

            #region random number generation except for 0 
            int number = 0;
            int newNumber;
            do
                newNumber = Random.Range(-4, 4);
            while (number == newNumber);

            number = newNumber;
            #endregion


            velocity = new Vector2(number, number);

            Rigidbody2D rigidbody2D = debris.GetComponent<Rigidbody2D>();          
            if (rigidbody2D != null) rigidbody2D.AddRelativeForce(velocity, ForceMode2D.Impulse);
        }
    }
}