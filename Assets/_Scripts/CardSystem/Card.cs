using System;
using SlotSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CardSystem
{
    public class Card : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _countText;
        [SerializeField] private RectTransform _rectTransform;
        
        private int m_Count;
        private SlotSO m_SlotSO;
        

        private void OnValidate()
        {
            InitializeImage();
            InitializeCountText();
            InitializeRectTransform();
        }
        
        
        private void InitializeCountText()
        {
            if (!_countText) _countText = GetComponentInChildren<TMP_Text>();
        }

        private void InitializeRectTransform()
        {
            if (!_rectTransform) _rectTransform = GetComponent<RectTransform>();
        }

        private void InitializeImage()
        {
            if (!_image) _image = GetComponentInChildren<Image>();
        }
        
        
        public RectTransform GetRectTransform()
        {
            return _rectTransform;
        }
        
        public void SetCount(int count)
        {
            m_Count = count;
            _countText.text = count.ToString();
        }

        public void SetImage(Sprite sprite)
        {
            _image.sprite = sprite;
        }
        
        public SlotSO GetSlotSO()
        {
            return m_SlotSO;
        }
        
        public void SetSlotSO(SlotSO slotSO)
        {
            m_SlotSO = slotSO;
        }
        
        public int GetCount()
        {
            return m_Count;
        }
    }
}