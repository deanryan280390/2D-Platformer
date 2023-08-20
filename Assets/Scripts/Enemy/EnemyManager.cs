using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Platformer.Interfaces;
using UnityEngine;

namespace Platformer
{
    public class EnemyManager : MonoBehaviour
    {
        [Header("Physics")] 
        private Rigidbody2D rigidBody;
        
        [Header("Sprite")] 
        private SpriteRenderer spriteRenderer;
        
        private WayPointsManager wayPointsManager;
        
        [SerializeField] 
        private float movementSpeed = 3f;

        void Awake()
        {
            rigidBody = GetComponent<Rigidbody2D>();
            rigidBody.freezeRotation = true;
            wayPointsManager = GetComponent<WayPointsManager>();
            spriteRenderer = transform.GetComponent<SpriteRenderer>();

            wayPointsManager.CreateWayPoints();

            wayPointsManager.CurrentWayPoint = wayPointsManager.WayPointsObjects.Last().transform;
        }
        
        private void Update()
        {
            if (wayPointsManager.CurrentWayPoint == wayPointsManager.WayPointsObjects.Last().transform)
            {
                spriteRenderer.flipX = false;
                rigidBody.velocity = new Vector2(movementSpeed, 0);
            }
            else
            {
                spriteRenderer.flipX = true;
                rigidBody.velocity = new Vector2(-movementSpeed, 0);
            }
            wayPointsManager.CheckCurrenPoint();
        }
    }
}
