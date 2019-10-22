using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D body;

    [SerializeField]
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var velocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        body.velocity = velocity.normalized * speed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Stage")
        {
            Debug.Log("Oops, your ship is hitted.");
        }
        else if(collision.gameObject.tag == "Fireball")
        {
            Debug.Log("Ow! A fireball is so hot!");
        }
    }
}
