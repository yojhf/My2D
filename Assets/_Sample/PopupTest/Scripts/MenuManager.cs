using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MySample
{
    public class MenuManager : PersistentSingleton<MenuManager>
    {
        public List<Popup> popupStack = new List<Popup>();
        // 팝업 메뉴의 부모캔버스 오브젝트
        [SerializeField] private Canvas canvas;

        private void OnEnable()
        {
            // 팝업의 스태틱 이벤트 함수 등록
            Popup.OnClosePopup += ClosePopup;
            Popup.OnBeforeClosePopup += BeforeClosePopup;
        }

        private void OnDisable()
        {
            // 팝업의 스태틱 이벤트 함수 해제
            Popup.OnClosePopup -= ClosePopup;
            Popup.OnBeforeClosePopup -= BeforeClosePopup;
        }

        void ClosePopup(Popup _popupClose)
        {
            if (popupStack.Count > 0)
            {
                popupStack.Remove(_popupClose);

                if (popupStack.Count > 0)
                {
                    var popup = popupStack.Last<Popup>();

                    popup.Show();
                }
            }
        }

        void BeforeClosePopup(Popup _popupClose)
        { 
            // TODO 페이드 효과  
        }

        // 팝업창 열기
        public T ShowPopup<T>(Action _onShow = null, Action<PopupResult> _onClose = null) where T : Popup
        {
            // 이미 창이 열렸는지 체크
            if (popupStack.OfType<T>().Any())
            {
                return popupStack.OfType<T>().First();
            }

            return (T)ShowPopup("Popups/" + typeof(T).Name, _onShow, _onClose);
        }

        public Popup ShowPopup(string _path, Action _onShow = null, Action<PopupResult> _onClose = null)
        {
            // 리소스 로드
            if (popupStack.Any(p => p.GetType().Name == _path.Split('/').Last()))
            {
                return popupStack.First(p => p.GetType().Name == _path.Split('/').Last());
            }

            // 없으면
            var popupPrefab = Resources.Load<Popup>(_path);

            if (popupPrefab == null)
            {
                Debug.Log("찾는 팝업이 없음");
                return null;
            }

            return ShowPopup(popupPrefab, _onShow, _onClose);
        }

        public Popup ShowPopup(Popup _popupPrefab, Action _onShow = null, Action<PopupResult> _onClose = null)
        {
            var popup = Instantiate(_popupPrefab, canvas.transform);

            if (popupStack.Count > 0)
            { 
                popupStack.Last().Hide();
            }

            popupStack.Add(popup);
            popup.Show<Popup>(_onShow, _onClose);

            var rectTransform = popup.GetComponent<RectTransform>();

            rectTransform.anchoredPosition = Vector2.zero;
            rectTransform.sizeDelta = Vector2.zero;

            return popup;
        }

        // 특정 타입(T)의 팝업을 찾는 함수
        public T GetPopupOpened<T>() where T : Popup
        {
            foreach (var popup in popupStack)
            {
                if (popup.GetType() == typeof(T))
                { 
                    return (T)popup;
                }
            }

            return null;
        }

        // 열려있는 모든 창 닫기
        public void CloseAllPopups()
        {
            for (int i = 0; i < popupStack.Count; i++)
            {
                popupStack[i].Close();
            }

            popupStack.Clear();
        }

        // 창이 하나라도 열려 있는지
        public bool IsAnyPopupOpened()
        {
            return popupStack.Count > 0;
        }

        // 마지막에 열려 있는 창
        public Popup GetLastPopup()
        { 
            return popupStack.Last();
        }

    }
}