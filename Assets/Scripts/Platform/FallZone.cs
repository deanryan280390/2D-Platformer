using UnityEngine;

namespace Platformer
{
    public class FallZone : MonoBehaviour,IInteractable
    {
        /// <summary>
        /// Calls the action to Reset the Game, if the player hits a fall zone
        /// </summary>
        public void Action()
        {
            GameManager.instance.ResetGame();
        }
    }
}
