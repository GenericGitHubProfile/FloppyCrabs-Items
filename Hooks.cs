using MonoMod;
using R2API.Utils;
using RoR2;
using System.Runtime.CompilerServices;

namespace FloppyCrabsItems
{
    class Hooks
    {
        internal static void Init()
        {
            On.RoR2.GlobalEventManager.OnHitEnemy += (orig, self, damageInfo, victim) =>
            {
                if (!victim || !damageInfo.attacker)
                    return;

                CharacterBody attacker = damageInfo.attacker.GetComponent<CharacterBody>();

                if (attacker.inventory)
                {
                    int aPistolCount = attacker.inventory.GetItemCount(Assets.aPistolItemIndex);
                    if (aPistolCount > 0)
                    {
                        attacker.AddTimedBuff(Assets.lightArmourBuffIndex, (1.0f * aPistolCount)); // 40 armour per stack
                    }
                }

                orig(self, damageInfo, victim);
            };

            GlobalEventManager.onCharacterDeathGlobal += (report) =>
            {
                if (!report.attacker || !report.attackerBody)
                    return;

                CharacterBody attacker = report.attackerBody;
                if(attacker.inventory)
                {
                    int garbCount = attacker.inventory.GetItemCount(Assets.garbItemIndex);
                    if(garbCount > 0 && Util.CheckRoll(5, attacker.master))
                    {
                        attacker.AddTimedBuff(BuffIndex.Cloak, 2 + (garbCount));
                    }
                }
            };

            On.RoR2.CharacterBody.RecalculateStats += (orig, self) =>
            {
                orig(self);
                if (self && self.HasBuff(Assets.lightArmourBuffIndex))
                {
                    self.SetPropertyValue("armor", self.armor + 40f);
                }
            };
        }
    }
}
