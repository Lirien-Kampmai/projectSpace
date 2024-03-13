using System;

namespace SpaceShooter
{
    [Serializable]
    public class Timer
    {
        private float currentTime;

        public bool IsFinished => currentTime <= 0;

        public Timer(float startTime) { Start(startTime); }

        public void Start(float startTimer) { currentTime = startTimer; }

        public void RemoveTime(float deltaTime)
        {
            if (currentTime <= 0) return;
            currentTime -= deltaTime;
        }
    }
}