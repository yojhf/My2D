using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MySample
{
    // Ŀ���� ��ư : ���� ��ư ��� �޾� ��� Ȯ��
    [RequireComponent(typeof(Animator), typeof(CanvasGroup))]
    public class CustomButton : Button
    {
        //public AudioClip overrrideClickSound;

        // ��ư ���� Ȯ��
        private bool isClicked;
        // ��ٿ� �ð� ���� ��ư�� �������� ������ �� ����
        private readonly float cooldownTime = 0.5f;

        // ��ư Ŭ�� �� ��ϵ� �Լ� ȣ��
        public new ButtonClickedEvent onClick;
        public new Animator animator;

        // ��� Ŀ���� ��ư ��� ����
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

            // TODO ������ ȿ����

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
