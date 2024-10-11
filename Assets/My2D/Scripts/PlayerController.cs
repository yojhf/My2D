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

        // 뛰기
        [SerializeField] private float runSpeed = 10f;
        [SerializeField] private bool isRun = false;
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

        // 좌우반전
        private bool isFacingRight = true;

        public bool IsFacingRight
        {
            get
            {
                return isFacingRight;
            }
            set
            {
                // 반전
                if(isFacingRight != value)
                {
                    transform.localScale *= new Vector2(-1, 1);
                }

                isFacingRight = value;
            }
        }

        Animator animator;
        Rigidbody2D rb;

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
            PlayerMove();
        }

        void Init()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            startSpeed = moveSpeed;
        }

        void PlayerMove()
        {
            rb.velocity = new Vector2(movePos.x * moveSpeed, rb.velocity.y);
            //rb.velocity = new Vector2(movePos.x * moveSpeed, movePos.y * moveSpeed);

            MoveAnimation();

            PlayerRot(movePos);

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

        // 바라보는 방향으로 전환
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
                moveSpeed = runSpeed;
                IsRun = true;
            }
            else if(context.canceled == true) // KeyUp
            {
                moveSpeed = startSpeed;
                IsRun = false;
            }

        }
        public void Jump(InputAction.CallbackContext context)
        {


        }

    }

}
