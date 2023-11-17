using UnityEngine;
using System.Collections;

public class Crater : MonoBehaviour
{

    public float craterForce = 5;

    private bool _playerCaught;
    private Player _player;
    private int _second = 1;

    private void Update()
    {
        if (_playerCaught)
        {
            _player.rb.velocity = Vector2.zero;
            _player.rb.position = Vector2.MoveTowards(_player.rb.position, transform.position, craterForce * Time.deltaTime);

        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            _player = player;
            _playerCaught = true;
            StartCoroutine(TimeToFree());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        var player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            _player = null;
            _playerCaught = false;
            StopCoroutine(TimeToFree());
        }
    }


    private void CallIventPlayerDie()
    {
        StopCoroutine(TimeToFree());
        _player.Die();

    }
    IEnumerator TimeToFree()
    {
        yield return new WaitForSeconds(_second);
        CallIventPlayerDie();
    }
}
