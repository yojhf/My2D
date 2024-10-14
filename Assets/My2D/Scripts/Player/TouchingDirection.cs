using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My2D
{
    public class TouchingDirection : MonoBehaviour
    {
        [SerializeField] private ContactFilter2D contactFilter;
        [SerializeField] private float groundDistance = 0.05f;
        [SerializeField] private float ceilingDistance = 0.05f;
        [SerializeField] private float wallDistance = 0.2f;

        private RaycastHit2D[] raycastHit = new RaycastHit2D[5];
        private RaycastHit2D[] wallHit = new RaycastHit2D[5];
        private RaycastHit2D[] ceilingHit = new RaycastHit2D[5];

        [SerializeField] private bool isGround;
        

        public bool IsGround
        {
            get
            {
                return isGround;
            }
            set
            {
                isGround = value;
                animator.SetBool(AnimationString.IsGround, value);
            }
        }


        [SerializeField] private bool isWall;
        public bool IsWall
        {
            get
            {
                return isWall;
            }
            set
            {
                isWall = value;
                animator.SetBool(AnimationString.IsWall, value);
            }
        }

        [SerializeField] private bool isCeiling;
        public bool IsCeiling
        {
            get
            {
                return isCeiling;
            }
            set
            {
                isCeiling = value;
                animator.SetBool(AnimationString.IsCeiling, value);
            }
        }



        private Vector2 WalkDirection => (transform.localScale.x > 0) ? Vector2.right : Vector2.left;

        Animator animator;
        CapsuleCollider2D capsulCol;
        PlayerController playerController;

        private void Awake()
        {
            capsulCol = GetComponent<CapsuleCollider2D>();
            animator = GetComponent<Animator>();
            playerController = GetComponent<PlayerController>();
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
            GroundCheck();
            WallCheck();
        }

        void GroundCheck()
        {
            int ground = capsulCol.Cast(Vector2.down, contactFilter, raycastHit, groundDistance);

            if (ground > 0)
            {
                IsGround = true;
            }
            else
            {
                IsGround = false;
            }
        }

        void WallCheck()
        {
            IsWall = (capsulCol.Cast(WalkDirection, contactFilter, wallHit, wallDistance) > 0);  
        }

        void CeilingCheck()
        {
            IsCeiling = (capsulCol.Cast(Vector2.up, contactFilter, ceilingHit, ceilingDistance) > 0);
        }

        
    }
}