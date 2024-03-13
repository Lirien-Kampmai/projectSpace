namespace SpaceShooter
{
    public class GlobalStat : SingletonBase<GlobalStat>
    {
        private int globalScore;
        public void StartStat() { globalScore += Player.Instance.Score; }
    }
}