using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My2D
{
    public class Attack : MonoBehaviour
    {
        [SerializeField] private float damage = 10f;
        public Vector2 knockBack = Vector2.zero;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            DamageAble damageAble = collision.GetComponent<DamageAble>();

            if (damageAble != null)
            {
                Vector2 knockBackPos = (transform.parent.localScale.x > 0) ? knockBack : new Vector2(-knockBack.x, knockBack.y);

                damageAble.TakeDamage(damage, knockBackPos);
            }
        }
    }
}