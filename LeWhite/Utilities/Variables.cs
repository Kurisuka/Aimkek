
using Aimtec;
using Aimtec.SDK.TargetSelector;

namespace LeWhite
{
    internal partial class LB
    {
        public static Obj_AI_Hero MyHero => ObjectManager.GetLocalPlayer();
        public Obj_AI_Hero target = TargetSelector.GetTarget(925);
        public bool delaycheck = false;
        public int lastp = 0;
        public bool combostart = false;

        //leblancq  leblancw leblancwreturn leblance leblancrtoggle leblancrwreturn
    }
}
