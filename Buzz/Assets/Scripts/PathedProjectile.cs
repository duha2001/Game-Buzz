using UnityEngine;
using System.Collections;
using System;

public class PathedProjectile : MonoBehaviour, ITakeDamage ////chiu trach nhiem cưa
{
    private Transform _destinnation;
    private float _speed;
    public AudioClip DetroySound;

    public GameObject DestroyEffect;
    public int PointToGivePlayer;

    public void Initalize(Transform destination, float speed)
    {
        _destinnation = destination;
        _speed = speed;
    }

    public void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _destinnation.position, Time.deltaTime * _speed);

        var distanceSquared = (_destinnation.transform.position - transform.position).sqrMagnitude;
        if (distanceSquared > .01f * .01f)
            return;

        if (DestroyEffect != null)
            Instantiate(DestroyEffect, transform.position, transform.rotation);

        if (DetroySound != null)
            AudioSource.PlayClipAtPoint(DetroySound, transform.position);

        Destroy(gameObject);


    }

    public void TakeDamage(int damage, GameObject instigator)
    {
        //throw new NotImplementedException();
        if (DestroyEffect)
            Instantiate(DestroyEffect, transform.position, transform.rotation);

        Destroy(gameObject);

        var projectile = instigator.GetComponent<Projectile>();
        if (projectile != null && projectile.Owner.GetComponent<Player>());
        {
            GameManager.Instance.AddPoints(PointToGivePlayer);
            FloatingText.Show(string.Format("+{0}", PointToGivePlayer), "PointStartext", new FromWorldPointTextPositioner(Camera.main, transform.position, 1.5f, 50));
        }
    }
}
