using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using ObjectPoolSystem;
using UnityEngine;

namespace WheelSystem.WheelEffectSystem
{
    public class WheelSlotEffectController : MonoBehaviour
    {
        [Header("Pooling")]
        [SerializeField] private ObjectPoolManagerUI _objectPoolManager;
        [SerializeField] private ObjectPoolType _objectPoolType = ObjectPoolType.None;

        [Header("UI References")]
        [SerializeField] private RectTransform _effectParent;

        [Header("Effect Settings")]
        [SerializeField] private float _scatterRadius = 40f;
        [SerializeField] private float _scatterDuration = 0.2f;
        [SerializeField] private float _moveDuration = 0.4f;
        [SerializeField] private float _betweenSpawnDelay = 0.03f;
        
        
        public UniTask PlayAsync(int spawnCount, RectTransform spawnTransform, RectTransform targetTransform)
        {
            return PlayInternalAsync(spawnCount, spawnTransform, targetTransform);
        }

        private async UniTask PlayInternalAsync(int spawnCount, RectTransform spawnTransform, RectTransform targetTransform)
        {
            if (_objectPoolManager == null || spawnTransform == null || targetTransform == null)
            {
                Debug.LogWarning("[WheelSlotEffectController] Referanslar eksik.", this);
                return;
            }

            if (_objectPoolType == ObjectPoolType.None)
            {
                Debug.LogWarning("[WheelSlotEffectController] ObjectPoolType.None kullanılamaz.", this);
                return;
            }

            if (spawnCount <= 0)
                return;

            for (int i = 0; i < spawnCount; i++)
            {
                SpawnSingleEffect(spawnTransform, targetTransform);

                if (_betweenSpawnDelay > 0f && i < spawnCount - 1)
                {
                    await UniTask.Delay(TimeSpan.FromSeconds(_betweenSpawnDelay));
                }
            }
        }

        private void SpawnSingleEffect(RectTransform spawnTransform, RectTransform targetTransform)
        {
            var instance = _objectPoolManager.Spawn(_objectPoolType, Vector3.zero, Quaternion.identity);
            if (instance == null)
                return;

            if (!instance.TryGetComponent<RectTransform>(out var rect))
            {
                Debug.LogError("[WheelSlotEffectController] Pooled obje RectTransform içermiyor.", instance);
                return;
            }

            // 1) Ortak parent
            var parentRect = _effectParent != null 
                ? _effectParent 
                : spawnTransform.parent as RectTransform;

            if (parentRect == null)
            {
                Debug.LogError("[WheelSlotEffectController] Effect parent RectTransform değil.", this);
                return;
            }

            // 2) Parent'ı ayarla
            rect.SetParent(parentRect, worldPositionStays: false);

            // 3) spawn ve target noktalarını world'ten parent localine çevir
            Vector2 spawnLocal = WorldToLocalInParent(parentRect, spawnTransform);
            Vector2 targetLocal = WorldToLocalInParent(parentRect, targetTransform);

            // 4) başlangıç pozisyonu
            rect.anchoredPosition = spawnLocal;

            // 5) scatter + target
            Vector2 randomOffset = UnityEngine.Random.insideUnitCircle * _scatterRadius;
            Vector2 scatterPos = spawnLocal + randomOffset;

            var sequence = DOTween.Sequence();
            sequence.Append(
                rect.DOAnchorPos(scatterPos, _scatterDuration)
                    .SetEase(Ease.OutQuad)
            );
            sequence.Append(
                rect.DOAnchorPos(targetLocal, _moveDuration)
                    .SetEase(Ease.InQuad)
            );

            float totalDuration = _scatterDuration + _moveDuration;
            _objectPoolManager.DespawnAfter(_objectPoolType, instance, totalDuration + 0.05f)
                .Forget();
        }

// Helper
        private static Vector2 WorldToLocalInParent(RectTransform parent, RectTransform child)
        {
            Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(null, child.position);
            RectTransformUtility.ScreenPointToLocalPointInRectangle(parent, screenPos, null, out var localPos);
            return localPos;
        }    }
}