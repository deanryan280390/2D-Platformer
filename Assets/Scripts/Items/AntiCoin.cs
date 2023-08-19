using Platformer.Interfaces;
using UnityEngine;

namespace Platformer
{
    public class AntiCoin : MonoBehaviour, IInteractable,IScoring<int>
    {
        private int coinScore = 5;
        public event IScoring<int>.UpdateUIScore OnUpdateUIScore; // added event for each item to Invoke the UI Score, Sol any time we want to item to be included in the scoring system just add IScoring
        
        /// <summary>
        /// PickUp - IInteractable , Destroys item and calls Score with Anti-Coin Value
        /// </summary>
        public void Pickup()
        {
            Destroy(gameObject);
            SetScore(coinScore);
        }

        /// <summary>
        /// Score - IScoring, Invokes event for Scoring System and manipulates the score based on the item value
        /// </summary>
        /// <param name="playerScored"></param>
        public void SetScore(int playerScored)
        {
            ScoringSystem.instance.Score -= playerScored;
            OnUpdateUIScore?.Invoke(playerScored);
        }
    }
}
