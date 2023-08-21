using UnityEngine;

namespace Platformer
{
    public class EnemyDamage : MonoBehaviour, IScoring<int>,IDamage<int>
    {
        public event IScoring<int>.UpdateUIScore OnUpdateUIScore;
        private EnemyManager enemyManager;
        
        private void Awake()
        {
            enemyManager = GetComponentInParent<EnemyManager>();
        }

        /// <summary>
        /// Sets the score when the player jumps on enemies head
        /// </summary>
        /// <param name="playerScored"></param>
        public void SetScore(int playerScored)
        {
            ScoringSystem.instance.Score += playerScored;
            OnUpdateUIScore?.Invoke(playerScored);
        }
        
        /// <summary>
        /// Damage on enemy
        /// Destroys the enemy game object
        /// Destroys the ememy way points objects
        /// damage taken is added to player score
        /// </summary>
        /// <param name="damageTaken"></param>
        public void Damage(int damageTaken)
        {
            Destroy(enemyManager.gameObject);
            foreach (var wayPointsObject in enemyManager.WayPointsObjects)
            {
                Destroy(wayPointsObject);
            }
            SetScore(damageTaken);
        }
    }
}
