using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if (collision.transform.tag == "Obstacle")
        {
            PlayerManager.isGameOver = true;
            gameObject.SetActive(false);
        }
        if (collision.transform.tag == "Winning")
        {
            PlayerManager.isGameWon = true;
            gameObject.SetActive(false);
        }
    }
}
