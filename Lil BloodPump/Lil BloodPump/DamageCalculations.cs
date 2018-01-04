using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aimtec;
using Aimtec.SDK.Extensions;
using Aimtec.SDK.Damage;

namespace Lil_BloodPump
{
    internal static class DamageCalcs
    {
        internal static double GetDamage(this Aimtec.SDK.Spell spell, Obj_AI_Base target)
        {
            if (!spell.Ready)
            {
                return 0;
            }

            var dmg = 0d;

            switch (spell.Slot)
            {
                case SpellSlot.Q:

                    if (Vladimir.isQActive)
                    {
                        dmg =
                            new[] { 148.00, 185.00, 185.00, 295.00, 296.00 }[
                                ObjectManager.GetLocalPlayer().GetSpell(SpellSlot.Q).Level - 1] +
                            1.11 * ObjectManager.GetLocalPlayer().TotalAbilityDamage;
                    }
                    else
                    {
                        dmg =
                            new[] { 80.00, 100.00, 100.00, 140.00, 160.00 }[
                                ObjectManager.GetLocalPlayer().GetSpell(SpellSlot.Q).Level - 1] +
                            0.60 * ObjectManager.GetLocalPlayer().TotalAbilityDamage;
                    }

                    break;
                case SpellSlot.W:
                    dmg =
                        new[] { 20.00, 33.75, 47.50, 61.25, 75.00 }[
                            ObjectManager.GetLocalPlayer().GetSpell(SpellSlot.W).Level - 1] +
                        0.025 * ObjectManager.GetLocalPlayer().Health;
                    break;
                case SpellSlot.E:
                    dmg =
                        new[] { 30.00, 45.00, 60.00, 75.00, 90.00 }[
                            ObjectManager.GetLocalPlayer().GetSpell(SpellSlot.E).Level - 1] +
                        0.025 * ObjectManager.GetLocalPlayer().MaxHealth;
                    break;
                case SpellSlot.R:
                    dmg =
                        new[] { 150.00, 250.00, 350.00 }[
                            ObjectManager.GetLocalPlayer().SpellBook.GetSpell(SpellSlot.R).Level - 1] +
                        0.70 * ObjectManager.GetLocalPlayer().TotalAbilityDamage;
                    break;
            }

            return dmg > 0 ? ObjectManager.GetLocalPlayer().CalculateDamage(target, DamageType.Magical, dmg) : 0d;
        }
    }
}
