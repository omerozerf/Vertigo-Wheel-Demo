using CardSystem;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RewardScreenSystem
{
    public class RewardCard : MonoBehaviour
    {
        [SerializeField] private TMP_Text _cardHeaderText;
        [SerializeField] private Image _cardImage;
        [SerializeField] private TMP_Text _cardCountText;
        [SerializeField] private Image _cardShineEffectImage;

        [SerializeField] private float _shineRotateDuration = 1f;
        [SerializeField] private float _shineColorDuration = 0.5f;

        private Tween m_ShineRotateTween;
        private Tween m_ShineColorTween;

        public void SetCardData(Card card)
        {
            var slotSO = card.GetSlotSO();
            
            _cardHeaderText.text = slotSO.GetName().Replace(" ", "\n");
            _cardImage.sprite = slotSO.GetIcon();
            _cardCountText.text = $"x{card.GetCount()}";
        }

        public void ShineEffect()
        {
            if (_cardShineEffectImage == null)
                return;

            StopShine();

            var rect = _cardShineEffectImage.rectTransform;
            m_ShineRotateTween = rect
                .DORotate(new Vector3(0f, 0f, 360f), _shineRotateDuration, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Restart);

            var startColor = Color.white;
            var midColor = new Color(0.5f, 0.5f, 0.5f, 1f);

            _cardShineEffectImage.color = startColor;

            m_ShineColorTween = _cardShineEffectImage
                .DOColor(midColor, _shineColorDuration)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Yoyo);
        }

        private void StopShine()
        {
            if (m_ShineRotateTween != null && m_ShineRotateTween.IsActive())
            {
                m_ShineRotateTween.Kill();
                m_ShineRotateTween = null;
            }

            if (m_ShineColorTween != null && m_ShineColorTween.IsActive())
            {
                m_ShineColorTween.Kill();
                m_ShineColorTween = null;
            }

            if (_cardShineEffectImage != null)
            {
                _cardShineEffectImage.rectTransform.localRotation = Quaternion.identity;
                _cardShineEffectImage.color = Color.white;
            }
        }

        private void OnDisable()
        {
            StopShine();
        }
    }
}