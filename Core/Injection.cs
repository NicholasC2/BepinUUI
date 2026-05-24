// CHATGPT cus idk how 2 do this :)

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.LowLevel;
using UnityEngine.PlayerLoop;

namespace BepInUUI.Core
{
    public static class PlayerLoopInjector
    {
        private static bool _initialized;

        private static readonly List<Action> _onUpdates = new List<Action>();

        public static void Inject(Action onUpdate)
        {
            if (!_initialized)
            {
                var loop = PlayerLoop.GetCurrentPlayerLoop();

                InsertIntoSubSystem(
                    ref loop,
                    typeof(Update),
                    typeof(UnityEngine.PlayerLoop.Update.ScriptRunBehaviourUpdate),
                    OnUpdate);

                PlayerLoop.SetPlayerLoop(loop);

                _initialized = true;
            }

            _onUpdates.Add(onUpdate);
        }

        private static void OnUpdate()
        {
            for (int i = 0; i < _onUpdates.Count; i++)
            {
                try
                {
                    _onUpdates[i]?.Invoke();
                }
                catch (Exception ex)
                {
                    UnityEngine.Debug.LogError($"[BepInUUI] Update error: {ex}");
                }
            }
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

                if (sys.type != targetSystemType)
                    continue;

                if (sys.subSystemList == null)
                    return;

                var list = sys.subSystemList.ToList();

                int index = list.FindIndex(s => s.type == insertAfterType);

                if (index < 0 || index >= list.Count)
                    index = list.Count;

                var newSystem = new PlayerLoopSystem
                {
                    type = typeof(BepInUUIPlayerLoop),
                    updateDelegate = function
                };

                list.Insert(index, newSystem);

                sys.subSystemList = list.ToArray();
                systems[i] = sys;
                loop.subSystemList = systems;

                return;
            }
        }

        private struct BepInUUIPlayerLoop { }
    }
}