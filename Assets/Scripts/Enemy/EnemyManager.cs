using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Platformer
{
    public class EnemyManager : MonoBehaviour
    {
        private const int WayPointCount = 2;
        
        [SerializeField]
        private float DistanceBetweenPoints = 1f;

        [Header("Physics")] 
        private Rigidbody2D rigidBody;
        
        [Header("Sprite")] 
        private SpriteRenderer spriteRenderer;

        [SerializeField] 
        private float movementSpeed = 3f;
 
        private Transform CurrentWayPoint { get; set; }
        [Header("Way Points Information")] 
        [SerializeField]
        private float distanceChecker = 0.5f;
        private float wayPointStartingPositionX;
        public List<GameObject> WayPointsObjects;
        
        void Awake()
        {
            rigidBody = GetComponent<Rigidbody2D>();
            rigidBody.freezeRotation = true;
            spriteRenderer = transform.GetComponent<SpriteRenderer>();
        }
        
        private void Update()
        {
            if (WayPointsObjects.Count > 0)
            {
                if (CurrentWayPoint == WayPointsObjects.Last().transform)
                {
                    spriteRenderer.flipX = false;
                    rigidBody.velocity = new Vector2(movementSpeed, 0);
                }
                else
                {
                    spriteRenderer.flipX = true;
                    rigidBody.velocity = new Vector2(-movementSpeed, 0);
                }

                CheckCurrenPoint();
            }
        }
        
        /// <summary>
        /// Check Current Point
        /// Checking distance between 2 points enemy and current point and if currentPoint is equals the current point against the other waypoint
        /// We have to check this for each waypoint
        /// </summary>
        public void CheckCurrenPoint()
        {
            if (Vector2.Distance(transform.position, CurrentWayPoint.position) < distanceChecker &&
                CurrentWayPoint == WayPointsObjects.Last().transform)
            {
                CurrentWayPoint = WayPointsObjects.First().transform;
            }

            if (Vector2.Distance(transform.position, CurrentWayPoint.position) < distanceChecker &&
                CurrentWayPoint == WayPointsObjects.First().transform)
            {
                CurrentWayPoint = WayPointsObjects.Last().transform;
            }
        }
        
        /// <summary>
        /// For testing purposes to draw line between the two wayPoints
        /// </summary>
        /// <param name="points"></param>
        private void DrawWayPointsDistance(List<GameObject> points)
        {
            Debug.DrawLine(points.First().transform.position, points.Last().transform.position, Color.red); 
        }
        
        /// <summary>
        /// Generates 2 points Way points
        /// Sets up the range, so distance between two way points, this can be adjustable and be different for each enemy or can be randomly generated
        /// Adds to the wayPointsObjects List
        /// </summary>
        public void CreateWayPoints(Vector2 enemyPosition)
        {
            for (var i = 0; i < WayPointCount; i++)
            {
                var wayPoint = new GameObject();
                string id = $"{gameObject.name}-{i}";
                wayPoint.name = $"Way-Point-{id}";
                wayPointStartingPositionX = enemyPosition.x - DistanceBetweenPoints;
                var newWayPointPosition = new Vector3(wayPointStartingPositionX,0, 0);
                DistanceBetweenPoints *= -1;
                wayPoint.transform.position = newWayPointPosition;
                WayPointsObjects.Add(wayPoint);
            }
            CurrentWayPoint = WayPointsObjects.Last().transform;
        }
    }
}
