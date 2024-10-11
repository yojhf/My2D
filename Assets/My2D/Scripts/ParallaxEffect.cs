using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My2D
{
    // �÷��̾� ������ ���� ������Ʈ �̵�
    public class ParallaxEffect : MonoBehaviour
    {
        public float test = 10f;

        public Camera playerCamera;
        public Transform targetObject;

        private Vector2 startPos;
        private float startPosZ; // ������ �� ����� z�� ��ġ ��

        // ���� �÷��̾���� z�� �Ÿ�
        private float zDistanceFromTarget => transform.position.z - targetObject.position.z;
        // 
        private float ClippingPlane => playerCamera.transform.position.z + (zDistanceFromTarget > 0 ? playerCamera.farClipPlane : playerCamera.nearClipPlane);

        // ������������ ���� ī�޶� �ִ� ��ġ������ �Ÿ�
        private Vector2 CamMoveSceneStart => startPos - (Vector2)playerCamera.transform.position;

        // ���� �Ÿ� factor
        private float ParallaxFactor => Mathf.Abs(zDistanceFromTarget) / ClippingPlane;

        // Start is called before the first frame update
        void Start()
        {
            Init();
        }

        // Update is called once per frame
        void Update()
        {
            MoveParallax();
        }

        void Init()
        {
            startPos = transform.position;
            startPosZ = transform.position.z;
        }

        void MoveParallax()
        {
            Vector2 newPos = startPos + CamMoveSceneStart * ParallaxFactor * test;

            transform.position = new Vector3(newPos.x, newPos.y, startPosZ);
        }
    }
}