using UnityEngine;

namespace Domain.Configs
{
    [CreateAssetMenu(fileName = "EconomicsConfig", menuName = "Configs/Economics")]
    public class EconomicsConfig : ScriptableObject
    {
        [field: SerializeField] public int TargetCoins { get; private set; } = 100;
    }
}