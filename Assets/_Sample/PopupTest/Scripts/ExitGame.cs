using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MySample
{
    // 게임 종료 창 : 예(게임종료), 아니오
    public class ExitGame : Popup
    {
        // 게임종료
        [SerializeField] private CustomButton yes;

        private void OnEnable()
        {
            // 옵션창 버튼 클릭 시 호출되는 함수 등록
            yes.onClick.AddListener(StopGame);
        }

        void StopGame()
        {
            // 게임종료
            Application.Quit();
            Debug.Log("게임종료");
        }
    }
}