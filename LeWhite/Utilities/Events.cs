
using Aimtec;
using Aimtec.SDK.Events;

namespace LeWhite
{
    internal partial class LB
    {

        public void LoadEvents()
        {
           Render.OnPresent += OnDraw;
            Game.OnUpdate += OnTick;
            //Game.OnUpdate += Game_OnUpdate;
            GameEvents.GameStart += GameEventsOnGameStart;
            Game.OnUpdate += UpdateSkin;
            GameObject.OnRevive += GameObjectOnOnRevive;
        }
    }
}
