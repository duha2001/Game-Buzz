using UnityEngine;
using System.Collections;
using System;

public class PointStar : MonoBehaviour, IPlayerRespawnListener
{
    public GameObject Effect;
    public int PointsToAdd = 10;
    public AudioClip StarSound;
    public Animator Animator;

    private bool _isCollected;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (_isCollected)
            return;

        if (other.GetComponent<Player>() == null)
            return;

        if (StarSound != null)
            AudioSource.PlayClipAtPoint(StarSound, transform.position);

        GameManager.Instance.AddPoints(PointsToAdd);
        Instantiate(Effect, transform.position, transform.rotation);
        FloatingText.Show(string.Format("+{0}", PointsToAdd), "PointStarText", new FromWorldPointTextPositioner(Camera.main, transform.position, 1.5f, 50));

        _isCollected = true;

        //gameObject.SetActive(false);

        Animator.SetTrigger("Collect");
    }

    public void FinishAnimationEvent()
    {
        gameObject.SetActive(false);
        //Destroy(gameObject);
    }

    public void OnPlayerReSpawninThisCheckpoint(Checkpoint checkpoint, Player player)
    {
        _isCollected = false;
        gameObject.SetActive(true);
    }
}
