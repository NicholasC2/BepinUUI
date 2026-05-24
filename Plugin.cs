using BepInEx;
using UnityEngine;

using BepInUUI.Core;

namespace BepInUUI
{
    [BepInPlugin("com.nick.bepin-universal-ui", "BepinUUI", "0.0.1")]
    public class Plugin : BaseUnityPlugin
    {
        private bool _initialized;

        private void Awake()
        {
            PlayerLoopInjector.Inject(Tick);
        }

        private void Tick()
        {
            if (!_initialized)
            {
                Render.Init();
                _initialized = true;
            }
            
            Render.Update();
        }
    }
}