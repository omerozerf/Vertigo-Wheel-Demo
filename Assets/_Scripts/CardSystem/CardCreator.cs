using SlotSystem;
using UnityEngine;

namespace CardSystem
{
    public class CardCreator : MonoBehaviour
    {
        [SerializeField] private Card _cardPrefab;


        public Card CreateCard(Slot slot)
        {
            var card = Instantiate(_cardPrefab, transform);
            var rt = card.GetRectTransform();

            rt.localRotation = Quaternion.identity;
            rt.localScale = Vector3.one;
            
            card.SetCount(0);
            card.SetImage(slot.GetSlotSO().GetIcon());
            
            return card;
        }
    }
}