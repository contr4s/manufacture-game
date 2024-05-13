using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace UI.Window
{
    public interface IWindowShowProcessor
    {
        IEnumerator Show(WindowView windowView, IList<WindowView> openedWindows, CancellationToken ct);
    }
}