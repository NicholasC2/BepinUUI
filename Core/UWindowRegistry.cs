using System.Collections.Generic;

namespace BepInUUI.Core
{
    public static class UWindowRegistry
    {
        private static List<UWindow> _windows = new List<UWindow>();
        public static IReadOnlyList<UWindow> Windows => _windows;
        
        public static void AddWindow(UWindow item)
        {
            if (item == null)
                return;

            if (_windows.Contains(item))
                return;

            _windows.Add(item);
        }

        public static bool RemoveWindow(UWindow item)
        {
            return _windows.Remove(item);
        }

        public static void MoveToTop(UWindow window)
        {
            if (window == null)
                return;

            if (!_windows.Remove(window))
                return;

            _windows.Add(window);
        }
    }
}