using UnityEngine;

namespace Util.ObjectPool
{
    public class PoolContainersHolder : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }
    }
}