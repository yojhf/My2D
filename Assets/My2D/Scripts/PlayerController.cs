using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace My2D
{
    public class PlayerController : MonoBehaviour
    {
        private Vector2 movePos;
        [SerializeField] private float moveSpeed = 4f;


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
        }

        void PlayerMove()
        {
            rb.velocity = new Vector2(movePos.x * moveSpeed, rb.velocity.y);

            if (movePos.x > 0f)
            {
                
            }

        }

        public void Move(InputAction.CallbackContext context)
        {
            movePos = context.ReadValue<Vector2>();       
        }
    }

}
