using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace My2D
{
    public class HealthText : MonoBehaviour
    {
        public TMP_Text healthText;
        private RectTransform textTransform;

        [SerializeField] private float moveSpeed = 100f;

        public float fadeTimer = 1f;
        private float count = 0f;

        private Color startColor;

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
            TextMove();
        }

        void Init()
        {
            healthText = GetComponent<TMP_Text>();
            textTransform = GetComponent<RectTransform>();

            startColor = healthText.color;
            count = fadeTimer;
        }

        void TextMove()
        {
            textTransform.position += Vector3.up * moveSpeed * Time.deltaTime;

            count -= Time.deltaTime;

            float newAlpha = startColor.a * (count / fadeTimer);

            healthText.color = new Color(startColor.r, startColor.g, startColor.b, newAlpha);

            if (count <= 0f)
            {
                Destroy(gameObject);
            }


        }
    }
}