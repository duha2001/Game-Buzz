using UnityEngine;
using System.Collections;

public class AutoDestroyParticalSystem : MonoBehaviour
{
    private ParticleSystem _particalsystem;

    public void Start()
    {
        _particalsystem = GetComponent<ParticleSystem>();
    }

    public void Update()
    {
        if (_particalsystem.isPlaying)
            return;
        Destroy(gameObject);
    }
}
