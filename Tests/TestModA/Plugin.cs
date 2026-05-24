using BepInEx;
using UnityEngine;

namespace TestModA
{
    [BepInPlugin(
        "test.mod.a",
        "Test Mod A",
        "1.0.0"
    )]
    public class Plugin : BaseUnityPlugin
    {
        private void Awake()
        {
            var window = new BepInUUI.Core.UWindow(300, 200)
            {
                X = 100,
                Y = 100
            };

            BepInUUI.Core.UWindowRegistry.AddWindow(window); 
        }
    }
}