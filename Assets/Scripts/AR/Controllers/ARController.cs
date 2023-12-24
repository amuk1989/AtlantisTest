using AR.Views;

namespace AR.Controllers
{
    internal class ARController
    {
        private readonly ARComponents.Factory _arFactory;
        
        private ARComponents _arComponents = null;

        public ARController(ARComponents.Factory arFactory)
        {
            _arFactory = arFactory;
        }

        public ARComponents ARComponent => _arComponents;

        public void CreateARComponent()
        {
            _arComponents ??= _arFactory.Create();
        }

        public void DestroyArComponent()
        {
            if (_arComponents == null) return;
            
            _arComponents.Dispose();
            _arComponents = null;
        }
    }
}