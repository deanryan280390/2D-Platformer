using Platformer.Interfaces;
using UnityEngine;

namespace Platformer
{
    public class EnemyDamage : MonoBehaviour, IScoring<int>,IDamage<int>
    {
        public event IScoring<int>.UpdateUIScore OnUpdateUIScore;
        public void SetScore(int playerScored)
        {
            ScoringSystem.instance.Score += playerScored;
            OnUpdateUIScore?.Invoke(playerScored);
        }

        public void Damage(int damageTaken)
        {
            Destroy(transform.parent.gameObject);
            SetScore(damageTaken);
        }
    }
}
