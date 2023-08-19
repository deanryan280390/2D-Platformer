using UnityEngine;

namespace Platformer
{
    public class InteractionManager : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            var item = col.GetComponent<IInteractable>();
            item?.Pickup();
        }
    }
}