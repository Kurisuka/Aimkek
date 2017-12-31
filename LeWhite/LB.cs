
using System;
using Aimtec;
using Aimtec.SDK.Menu.Components;
using Aimtec.SDK.Menu.Config;
using Aimtec.SDK.Orbwalking;
using Aimtec.SDK.TargetSelector;

namespace LeWhite
{
    internal partial class LB
    {
        public static Orbwalker Orbwalker = new Orbwalker();
        private int Uhh;

        public LB()
        {
            this.LoadMenuAsync();
            this.LoadSpells();
            this.LoadEvents();
        }

        private void OnTick()
        {

            if (RootM["combo"]["combologics"]["switchkey"].As<MenuKeyBind>().Value)
            {
                if (Uhh < Game.TickCount)
                {
                    if (RootM["combo"]["combologics"]["mCombo"].As<MenuList>().Value == 0)
                    {
                        RootM["combo"]["combologics"]["mCombo"].As<MenuList>().Value = 1;
                        Uhh = Game.TickCount + 300;
                        return;
                    }
                    if (RootM["combo"]["combologics"]["mCombo"].As<MenuList>().Value ==1)
                    {
                        RootM["combo"]["combologics"]["mCombo"].As<MenuList>().Value = 2;
                        Uhh = Game.TickCount + 300;
                        return;
                    }
                    if (RootM["combo"]["combologics"]["mCombo"].As<MenuList>().Value == 2)
                    {
                        RootM["combo"]["combologics"]["mCombo"].As<MenuList>().Value = 3;
                        Uhh = Game.TickCount + 300;
                        return;
                    }

                    if (RootM["combo"]["combologics"]["mCombo"].As<MenuList>().Value == 3)
                    {
                        RootM["combo"]["combologics"]["mCombo"].As<MenuList>().Value = 4;
                        Uhh = Game.TickCount + 300;
                        return;
                    }

                    if (RootM["combo"]["combologics"]["mCombo"].As<MenuList>().Value == 4)
                    {
                        RootM["combo"]["combologics"]["mCombo"].As<MenuList>().Value = 5;
                        Uhh = Game.TickCount + 300;
                        return;
                    }

                    if (RootM["combo"]["combologics"]["mCombo"].As<MenuList>().Value == 5)
                    {
                        RootM["combo"]["combologics"]["mCombo"].As<MenuList>().Value = 6;
                        Uhh = Game.TickCount + 300;
                        return;
                    }
                    if (RootM["combo"]["combologics"]["mCombo"].As<MenuList>().Value == 6)
                    {
                        RootM["combo"]["combologics"]["mCombo"].As<MenuList>().Value = 7;
                        Uhh = Game.TickCount + 300;
                        return;
                    }

                    if (RootM["combo"]["combologics"]["mCombo"].As<MenuList>().Value == 7)
                    {
                        RootM["combo"]["combologics"]["mCombo"].As<MenuList>().Value = 0;
                        Uhh = Game.TickCount + 300;
                        return;
                    }


                }
            }

            if (MyHero.IsDead)
            {
                return;
            }

            target = TargetSelector.GetTarget(1600);

            switch (Orbwalker.Mode)
            {
                case OrbwalkingMode.Combo:
                    DoCombo();
                    break;
                case OrbwalkingMode.Mixed:
                    DoHarass();
                    break;
                case OrbwalkingMode.Lasthit:
                    DoLastHit();
                    break;
                case OrbwalkingMode.Laneclear:
                    DoLaneClear();
                    DoJungleClear();
                    break;
            }

            if (RootM["escape"]["fleekey"].Enabled) DoEscape();

            DoKillSteal();
            // OnlyE();
        }


        private void OnDraw()
        {
            if (MyHero.IsDead)
            {
                return;
            }

            DoDraws();

        }

        /*private void Game_OnUpdate()
        {
            if (RootM["keys"]["skinhackerino"].Enabled)
            {
                if (Uhh < Game.TickCount)
                {
                    if (RootM["misc"]["self"]["mySkin"].As<MenuList>().Value == 0)
                    {
                        RootM["misc"]["self"]["mySkin"].As<MenuList>().Value = 1;
                        Uhh = Game.TickCount + 300;
                        return;
                    }
                    if (RootM["misc"]["self"]["mySkin"].As<MenuList>().Value == 1)
                    {
                        RootM["misc"]["self"]["mySkin"].As<MenuList>().Value = 2;
                        Uhh = Game.TickCount + 300;
                        return;
                    }
                    if (RootM["misc"]["self"]["mySkin"].As<MenuList>().Value == 2)
                    {
                        RootM["misc"]["self"]["mySkin"].As<MenuList>().Value = 3;
                        Uhh = Game.TickCount + 300;
                        return;
                    }
                    if (RootM["misc"]["self"]["mySkin"].As<MenuList>().Value == 3)
                    {
                        RootM["misc"]["self"]["mySkin"].As<MenuList>().Value = 4;
                        Uhh = Game.TickCount + 300;
                        return;
                    }
                    if (RootM["misc"]["self"]["mySkin"].As<MenuList>().Value == 4)
                    {

                        RootM["misc"]["self"]["mySkin"].As<MenuList>().Value = 5;
                        Uhh = Game.TickCount + 300;
                        return;
                    }

                    if (RootM["misc"]["self"]["mySkin"].As<MenuList>().Value == 5)
                    {
                        RootM["misc"]["self"]["mySkin"].As<MenuList>().Value = 0;
                        Uhh = Game.TickCount + 300;
                        return;
                    }
                }
            }
            
    }
    /*public void SkinHack()
    {

    }*/


    }
}
