using System.Linq;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float speed = 30;

    private PlayerController playerControllerScript;
    private GameManager gameManager;

    private float leftBound = -15;
    private string[] outBoundTag = { "Obstacle", "Power" };

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameActive)
        {
            if (playerControllerScript.gameOver == false)
            {
                if (playerControllerScript.doubleSpeed)
                {
                    transform.Translate(Vector3.left * Time.deltaTime * speed * 2);
                }
                else if (playerControllerScript.powerMode)
                {
                    transform.Translate(Vector3.left * Time.deltaTime * speed * 3);
                }
                else
                {
                    transform.Translate(Vector3.left * Time.deltaTime * speed);
                }
            }

            if (transform.position.x < leftBound && outBoundTag.Contains(gameObject.tag))
            {
                Destroy(gameObject);
            }
        }
    }
}