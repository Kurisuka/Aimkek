using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aimtec;
using Aimtec.SDK;
namespace E_Girl_Diana
{
    internal partial class egrilldiana
    {
        public static Obj_AI_Hero Player => ObjectManager.GetLocalPlayer();
        public Obj_AI_Hero target = Aimtec.SDK.TargetSelector.TargetSelector.GetTarget(925);
    }
}
