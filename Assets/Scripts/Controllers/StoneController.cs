using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneController : MonoBehaviour
{
    ParticleSystem particle;
    void Awake()
    {
        particle = GetComponentInChildren<ParticleSystem>();
    }

    public void DestroyStoneOnEnemyHit()
    {
        GetComponent<MeshRenderer>().enabled = false;
        particle.Play();
        Destroy(this, 0.5f);
    }
}
