namespace Platformer.Interfaces
{
    public interface IScoring<T>
    {
        public delegate void UpdateUIScore(int score);
        public event UpdateUIScore OnUpdateUIScore;
        void SetScore(T playerScored);
    }
}