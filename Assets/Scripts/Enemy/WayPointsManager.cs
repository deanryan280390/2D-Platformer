using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Platformer
{
    public class WayPointsManager : MonoBehaviour
    {
        private const int WayPointCount = 2;
        [SerializeField] private float distanceBetweenPoints = 1f;

        public List<GameObject> WayPointsObjects;
        public Transform CurrentWayPoint { get; set; }

        public float distanceChecker = 0.5f;

        [Header("Physics")] 
        private Rigidbody2D rigidBody;
        
        private void Awake()
        {
            WayPointsObjects = new List<GameObject>();
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
        public void CreateWayPoints()
        {
            for (var i = 0; i < WayPointCount; i++)
            {
                var wayPoint = new GameObject();
                wayPoint.name = $"Way-Point{i}";
                var newWayPointPosition = new Vector3(transform.position.x - distanceBetweenPoints, wayPoint.transform.position.y, 0);
                distanceBetweenPoints *= -1;
                wayPoint.transform.position = transform.GetComponent<Collider2D>().bounds.center + newWayPointPosition;
                WayPointsObjects.Add(wayPoint);
            }
        }
    }
}
