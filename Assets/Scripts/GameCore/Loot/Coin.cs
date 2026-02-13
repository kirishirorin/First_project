using GameCore.UI;
using UnityEngine;
using Zenject;

namespace GameCore.Loot
{
    public class Coin : Loot
    {
        private CoinsUIUpdater  _coinsUIUpdater;
        private CoinsKeeper _coinsKeeper;


        protected override void Pickup()
        {
            base.Pickup();
            _coinsKeeper.AddCoin();
            _coinsUIUpdater.OnCountChanged?.Invoke();
        }


        [Inject]
        private void Construct(CoinsUIUpdater coinsUIUpdater, CoinsKeeper coinsKeeper)
        {
            _coinsUIUpdater =  coinsUIUpdater;
            _coinsKeeper = coinsKeeper;
        }
    }
}