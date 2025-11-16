using System;
using System.Collections.Generic;
using SlotSystem;
using UnityEngine;
using WheelSystem;

namespace CardSystem
{
    public class CardPanelController : MonoBehaviour
    {
        [SerializeField] private CardCreator _cardCreator;
        
        private List<Card> m_CardList = new List<Card>();
        
        
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
            foreach (var card in m_CardList)
            {
                var isSameSlotSO = card.GetSlotSO() == slot.GetSlotSO();
                if (isSameSlotSO)
                {
                    card.SetCount(card.GetCount() + slot.GetCount());
                    return;
                }
            }
            var newCard = _cardCreator.CreateCard(slot);
            m_CardList.Add(newCard);
        }
        
        
        private void InitializeCardCreator()
        {
            if (!_cardCreator) _cardCreator = GetComponent<CardCreator>();
        }
    }
}