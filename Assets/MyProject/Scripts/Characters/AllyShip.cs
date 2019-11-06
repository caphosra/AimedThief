using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class AllyShip : ActivatableObject, IHitPoint
{
    [SerializeField]
    private CharacterStatusTable table;

    [SerializeField]
    private Rigidbody2D body;

    [SerializeField]
    private SpriteRenderer sprite;

    [SerializeField]
    private BulletStatusTable beam;

    private int hp = 0;
    public int HP { get => hp; }

    public int MaxHP { get => table.HP; }

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        hp = table.HP;

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.OnGameStateChanged.AddListener(() =>
        {
            if(gameManager.CurrentGameState == GameState.GAME_NOW)
            {
                SetActive(true);
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (ActiveSelf)
        {
            var velocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            body.velocity = velocity.normalized * table.MovementSpeed;

            if (Input.GetButtonDown("Fire") && fireableFlag)
            {
                Fire();
                StartCoroutine(ReloadBeam());
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (ActiveSelf)
        {
            if (collision.gameObject.tag == "Stage")
            {
                Debug.Log("Oops, your ship is attacked.");
                Damage(table.HP);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (ActiveSelf)
        {
            if (collider.gameObject.tag == "EnemyBullet")
            {
                var damage = collider.gameObject.GetComponent<BulletController>().Table.Damage;
                Damage(damage);
                collider.gameObject.SetActive(false);
            }
            else if (collider.gameObject.tag == "Enemy")
            {
                var controller = collider.gameObject.GetComponent<EnemyController>();
                Damage(controller.Table.HitDamage);
                Destroy(collider.gameObject);
            }
            else if (collider.gameObject.tag == "Goal")
            {
                gameManager.ChangeGameState(GameState.GO_TO_NEXTSTAGE);
                SetActive(false);
            }
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
        if(ActiveSelf)
        {
            GameObject.FindObjectOfType<AudioSource>().PlayOneShot(table.DieSound);

            Debug.Log($"{damage} damage received!");
            hp -= damage;
            if (hp <= 0)
            {
                Debug.Log("You are died...");
                gameManager.ChangeGameState(GameState.GAME_OVER);

                var dieEffect =  Instantiate(table.DieEffect);
                dieEffect.transform.position = transform.position;
                sprite.enabled = false;

                SetActive(false);
            }
        }
    }
}
