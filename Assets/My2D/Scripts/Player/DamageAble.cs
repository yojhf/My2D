using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My2D
{
    public class DamageAble : MonoBehaviour
    {
        [SerializeField] private float maxHealth = 100f;

        public float MaxHealth
        {
            get
            {
                return maxHealth;
            }
            private set
            {
                maxHealth = value;
            }
        }

        private float currentHealth;

        public float CurrentHealth
        {
            get
            {
                return currentHealth;
            }
            private set
            {
                currentHealth = value;

                if(currentHealth <= 0)
                {
                    IsDeath = true;
                }
            }
        }

        private bool isDeath;

        public bool IsDeath
        {
            get
            {
                return isDeath;
            }
            private set
            {
                isDeath = value;

                animator.SetBool(AnimationString.IsDeath, value);
            }
        }

        private bool isInvincible = false;
        [SerializeField] private float invincibleTimer = 3f;
        private float count = 0f;

        Animator animator;

        private void Awake()
        {
            Init();
        }

        private void Update()
        {

            Invincible();
           
        }

        void Init()
        {
            animator = GetComponent<Animator>();
            CurrentHealth = MaxHealth;
        }

        public void TakeDamage(float damage)
        {
            if (!IsDeath && !isInvincible)
            {
                isInvincible = true;

                CurrentHealth -= damage;
                animator.SetTrigger(AnimationString.HitTrigger);

                Debug.Log(CurrentHealth);
            }
        }
        
        void Invincible()
        {
            if(isInvincible == true)
            {
                count += Time.deltaTime;

                if (count >= invincibleTimer)
                {
                    count = 0;
                    isInvincible = false;
                }
            }

        }

    }
}