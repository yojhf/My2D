using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My2D
{
    public class PlayerAttack : MonoBehaviour
    {
        private float damage = 10f;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            DamageAble damageAble = collision.GetComponent<DamageAble>();

            if (damageAble != null)
            {
                damageAble.TakeDamage(damage);
            }
        }
    }
}