using Managers;
using UnityEngine;

namespace ZoneInfoSystem
{
    public class SuperZoneInfo : ZoneInfo
    {
        private int m_CurrentSuperZoneNumber;
        
        private void Start()
        {
            // İlk super zone’u ayarla (örneğin: 30)
            m_CurrentSuperZoneNumber = GameCommonVariableManager.GetFirstSuperZoneNumber();
            SetZoneNumber(m_CurrentSuperZoneNumber);
        }

        protected override void HandleZoneChanged(int zoneNumber)
        {
            if (zoneNumber <= 0) return;

            var superInterval = GameCommonVariableManager.GetSuperZoneInterval();

            if (zoneNumber < GameCommonVariableManager.GetFirstSuperZoneNumber())
                return;

            if (zoneNumber == GameCommonVariableManager.GetFirstSuperZoneNumber())
            {
                m_CurrentSuperZoneNumber = GameCommonVariableManager.GetFirstSuperZoneNumber();
                SetZoneNumber(m_CurrentSuperZoneNumber + superInterval);
                return;
            }

            if (zoneNumber % superInterval == 0)
            {
                m_CurrentSuperZoneNumber = zoneNumber;
                SetZoneNumber(m_CurrentSuperZoneNumber + superInterval);
            }
        }
    }
}