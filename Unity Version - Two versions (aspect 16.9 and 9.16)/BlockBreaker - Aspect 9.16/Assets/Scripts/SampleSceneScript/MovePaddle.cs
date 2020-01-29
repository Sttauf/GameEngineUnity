using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MovePaddle : MonoBehaviour {

    [SerializeField] private Slider slider;
    [SerializeField] private float positionLimit = 2.0f;
    [SerializeField] private int changePower;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject protection;
    

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float pos = slider.value;
        transform.position = new Vector3(pos * positionLimit, transform.position.y, transform.position.z);		
	}
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("power"))
        {
            changePower = Random.Range(1, 5);
            switch (changePower)
            {
                case 1: StartCoroutine(GetBigger()); break;
                case 2: StartCoroutine(GetSmaller()); break;
                case 3: StartCoroutine(ProtectEnd()); break;
                case 4: StartCoroutine(SlowBall()); break;
                case 5: StartCoroutine(FastBall()); break;
            }
            Destroy(col.gameObject);
        }
    }
    IEnumerator GetBigger()
    {
        player.transform.localScale = new Vector3(1, 0.5f);
        yield return new WaitForSeconds(7);
        player.transform.localScale = new Vector3(0.5f, 0.5f);
    }
    IEnumerator GetSmaller()
    {
        player.transform.localScale = new Vector3(0.2f, 0.3f);
        yield return new WaitForSeconds(7);
        player.transform.localScale = new Vector3(0.5f, 0.5f);
    }
    IEnumerator ProtectEnd()
    {
        protection.SetActive(true);  
        yield return new WaitForSeconds(7);
        protection.SetActive(false);
    }
    IEnumerator SlowBall()
    {
        Time.timeScale = 2.0f;
        yield return new WaitForSeconds(7);
        Time.timeScale = 1.0f;
    }
    IEnumerator FastBall()
    {
        Time.timeScale = 0.5f;
        yield return new WaitForSeconds(7);
        Time.timeScale = 1.0f;
    }
}
