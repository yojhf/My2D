using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My2D
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager instance;

        [SerializeField] private GameObject damageText;
        [SerializeField] private GameObject healText;
        [SerializeField] private Vector2 offset;
        private Canvas canvas;

        private void Awake()
        {
            instance = this;
        }
        // Start is called before the first frame update
        void Start()
        {
            Init();
        }

        private void OnEnable()
        {
            CharacterEvent.characterDamaged += DamageText;
            CharacterEvent.characterHealed += HealText;
        }
        private void OnDisable()
        {
            CharacterEvent.characterDamaged -= DamageText;
            CharacterEvent.characterHealed -= HealText;
        }

        // Update is called once per frame
        void Update()
        {

        }

        void Init()
        {
            canvas = FindObjectOfType<Canvas>();
        }

        public void DamageText(GameObject character, float damage)
        {
            Vector3 damagePos = Camera.main.WorldToScreenPoint(new Vector3(character.transform.position.x, character.transform.position.y + offset.y, 0));

            GameObject tmpText = Instantiate(damageText, damagePos, Quaternion.identity, canvas.transform);

            tmpText.GetComponent<HealthText>().healthText.text = damage.ToString();
        }

        public void HealText(GameObject character, float heal)
        {
            Vector3 healPos = Camera.main.WorldToScreenPoint(new Vector3(character.transform.position.x, character.transform.position.y + offset.y, 0));

            GameObject tmpText = Instantiate(healText, healPos, Quaternion.identity, canvas.transform);

            tmpText.GetComponent<HealthText>().healthText.text = heal.ToString();
        }
    }
}