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

        

        private void ClickEvent(WndProcEventArgs e)
        {
            if (RootM["key"]["modeswitch"].Enabled)
            {
                if (Uhh < Game.TickCount)
                {
                    if (RootM["key"]["rcombo"].As<MenuList>().Value == 0)
                    {

                        RootM["key"]["rcombo"].As<MenuList>().Value = 1;
                        Uhh = Game.TickCount + 300;
                        return;

                    }
                    if (RootM["key"]["rcombo"].As<MenuList>().Value == 1)
                    {
                        RootM["key"]["rcombo"].As<MenuList>().Value = 2;
                        Uhh = Game.TickCount + 300;
                        return;
                    }
                    if (RootM["key"]["rcombo"].As<MenuList>().Value == 2)
                    {
                        RootM["key"]["rcombo"].As<MenuList>().Value = 0;
                        Uhh = Game.TickCount + 300;
                        return;
                    }
                }
            }
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



        }
    }
}