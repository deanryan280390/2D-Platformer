using UnityEngine;

namespace Platformer
{
    public class EndPoint : MonoBehaviour, IInteractable
    {    
        public delegate void GameComplete();
        public static event GameComplete OnGameComplete;
        
        /// <summary>
        /// Calls action, Invokes the Game Complete event 
        /// </summary>
        public void Action()
        {
            OnGameComplete?.Invoke();
        }
    }
}
