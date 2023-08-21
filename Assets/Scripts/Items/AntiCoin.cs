using UnityEngine;

namespace Platformer
{
    public class AntiCoin : MonoBehaviour, IInteractable,IScoring<int>
    {
        private int coinScore = 5;
        public event IScoring<int>.UpdateUIScore OnUpdateUIScore; // added event for each item to Invoke the UI Score, Sol any time we want to item to be included in the scoring system just add IScoring
        
        protected void Awake()
        {
            GameManager.RestartGame += CoinReset;
        }
        
        /// <summary>
        /// OnDestroy Event
        /// Unsubscribes from event  
        /// </summary>
        private void OnDestroy()
        {
            GameManager.RestartGame -= CoinReset;
        }
        
        /// <summary>
        /// Coin Reset, set game object active/show
        /// Coin Collider enabled 
        /// </summary>
        private void CoinReset()
        {
            gameObject.SetActive(true);
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
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
