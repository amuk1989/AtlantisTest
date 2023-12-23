using UnityEngine;
using Zenject;

namespace AR.Views
{
    internal class ARComponents: MonoBehaviour
    {
        internal class Factory:PlaceholderFactory<ARComponents> {}
        
    }
}