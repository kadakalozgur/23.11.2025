using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public Transform player;
    public BoxCollider2D spawnZone;
    public int maxEnemies = 5;
    public float spawnInterval = 5f;

    private int currentEnemies = 0;
    private float spawnTimer = 0f;
    private bool triggered = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        // Oyuncu "enemy spawnplatform" taglý yere deðince
        if (other.CompareTag("Player") && !triggered)
        {
            triggered = true;

            // Hemen spawn
            int initialSpawn = Mathf.Min(maxEnemies - currentEnemies, maxEnemies);
            for (int i = 0; i < initialSpawn; i++)
            {
                SpawnEnemy();
            }
        }
    }

    void Update()
    {
        // Tetiklendiyse belirli aralýklarla spawn devam eder
        if (triggered)
        {
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= spawnInterval && currentEnemies < maxEnemies)
            {
                SpawnEnemy();
                spawnTimer = 0f;
            }
        }
    }

    void SpawnEnemy()
    {
        Vector2 randomPos = new Vector2(
            Random.Range(spawnZone.bounds.min.x, spawnZone.bounds.max.x),
            Random.Range(spawnZone.bounds.min.y, spawnZone.bounds.max.y)
        );

        int randIndex = Random.Range(0, enemyPrefabs.Length);
        GameObject enemy = Instantiate(enemyPrefabs[randIndex], randomPos, Quaternion.identity);

        EnemyAI ai = enemy.GetComponent<EnemyAI>();
        if (ai != null)
            ai.player = player;

        currentEnemies++;
    }

    // Düþman öldüðünde bu metodu çaðýr
    public void EnemyDied()
    {
        currentEnemies--;
        if (currentEnemies < 0)
            currentEnemies = 0;
    }
}