using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class AllyShip : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D body;

    [SerializeField]
    private ObjectPooling beamsPool;

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

        if(Input.GetButtonDown("Fire")  && fireableFlag)
        {
            Fire();
            StartCoroutine(ReloadBeam());
        }
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

    private bool fireableFlag = true;
    public void Fire()
    {
        var beam = beamsPool.GetObject();
        beam.transform.position = transform.position;
        beam.GetComponent<Rigidbody2D>().velocity = new Vector2(10f, 0f);

        fireableFlag = false;
    }

    private IEnumerator ReloadBeam()
    {
        yield return new WaitForSeconds(0.1f);
        fireableFlag = true;
    }
}
