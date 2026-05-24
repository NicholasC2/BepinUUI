using BepInEx;
using UnityEngine;

[BepInPlugin("bepinuui.core", "BepInUUI Core", "1.0.0")]
public class UUIPlugin : BaseUnityPlugin
{
    private void Awake()
    {
        var go = new GameObject("UUI Root");
        go.AddComponent<BepInUUI.Core.UUIRoot>();
    }
}