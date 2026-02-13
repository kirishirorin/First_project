using System;

namespace GameCore.Loot
{
    public class CoinsKeeper
    {
        public int Coins {get; private set;}

        public void AddCoin()
        {
            Coins++;
        }

        public void AddCoins(int amount)
        {
            if (amount > 0)
            {
                Coins += amount;
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(amount));
            }
        }
    }
}