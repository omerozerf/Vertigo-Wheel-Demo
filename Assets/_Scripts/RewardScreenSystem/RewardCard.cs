using CardSystem;
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
        
        
        public void SetCardData(Card card, int count)
        {
            var slotSO = card.GetSlotSO();
            
            _cardHeaderText.text = slotSO.GetName();
            _cardImage.sprite = slotSO.GetIcon();
            _cardCountText.text = count.ToString();
        }
    }
}