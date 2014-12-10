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
// Création de méthodes pour identifier clairement les traitements
// Définition de InvestPlan dans un namespace
// Création du type BestInvestment et création de la méthode GetBestInvestment
// Découpage de la méthode GetBestInvestment
// Reprise des conditions de la double boucle for pour supprimer le if
//
// ==> l'organisation des méthodes dans les classes n'est pas optimal


using System;
using System.Collections.Generic;
using System.Globalization;

namespace Katastrophic
{
    public class InvestPlan
    {
        public static InvestPlan Input(string investPlans)
        {
            var result = new InvestPlan(investPlans);
            result.ProcessInvestment();
            return result;
        }

        public const int MonthCount = 12;

        private string output;
        private readonly string input;

        private InvestPlan(string input)
        {
            this.input = input;
            this.output = string.Empty;
        }

        private void ProcessInvestment()
        {
            IList<string> inputs = this.input.Split(Environment.NewLine.ToCharArray(),
                StringSplitOptions.RemoveEmptyEntries);

            for (int investmentCount = 1; investmentCount < inputs.Count; investmentCount += 2)
            {
                var investAmount = int.Parse(inputs[investmentCount], NumberStyles.Integer, CultureInfo.InvariantCulture);
                var prices = GetPrices(inputs[investmentCount + 1]);
                var casNumero = (investmentCount + 1)/2;
                ProcessSingleInvestment(casNumero, investAmount, prices);
            }
        }

        private static IList<int> GetPrices(string prices)
        {
            IList<int> pricePerMonth = new List<int>();
            string[] pricesList = prices.Split(' ');
            for (int i = 0; i < MonthCount; i++)
            {
                pricePerMonth.Add(int.Parse(pricesList[i], NumberStyles.Integer, CultureInfo.InvariantCulture));
            }
            return pricePerMonth;
        }

        private void ProcessSingleInvestment(int casNumero, int investAmount, IList<int> prices)
        {
            var bestInvestment = GetBestInvestment(investAmount, prices);
            if (output.Length > 0)
            {
                output += "\n";
            }
            output += string.Format(CultureInfo.InvariantCulture, "Case {0:d}: {1}", casNumero, bestInvestment);
        }

        private static BestInvestment GetBestInvestment(int investAmount, IList<int> prices)
        {
            var bestInvestment = new BestInvestment();
            for (int buyMonth = 1; buyMonth < MonthCount; buyMonth++)
            {
                for (int sellMonth = buyMonth + 1; sellMonth <= MonthCount; sellMonth++)
                {
                    int buyPrice = prices[buyMonth - 1];
                    int sellPrice = prices[sellMonth - 1];
                    var profit = BestInvestment.EvaluateProfit(investAmount, buyPrice, sellPrice);
                    bestInvestment.SetBestProfit(profit, buyMonth, sellMonth);
                }
            }
            return bestInvestment;
        }

        public string Output()
        {
            return output;
        }
    }
}