using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace UI.Window.ShowProcessors
{
    public class OverrideShowProcessor : IWindowShowProcessor
    {
        public IEnumerator Show(WindowView windowView, IList<WindowView> openedWindows, CancellationToken ct)
        {
            yield return windowView.Show(ct);

            if (ct.IsCancellationRequested)
            {
                yield break;
            }
            
            openedWindows.Add(windowView);
        }
    }
}