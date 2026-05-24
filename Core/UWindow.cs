using System.Collections.Generic;

namespace BepInUUI.Core
{
    public enum UWindowState
    {
        Normal,
        Minimized,
        Closed
    }

    public class UWindow
    {
        public float X;
        public float Y;
        public string Id;
        public int Width { get; set; }
        public int Height { get; set; }
        public UWindowState State;
        private List<UWindowItem> _items = new List<UWindowItem>();
        public IReadOnlyList<UWindowItem> WindowItems => _items;

        public UWindow(int width, int height)
        {
            Id = System.Guid.NewGuid().ToString();

            State = UWindowState.Normal;

            Width = width;
            Height = height;
        }

        public void AddItem(UWindowItem item)
        {
            _items.Add(item);
        }

        public bool RemoveItem(UWindowItem item)
        {
            return _items.Remove(item);
        }
    }
}