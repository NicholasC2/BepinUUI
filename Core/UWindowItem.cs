using System.Collections.Generic;

namespace BepInUUI.Core
{
    public abstract class UWindowItem
    {
        public string Name;
        public float Width;
        public float Height;
    }

    public class UWindowRow : UWindowItem
    {
        private List<UWindowItem> _items = new List<UWindowItem>();
        public IReadOnlyList<UWindowItem> RowItems => _items;
        
        public void AddItem(UWindowItem item)
        {
            _items.Add(item);
        }

        public bool RemoveItem(UWindowItem item)
        {
            return _items.Remove(item);
        }
    }

    public class UWindowTextBox : UWindowItem
    {
        public string value;
    }
}