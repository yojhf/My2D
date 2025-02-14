using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MySample
{
    // 옵션창
    public class Settings : Popup
    {
        // 게임종료 창 오픈
        [SerializeField] private CustomButton back;
        // 개인정보 동의 창
        [SerializeField] private CustomButton privacy;

        private void OnEnable()
        {
            // 옵션창 버튼 클릭 시 호출되는 함수 등록
            back.onClick.AddListener(BackToMain);
            privacy.onClick.AddListener(PrivacyPolicy);
        }

        void BackToMain()
        {
            StopInteraction();
            Close();

            // 게임종료 창 오픈
            MenuManager.Instance.ShowPopup<ExitGame>();
        }

        void PrivacyPolicy()
        {
            StopInteraction();
            Close();

            // 개인정보 동의 창 오픈
            MenuManager.Instance.ShowPopup<GDPR>();
        }
    }
}