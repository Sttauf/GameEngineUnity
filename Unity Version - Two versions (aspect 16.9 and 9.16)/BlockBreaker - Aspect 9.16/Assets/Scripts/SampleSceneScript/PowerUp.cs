using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

    public float s;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update() {
        transform.Translate(new Vector2(0f, -1f) * Time.deltaTime * s);
        if (transform.position.y < -6f) { Destroy(gameObject); }
	}
}
