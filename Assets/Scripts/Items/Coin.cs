using UnityEngine;

namespace Platformer
{
    public class Coin : MonoBehaviour, IInteractable,IScoring<int>
    {
        private int coinScore = 10;
        public event IScoring<int>.UpdateUIScore OnUpdateUIScore;
        
        protected void Awake()
        {
            GameManager.RestartGame += CoinReset;
        }
                
        /// <summary>
        /// OnDestroy Event
        /// Unsubscribes from event  
        /// </summary>
        protected void OnDestroy()
        {
            GameManager.RestartGame -= CoinReset;
        }

        /// <summary>
        /// IInteractable , Destroys item and calls Score with Coin Value
        /// </summary>
        public void Action()
        {
            gameObject.SetActive(false);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            SetScore(coinScore);
        }

        /// <summary>
        /// Resets coin to original state
        /// </summary>
        private void CoinReset()
        {
            gameObject.SetActive(true);
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
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
