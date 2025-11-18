using Managers;
using UnityEngine;

namespace ZoneInfoSystem
{
    public class SafeZoneInfo : ZoneInfo
    {
        private int m_CurrentSafeZoneNumber;

        private void Start()
        {
            m_CurrentSafeZoneNumber = GameCommonVariableManager.GetFirstSafeZoneNumber();
            SetZoneNumber(m_CurrentSafeZoneNumber);
        }

        protected override void HandleZoneChanged(int zoneNumber)
        {
            if (zoneNumber <= 0) return;

            var safeInterval = GameCommonVariableManager.GetSafeZoneInterval();
            var superInterval = GameCommonVariableManager.GetSuperZoneInterval();

            if (zoneNumber == GameCommonVariableManager.GetFirstSafeZoneNumber())
            {
                m_CurrentSafeZoneNumber = GameCommonVariableManager.GetFirstSafeZoneNumber();
                SetZoneNumber(safeInterval);
                return;
            }

            
            switch (zoneNumber % safeInterval)
            {
                case 0 when (zoneNumber + safeInterval) % superInterval != 0:
                {
                    m_CurrentSafeZoneNumber = zoneNumber;
                    SetZoneNumber(m_CurrentSafeZoneNumber + safeInterval);
                    break;
                }
                case 0 when (zoneNumber + safeInterval) % superInterval == 0: // Super Zone skipped
                {
                    m_CurrentSafeZoneNumber = zoneNumber + safeInterval;
                    SetZoneNumber(m_CurrentSafeZoneNumber + safeInterval);
                    break;
                }
            }
            
        }
    }
}