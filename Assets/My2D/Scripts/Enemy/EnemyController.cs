using UnityEngine;

namespace My2D
{
    public enum WalkableDirection
    {
        Left,
        Right
    }


    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private float e_runSpeed = 4f;
        private Vector2 e_direction = Vector2.right;

        private WalkableDirection walkDirection = WalkableDirection.Right;

        public WalkableDirection WalkDirection
        {
            get 
            { 
                return walkDirection; 
            }
            private set
            {
                transform.localScale *= new Vector2(-1, 1);

                if(value == WalkableDirection.Left)
                {
                    e_direction = Vector2.left;
                }
                else if(value == WalkableDirection.Right)
                {
                    e_direction = Vector2.right;
                }

                walkDirection = value;
            }
        }

        private bool hasTarget = false;

        public bool HasTarget
        {
            get
            {
                return hasTarget;
            }
            set
            {
                hasTarget = value;
                e_animator.SetBool(AnimationString.HasTarget, value);
            }
        }

        public bool E_CanMove
        {
            get
            {
                return e_animator.GetBool(AnimationString.CanMove);
            }
        }

        private float stopRate = 0.2f;

        DetectionZone detectionZone;
        // ³¶¶³¾îÁö °¨Áö
        DetectionZone detectionCliff;
        TouchingDirection touchingDirection;
        Rigidbody2D e_rb;
        Animator e_animator;
        DamageAble damageAble;

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
            EnemyAttack();
        }

        private void FixedUpdate()
        {
            if (damageAble.LockVelocity == false)
            {
                EnemyMove();
            }
        }

        void Init()
        {
            e_animator = GetComponent<Animator>();
            e_rb = GetComponent<Rigidbody2D>();
            touchingDirection = GetComponent<TouchingDirection>();
            detectionZone = transform.GetChild(0).GetComponent<DetectionZone>();
            detectionCliff = transform.GetChild(1).GetComponent<DetectionZone>();
            damageAble = GetComponent<DamageAble>();

            damageAble.hitAction += Hit;
            detectionCliff.noColRemain += Cliff;
        }

        void EnemyMove()
        {
            if (touchingDirection.IsWall == true && touchingDirection.IsGround == true)
            {
                Flip();
            }


            if (E_CanMove)
            {
                e_rb.velocity = new Vector2(e_direction.x * e_runSpeed, e_direction.y);
            }
            else
            {
                e_rb.velocity = new Vector2(Mathf.Lerp(e_rb.velocity.x, 0f, stopRate), e_direction.y);
            }

        }

        void EnemyAttack()
        {
            HasTarget = (detectionZone.detectedColliders.Count > 0);
        }

        public void Flip()
        {
            Vector3 rotation = transform.eulerAngles;


            if (WalkDirection == WalkableDirection.Left)
            {
                WalkDirection = WalkableDirection.Right;
            }
            else if(WalkDirection == WalkableDirection.Right)
            {
                WalkDirection = WalkableDirection.Left;
            }
            else
            {
                Debug.Log("Error");
            }

            transform.eulerAngles = rotation;
        }

        public void Hit(Vector2 knockback)
        {
            e_rb.velocity = new Vector2(knockback.x, e_rb.velocity.y + knockback.y);
        }
        public void Cliff()
        {
            if(touchingDirection.IsGround)
            {
                Flip();
            }

        }
    }
}