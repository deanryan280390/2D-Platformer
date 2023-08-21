using System;
using UnityEngine;

namespace Platformer
{
    public class InteractionManager : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            var interactable = col.GetComponent<IInteractable>();
            interactable?.Action();

            var damage = col.GetComponent<IDamage<int>>();
            if (!transform.GetComponent<PlayerMovement>().IsGrounded())
            {
                damage?.Damage(10);
            }
        }
    }
}