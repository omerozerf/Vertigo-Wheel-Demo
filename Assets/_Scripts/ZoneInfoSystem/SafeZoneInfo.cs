using UnityEngine;

namespace ZoneInfoSystem
{
    public class SafeZoneInfo : ZoneInfo
    {
        [SerializeField] private int _firstSafeZoneNumber;
        [SerializeField] private int _safeZoneNumberInterval;
        
        private int m_CurrentSafeZoneNumber;
        
        
        private void Start()
        {
            SetZoneNumber(_firstSafeZoneNumber);
            m_CurrentSafeZoneNumber = _firstSafeZoneNumber;
        }


        protected override void HandleZoneChanged(int obj)
        {
            if (obj <= 0) return;
            
            if (obj == _firstSafeZoneNumber)
            {
                SetZoneNumber(_safeZoneNumberInterval);
                m_CurrentSafeZoneNumber = _safeZoneNumberInterval;
            }
            else if (obj % _safeZoneNumberInterval == 0)
            {
                SetZoneNumber(m_CurrentSafeZoneNumber + _safeZoneNumberInterval);
                m_CurrentSafeZoneNumber += _safeZoneNumberInterval;
            }
        }
    }
}