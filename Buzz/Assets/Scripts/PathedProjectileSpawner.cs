using UnityEngine;
using System.Collections;

public class PathedProjectileSpawner : MonoBehaviour //chiu trach nhiem cưa
{
    public Transform Destination;
    public PathedProjectile Projectile;

    public GameObject SpawnEffect;
    public float Speed;
    public float FireRate;
    public AudioClip SpawProjectileSound;
    //public AudioSource SpawProjectileSound;

    public Animator animator;

    private float _nextShotInSecond;

    public void Start()
    {
        _nextShotInSecond = FireRate;
    }

    public void Update()
    {
        if ((_nextShotInSecond -= Time.deltaTime) > 0)
            return;

        _nextShotInSecond = FireRate;
        var projectile = (PathedProjectile)Instantiate(Projectile, transform.position, transform.rotation);
        projectile.Initalize(Destination, Speed);

        if (SpawnEffect != null)
            Instantiate(SpawnEffect, transform.position, transform.rotation);

        if (SpawProjectileSound != null)
            AudioSource.PlayClipAtPoint(SpawProjectileSound, transform.position);

        if (animator != null)
            animator.SetTrigger("fire");
    }

    public void OnDrawGizmos()
    {
        if (Destination == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, Destination.position); //vẽ đường
    }
}
