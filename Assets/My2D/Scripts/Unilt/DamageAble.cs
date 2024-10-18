using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace My2D
{
    public class DamageAble : MonoBehaviour
    {
        // 데미지를 입을 때 등록된 함수 호출
        public UnityAction<Vector2> hitAction;


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

        public bool LockVelocity
        {
            get
            {
                return animator.GetBool(AnimationString.LockVelocity);
            }
            private set
            {
                animator.SetBool(AnimationString.LockVelocity, value);
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

        public void TakeDamage(float damage, Vector2 knockback)
        {
            if (!IsDeath && !isInvincible)
            {
                isInvincible = true;
                LockVelocity = true;

                CharacterEvent.characterDamaged?.Invoke(gameObject, damage);

                CurrentHealth -= damage;
                animator.SetTrigger(AnimationString.HitTrigger);

                Debug.Log(CurrentHealth);

                // 데미지 효과
                if(hitAction != null)
                {
                    hitAction.Invoke(knockback);
                }


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

        public bool HealHp(float hp)
        {
            if(CurrentHealth >= maxHealth)
            {
                return false;
            }
            
            float chargeHp = CurrentHealth += hp;

            if(chargeHp > maxHealth)
            {
                hp = CurrentHealth - maxHealth;

                CurrentHealth = maxHealth;         
            }
            else
            {
                CurrentHealth = chargeHp;
            }


            CharacterEvent.characterHealed?.Invoke(gameObject, hp);

            Debug.Log(CurrentHealth);

            return true;
        }

    }
}