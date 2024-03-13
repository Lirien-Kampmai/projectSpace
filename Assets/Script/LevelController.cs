using UnityEngine;
using UnityEngine.Events;
namespace SpaceShooter
{
    public interface LevelCondition { bool IsComplited { get; } }

    public class LevelController : SingletonBase<LevelController>
    {
        // время за которое начисляются очки
        [SerializeField] private int referenceTime;
        public int ReferenceTime => referenceTime;

        // эвент при завершении уровня
        [SerializeField] private UnityEvent eventLevelComplited;

        private LevelCondition[] conditions;

        private bool  isLevelComplited;
        private float levelTime;
        public float LevelTime => levelTime;

        private void Start() { conditions = GetComponentsInChildren<LevelCondition>(); }

        private void Update()
        {
            if(!isLevelComplited)
            {
                levelTime += Time.deltaTime;
                CheckLevelCondition();
            }
        }

        private void CheckLevelCondition()
        {
            if (conditions == null || conditions.Length == 0) return;

            int numCompleted = 0;

            foreach(var v in conditions)
                if (v.IsComplited) numCompleted++;

            if (numCompleted == conditions.Length)
            {
                isLevelComplited = true;
                eventLevelComplited?.Invoke();
                LevelSequenceController.Instance.FinishCurrentLevel(true);
            }
        }
    }
}