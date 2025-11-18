using System;
using UnityEngine;

namespace Managers
{
    public class GameCommonVariableManager : MonoBehaviour
    {
        [SerializeField] private int _safeZoneInterval;
        [SerializeField] private int _superZoneInterval;
        [SerializeField] private float _wheelScale;
        [SerializeField] private Transform _wheelTransform;
    
        private static GameCommonVariableManager ms_Instance;


        private void Awake()
        {
            ms_Instance = this;
        }
        
        private void OnValidate()
        {
            SetWheelTransformScale();
        }
        
        
        private void SetWheelTransformScale()
        {
            if (_wheelTransform) _wheelTransform.localScale = Vector3.one * _wheelScale;
        }


        public static int GetSafeZoneInterval()
        {
            return ms_Instance._safeZoneInterval;
        }
    
        public static int GetSuperZoneInterval()
        {
            return ms_Instance._superZoneInterval;
        }
    }
}