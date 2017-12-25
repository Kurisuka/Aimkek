using Aimtec;
using Aimtec.SDK.Extensions;
using Aimtec.SDK.Menu.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Girl_Diana
{
    internal partial class egrilldiana
    {
        public void DoHarass()
        {
            if (Player.ManaPercent() < (RootM["harass"]["mana"].As<MenuSliderBool>().Value) || target == null || !target.IsValid) return;
            bool useQ = RootM["combo"]["useq"].As<MenuBool>().Enabled;
            bool useW = RootM["combo"]["usew"].As<MenuBool>().Enabled;
            bool Qmana = Player.Mana > Player.SpellBook.GetSpell(SpellSlot.Q).Cost;
            bool Wmana = Player.Mana > Player.SpellBook.GetSpell(SpellSlot.W).Cost;

            if (useQ && Qmana) Q.Cast(target);
            if (Player.Distance(target) < W.Range && useW && Wmana) W.Cast();


        }
    }
}
