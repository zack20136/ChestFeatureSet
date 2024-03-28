﻿using ChestFeatureSet.CraftFromChests;
using ChestFeatureSet.StashToChests;
using StardewModdingAPI;

namespace ChestFeatureSet
{
    public class ModEntry : Mod
    {
        public ModConfig Config { get; private set; }

        internal static StashToChestsModule? StashToChests { get; private set; }
        internal LockItemsModule? LockItems { get; private set; }
        internal static CraftFromChestsModule? CraftFromChests { get; private set; }

        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
            this.Config = helper.ReadConfig<ModConfig>();

            helper.Events.GameLoop.GameLaunched += (sender, e) => this.OnGameLaunched();
            helper.Events.GameLoop.SaveLoaded += (sender, e) => this.LoadModules();
            helper.Events.GameLoop.ReturnedToTitle += (sender, e) => this.UnloadModules();
        }

        private void OnGameLaunched()
        {
            ConfigMenu.StartConfigMenu(this.Helper, this.ModManifest, this.Config);
        }

        private void LoadModules()
        {
            if (Config.StashToChests)
            {
                StashToChests = new StashToChestsModule(this);
                StashToChests.Activate();
            }
            if (Config.LockItems)
            {
                LockItems = new LockItemsModule(this);
                LockItems.Activate();
            }
            if (Config.CraftFromChests)
            {
                CraftFromChests = new CraftFromChestsModule(this);
                CraftFromChests.Activate();
            }
        }

        private void UnloadModules()
        {
            if (StashToChests != null)
            {
                StashToChests.Deactivate();
                StashToChests = null;
            }
            if (LockItems != null)
            {
                LockItems.Deactivate();
                LockItems = null;
            }
            if (CraftFromChests != null)
            {
                CraftFromChests.Deactivate();
                CraftFromChests = null;
            }
        }
    }
}