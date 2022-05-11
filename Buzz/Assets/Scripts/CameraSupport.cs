using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSupport : MonoBehaviour {

    public GameObject player;
    private Vector3 offset;

    public float _x = 3,
                 _y = -10,
                 _z = -10;

    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + offset;

        //transform.position = new Vector3(player.transform.position.x + offset.x, player.transform.position.y + offset.y, offset.z);

        //transform.position = new Vector3(player.transform.position.x + _x, _y, _z); // Camera follows the player but 6 to the right
    }
}
