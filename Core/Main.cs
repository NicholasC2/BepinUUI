using UnityEngine;

namespace BepInUUI.Core
{
    public class UUIRoot : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            Render.Init();
        }

        private void OnGUI()
        {
            Render.Update();
        }
    }
}