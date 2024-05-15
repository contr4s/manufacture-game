using Domain;
using UI.Window;
using UI.Window.ShowProcessors;
using UI.Window.StartWindow;
using Zenject;

namespace UI
{
    public class UiInitializer : IInitializable
    {
        private readonly IWindowShowController _windowShowController;
        private readonly WorldInitializer _worldInitializer;
        
        public UiInitializer(IWindowShowController windowShowController, WorldInitializer worldInitializer)
        {
            _windowShowController = windowShowController;
            _worldInitializer = worldInitializer;
        }

        public void Initialize()
        {
            _windowShowController.Show<StartWindowView, InstantlyShowProcessor, WorldInitializer>(_worldInitializer);
        }
    }
}