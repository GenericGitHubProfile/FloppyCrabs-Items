using BepInEx;
using HarmonyLib;
using Mono.Cecil;
using MonoMod;
using R2API;
using R2API.AssetPlus;
using R2API.Utils;
using RoR2;
using RoR2.UI;
using System;
using System.Linq;
using UnityEngine;

namespace FloppyCrabsItems
{
    [BepInDependency("com.bepis.r2api")]

    [BepInPlugin("com.FloppyCrab.FloppyCrabsItems","FloppyCrab's Items","0.1.0")]

    [R2APISubmoduleDependency(nameof(ItemAPI), nameof(BuffAPI), nameof(LanguageAPI), nameof(ItemDropAPI), nameof(ResourcesAPI))]
    public class CustomItems : BaseUnityPlugin
    {
        private static RoR2.ItemDef cutGarb;
        private static RoR2.ItemDef aPistol;
        private static RoR2.BuffDef lightArmour;
        public void Awake()
        {
            Assets.Init();
            Hooks.Init();

            RoR2.Chat.AddMessage("Loaded FloppyCrab's Itempack mod");
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.F2))
            {
                var transform = PlayerCharacterMasterController.instances[0].master.GetBodyObject().transform;
                PickupDropletController.CreatePickupDroplet(PickupCatalog.FindPickupIndex(Assets.aPistolItemIndex), transform.position, transform.forward * 20f);
            }
            if(Input.GetKeyDown(KeyCode.F3))
            {
                var transform = PlayerCharacterMasterController.instances[0].master.GetBodyObject().transform;
                PickupDropletController.CreatePickupDroplet(PickupCatalog.FindPickupIndex(Assets.garbItemIndex), transform.position, transform.forward * 20f);
            }
        }
    }
}
