using Aimtec;
using Aimtec.SDK.Extensions;
using Aimtec.SDK.Menu;
using Aimtec.SDK.Menu.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Girl_Diana
{
    internal partial class egrilldiana
    {
        //soon bik skin
        private static void GameEventsOnGameStart()
        {

            Self["mySkin"].OnValueChanged += (sender, args) => Game.OnUpdate += UpdateSkin;

        }
        private static void UpdateSkin()
        {
            
            var mySkin = Misc["mySkin"].Value;
            Player.SetSkinId(mySkin);
            Game.OnUpdate -= UpdateSkin;
        }
        private static void GameObjectOnOnRevive(GameObject sender)
        {
            if (sender is Obj_AI_Hero hero)
            {
                if (hero.IsMe)
                {
                    var mySkin = Self["mySkin"].Value;
                    Player.SetSkinId(mySkin);
                }
            }
        }

    }
}
