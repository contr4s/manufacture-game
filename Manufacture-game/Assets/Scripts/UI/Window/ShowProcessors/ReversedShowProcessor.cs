using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace UI.Window.ShowProcessors
{
    public class ReversedShowProcessor : IWindowShowProcessor
    {
        public IEnumerator Show(WindowView windowView, IList<WindowView> openedWindows, CancellationToken ct)
        {
            yield return windowView.Show(ct);
            
            foreach (WindowView openedWindow in openedWindows)
            {
                yield return openedWindow.Hide(ct);
            }
            
            if (ct.IsCancellationRequested)
            {
                yield break;
            }
            
            openedWindows.Clear();
            openedWindows.Add(windowView);
        }
    }
}