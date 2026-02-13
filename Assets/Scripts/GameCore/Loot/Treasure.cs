using GameCore.UI;
using UnityEngine;
using Zenject;

namespace GameCore.Loot
{
    public class Treasure : Loot
    {
        private TreasureWindow  _treasureWindow;
        
        
        protected override void Pickup()
        {
            base.Pickup();
            _treasureWindow.Activate();
        }


        [Inject]
        public void Construct(TreasureWindow treasureWindow)
        {
            _treasureWindow = treasureWindow;
        }
    }
}