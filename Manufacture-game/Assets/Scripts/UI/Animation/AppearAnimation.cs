using System.Collections;
using System.Threading;
using UnityEngine;

namespace UI.Animation
{
    public abstract class AppearAnimation : MonoBehaviour
    {
        public abstract IEnumerator ShowAnimation(CancellationToken ct);

        public abstract IEnumerator HideAnimation(CancellationToken ct);
    }
}