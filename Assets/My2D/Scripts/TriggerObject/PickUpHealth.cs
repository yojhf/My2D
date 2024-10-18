using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My2D
{
    public class PickUpHealth : MonoBehaviour
    {
        [SerializeField] private Vector3 rotSpeed = new Vector3(0f, 100f, 0f);
        [SerializeField] private float hpUp = 20f;

        // Start is called before the first frame update
        void Start()
        {

        }

        void Update()
        {
            RotObject();
        }

        void RotObject()
        {
            transform.eulerAngles += rotSpeed * Time.deltaTime;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                DamageAble damageAble = collision.GetComponent<DamageAble>();

                if(damageAble != null)
                {
                    bool isHeal = damageAble.HealHp(hpUp);

                    if(isHeal)
                    {
                        Destroy(gameObject);
                    }

                }

            }
        }
    }
}