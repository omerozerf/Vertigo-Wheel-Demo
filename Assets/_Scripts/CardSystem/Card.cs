using System;
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
            _countText.text = count.ToString();
        }

        public void SetImage(Sprite sprite)
        {
            _image.sprite = sprite;
        }
    }
}