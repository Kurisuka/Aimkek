using Aimtec;
using Aimtec.SDK.Menu.Components;
using Aimtec.SDK.Events;
using Aimtec.SDK.Menu;
using Aimtec.SDK.TargetSelector;
using Aimtec.SDK.Orbwalking;
using System;

namespace E_Girl_Diana
{
    internal partial class egrilldiana
    {
        private int Uhh;

        public egrilldiana()
        {
            LoadMenuAsync();
            LoadSpells();
            LoadEvents();

        }

        private void Render_OnPresent()
        {
            if (Player.IsDead) return;
            else DoDrawing();
        }
        private void Game_OnUpdate()
        {

            if (Player.IsDead || MenuGUI.IsChatOpen())
            {
                return;
            }
            switch (Orbwalker.Implementation.Mode)
            {
                case OrbwalkingMode.Combo:
                   
                    DoCombo();

                    break;
                case OrbwalkingMode.Mixed:
                    DoHarass();
                    break;
                case OrbwalkingMode.Lasthit:
                    //DoLasthit();
                    break;
                case OrbwalkingMode.Laneclear:
                    DoLaneClear();
                    DoJungleClear();
                    break;
            }
            if (RootM["combo"]["modeswitch"].Enabled)
            {
                if (Uhh < Game.TickCount)
                {
                    if (RootM["combo"]["rcombo"].As<MenuList>().Value == 0)
                    {

                        RootM["combo"]["rcombo"].As<MenuList>().Value = 1;
                        Uhh = Game.TickCount + 300;
                        return;

                    }
                    if (RootM["combo"]["rcombo"].As<MenuList>().Value == 1)
                    {
                        RootM["combo"]["rcombo"].As<MenuList>().Value = 2;
                        Uhh = Game.TickCount + 300;
                        return;
                    }
                    if (RootM["combo"]["rcombo"].As<MenuList>().Value == 2)
                    {
                        RootM["combo"]["rcombo"].As<MenuList>().Value = 0;
                        Uhh = Game.TickCount + 300;
                        return;
                    }
                }
            }



        }
    }
}