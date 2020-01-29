using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoveBall : MonoBehaviour
{

    [SerializeField] private float dir = 150.0f;
    public Rigidbody2D ball;
    private bool onlyOnce = false;
    private GameManager code;

    private Transform myParent;
    private Vector3 initPos;
    [SerializeField] private float paddleDirBlindspot = 0.2f;
    [SerializeField] private float vForceMin = 0.6f;
    [SerializeField] private float vForceMultiplier = 2f;
    public float velX;

    [SerializeField] private Transform pUp;


    // Use this for initialization
    void Start()
    {
        code = GameObject.Find("GameManager").GetComponent<GameManager>();
        ball = GetComponent<Rigidbody2D>();
        ball.simulated = false;
        initPos = transform.localPosition;
        myParent = transform.parent;
        vForceMin = 0.6f;
        vForceMultiplier = 2f;
    }

    public void Init()
    {
        transform.parent = myParent;
        transform.localPosition = initPos;
        ball.simulated = false;
        ball.velocity = new Vector2(0, 0);
        onlyOnce = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonUp("Jump") && !onlyOnce)
        {
            onlyOnce = true;
            ball.simulated = true;
            ball.transform.parent = null;
            //ball.AddForce(new Vector2(dir, dir));
            ball.AddForce(new Vector2(0, dir));
        }
    }

    void OnCollisionExit2D(Collision2D col) {

        if (Mathf.Abs(ball.velocity.y) < vForceMin) {
            velX = ball.velocity.x;
            if (ball.velocity.y >= 0)
            {
                ball.velocity = new Vector2(velX, vForceMin * vForceMultiplier);
            }
            else { ball.velocity = new Vector2(velX, -vForceMin * vForceMultiplier); }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        AudioSource aBall = GetComponent<AudioSource>();
        //AudioSource aBrick = GetComponent<AudioSource>();        
        //AudioSource aDeath = GetComponent<AudioSource>();

        if (col.gameObject.tag == "bricks")
        {
            //aBrick.Play();
            BrickBehaviour bScript = col.gameObject.GetComponent<BrickBehaviour>();
            if (bScript.hits > 1)
            {
                bScript.Break();
            }
            else
            {
                int random = Random.Range(1, 101);
                if (random < 50)
                {
                    Instantiate(pUp, col.transform.position, col.transform.rotation);
                }
                code.AddPoints();
                Destroy(col.gameObject);

            }            
        }
        if (col.gameObject.tag == "death")
        {
            //aDeath.Play();
            code.Death();
        }
        if (col.gameObject.tag == "paddle")
        {
            aBall.Play();
            float diffX = transform.position.x - col.transform.position.x;
            if (diffX < -paddleDirBlindspot) {
                //we're on the left side of the blindspot
                ball.velocity = new Vector2(0, 0);
                ball.AddForce(new Vector2(-dir, dir));
            }
            else if (diffX > paddleDirBlindspot) {
                //we're on the right side of the blindspot
                ball.velocity = new Vector2(0, 0);
                ball.AddForce(new Vector2(-dir, dir));
            }
        }
    }

}
