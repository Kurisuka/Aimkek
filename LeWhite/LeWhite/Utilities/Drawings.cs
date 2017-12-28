
using System.Drawing;
using Aimtec;
using Aimtec.SDK.Menu.Components;

namespace LeWhite
{
    internal partial class LB
    {
        public void DoDraws()
        {
            #region Draw Spells

            if (RootM["draw"]["disable"].As<MenuBool>().Enabled)
            {
                return;
            }
            if (RootM["draw"]["drawS"]["qdraw"].As<MenuBool>().Enabled && Q.Ready)
            {
                Render.Circle(MyHero.Position, Q.Range, 50, Color.Aquamarine);
            }
            if (RootM["draw"]["drawS"]["wdraw"].As<MenuBool>().Enabled && W.Ready)
            {
                Render.Circle(MyHero.Position, W.Range, 50, Color.Cornsilk);
            }
            if (RootM["draw"]["drawS"]["edraw"].As<MenuBool>().Enabled && E.Ready)
            {
                Render.Circle(MyHero.Position, E.Range, 50, Color.LightGreen);
            }
            if (RootM["draw"]["drawS"]["rdraw"].As<MenuBool>().Enabled && R.Ready)
            {
                Render.Circle(MyHero.Position, R.Range, 50, Color.Brown);
            }
            /*var drawpos = "Default";
            if (RootM["combo"]["combologics"]["rlogic"].As<MenuList>().Value == 0)
            {
                var dp = "DF";
                switch (RootM["combo"]["combologics"]["rslogic"].As<MenuList>().Value)
                {
                    case 0:
                        dp = " Q";
                        break;
                    case 1:
                        dp = " E";
                        break;
                    case 2:
                        dp = " W";
                        break;
                }
                drawpos = "Manual R Selection :" + dp;
            }
            else
            {
                if (RootM["draw"]["combomode"].As<MenuBool>().Enabled)
                {
                    if (RootM["combo"]["combologics"]["select"].As<MenuList>().Value == 0)
                    {
                        drawpos = "Current Combo Mode: Dynamic Combo";
                    }
                    else if (RootM["combo"]["combologics"]["select"].As<MenuList>().Value == 1)
                    {
                        switch (RootM["combo"]["combologics"]["mCombo"].As<MenuList>().Value)
                        {
                            case 0:
                                drawpos = "Current Combo Mode: Q>E>W>R";
                                break;
                            case 1:
                                drawpos = "Current Combo Mode: Q>R>E>W";
                                break;
                            case 2:
                                drawpos = "Current Combo Mode: E>Q>W>R";
                                break;
                            case 3:
                                drawpos = "Current Combo Mode: E>W>Q>R";
                                break;
                            case 4:
                                drawpos = "Current Combo Mode: W>R>Q>E";
                                break;
                            case 5:
                                drawpos = "Current Combo Mode: W>Q>R>E";
                                break;
                            case 6:
                                drawpos = "Current Combo Mode: Q>R>W>E";
                                break;
                            case 7:
                                drawpos = "Current Combo Mode: Double Stun";
                                break;
                        }
                    }
                }
              
            }
            var pos = MyHero.FloatingHealthBarPosition;
            pos.X += 55;
            pos.Y += 55;
            Render.Text(pos, Color.White, drawpos);*/

            #endregion
        }
    }
}
