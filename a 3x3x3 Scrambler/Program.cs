/*
 * Created by SharpDevelop.
 * User: mrhitj
 * Date: 2/5/2024
 * Time: 3:01 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace a_3x3x3_Scrambler
{
	class Program
	{
		public static void Main(string[] args)
		{
			// Example program
//			int randomSeed = Convert.ToInt32(DateTime.Now.ToString("hhmmss"));
			Random Randomizer = new Random();
			Scrambler scrambler = new Scrambler(Randomizer.Next(20, 30));
			
			scrambler.generateScramble();
			foreach (var element in scrambler.Scramble){
				Console.Write(element + " ");
			}
			Console.WriteLine();
			
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
	}
}