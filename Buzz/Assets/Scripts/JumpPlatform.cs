using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPlatform : MonoBehaviour {

    public float JumpMagntitude = 20;
    public AudioClip JumpSound;

    public void ControllerEnter2D(CharacterController2D controller) // hàm điều khiển va chạm vào (EMTER)
    {
        if (JumpSound != null)
            AudioSource.PlayClipAtPoint(JumpSound, transform.position);
        //controller.SetForce(new Vector2(controller.Velocity.x, JumpMagntitude));
        controller.SetVerticalForce(JumpMagntitude);
    }
}
