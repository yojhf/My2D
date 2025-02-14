using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MySample
{
    // 커스텀 버튼 : 기존 버튼 상속 받아 기능 확장
    [RequireComponent(typeof(Animator), typeof(CanvasGroup))]
    public class CustomButton : Button
    {
        //public AudioClip overrrideClickSound;

        // 버튼 눌림 확인
        private bool isClicked;
        // 쿨다운 시간 동안 버튼이 연속으로 눌리는 것 방지
        private readonly float cooldownTime = 0.5f;

        // 버튼 클릭 시 등록된 함수 호출
        public new ButtonClickedEvent onClick;
        public new Animator animator;

        // 모든 커스텀 버튼 기능 정지
        private static bool blockInput;

        protected override void OnEnable()
        {
            base.OnEnable();

            animator = GetComponent<Animator>();
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            if (blockInput || isClicked)
                return;

            // TODO 누르는 효과음

            Press();

            isClicked = true;

            if(gameObject.activeInHierarchy)
            {
                StartCoroutine(CoolDown());
            }

            base.OnPointerClick(eventData);
        }

        private void Press()
        {
            if (blockInput)
            {
                return;
            }

            onClick?.Invoke();
        }

        IEnumerator CoolDown()
        { 
            yield return new WaitForSeconds(cooldownTime);  

            isClicked = false;
        }

        private bool IsAnimationPlay()
        {
            var stateInfo = animator.GetCurrentAnimatorStateInfo(0);

            return stateInfo.loop || stateInfo.normalizedTime > 1f;
        }

        public static void SetBlockInput(bool block)
        {
            blockInput = block;
        }

    }
    
}
