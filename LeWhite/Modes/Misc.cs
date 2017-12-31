

using System;
using Aimtec;
using Aimtec.SDK.Extensions;
using Aimtec.SDK.Menu.Components;

namespace LeWhite
{
    internal partial class LB
    {

        public bool IsW1()
        {
            return MyHero.SpellBook.GetSpell(SpellSlot.W).Name.ToLower() == "leblancw";
        }

        public bool IsW2()
        {
            return MyHero.SpellBook.GetSpell(SpellSlot.W).Name.ToLower() != "leblancw";
        }
        public bool IsR1()
        {
            return MyHero.SpellBook.GetSpell(SpellSlot.R).Name.ToLower() == "leblancrtoggle";
        }
        public bool IsR2()
        {
            return MyHero.SpellBook.GetSpell(SpellSlot.R).Name.ToLower() != "leblancrtoggle";
        }

        public bool IsPassive(Obj_AI_Base hero)
        {
            return hero.HasBuff("LeblancPMark") && Game.ClockTime - hero.GetBuff("LeblancPMark").StartTime > 1;
        }
        public bool IsPassiveM(Obj_AI_Base hero)
        {
            return hero.HasBuff("leblancpminion") && Game.ClockTime - hero.GetBuff("leblancpminion").StartTime > 1;
        }

        public string GetComboName()
        {
            if (target == null || !target.IsValid)
            {
                return "DF";
            }
            if (MyHero.Distance(target) < W.Range)
            {
                return "W";
            }
            else if (MyHero.Distance(target) < E.Range)
            {
                return "RE";
            }
            else if (target.IsValidTarget(W.Range + Q.Range))
            {
                return "Gap";
            }
            return "DF";
        }

        public void OnlyE()
        {
            if (!target.IsValid || target == null)
            {
                return;
            }
            if (RootM["keys"]["onlye"].As<MenuKeyBind>().Enabled)
            {
                CastE(target);
            }
        }

        private static void GameEventsOnGameStart()
        {

            Self["mySkin"].OnValueChanged  += (sender, args) => Game.OnUpdate += UpdateSkin;

        }
        private static void UpdateSkin()
        {

            var mySkin = Misc["mySkin"].Value;
            MyHero.SetSkinId(mySkin);
            Game.OnUpdate -= UpdateSkin;
        }
        private static void GameObjectOnOnRevive(GameObject sender)
        {
            if (sender is Obj_AI_Hero hero)
            {
                if (hero.IsMe)
                {
                    var mySkin = Self["mySkin"].Value;
                    MyHero.SetSkinId(mySkin);
                }
            }
        }

    }
}



