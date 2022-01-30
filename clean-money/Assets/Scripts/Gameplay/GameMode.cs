using UnityEngine;
using TMPro;
namespace cm.gameplay
{
    public class GameMode : MonoBehaviour
    {
        public int money = 0;

        [SerializeField]
        private Vector2 irsChance = new Vector2(5.0f, 30.0f);
        [SerializeField]
        private float countDown = 0.0f;
        [SerializeField]
        private GameObject irsPrefab = null;

        [SerializeField]
        private TMP_Text scoreText = null;

        private void Start()
        {
            countDown = GetRandomSpawnTime();
        }

        private void FixedUpdate()
        {
            countDown -= Time.deltaTime;
            if (countDown <= 0)
            {
                countDown = GetRandomSpawnTime();
                SpawnIRSAgent();
            }
        }

        public void IncreaseMoney(int value)
        {
            money += value;
            
            if (scoreText)
                scoreText.text = money.ToString();
        }

        private void SpawnIRSAgent()
        {
            Instantiate(irsPrefab, AIPath.Instance.GetPoint(0).position, Quaternion.identity);
        }

        private float GetRandomSpawnTime()
        {
            return Random.Range(irsChance.x, irsChance.y);
        }
    }
}