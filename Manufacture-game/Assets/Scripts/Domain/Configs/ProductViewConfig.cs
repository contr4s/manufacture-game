using Domain.Items;
using UnityEngine;

namespace Domain.Configs
{
    [CreateAssetMenu(fileName = "ProductViewConfig", menuName = "Configs/ProductView")]
    public class ProductViewConfig : ItemViewConfig<ProductType>
    {
    }
}