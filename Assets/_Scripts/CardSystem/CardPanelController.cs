using System;
using SlotSystem;
using UnityEngine;
using WheelSystem;

namespace CardSystem
{
    public class CardPanelController : MonoBehaviour
    {
        [SerializeField] private CardCreator _cardCreator;
        
        private void Awake()
        {
            WheelSlotSelector.OnSlotSelected += HandleSlotSelected;
        }
        
        private void OnDestroy()
        {
            WheelSlotSelector.OnSlotSelected -= HandleSlotSelected;
        }

        private void OnValidate()
        {
            InitializeCardCreator();
        }
        
        
        private void HandleSlotSelected(Slot slot)
        {
            _cardCreator.CreateCard(slot);
        }
        
        
        private void InitializeCardCreator()
        {
            if (!_cardCreator) _cardCreator = GetComponent<CardCreator>();
        }
    }
}