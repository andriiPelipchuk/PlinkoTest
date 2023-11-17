using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public ParticleSystem _particleSystem;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnEnableParticleSystem();
        var player = collision.gameObject.transform.parent.GetComponent<Player>();
        player.Win();
    }
    private void OnEnableParticleSystem()
    {
        _particleSystem.Play();
        StartCoroutine(TimeToNextLevel());
    }

    IEnumerator TimeToNextLevel() 
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
