using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aimtec;
using Aimtec.SDK;
using Aimtec.SDK.Menu;
using Aimtec.SDK.Menu.Components;
using System.Drawing;

namespace E_Girl_Diana
{
    internal partial class egrilldiana
    {
        public void DoDrawing()
        {
            if (RootM["draw"]["disabled"].As<MenuBool>().Enabled) return;

            if (RootM["draw"]["drawq"].As<MenuBool>().Enabled) Render.Circle(Player.Position, Q.Range, 50, Color.Gold);
            if (RootM["draw"]["draww"].As<MenuBool>().Enabled) Render.Circle(Player.Position, W.Range, 50, Color.GreenYellow);
            if (RootM["draw"]["drawe"].As<MenuBool>().Enabled) Render.Circle(Player.Position, E.Range, 50, Color.HotPink);
            if (RootM["draw"]["drawr"].As<MenuBool>().Enabled) Render.Circle(Player.Position, R.Range, 50, Color.Aquamarine);

            /*string drawpos = "";
            if (RootM["draw"]["combomode"].As<MenuBool>().Enabled)
            {
                switch (RootM["combo"]["combomode"].As<MenuList>().Value)
                {
                    case 0:
                        drawpos = "Combo Mode: Burst";
                        break;
                    case 1:
                        drawpos = "Combo Mode: Marked Only";
                        break;
                    case 2:
                        drawpos = "Combo Mode: Like Flash";
                        break;
                }
            }
            var pos = Player.FloatingHealthBarPosition;
            pos.X += 50;
            pos.Y += 30;
#pragma warning disable CS0618 // Type or member is obsolete
            Render.Text(pos, Color.White, drawpos);
#pragma warning restore CS0618 // Type or member is obsolete*/
        }
    }
}
