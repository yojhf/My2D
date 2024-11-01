using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �÷��̾� �ܻ� ȿ�� : 2�ʵ��� �ܻ�ȿ�� �߻�
namespace My2D
{
    public class TrailEffect : MonoBehaviour
    {
        [SerializeField] private float trailTime = 2f; // �ܻ���ȿ�ð�
        [SerializeField] private float trailRefreshRate = 0.1f; // �ܻ���� �߻� ����
        [SerializeField] private float trailDelay = 1f; // 1���� ų -> ���̵� �ƿ�

        [SerializeField] private string shaderValueRef = "_Alpha";
        [SerializeField] private float shaderValueRate = 0.1f; // ���İ� ���� ����
        [SerializeField] private float shaderValueRefRate = 0.05f; // ���İ� ���ҵǴ� �ð� ����

        private bool isTrailActive = false;

        public Material ghostMaterial; // �ܻ� ���׸���
        
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

        // 2�ʵ��� �ܻ�ȿ�� �߻�
        public void StartActiveTrail()
        {
            if(isTrailActive == true)
            {
                return;
            }

            isTrailActive = true;

            StartCoroutine(ActiveTrail(trailTime));
        }

        // activeTime���� �ܻ�ȿ�� �߻�
        IEnumerator ActiveTrail(float activeTime)
        {
            while(activeTime > 0)
            {
                activeTime -= trailRefreshRate;

                // �ܻ�ȿ��
                // Ʈ������
                GameObject ghostObj = new GameObject();

                ghostObj.transform.SetPositionAndRotation(transform.position, transform.rotation);

                ghostObj.transform.localScale = transform.localScale;

                // ��������Ʈ
                SpriteRenderer renderer =  ghostObj.AddComponent<SpriteRenderer>();

                renderer.sprite = playerRenderer.sprite;

                renderer.sortingLayerName = playerRenderer.sortingLayerName;

                renderer.sortingOrder = playerRenderer.sortingOrder - 1;

                renderer.material = ghostMaterial;

                // Material(���İ�) ����

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