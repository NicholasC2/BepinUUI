using System.Collections.Generic;
using UnityEngine;

namespace BepInUUI.Core
{
    public static class Render
    {
        private static Material _mat;

        public static void Init()
        {
            Shader shader = Shader.Find("Hidden/Internal-Colored");
            _mat = new Material(shader)
            {
                hideFlags = HideFlags.HideAndDontSave
            };

            _mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            _mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            _mat.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
            _mat.SetInt("_ZWrite", 0);
        }

        public static void Update()
        {
            if (_mat == null)
                Init();

            GL.PushMatrix();
            _mat.SetPass(0);

            GL.LoadPixelMatrix();

            DrawWindows();

            GL.PopMatrix();
        }

        private static void DrawWindows()
        {
            foreach (var window in UWindowRegistry.Windows)
            {
                DrawWindow(window);
            }
        }

        private static void DrawWindow(UWindow window)
        {
            float x = window.X;
            float y = window.Y;
            float w = window.Width;
            float h = window.Height;

            DrawRect(x, y, w, h, new Color(0f, 0f, 0f, 0.85f));

            DrawRect(x, y, w, 20f, new Color(0.15f, 0.15f, 0.15f, 1f));
        }
        
        private static void DrawRect(float x, float y, float w, float h, Color color)
        {
            GL.Begin(GL.QUADS);
            GL.Color(color);

            GL.Vertex3(x, y, 0);
            GL.Vertex3(x + w, y, 0);
            GL.Vertex3(x + w, y + h, 0);
            GL.Vertex3(x, y + h, 0);

            GL.End();
        }
    }
}