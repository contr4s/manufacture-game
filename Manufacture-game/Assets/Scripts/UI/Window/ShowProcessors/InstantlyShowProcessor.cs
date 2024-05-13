using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace UI.Window.ShowProcessors
{
    public class InstantlyShowProcessor : IWindowShowProcessor
    {
        public IEnumerator Show(WindowView windowView, IList<WindowView> openedWindows, CancellationToken ct)
        {
            foreach (WindowView openedWindow in openedWindows)
            {
                openedWindow.InstantlyHide();
            }
            
            windowView.InstantlyShow();
            openedWindows.Clear();
            openedWindows.Add(windowView);
            yield break;
        }
    }
}