using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aimtec;
using Aimtec.SDK.Events;
using Aimtec.SDK.Menu.Components;

namespace E_Girl_Diana
{
    internal partial class egrilldiana
    {
        public void LoadEvents()
        {
            Render.OnPresent += Render_OnPresent;
            //.OnUpdate += OnTick;
            //Game.OnWndProc += ClickEvent;
            Game.OnUpdate += Game_OnUpdate;
            GameEvents.GameStart += GameEventsOnGameStart;
            Game.OnUpdate += UpdateSkin;
            GameObject.OnRevive += GameObjectOnOnRevive;

        }
    }
}
