using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Platformer
{
    public class ScoringSystem : MonoBehaviour
    {
        [SerializeField]
        private Text scoreDisplayed;
        public float Score { get; set; }

        public static ScoringSystem instance;

        private IScoring<int>[] scoringItems;

        private void Awake()
        {
            //Setting up this class as a singleton, because we know there will only be one instance of the scoring system
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            Init();
            GameManager.RestartGame += ResetScore;
        }

        /// <summary>
        /// Sets the score to the UI text label
        /// </summary>
        /// <param name="score"></param>
        private void SetScore(int score)
        {
            scoreDisplayed.text = $"Score: {Score}";
        }

        private void ResetScore()
        {
            Debug.Log("Score Reset");
            Score = 0;
            scoreDisplayed.text = $"Score: {Score}";
        }
        
        /// <summary>
        /// Initialise the IScoring Objects in the scene
        /// </summary>
        private void Init()
        {
            //getting the objects in the scene that use IScoring so we can Subscribe to event
            scoringItems = FindObjectsOfType<MonoBehaviour>(true).OfType<IScoring<int>>().ToArray();

            //subsribes the event to function
            foreach (var scoringItem in scoringItems)
            {
                scoringItem.OnUpdateUIScore += SetScore;
            }
        }
    }
}
