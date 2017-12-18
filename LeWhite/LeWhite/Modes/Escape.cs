

using Aimtec;
using Aimtec.SDK.Extensions;
using Aimtec.SDK.Menu.Components;

namespace LeWhite
{
    internal partial class LB
    {
        public void DoEscape()
        {
            var pos = MyHero.ServerPosition.Extend(Game.CursorPos, W.Range);
            if (RootM["escape"]["useW"].As<MenuBool>().Enabled && W.Ready && IsW1())
            {
                CastW(pos);
            }else if (RootM["escape"]["useR"].As<MenuBool>().Enabled && R.Ready )
            {
                if (IsR1())
                {
                    R.Cast();
                }
                else
                {
                    this.CastW(pos);
                }
            }
            MyHero.IssueOrder(OrderType.MoveTo, Game.CursorPos);
        }
    }
}
