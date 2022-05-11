using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, ITakeDamage
{

    /// <summary>
    /// 
    /// </summary>

    private bool _isFacingRight;
    private CharacterController2D _controller;
    private float _normalizedHorizontalSpeed;
    //Rigidbody2D rb;

    public float MaxSpeed = 8;
    public float SpeedAccelerationOnGround = 10f;
    public float SpeedAccelerationInAir = 5f;
    public int MaXHealth = 100;
    public GameObject OuchEffect;
    public Projectile Projectile;
    public float FireRate, forcemove = 10f;
    public Transform ProjectileFileLocation;
    public GameObject FireProjectileEffect;
    public AudioClip PlayerHit;
    public AudioClip PlayerShootSound;
    public AudioClip PlayerHealth;
    //public Animator Animator;


    public int Health { get; private set; }
    public bool IsDead { get; private set; }

    private float _canFireIn;
    public void Awake()
    {
        _controller = GetComponent<CharacterController2D>();
        _isFacingRight = transform.localScale.x > 0;
        Health = MaXHealth;
    }
    //void Start() {
    //    rb = GetComponent<Rigidbody2D>();
    //}
    public void Update()
    {
        _canFireIn -= Time.deltaTime;

        if(!IsDead)
            HandleInput();

        var movementFactor = _controller.State.IsGrounded ? SpeedAccelerationOnGround : SpeedAccelerationInAir;
        ///////////////////
        if (IsDead)
            //rb.AddForce(Vector2.right * _normalizedHorizontalSpeed * 0);
            _controller.SetHorizontalForce(0);
        else
            //Debug.Log(_normalizedHorizontalSpeed);
            //rb.AddForce(Vector2.right * forcemove * _normalizedHorizontalSpeed);
            _controller.SetHorizontalForce(Mathf.Lerp(_controller.Velocity.x, _normalizedHorizontalSpeed * MaxSpeed, Time.deltaTime * movementFactor));

        //Animator.SetBool("IsGrounded", _controller.State.IsGrounded);
        //Animator.SetBool("IsDead", IsDead);
        //Animator.SetFloat("Speed", Mathf.Abs(_controller.Velocity.x) / MaxSpeed);
    }

    public void FinishLevel()
    {
        enabled = false;
        _controller.enabled = false;
        this.gameObject.GetComponent<Collider2D>().enabled = false;
    }

    public void Kill()
    {
        _controller.HandleCollisions = false;
        this.gameObject.GetComponent<Collider2D>().enabled = false;
        //GetComponent<Collider2D>().enabled = false;
        IsDead = true;
        Health = 0;

        _controller.SetForce(new Vector2(0, 20));
    }

    public void RespawnAt( Transform spawnPoint)
    {
        if (!_isFacingRight)
            Flip();

        IsDead = false;
        this.gameObject.GetComponent<Collider2D>().enabled = true;
        //GetComponent<Collider2D>().enabled = false;
        _controller.HandleCollisions = true;
        Health = MaXHealth;

        transform.position = spawnPoint.position;
    }

    public void TakeDamage(int damage, GameObject instagator)
    {
        AudioSource.PlayClipAtPoint(PlayerHit, transform.position);

        FloatingText.Show(string.Format("-{0}", damage), "PlayerTakeDamageText", new FromWorldPointTextPositioner(Camera.main, transform.position, 2f, 60f));

        Instantiate(OuchEffect, transform.position, transform.rotation);
        Health -= damage;

        if(Health <= 0)
        {
            LevelManager.Instance.KillPlayer();
        }
    }

    public void GiveHealth(int health, GameObject instagator)
    {
        AudioSource.PlayClipAtPoint(PlayerHealth, transform.position);
        FloatingText.Show(string.Format("+{0}", health), "PlayerGotHealthText", new FromWorldPointTextPositioner(Camera.main, transform.position, 2f, 50));
        Health += health;
        Health = Mathf.Min(Health + health, MaXHealth);
    }

    private void HandleInput()
    {
        if(Input.GetKey(KeyCode.D))
        {
            _normalizedHorizontalSpeed = 1;
            if (!_isFacingRight)
                Flip();
        }
        else if(Input.GetKey(KeyCode.A))
        {
            _normalizedHorizontalSpeed = -1;
            if (_isFacingRight)
                Flip();
        }
        else
        {
            _normalizedHorizontalSpeed = 0;
        }

        if(_controller.CanJump && Input.GetKeyDown(KeyCode.Space))
        {
            _controller.Jump();
        }

        if (Input.GetMouseButtonDown(0))
            FireProjectile();
    }

    private void FireProjectile()
    {
        if (_canFireIn > 0)
            return;

        if (FireProjectileEffect != null)
        {
            var effect = (GameObject)Instantiate(FireProjectileEffect, ProjectileFileLocation.position, ProjectileFileLocation.rotation);
            effect.transform.parent = transform;
        }

        var direction = _isFacingRight ? Vector2.right : -Vector2.right;

        var projectile = (Projectile)Instantiate(Projectile, ProjectileFileLocation.position, ProjectileFileLocation.rotation);
        projectile.Initialize(gameObject, direction, _controller.Velocity);

        _canFireIn = FireRate;

        AudioSource.PlayClipAtPoint(PlayerShootSound, transform.position);

        //Animator.SetTrigger("Fire");

    }

    private void Flip()
    {
        //_isFacingRight = !_isFacingRight;
        //Vector3 theScale = transform.localScale;
        //theScale.x += -1;
        //transform.localScale = -theScale;
        ////////////////////
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        _isFacingRight = transform.localScale.x > 0;
    }

    //public void FixedUpdate()
    //{
    //    float move = Input.GetAxis("Horizontal");
    //    this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(move * MaxSpeed, this.gameObject.GetComponent<Rigidbody2D>().velocity.y);

    //    if (move > 0 && !_isFacingRight)
    //        Flip();
    //    else if (move < 0 && _isFacingRight)
    //        Flip();
    //}

    //public void OnTriggerEnter2D(Collider2D collision)
    //{
    //    Debug.Log(gameObject);
    //}
}
