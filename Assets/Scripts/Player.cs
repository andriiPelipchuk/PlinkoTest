using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public event Action PlayerDied, PlayerWin;
    private ParticleSystem _particleSystem;

    public GameObject view, particleChield;
    public Rigidbody2D rb;

    void Awake()
    {
        _particleSystem = particleChield.GetComponent<ParticleSystem>();

    }
    public void OnEnableParticleSystem(float time)
    {
        _particleSystem.Play();
        Destroy(gameObject, time);
    }
    public void Die()
    {
        PlayerDied?.Invoke();
        view.SetActive(false);
        OnEnableParticleSystem(_particleSystem.startLifetime);
    }
    public void Win()
    {
        PlayerWin?.Invoke();
        view.SetActive(false);
    }

}