using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRB : MonoBehaviour {
    Rigidbody2D rb;
    [SerializeField] float forcemove = 10f ;
    int chieu = 0;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        rb.AddForce(Vector2.right*forcemove*chieu);
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Debug.Log("Phai");
            chieu = 1;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Debug.Log("Trai");
            chieu = -1;
        }
        else chieu = 0;
	}
}
