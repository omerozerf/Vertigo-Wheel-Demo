using UnityEngine;

namespace ZoneInfoSystem
{
    public class SuperZoneInfo : ZoneInfo
    {
        [SerializeField] private int _firstSuperZoneNumber;
        [SerializeField] private int _superZoneNumberInterval;
        
        private int m_CurrentSafeZoneNumber;
        
        
        private void Start()
        {
            SetZoneNumber(_firstSuperZoneNumber);
        }


        protected override void HandleZoneChanged(int obj)
        {
            if (obj == _firstSuperZoneNumber)
            {
                SetZoneNumber(_superZoneNumberInterval);
                m_CurrentSafeZoneNumber = _superZoneNumberInterval;
            }
            else if (obj % _superZoneNumberInterval == 0)
            {
                SetZoneNumber(m_CurrentSafeZoneNumber + _superZoneNumberInterval);
                m_CurrentSafeZoneNumber += _superZoneNumberInterval;
            }
        }
    }
}