using Domain.Items;
using UnityEngine;

namespace Domain.Configs
{
    [CreateAssetMenu(fileName = "ResourceViewConfig", menuName = "Configs/ResourceView")]

    public class ResourceViewConfig : ItemViewConfig<ResourceType>
    {
    }
}