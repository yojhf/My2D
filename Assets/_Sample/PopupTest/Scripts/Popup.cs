using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace MySample
{
    // 팝업 UI를 관리하는 부모 클래스 : show, hide 기능(애니메이션 구현), close(버튼) - 결과를 리턴
    [RequireComponent(typeof(Animator), typeof(CanvasGroup))]
    public class Popup : MonoBehaviour
    {
        // 팝업 페이드 기능
        public bool fade = false;
        // 창 닫기 버튼
        public CustomButton closeButton;
        
        Animator animator;
        CanvasGroup canvasGroup;

        // 팝업창 show 할 때 등록된 함수 호출
        public Action OnShowAction;
        // 팝업창 close 할 때 등록된 함수 호출
        public Action<PopupResult> OnCloseAction;
        protected PopupResult popupResult;

        public delegate void PopupEvents(Popup _popup);

        public static event PopupEvents OnOpenPopup;
        public static event PopupEvents OnClosePopup;
        public static event PopupEvents OnBeforeClosePopup;

        private void Awake()
        {
            Init();
        }

        void Init()
        {
            animator = GetComponent<Animator>();
            canvasGroup = GetComponent<CanvasGroup>();

            // close 버튼 클릭 시 호출되는 함수 등록
            if (closeButton != null)
            {
                closeButton.onClick.AddListener(Close);
            }
        }

        public void Show<T>(Action _onShow = null, Action<PopupResult> _onClose = null)
        {
            if (_onShow != null)
            {
                OnShowAction = _onShow;
            }

            if (_onClose != null)
            {
                OnCloseAction = _onClose;
            }
            
            OnOpenPopup?.Invoke(this);

            PlayerShowAnimation();
        }

        public virtual void Close()
        {
            if (closeButton != null)
            {
                closeButton.interactable = false;
            }

            canvasGroup.interactable = false;

            OnBeforeClosePopup?.Invoke(this);

            if (animator != null)
            {
                animator.Play("popup_hide");
            }
        }

        void PlayerShowAnimation()
        {
            if (animator != null)
            {
                animator.Play("popup_show");
            }
        }

        // Show 애니메이션 중간에 이벤트 함수 등록하여 효과음 재생
        public virtual void ShowAnimationSound()
        { 
            // TODO 등장효과음 재생
        }
        // Hide 애니메이션 중간에 이벤트 함수 등록하여 효과음 재생
        public virtual void HideAnimationSound()
        {
            // TODO 창 닫을 때 효과음 재생
        }

        public virtual void AfterShowAnimation()
        {
            OnShowAction?.Invoke();
        }
        public virtual void AfterHideAnimation()
        {
            OnClosePopup?.Invoke(this);
            OnCloseAction?.Invoke(popupResult);

            Destroy(gameObject, 0.5f);
        }

        public void Show()
        {
            canvasGroup.interactable = true;
            // 페이드 효과
            canvasGroup.alpha = 1.0f;
        }

        public virtual void Hide()
        {
            canvasGroup.interactable = false;
            // 페이드 효과
            canvasGroup.alpha = 0.0f;
        }

        protected void StopInteraction()
        {
            canvasGroup.interactable = false;
        }
    }
}