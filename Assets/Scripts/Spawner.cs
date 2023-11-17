using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Player player;

    public Player SpawnPlayer()
    {
        var newPlayer = Instantiate(player, gameObject.transform.position, Quaternion.identity);
        return newPlayer;
    }

}
