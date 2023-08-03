using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    public GameObject powerPrefab;

    private Vector3 spawnPos = new Vector3(39, 0, 0);
    private float startDelay = 1;
    private float repeatRate = 2;
    private int randomObstacle;

    private GameManager gameManager;
    private PlayerController playerControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnItem", startDelay, repeatRate);
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void SpawnItem()
    {
        if (gameManager.isGameActive && playerControllerScript.gameOver == false)
        {
            randomObstacle = Random.Range(0, obstaclePrefabs.Length);
            GameObject obstacleObj = Instantiate(obstaclePrefabs[randomObstacle], spawnPos,
                obstaclePrefabs[randomObstacle].transform.rotation);

            // Spawn power item
            Vector3 randomPosition = new Vector3(Random.Range(35f, 45f),
                Random.Range(obstacleObj.GetComponent<BoxCollider>().bounds.size.y, 9f), 0);
            for (float i = 0; i < Random.Range(1, 4); i++)
            {
                for (float j = 0; j < Random.Range(1, 4); j++)
                {
                    Instantiate(powerPrefab, new Vector3(randomPosition.x + i, randomPosition.y + j, 0),
                        powerPrefab.transform.rotation);
                }
            }
        }
    }
}