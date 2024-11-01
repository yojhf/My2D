using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어 잔상 효과 : 2초동안 잔상효과 발생
namespace My2D
{
    public class TrailEffect : MonoBehaviour
    {
        [SerializeField] private float trailTime = 2f; // 잔상유효시간
        [SerializeField] private float trailRefreshRate = 0.1f; // 잔상들의 발생 간격
        [SerializeField] private float trailDelay = 1f; // 1초후 킬 -> 페이드 아웃

        [SerializeField] private string shaderValueRef = "_Alpha";
        [SerializeField] private float shaderValueRate = 0.1f; // 알파값 감소 비율
        [SerializeField] private float shaderValueRefRate = 0.05f; // 알파값 감소되는 시간 간격

        private bool isTrailActive = false;

        public Material ghostMaterial; // 잔상 마테리얼
        
        SpriteRenderer playerRenderer;

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

        void Init()
        {
            playerRenderer = GetComponent<SpriteRenderer>();
        }

        // 2초동안 잔상효과 발생
        public void StartActiveTrail()
        {
            if(isTrailActive == true)
            {
                return;
            }

            isTrailActive = true;

            StartCoroutine(ActiveTrail(trailTime));
        }

        // activeTime동안 잔상효과 발생
        IEnumerator ActiveTrail(float activeTime)
        {
            while(activeTime > 0)
            {
                activeTime -= trailRefreshRate;

                // 잔상효과
                // 트랜스폼
                GameObject ghostObj = new GameObject();

                ghostObj.transform.SetPositionAndRotation(transform.position, transform.rotation);

                ghostObj.transform.localScale = transform.localScale;

                // 스프라이트
                SpriteRenderer renderer =  ghostObj.AddComponent<SpriteRenderer>();

                renderer.sprite = playerRenderer.sprite;

                renderer.sortingLayerName = playerRenderer.sortingLayerName;

                renderer.sortingOrder = playerRenderer.sortingOrder - 1;

                renderer.material = ghostMaterial;

                // Material(알파값) 감소

                StartCoroutine(AnimateMaterialFloat(renderer.material, shaderValueRef, 0f, shaderValueRate, shaderValueRefRate));

                Destroy(ghostObj, trailDelay);

                yield return new WaitForSeconds(trailRefreshRate);
            }

            isTrailActive = false;

        }

        IEnumerator AnimateMaterialFloat(Material mat, string valueRef, float goal, float rate, float refRate)
        {
            float valueToAnimate = mat.GetFloat(valueRef);

            while (valueToAnimate > goal)
            {
                valueToAnimate -= rate;

                mat.SetFloat(valueRef, valueToAnimate);


                yield return new WaitForSeconds(refRate);
            }


        }
    }
}