using UnityEngine;
using UnityEngine.SceneManagement;

public class Follower : MonoBehaviour
{
    public Transform player;
    public float followRange = 3f;
    public float speed = 2f;

    public GameObject gameOverCanvas; 

    private bool gameOverTriggered = false;

    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance < followRange && !gameOverTriggered)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!gameOverTriggered && collision.CompareTag("Player"))
        {
            gameOverTriggered = true;
            gameOverCanvas.SetActive(true); 
            Time.timeScale = 0f; 
        }
    }


    public void RestartGame()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
