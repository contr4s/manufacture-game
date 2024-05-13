using System.Threading;

namespace Util.Extensions
{
    public static class CancellationTokenSourceExtensions
    {
        public static CancellationTokenSource Refresh(this CancellationTokenSource cst)
        {
            cst?.Cancel();
            cst?.Dispose();
            return new CancellationTokenSource();
        }
    }
}