using System;
using DG.Tweening;
using UnityEngine;

namespace Code
{
    public class MenuManager : MonoBehaviour
    {
        [Header("Dead Menu")]
        [SerializeField] private GameObject deadMenu;
        [SerializeField] private CanvasGroup deadMenuCanvasGroup;

        private void Awake()
        {
            deadMenu.SetActive(false);
            deadMenuCanvasGroup.alpha = 0;
        }

        public void ShowDeadMenu()
        {
            deadMenu.SetActive(true);
            deadMenuCanvasGroup.DOFade(1, 0.5f);
        }

        public void HideDeadMenu()
        {
            deadMenu.SetActive(false);
            deadMenuCanvasGroup.DOFade(0, 0.5f);
        }
    }
}