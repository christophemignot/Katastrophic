namespace Katastrophic
{
    public class BestInvestment
    {
        private int Profit;
        private int BuyMonth;
        private int SellMonth;

        public void SetBestProfit(int profit, int buyMonth, int sellMonth)
        {
            if (profit <= this.Profit)
                return;
            Profit = profit;
            BuyMonth = buyMonth;
            SellMonth = sellMonth;
        }

        public static int EvaluateProfit(int investAmount, int buyPrice, int sellPrice)
        {
            int quantity = investAmount / buyPrice;
            return quantity * (sellPrice - buyPrice);
        }

        public override string ToString()
        {
            if (this.Profit <= 0)
            {
                return "IMPOSSIBLE";
            }

            return string.Join(" ", this.BuyMonth, this.SellMonth, this.Profit);
        }
    }
}
