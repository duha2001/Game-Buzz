using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HD : MonoBehaviour {

    public Text Phai;
    public Text Trai;
    GameObject z;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "HD")
        {
            Phai.gameObject.SetActive(false);
            Trai.gameObject.SetActive(true);
            Destroy(collision.gameObject);
        }
        if (collision.tag == "HA")
        { 
            Trai.gameObject.SetActive(false);
        }
    }
}
