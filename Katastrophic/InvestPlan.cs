//remplacement des types String -> string Object -> object pour respect des normes de dev
// ==> suppression du using System
//Ajout de tests sur Input(string input) pour découvrir la structure
// Il y a des variables dans la structure qui ne sont pas utilisées, des noms de variable étranges et des types inadaptés
// ==> replacement  currency ?? "€" pour currency
// ==> renommage de variables et suppression de variables inutilisées
// ==> suppression static et passage en instance
//        ==> suppression du lock et du clear sur la liste : allowingProfits
//        ==> inversion de la méthode de construction/évaluation du retour
//        ==> expostion de la méthode input static pour encapsuler le traitement et maintenir le fonctonnement du TU
//        ==> currency est vide, on le supprime
// ==> correction du formatage de sortie case #%d
// Ajout de TU
// Renommage de variables/classe/fichier
// Suppression des stream qui nécessite un appel à Dispose


using System;
using System.Collections.Generic;
using System.Globalization;

public class InvestPlan
{
    public static InvestPlan Input(string input)
    {
        var result = new InvestPlan(input);
        result.ProcessInvestment();
        return result;
    }

    public const int MonthCount = 12;

    private string output;
    private readonly string input;
    private readonly List<string> allowingProfits;

    private InvestPlan(string input)
    {
        this.input = input;
        allowingProfits = new List<string>();
    }

    private void ProcessInvestment()
    {
        IList<string> inputs = this.input.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
        int.Parse(inputs[0]);

        for (int investmentCount = 1; investmentCount < inputs.Count; investmentCount+=2)
        {
            ProcessSingleInvestment(inputs[investmentCount], inputs[investmentCount+1]);
        }

        string result = string.Empty;
        for (int i = 0; i < allowingProfits.Count; i++)
        {
            if (i > 0)
            {
                result += "\n";
            }
            result += string.Format("Case {0:d}: {1}", i + 1, allowingProfits[i]);
        }
        this.output = result.ToString(CultureInfo.InvariantCulture);
    }

    private void ProcessSingleInvestment(string amount, string pricesPerMonth)
    {
        int investAmount = int.Parse(amount);
        List<int> prices = new List<int>();

        // Max profit is set to min value for initialization purposes. 
        int maxProfit = int.MinValue;
        string[] strings = pricesPerMonth.Split(' ');
        for (int i = 0; i < MonthCount; i++)
        {
            prices.Add(int.Parse(strings[i]));
        }

        // We use BigInteger for optimization on ARM processors 
        int bestBuyMonth = int.MinValue;
        int bestSellMonth = int.MinValue;

        for (int buyMonth = 1; buyMonth <= MonthCount; buyMonth++)
        {
            for (int sellMonth = 1; sellMonth <= MonthCount; sellMonth++)
            {
                int profit = int.MinValue;
                if (buyMonth < sellMonth)
                {
                    int sellPrice = prices[buyMonth - 1];
                    int quantity = investAmount/sellPrice;
                    profit = -quantity*sellPrice;
                    int buyPrice = prices[sellMonth - 1];
                    int rev = quantity*buyPrice;
                    profit += rev;
                }
                if (profit > maxProfit)
                {
                    maxProfit = profit;
                    bestBuyMonth = buyMonth;
                    bestSellMonth = sellMonth;
                }
            }
        }

        string bestProfit;
        if (maxProfit <= 0)
        {
            bestProfit = "IMPOSSIBLE";
        }
        else
        {
            bestProfit = bestBuyMonth + " " + bestSellMonth + " " + maxProfit;
        }
        allowingProfits.Add(bestProfit);
    }


    public string Output()
    {
        return output;
    }
} 
