using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Boss : MonoBehaviour
{
    public float speed;
    public float health;
    public Sprite[] sprites;

    public float maxShotDelay;
    public float curShotDelay;

    public GameObject bulletObjA;
    public GameObject bulletObjB;

    SpriteRenderer spriteRenderer;
    Rigidbody2D rigid;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        rigid.linearVelocity = Vector2.down * speed;
    }

    void Update()
    {
        Move();
        Fire();
        Reload();
    }

    void OnHit(int dmg)
    {
        health -= dmg;
        spriteRenderer.sprite = sprites[1];
        Invoke("ReturnSprite", 0.1f);

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void ReturnSprite()
    {
        spriteRenderer.sprite = sprites[0];
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BorderBullet")
            Destroy(gameObject);
        else if (collision.gameObject.tag == "PlayerBullet")
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            OnHit(bullet.dmg);

            Destroy(collision.gameObject);
        }
    }
}