
using System;
using System.Linq;
using Aimtec;
using Aimtec.SDK.Damage;
using Aimtec.SDK.Extensions;
using Aimtec.SDK.Menu.Components;
using Aimtec.SDK.Util.Cache;

namespace LeWhite
{
    internal partial class LB
    {
        public void DoLastHit()
        {
            bool Qmana = MyHero.Mana> MyHero.SpellBook.GetSpell(SpellSlot.Q).Cost;
            bool Wmana = MyHero.Mana> MyHero.SpellBook.GetSpell(SpellSlot.W).Cost;
            bool Emana = MyHero.Mana > MyHero.SpellBook.GetSpell(SpellSlot.E).Cost;
            foreach (var mt in GameObjects.EnemyMinions.Where(m => m.IsValidTarget(900)))
            {
                double Qdamage = MyHero.GetSpellDamage(mt, SpellSlot.Q);
                double Wdamage = MyHero.GetSpellDamage(mt, SpellSlot.W);
                double Edamage = MyHero.GetSpellDamage(mt, SpellSlot.E);
                if (RootM["farm"]["lasthit"]["useQ"].As<MenuBool>().Enabled && Qdamage > mt.Health && Q.Ready &&
                    Qmana)
                {
                    Q.Cast(mt);
                }
                if (RootM["farm"]["lasthit"]["useE"].As<MenuBool>().Enabled && Edamage > mt.Health && E.Ready &&
                    mt.IsValidTarget(E.Range) && Emana)
                {
                    E.Cast(mt);
                }
                if (RootM["farm"]["lasthit"]["useW"].As<MenuBool>().Enabled && Wdamage > mt.Health && W.Ready &&
                    Wmana)
                {
                    W.Cast(mt);
                }
            }
        }
    }
}
