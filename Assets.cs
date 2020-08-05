using RoR2;
using R2API;
using R2API.Utils;
using System.Reflection;
using UnityEngine;
using System;

namespace FloppyCrabsItems
{

    [R2APISubmoduleDependency(nameof(LanguageAPI))]
    internal static class Assets
    {
        // Items
        internal static ItemIndex aPistolItemIndex;
        internal static ItemIndex garbItemIndex;

        // Buffs
        internal static BuffIndex lightArmourBuffIndex;

        private const string ModPrefix = "@FloppyCrabsItems:";

        internal static void Init()
        {
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("FloppyCrabsItems.floppycrabsitems"))
            {
                var bundle = AssetBundle.LoadFromStream(stream);
                var provider = new AssetBundleResourcesProvider(ModPrefix.TrimEnd(':'), bundle);
                ResourcesAPI.AddProvider(provider);
            }

            // null = Do not display on Character
            var nullDisplay = new ItemDisplayRuleDict(null);

            aPistolAsRedTierItem(nullDisplay);
            garbAsGreenTierItem(nullDisplay);

            lightArmourAsBuff();

            // Adds string definitions for the language tokens defined below
            AddLanguageTokens();
        }

        /*
         * ITEMS
         */
        private static void aPistolAsRedTierItem(ItemDisplayRuleDict nullDisplay)
        {
            var aPistolItemDef = new ItemDef
            {
                // Internally used name
                name="ArmouredPistol",
                // Name to be assigned for the user to see
                nameToken = "ARMOURONHIT_NAME",
                pickupToken = "ARMOURONHIT_PICKUP",
                descriptionToken = "ARMOURONHIT_DESC",
                loreToken = "ARMOURONHIT_LORE",
                // Item rarity
                tier = ItemTier.Tier3,
                pickupIconPath = ModPrefix + "Assets/Import/aPistol/aPistol.png",
                pickupModelPath = ModPrefix + "Assets/Import/aPistol/aPistol.fbx",
                // Used for item upgrades and 3D printers
                canRemove = true,
                // Visible in the top item list
                hidden = false,
                // Item tags, used for identification and searching
                tags = new[]
                {
                    ItemTag.Utility,
                }
            };
            // Add the Items to the R2API
            var aPistol = new R2API.CustomItem(aPistolItemDef, nullDisplay);
            // Create Itemindex variables by adding items to ItemAPI
            aPistolItemIndex = ItemAPI.Add(aPistol);
        }

        private static void garbAsGreenTierItem(ItemDisplayRuleDict nullDisplay)
        {
            var garbItemDef = new ItemDef
            {
                name = "Garb",
                nameToken = "CLOAKONKILL_NAME",
                pickupToken = "CLOAKONKILL_PICKUP",
                descriptionToken = "CLOAKONKILL_DESC",
                loreToken = "CLOAKONKILL_LORE",
                tier = ItemTier.Tier2,
                pickupIconPath = ModPrefix + "Assets/Import/garb/garb.PNG",
                pickupModelPath = ModPrefix + "Assets/Import/garb/garb.fbx",
                canRemove = true,
                hidden = false,
                tags = new[]
                {
                    ItemTag.Utility,
                    ItemTag.OnKillEffect,
                    ItemTag.AIBlacklist
                }
            };

            var garb = new R2API.CustomItem(garbItemDef, nullDisplay);
            garbItemIndex = ItemAPI.Add(garb);
        }

        /*
         * BUFFS
         */
         private static void lightArmourAsBuff()
        {
            var lightArmourBuffDef = new BuffDef
            {
                name = "LightArmour",
                iconPath = "Textures/bufficons/texBuffGenericShield",
                buffColor = UnityEngine.Color.yellow,
                isDebuff = false,
                canStack = false
            };

            var LightArmour = new R2API.CustomBuff(lightArmourBuffDef);
            lightArmourBuffIndex = BuffAPI.Add(LightArmour);
        }

        private static void AddLanguageTokens()
        {
            // The name should be self explanatory
            R2API.LanguageAPI.Add("ARMOURONHIT_NAME", "Armoured Pistol");
            // Short text when first picked up. Short and to the point. Numbers omitted
            R2API.LanguageAPI.Add("ARMOURONHIT_PICKUP", "Grants armour on hit");
            // Description is a longer description found in the item log in game. Give actual numbers and a complete description
            R2API.LanguageAPI.Add("ARMOURONHIT_DESC", "Whenever you <style=cIsDamage>Hit an enemy</style> you gain <style=cIsUtility>25</style> <style=cStack>(+25 per stack)</style> armour for the duration of the buff");
            // Lore is flavour.
            R2API.LanguageAPI.Add("ARMOURONHIT_LORE", "A Gunslinger won a duel despite drawing second. They blocked the bullet with their gun, then finished the fight in a millisecond.");

            R2API.LanguageAPI.Add("CLOAKONKILL_NAME", "Cuthroat's Garb");
            R2API.LanguageAPI.Add("CLOAKONKILL_PICKUP", "Chance to cloak on kill");
            R2API.LanguageAPI.Add("CLOAKONKILL_DESC", "Whenever you <style=cIsDamage>kill an enemy</style>, you have a <style=cIsUtility>5%</style> chance to cloak for <style=cIsUtility>4s</style> <style=cStack>(+1s per stack)</style>");
            R2API.LanguageAPI.Add("CLOAKONKILL_LORE", "Those who visit in the night are either praying for a favour, or preying on a neighbour.");

            //R2API.AssetPlus.Languages.AddToken (old way for assigning tokens string values)
        }
    }
}
