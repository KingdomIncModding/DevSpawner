using System;

namespace KingdomDevSpawner
{
    /// <summary>Public change notifications for DevSpawner item selections.</summary>
    public static class DevSpawnerEvents
    {
        /// <summary>Monotonically incremented whenever a placed spawner's configured item changes.</summary>
        public static long Revision { get; private set; }

        /// <summary>Raised with the modified spawner building GUID when its configured spawned item changes.</summary>
        public static event Action<string> SpawnerItemChanged;

        internal static void NotifyItemChanged(string spawnerGuid)
        {
            Revision++;
            SpawnerItemChanged?.Invoke(spawnerGuid);
        }

        internal static void ResetForUnload()
        {
            SpawnerItemChanged = null;
        }
    }
}

