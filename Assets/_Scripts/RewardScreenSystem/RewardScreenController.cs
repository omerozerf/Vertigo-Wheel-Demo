using ButtonSystem;
using DG.Tweening;
using ScreenSystem;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RewardScreenSystem
{
    public class RewardScreenController : ScreenController, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private RectTransform _dragTarget;   // Kaydırılacak olan rect
        
        private Vector2 m_StartAnchoredPos;
        private Vector2 m_StartPointerLocalPos;
        
        public override void Show()
        {
            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.DOFade(1f, _fadeInDuration).SetEase(Ease.InOutSine);
        }

        public override void Hide()
        {
            _canvasGroup.blocksRaycasts = false;
            _canvasGroup.DOFade(0f, _fadeInDuration).SetEase(Ease.InOutSine);
        }
        
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            if (_dragTarget == null) return;

            // Başlangıç anchored position
            m_StartAnchoredPos = _dragTarget.anchoredPosition;

            // Pointer'ı parent rect uzayına çevir (ekrandan bağımsız olsun diye)
            var parentRect = _dragTarget.parent as RectTransform;
            if (parentRect == null) return;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                parentRect,
                eventData.position,
                eventData.pressEventCamera,
                out m_StartPointerLocalPos
            );
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_dragTarget == null) return;

            var parentRect = _dragTarget.parent as RectTransform;
            if (parentRect == null) return;

            // Şu anki pointer pozisyonunu aynı parent uzayına çevir
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                parentRect,
                eventData.position,
                eventData.pressEventCamera,
                out var currentPointerLocalPos
            );

            // Sadece X ekseninde kaydır
            var delta = currentPointerLocalPos - m_StartPointerLocalPos;
            _dragTarget.anchoredPosition = m_StartAnchoredPos + new Vector2(delta.x, 0f);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (_dragTarget == null) return;

            // İstersen burada DOTween ile snap back / sayfa geçişi yaparsın
            // Örn: _dragTarget.DOAnchorPos(m_StartAnchoredPos, 0.2f).SetEase(Ease.OutQuad);
        }
    }
}