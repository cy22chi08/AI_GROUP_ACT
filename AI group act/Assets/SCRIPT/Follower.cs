using UnityEngine;

public class Follower : MonoBehaviour
{
    public Transform player;
    public float followRange = 3f;
    public float speed = 2f;

    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance < followRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
    }
}
