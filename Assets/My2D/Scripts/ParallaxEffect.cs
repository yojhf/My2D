using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My2D
{
    // 플레이어 시차에 따른 오브젝트 이동
    public class ParallaxEffect : MonoBehaviour
    {
        public float test = 10f;

        public Camera playerCamera;
        public Transform targetObject;

        private Vector2 startPos;
        private float startPosZ; // 시작할 때 배경의 z축 위치 값

        // 배경과 플레이어와의 z축 거리
        private float zDistanceFromTarget => transform.position.z - targetObject.position.z;
        // 
        private float ClippingPlane => playerCamera.transform.position.z + (zDistanceFromTarget > 0 ? playerCamera.farClipPlane : playerCamera.nearClipPlane);

        // 시작지점으로 부터 카메라가 있는 위치까지의 거리
        private Vector2 CamMoveSceneStart => startPos - (Vector2)playerCamera.transform.position;

        // 시차 거리 factor
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