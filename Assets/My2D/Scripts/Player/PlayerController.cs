using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace My2D
{
    public class PlayerController : MonoBehaviour
    {
        private Vector2 movePos;
        private float rotY = 0f;
        [SerializeField] private float moveSpeed = 4f;
        private float startSpeed;

        DamageAble damageAble;

        public float CurrentSpeed
        {
            get
            {
                if(CanMove)
                {
                    if (IsMove && touchingDirection.IsWall == false)
                    {
                        if (touchingDirection.IsGround)
                        {
                            if (IsRun)
                            {
                                return runSpeed;
                            }
                            else
                            {
                                return moveSpeed;
                            }

                        }
                        else
                        {
                            return airSpeed;
                        }
                    }
                    else
                    {
                        return 0f; // Idle state, �������� ���� ��
                    }
                }
                else
                {
                    return 0f;
                }

            }

        }

        public bool CanMove
        {
            get
            {
                return animator.GetBool(AnimationString.CanMove);
            }
        }

        private bool isMove = false;
        public bool IsMove 
        { 
            get 
            { 
                return isMove; 
            } 
            set 
            { 
                isMove = value;
                //animator.SetBool(AnimationString.IsWalk, value);
            } 
        }

        // �ٱ�
        [SerializeField] private float runSpeed = 10f;
        [SerializeField] private bool isRun = false;

        [SerializeField] private float jumpPower = 8f;
        [SerializeField] private float airSpeed = 2f;

        public bool IsRun
        {
            get
            {
                return isRun;
            }
            set
            {
                isRun = value;
                animator.SetBool(AnimationString.IsRun, value);
            }
        }

        // �¿����
        private bool isFacingRight = true;

        public bool IsFacingRight
        {
            get
            {
                return isFacingRight;
            }
            set
            {
                // ����
                if(isFacingRight != value)
                {
                    transform.localScale *= new Vector2(-1, 1);
                }

                isFacingRight = value;
            }
        }

        public bool IsDeath
        {
            get
            {
                return animator.GetBool(AnimationString.IsDeath);
            }
        }


        TouchingDirection touchingDirection;
        Animator animator;
        Rigidbody2D rb;
        TrailEffect trailEffect;

        private void Awake()
        {
            Init();
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void FixedUpdate()
        {
            if(damageAble.LockVelocity == false)
            {
                PlayerMove();
            }



            animator.SetFloat(AnimationString.YVelocity, rb.velocity.y);
        }

        void Init()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            touchingDirection = GetComponent<TouchingDirection>();
            damageAble = GetComponent<DamageAble>();
            trailEffect = GetComponent<TrailEffect>();
            damageAble.hitAction += Hit; // UnityAction ��������Ʈ �Լ��� ���
            //startSpeed = moveSpeed;
        }

        void PlayerMove()
        {
            rb.velocity = new Vector2(movePos.x * CurrentSpeed, rb.velocity.y);
            //rb.velocity = new Vector2(movePos.x * moveSpeed, movePos.y * moveSpeed);

            MoveAnimation();

            if(CanMove)
            {
                PlayerRot(movePos);
            }


            if (trailEffect != null)
            {
                trailEffect.StartActiveTrail();
            }

            //if (movePos.x > 0f)
            //{
            //    rotY = 0f;
            //}
            //else if (movePos.x < 0f)
            //{
            //    rotY = 180f;
            //}


            //transform.rotation = Quaternion.Euler(transform.rotation.x, rotY, transform.rotation.z);

        }

        // �ٶ󺸴� �������� ��ȯ
        void PlayerRot(Vector2 moveInput)
        {
            if(moveInput.x > 0f && IsFacingRight == false)
            {
                IsFacingRight = true;

            }
            else if(moveInput.x < 0f && IsFacingRight == true)
            {
                IsFacingRight = false;

            }
        }

        void MoveAnimation()
        {
            if(movePos.x != 0)
            {
                animator.SetBool(AnimationString.IsWalk, true);
            }
            else
            {
                animator.SetBool(AnimationString.IsWalk, false);
            }
        }

        public void Move(InputAction.CallbackContext context)
        {
            movePos = context.ReadValue<Vector2>();

            IsMove = (movePos != Vector2.zero);
        }

        public void Run(InputAction.CallbackContext context)
        {
            if(context.started == true)
            {
                //moveSpeed = runSpeed;
                IsRun = true;
            }
            else if(context.canceled == true) // KeyUp
            {
                //moveSpeed = startSpeed;
                IsRun = false;
            }

        }
        public void Jump(InputAction.CallbackContext context)
        {
            if (context.started == true)
            {
                if (touchingDirection.IsGround == true)
                {
                    PlayerJump();
                }

            }

        }

        public void Attack(InputAction.CallbackContext context)
        {
            if (context.started == true)
            {
                if (touchingDirection.IsGround == true)
                {
                    animator.SetTrigger(AnimationString.AttackTrigger);
                }
            }
        }

        public void BowAttack(InputAction.CallbackContext context)
        {
            if (context.started == true)
            {
                if (touchingDirection.IsGround == true)
                {
                    animator.SetTrigger(AnimationString.BowTrigger);
                }
            }
        }

        void PlayerJump()
        {
            animator.SetTrigger(AnimationString.JumpTrigger);

            rb.velocity = new Vector2(rb.velocity.x, jumpPower);

            if(trailEffect != null)
            {
                trailEffect.StartActiveTrail(); 
            }
        }

        public void Hit(Vector2 knockback)
        {
            rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
        }

    }

}
