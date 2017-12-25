using Aimtec;
using Aimtec.SDK.Damage;
using Aimtec.SDK.Extensions;
using Aimtec.SDK.Menu.Components;
using Aimtec.SDK.Util.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Girl_Diana
{
    internal partial class egrilldiana
    {
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

                var dmgQ = Player.GetSpellDamage(hptarget, SpellSlot.Q);
                if (Player.Distance(hptarget) < Q.Range && Health < dmgQ &&
                    RootM["killsteal"]["useq"].As<MenuBool>().Enabled)
                {
                    Q.Cast(hptarget);
                }

                var dmgR = Player.GetSpellDamage(hptarget, SpellSlot.Q);
                if (Player.Distance(hptarget) < Q.Range && Health < dmgR &&
                    RootM["killsteal"]["user"].As<MenuBool>().Enabled)
                {
                    R.Cast(hptarget);
                }
            }
        }
    }
}

