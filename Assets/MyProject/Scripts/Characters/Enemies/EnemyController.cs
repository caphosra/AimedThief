using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class EnemyController : ActivatableObject, IHitPoint
{
    [SerializeField]
    private CharacterStatusTable table;
    public CharacterStatusTable Table { get => table; }

    private int hp;
    public int HP { get => hp; }

    void Start()
    {
        hp = table.HP;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(ActiveSelf)
        {
            if(collider.gameObject.tag == "PlayerBullet")
            {
                var bulletController = collider.gameObject.GetComponent<BulletController>();
                Damage(bulletController.Table.Damage);
                collider.gameObject.SetActive(false);
            }
        }
    }

    public void Damage(int damage)
    {
        hp -= damage;
        if(hp <= 0)
        {
            Debug.Log($"{Table.CharacterName} was destoryed by the ship!");
            ScoreDatabase.Score += table.Score;
            GameObject.FindObjectOfType<AudioSource>().PlayOneShot(Table.DieSound);
            Destroy(gameObject);
        }
    }
}
