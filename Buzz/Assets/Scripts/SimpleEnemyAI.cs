using UnityEngine;
using System.Collections;
using System;

public class SimpleEnemyAI : MonoBehaviour, ITakeDamage, IPlayerRespawnListener
{
    public float Speed;
    public float FireRate = 1;
    public Projectile Projectile;
    public GameObject DestroyEffect;
    public int PointToGivePlayer;
    public AudioClip ShootSound;

    private CharacterController2D _controller;
    private Vector2 _direction;
    private Vector2 _starPosition;
    private float _canFireIn;

    public void Start()
    {
        _controller = GetComponent<CharacterController2D>();
        _direction = new Vector2(-1, 0);
        _starPosition = transform.position;
    }

    public void Update()
    {
        _controller.SetHorizontalForce(_direction.x * Speed);

        if((_direction.x < 0 && _controller.State.IsCollidingLeft) || (_direction.x > 0 && _controller.State.IsCollidingRight))
        {
            _direction = -_direction;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }

        if ((_canFireIn -= Time.deltaTime) > 0)
            return;

        var raycast = Physics2D.Raycast(transform.position, _direction, 10, 1 << LayerMask.NameToLayer("Player"));
        if (!raycast)
            return;

        var projectile = (Projectile)Instantiate(Projectile, transform.position, transform.rotation);
        projectile.Initialize(gameObject, _direction, _controller.Velocity);
        _canFireIn = FireRate;

        if (ShootSound != null)
            AudioSource.PlayClipAtPoint(ShootSound, transform.position);
    }

    public void OnPlayerReSpawninThisCheckpoint(Checkpoint checkpoint, Player player)
    {
        //throw new NotImplementedException();

        _direction = new Vector2(-1, 0);
        transform.localScale = new Vector3(1, 1, 1);
        transform.position = _starPosition;
        gameObject.SetActive(true);
    }

    public void TakeDamage(int damage, GameObject instigator)
    {
        //throw new NotImplementedException();
        if(PointToGivePlayer != 0)
        {
            var projectile = instigator.GetComponent<Projectile>();
            if(projectile != null && projectile.Owner.GetComponent<Player>() != null)
            {
                GameManager.Instance.AddPoints(PointToGivePlayer);
                FloatingText.Show(string.Format("+{0}", PointToGivePlayer), "PointStarText", new FromWorldPointTextPositioner(Camera.main, transform.position, 1.5f, 50));
            }
        }

        Instantiate(DestroyEffect, transform.position, transform.rotation);
        gameObject.SetActive(false);
    }
}
