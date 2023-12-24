using AR.Controllers;
using AR.Interfaces;

namespace AR.Services
{
    internal class ARService: IARService
    {
        private readonly ARController _arController;

        public ARService(ARController arController)
        {
            _arController = arController;
        }

        public void AREnable()
        {
            _arController.CreateARComponent();
        }

        public void ARDisable()
        {
            _arController.DestroyArComponent();
        }
    }
}