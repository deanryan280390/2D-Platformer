using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Platformer
{
    public class GameManager : MonoBehaviour
    {
        public static event Action RestartGame;
        public static GameManager instance;
        
        [Header("Keybinds")]
        [SerializeField]
        private KeyCode RestartGameKey = KeyCode.R;
        
        [Header("Enemy Information")]
        [SerializeField]
        private int EnemyCount = 5;
        public List<GameObject> Enemies = new List<GameObject>();
        public List<Vector2> EnemyPositions;
        private string enemyFilePath;

        /// <summary>
        /// ResetGame, Invokes the event
        /// Cursor turned off
        /// </summary>
        public void ResetGame()
        {
            RestartGame?.Invoke();
            EnemyCleanup();
            SetUpEnemies();
            Cursor.visible = false;
        }

        /// <summary>
        /// Sets the Game Manager as a singleton, as we only have one of these in the game
        /// </summary>
        private void Awake()
        {
            enemyFilePath = "Prefabs/Characters/Enemy";
            Cursor.visible = false;
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            SetUpEnemies();
        }
        
        private void Update()
        {
            if (Input.GetKeyDown(RestartGameKey))
            {
                ResetGame();
            }
        }

        /// <summary>
        /// Set up Enemies information
        /// Create the Enemy object based on the amount of Enemy Count set
        /// A creates wayPoints for the enemy to move left and right
        /// </summary>
        void SetUpEnemies()
        {
            for (int i = 0; i < EnemyCount; i++)
            {
                var enemy = Helper.CreateGameObject(enemyFilePath, $"Enemy-{i}", EnemyPositions[i], ref Enemies);
                enemy.GetComponent<EnemyManager>().CreateWayPoints(EnemyPositions[i]);
            }
        }

        /// <summary>
        /// Enemy Cleanup, Destroys the enemy, enemy waypoints and clears the Enemies reference list and Enemy Waypoint list
        /// </summary>
        private void EnemyCleanup()
        {
            foreach (var enemy in Enemies.Where(enemy => enemy != null))
            {
                foreach (var wayPoints in enemy.GetComponent<EnemyManager>().WayPointsObjects)
                {
                    Destroy(wayPoints);
                }
                enemy.GetComponent<EnemyManager>().WayPointsObjects.Clear();
                Destroy(enemy);
            }

            Enemies.Clear();
        }
        
       
    }
}