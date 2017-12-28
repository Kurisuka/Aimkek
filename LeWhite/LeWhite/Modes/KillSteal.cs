
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
        #region Killstea part
        public void DoKillSteal()
        {
            if (!RootM["killsteal"]["ks"].As<MenuBool>().Enabled)
            {
                return;
            }
            foreach (var hptarget in GameObjects.EnemyHeroes.Where(a => a.IsValidTarget(1200) && !a.IsDead))
            {
                if (!hptarget.IsValid || hptarget.IsDead || hptarget == null)
                {
                    return;
                }
                var Health = hptarget.Health;
                var dmgE = MyHero.GetSpellDamage(hptarget, SpellSlot.E);
                if (MyHero.Distance(hptarget) < E.Range && Health < dmgE && !RootM["keys"]["combokey"].As<MenuKeyBind>().Enabled &&
                    RootM["killsteal"]["useE"].As<MenuBool>().Enabled)
                {
                  E.Cast(hptarget);
                }
                var dmgQ = MyHero.GetSpellDamage(hptarget, SpellSlot.Q);
                if (MyHero.Distance(hptarget) < Q.Range && Health < dmgQ && !RootM["keys"]["combokey"].As<MenuKeyBind>().Enabled &&
                    RootM["killsteal"]["useQ"].As<MenuBool>().Enabled)
                {
                    Q.Cast(hptarget);
                }
                var dmgW = MyHero.GetSpellDamage(hptarget, SpellSlot.W);
                if (MyHero.Distance(hptarget) < W.Range && Health < dmgW && !RootM["keys"]["combokey"].As<MenuKeyBind>().Enabled &&
                    RootM["killsteal"]["useW"].As<MenuBool>().Enabled)
                {
                    W.Cast(hptarget);
                }
                var dmgR = MyHero.GetSpellDamage(hptarget, SpellSlot.Q);
                if (MyHero.Distance(hptarget) < Q.Range && Health < dmgR && !RootM["keys"]["combokey"].As<MenuKeyBind>().Enabled &&
                    RootM["killsteal"]["useR"].As<MenuBool>().Enabled)
                {
                   CastR("RQ",hptarget);
                }
                if (!IsIgnite) continue;
                var dmgI = (50 + ((MyHero.Level) * 20));
                if (MyHero.Distance(hptarget) < Q.Range && Health < dmgI && !RootM["keys"]["combokey"].As<MenuKeyBind>().Enabled &&
                    RootM["killsteal"]["useI"].As<MenuBool>().Enabled)
                {
                    Ignite.CastOnUnit(hptarget);
                }
                // this.DoOneShotCombo(hptarget);
            }


        }
        #endregion
    }
}
