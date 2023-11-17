using UnityEngine;

public class Blocks : MonoBehaviour
{
    protected void OnCollisionEnter2D(Collision2D collision)
    {
        GameOver(collision);
    }

    private void GameOver(Collision2D collision)
    {
        var player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            player.Die();
        }
    }
}
