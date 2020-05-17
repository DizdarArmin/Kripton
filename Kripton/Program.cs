using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kripton
{
    class Program
    {

        static void Main(string[] args)
        {
            Coiner coiner = new Coiner();

            Console.WriteLine("");

            coiner.CoinInit();
            coiner.multipleSearch();

           
           
        }
       
        



        public class Coiner
        {
            Client client = new Client();
            Coins coins = new Coins();
            List<Coin> searchedCoins = new List<Coin>();

            
            public void multipleSearch() {
                string search = Console.ReadLine();
                search = search.ToUpper();
                string[] searchArray = search.Split(new char[] { ',', '\t' }, System.StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < searchArray.Length; i++)
                {
                    SearchForCoin(searchArray[i]);
                }
                CoinPrint();
                multipleSearch();
            }


            public void CoinInit()
            {
                client.makeRequest("https://api.coincap.io/v2/assets");
                coins = client.GetCoins();
            }

            public void SearchForCoin(string symbol) 
            {
                Console.Clear();
                foreach (Coin coin in coins.data) 
                {
                    if (coin.symbol == symbol) 
                    {   
                        searchedCoins.Add(coin);    
                    }
                  
                }

            }

            public void CoinPrint() 
            {
                Console.WriteLine("");
                Console.WriteLine("");
                foreach (Coin coin in searchedCoins) {
                    string empty = "                    ";
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(empty + coin.name + " ");

                    if (Convert(coin.changePercent24Hr) > 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }

                    else if (Convert(coin.changePercent24Hr) < 0) 
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else { Console.ForegroundColor = ConsoleColor.Gray; }


                        Console.Write(empty + Convert(coin.priceUsd) + "$ ");

                    Console.Write(empty + Convert(coin.changePercent24Hr) + "%");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine();

                }
                searchedCoins.Clear();
                
            }

            public float Convert(string value) {
                return
                    (float)Math.Round(float.Parse(value, CultureInfo.InvariantCulture.NumberFormat) * 100f) / 100f;
                



            }







        }
        



       
    }





}
