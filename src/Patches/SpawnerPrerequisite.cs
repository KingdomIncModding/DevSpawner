using System;
using System.Collections.Generic;
using KingdomFoundation.Api;

namespace KingdomDevSpawner
{
    // Keep the game's native node type and save data; only change its build prerequisite.
    internal static class SpawnerPrerequisite
    {
        private static readonly List<(WeakReference<WorkspaceObject> Target, GameResearchNode OriginalResearch)> Tracked =
            new List<(WeakReference<WorkspaceObject>, GameResearchNode)>();

        internal static void Postfix(WorkspaceObject __instance)
        {
            if (__instance == null)
                return;
            Tracked.Add((new WeakReference<WorkspaceObject>(__instance), __instance.RequiredResearch));
            __instance.RequiredResearch = global::KingdomDevSpawner.DevSpawnerPlugin.SpawnerResearch;
        }

        internal static void RestoreAll()
        {
            foreach (var entry in Tracked)
            {
                if (entry.Target.TryGetTarget(out var spawner))
                    spawner.RequiredResearch = entry.OriginalResearch;
            }
            Tracked.Clear();
        }
    }
}
