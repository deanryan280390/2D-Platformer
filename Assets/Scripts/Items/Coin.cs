using Platformer.Interfaces;
using UnityEngine;

namespace Platformer
{
    public class Coin : MonoBehaviour, IInteractable,IScoring<int>
    {
        private int coinScore = 10;
        public event IScoring<int>.UpdateUIScore OnUpdateUIScore;

        /// <summary>
        /// PickUp - IInteractable , Destroys item and calls Score with Coin Value
        /// </summary>
        public void Pickup()
        {
            Destroy(gameObject);
            SetScore(coinScore);
        }

        /// <summary>
        /// Score - IScoring, Invokes event for Scoring System and manipulates the score based on the coin value
        /// </summary>
        /// <param name="playerScored"></param>
        public void SetScore(int playerScored)
        {
            ScoringSystem.instance.Score += playerScored;
            OnUpdateUIScore?.Invoke(playerScored);
        }
    }
}
