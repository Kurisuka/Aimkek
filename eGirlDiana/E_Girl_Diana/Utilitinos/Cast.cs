using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aimtec;
using Aimtec.SDK;
using Aimtec.SDK.Extensions;

namespace E_Girl_Diana
{
    internal partial class egrilldiana
    {
        /*public void CastQ(Obj_AI_Base unit)
        {
            if (!Q.Ready) return;
            if (Player.Distance(unit) < Q.Range) Q.Cast(Q.GetPrediction(unit).CastPosition);
        }*/

        public void CastW()
        {
            if (!W.Ready) return;
            W.Cast();
        }

        public void CastE(Obj_AI_Base unit)
        {
            if (!E.Ready) return;
            if (Player.Distance(unit) < E.Range) E.Cast(target);
        }

        /*public void CastR(Obj_AI_Base unit)
        {
            if (!R.Ready) return;
            if (Player.Distance(unit) < R.Range) R.Cast(target);
        }*/
    }
}
