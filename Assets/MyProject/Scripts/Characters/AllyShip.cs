using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class AllyShip : MonoBehaviour, IHitPoint
{
    [SerializeField]
    private CharacterStatusTable table;

    [SerializeField]
    private Rigidbody2D body;

    [SerializeField]
    private BulletStatusTable beam;

    private int hp = 0;
    public int HP { get => hp; }

    // Start is called before the first frame update
    void Start()
    {
        hp = table.HP;
    }

    // Update is called once per frame
    void Update()
    {
        var velocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        body.velocity = velocity.normalized * table.MovementSpeed;

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
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "EnemyBullet")
        {
            var damage = collider.gameObject.GetComponent<BulletController>().Table.Damage;
            Damage(damage);
            collider.gameObject.SetActive(false);
        }
        else if(collider.gameObject.tag == "Enemy")
        {
            // Destroy
            Damage(table.HP);
        }
    }

    private bool fireableFlag = true;
    public void Fire()
    { 
        var pool = beam.GetBulletPool();
        var beamobj = pool.GetObject();
        beamobj.transform.position = transform.position;
        beamobj.GetComponent<Rigidbody2D>().velocity = Vector2.right * table.BulletSpeed;

        fireableFlag = false;
    }

    private IEnumerator ReloadBeam()
    {
        yield return new WaitForSeconds(table.BulletInterval);
        fireableFlag = true;
    }

    public void Damage(int damage)
    {
        Debug.Log($"{damage} damage received!");
        hp -= damage;
        if(hp <= 0)
        {
            Debug.Log("You are died...");
            Destroy(gameObject);
        }
    }
}
