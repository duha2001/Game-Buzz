using UnityEngine;
using System.Collections;

public class GiveDamageToPlayer : MonoBehaviour
{
    public int DamageToGive = 10;

    private Vector2
        _lastPosition,
        _verlocity;

    public void LateUpdate()
    {
        _verlocity = (_lastPosition - (Vector2)transform.position) / Time.deltaTime;
        _lastPosition = transform.position;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<Player>();
        if (player == null)
            return;

        player.TakeDamage(DamageToGive, gameObject);
        var controller = player.GetComponent<CharacterController2D>();
        var totalverlocity = controller.Velocity + _verlocity;

        controller.SetForce(new Vector2(
            -1 * Mathf.Sign(totalverlocity.x) * Mathf.Clamp(Mathf.Abs(totalverlocity.x) * 6, 10, 40),
            -1 * Mathf.Sign(totalverlocity.y) * Mathf.Clamp(Mathf.Abs(totalverlocity.y) * 6, 5, 30)));
    }

}
