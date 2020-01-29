using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickBehaviour : MonoBehaviour {
    private GameManager code;
    public int hits;
    [SerializeField] private Sprite hitsS;
    
	// Use this for initialization
	void Start () {
        code = GameObject.Find("GameManager").GetComponent<GameManager>();
        code.AddBlock();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Break() {
        hits--;
        GetComponent<SpriteRenderer>().sprite = hitsS;
    }

}
