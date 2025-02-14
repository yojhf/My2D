using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace MySample
{
    // �˾� UI�� �����ϴ� �θ� Ŭ���� : show, hide ���(�ִϸ��̼� ����), close(��ư) - ����� ����
    [RequireComponent(typeof(Animator), typeof(CanvasGroup))]
    public class Popup : MonoBehaviour
    {
        // �˾� ���̵� ���
        public bool fade = false;
        // â �ݱ� ��ư
        public CustomButton closeButton;
        
        Animator animator;
        CanvasGroup canvasGroup;

        // �˾�â show �� �� ��ϵ� �Լ� ȣ��
        public Action OnShowAction;
        // �˾�â close �� �� ��ϵ� �Լ� ȣ��
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

            // close ��ư Ŭ�� �� ȣ��Ǵ� �Լ� ���
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

        // Show �ִϸ��̼� �߰��� �̺�Ʈ �Լ� ����Ͽ� ȿ���� ���
        public virtual void ShowAnimationSound()
        { 
            // TODO ����ȿ���� ���
        }
        // Hide �ִϸ��̼� �߰��� �̺�Ʈ �Լ� ����Ͽ� ȿ���� ���
        public virtual void HideAnimationSound()
        {
            // TODO â ���� �� ȿ���� ���
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
            // ���̵� ȿ��
            canvasGroup.alpha = 1.0f;
        }

        public virtual void Hide()
        {
            canvasGroup.interactable = false;
            // ���̵� ȿ��
            canvasGroup.alpha = 0.0f;
        }

        protected void StopInteraction()
        {
            canvasGroup.interactable = false;
        }
    }
}