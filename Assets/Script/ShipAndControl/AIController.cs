using System;
using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(SpaseShip))]
    public class AIController : MonoBehaviour
    {
        // тип поведения ИИ
        public enum AIBehaviour
        {
            Null,
            Patrol
        }

        [SerializeField] private AIBehaviour aIBehaviour;

        [SerializeField] private AIPointerPatrol[] pointPatrol;

        // скорость движения
        [Range(0.0f, 1.0f)]
        [SerializeField] private float navigationLinear;

        // скорость поворота
        [Range(0.0f, 1.0f)]
        [SerializeField] private float navigationAngular;

        // время упреждения выстрела
        [Range(0.0f, 5.0f)]
        [SerializeField] private float timePreemptiveShot;

        // каждые N секунд выбирается новая сулчайная точка
        [SerializeField] private float randomSelectMovePointTime;

        [SerializeField] private float findNewTargetTime;

        [SerializeField] private float shootDelay;

        // длинна рэйкаста
        [SerializeField] private float evadeRayLength;

        private Rigidbody2D rigitbodytarget;
        private Vector3 speed=> rigitbodytarget.velocity;

        private SpaseShip spaseShip;

        private SpawnerEntity spawnerEntity;

        private Vector3 movePosition;

        private Destructible selectedTarget;

        private Timer randomizeDirectionTimer;
        private Timer findNewTargetTimer;
        private Timer fireTimer;

        private const float MAX_ANGLE = 45.0f;

        private void Start()
        {
            InitTimer();
            spaseShip = GetComponent<SpaseShip>();
            spawnerEntity = GetComponentInParent<SpawnerEntity>();
            pointPatrol = spawnerEntity.PointPatroll;
            spaseShip.transform.SetParent(null);

        }

        private void Update()
        {
            UpdateTimer();
            UpdateAi();
        }

        private void UpdateAi()
        {
            if (aIBehaviour == AIBehaviour.Null)   SetPatrolBehaviour(pointPatrol);
            if (aIBehaviour == AIBehaviour.Patrol) UpdateAIBehaviourPatrol();
        }

        public void UpdateAIBehaviourPatrol()
        {
            ActionFindNewMovePosition();
            ActionControlShip();
            ActionFindNewAttackTarget();
            ActionFire();
            ActionEvadeCollision();
        }

        // метод стрельбы ИИ
        private void ActionFire()
        {
            if(selectedTarget != null)
            {
                if(fireTimer.IsFinished == true)
                {
                    spaseShip.Fire(TurretMode.Primary);
                    fireTimer.Start(shootDelay);
                }
            }
        }

        // метод поиска новой цели для атаки
        private void ActionFindNewAttackTarget()
        {

            if (findNewTargetTimer.IsFinished == true)
            {
                selectedTarget = FindNearestDestructibleTarget();
                findNewTargetTimer.Start(shootDelay);
            }
        }

        private Destructible FindNearestDestructibleTarget()
        {
            float maxDist = 50.0f;

            Destructible potentialTarget = null;

            foreach (var v in Destructible.AllDestructible)
            {
                if (v.GetComponent<SpaseShip>() == spaseShip) continue;
                if (v.TeamId == Destructible.TeamIdNeutral)   continue;
                if (v.TeamId == spaseShip.TeamId)             continue;

                float dist = Vector2.Distance(spaseShip.transform.position, v.transform.position);

                if (dist < maxDist)
                {
                    maxDist = dist;
                    potentialTarget = v;
                    rigitbodytarget = v.GetComponent<Rigidbody2D>();
                }
            }
            return potentialTarget;
        }

        // метод навигации ИИ
        private void ActionControlShip()
        {
            spaseShip.ThrustControl = navigationLinear;
            spaseShip.TorqueControl = ComputeAlliginTorqueNormalized(movePosition, spaseShip.transform) * navigationAngular;
        }

        private static float ComputeAlliginTorqueNormalized(Vector3 targetPosition, Transform ship)
        {
            // переводим позицию в локальные координаты
            Vector2 localTargetPosition = ship.InverseTransformPoint(targetPosition);

            // получаем угол между двумя векторами
            float angle = Vector3.SignedAngle(localTargetPosition, Vector3.up, Vector3.forward);

            // ограничиваем полученый угол до максимума в -45/45 и нормализуем его делением на 45
            // при максимальном угле корабль поворачиваем максимально быстро
            angle = Mathf.Clamp(angle, -MAX_ANGLE, MAX_ANGLE) / MAX_ANGLE;

            return -angle;
        }

        private void ActionFindNewMovePosition()
        {
            if (aIBehaviour == AIBehaviour.Patrol)
            {
                if (selectedTarget != null)
                    movePosition = selectedTarget.transform.position + (speed * timePreemptiveShot);
                else
                {
                    int i = UnityEngine.Random.Range(0, pointPatrol.Length);
                    //
                    if (pointPatrol[i] != null)
                    {
                        // внутри зоны патрулирования
                        bool isInsidePatrolZone = (pointPatrol[i].transform.position - transform.position).sqrMagnitude < pointPatrol[i].Radius * pointPatrol[i].Radius;

                        if (isInsidePatrolZone == true)
                        {
                            if (randomizeDirectionTimer.IsFinished == true)
                            {
                                Vector2 newPoint = (UnityEngine.Random.onUnitSphere * pointPatrol[i].Radius) + pointPatrol[i].transform.position;
                                movePosition = newPoint;
                                randomizeDirectionTimer.Start(randomSelectMovePointTime);
                            }
                        }
                        else
                            movePosition = pointPatrol[i].transform.position;
                    }
                }
            }
        }

        // дефолтный уклонятор вправо от препятствия
        private void ActionEvadeCollision()
        {
            if (Physics2D.Raycast(transform.position, transform.up, evadeRayLength) == true)
                movePosition = transform.position + (transform.right * 100.0f);
        }


        #region Timer
        // инициализация таймера
        private void InitTimer ()
        {
            randomizeDirectionTimer = new Timer(randomSelectMovePointTime);
            fireTimer = new Timer(shootDelay);
            findNewTargetTimer = new Timer(findNewTargetTime);
        }

        // обновление таймера
        private void UpdateTimer()
        {
            randomizeDirectionTimer.RemoveTime(Time.deltaTime);
            fireTimer.RemoveTime(Time.deltaTime);
            findNewTargetTimer.RemoveTime(Time.deltaTime);
        }
        #endregion

        public void SetPatrolBehaviour(AIPointerPatrol[] point)
        {
            aIBehaviour = AIBehaviour.Patrol;
            pointPatrol = point;
        }
    }
}