using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My2D
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private GameObject explosion;
        [SerializeField] private Vector2 speed = new Vector2(5f, 0f);
        [SerializeField] private float attackDamage = 10f;
        [SerializeField] private Vector2 knockback = new Vector2(2f, 0f);

        Rigidbody2D rb;

        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();

            rb.velocity = new Vector2(speed.x * transform.localScale.x, speed.y);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy"))
            {
                DamageAble damageAble = collision.GetComponent<DamageAble>();

                if (damageAble != null)
                {
                    Vector2 knockBackPos = (transform.localScale.x > 0) ? knockback : new Vector2(-knockback.x, knockback.y);

                    damageAble.TakeDamage(attackDamage, knockBackPos);

                    GameObject tmp_ex = Instantiate(explosion, collision.transform.position, Quaternion.identity);

                    Destroy(tmp_ex, 0.5f);

                    Destroy(gameObject);
                }

            }
        }

    }
}