using System.Collections.Generic;
using UnityEngine;

namespace Domain.Configs
{
    [CreateAssetMenu(fileName = "ResourceConfig", menuName = "Configs/Resource")]
    public class ResourceConfig : ScriptableObject
    {
        [SerializeField] private float[] buildingsProductionSpeed;

        public IReadOnlyList<float> BuildingsProductionSpeed => buildingsProductionSpeed;
    }
}