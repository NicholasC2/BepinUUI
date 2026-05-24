using BepInEx;

using BepInUUI.Core;

using System.Collections.Generic;

using System.Linq;

using System;

using UnityEngine.LowLevel;

using UnityEngine.PlayerLoop;

using UnityEngine;





namespace BepInUUI
{
    [BepInPlugin("com.nick.bepin-universal-ui", "BepinUUI", "0.0.1")]
    public class Plugin : BaseUnityPlugin
    {
        private void Awake()
        {
            PlayerLoopInjector.Inject(Tick);
        }

        private void Tick()
        {
        }
    }
}


namespace BepInUUI.Core
{
    public static class PlayerLoopInjector
    {
        private static bool _initialized;

        private static Action _onUpdate;

        public static void Inject(Action onUpdate)
        {
            if (_initialized)
                return;

            _onUpdate = onUpdate;

            var loop = PlayerLoop.GetCurrentPlayerLoop();

            InsertIntoSubSystem(ref loop, typeof(Update), typeof(UnityEngine.PlayerLoop.Update.ScriptRunBehaviourUpdate), OnUpdate);

            PlayerLoop.SetPlayerLoop(loop);

            _initialized = true;
        }

        private static void OnUpdate()
        {
            _onUpdate?.Invoke();
        }

        private static void InsertIntoSubSystem(
            ref PlayerLoopSystem loop,
            Type targetSystemType,
            Type insertAfterType,
            PlayerLoopSystem.UpdateFunction function)
        {
            var systems = loop.subSystemList;

            if (systems == null)
                return;

            for (int i = 0; i < systems.Length; i++)
            {
                var sys = systems[i];

                if (sys.type == targetSystemType)
                {
                    var list = sys.subSystemList.ToList();

                    int index = list.FindIndex(s => s.type == insertAfterType);

                    if (index == -1)
                        index = list.Count - 1;

                    var newSystem = new PlayerLoopSystem
                    {
                        type = typeof(BepInUUIPlayerLoop),
                        updateDelegate = function
                    };

                    list.Insert(index + 1, newSystem);

                    sys.subSystemList = list.ToArray();
                    systems[i] = sys;
                    loop.subSystemList = systems;

                    return;
                }
            }
        }

        private struct BepInUUIPlayerLoop { }
    }
}




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