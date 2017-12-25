using Aimtec;
using Aimtec.SDK.Menu.Components;
using Aimtec.SDK.Events;
using Aimtec.SDK.Menu;
using Aimtec.SDK.TargetSelector;
using Aimtec.SDK.Orbwalking;

namespace E_Girl_Diana
{
    internal partial class egrilldiana
    {

        
        public egrilldiana()
        {
            LoadMenu();
            LoadSpells();
            LoadEvents();

        }

        /*private void ClickEvent(WndProcEventArgs e)
        {
            if (RootM["keys"]["combomode"].As<MenuKeyBind>().Enabled)
            {
                RootM["combo"]["rcombo"].As<MenuList>().Value += 1;
                RootM["keys"]["combomode"].As<MenuKeyBind>().Value =
                    !RootM["keys"]["combomode"].As<MenuKeyBind>().Enabled;
                if (RootM["combo"]["rcombo"].As<MenuList>().Value > 2)
                {
                    RootM["combo"]["rcombo"].As<MenuList>().Value = 0;
                }
            }
        }*/

        /*private void Render_OnPresent()
        {
            if (Player.IsDead) return;
            DoDrawing();
        }*/
        private void Game_OnUpdate()
        {
            Orbwalker.Implementation.AttackingEnabled = true;

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