/*
 * Created by SharpDevelop.
 * User: mrhitj
 * Date: 2/6/2024
 * Time: 1:50 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Dynamic;

namespace a_3x3x3_Scrambler
{
	/// <summary>
	/// Description of Scrambler.
	/// </summary>
	public class Scrambler
	{
		
		readonly private string[] notationDefault = {"U", "U'", "U2",
		                                     "R", "R'", "R2", 
		                                     "D", "D'", "D2", 
		                                     "L", "L'", "L2", 
		                                     "F", "F'", "F2", 
		                                     "B", "B'", "B2"};
		
		readonly private string[,] notationByGroup = {{"U", "U'", "U2"},
											{"R", "R'", "R2"},
											{"D", "D'", "D2"},
											{"L", "L'", "L2"},
											{"F", "F'", "F2"},
											{ "B", "B'", "B2"}};
		public string[] notation; // 
		public int length; // Scramble length
		public string[] Scramble;
		private Random pickRandomNotation = new Random();
		private char[] rawScramble; // Scramble's notation without " ' " and "2"

		public Scrambler(int Length)
		{
			notation = new string[notationDefault.Length];
			length = Length;
			Scramble = new string[length];
			rawScramble = new char[length];
		}
		
		private string[] restoreNotation()
		{
			string[] temp = new string[notationDefault.Length];
			for(int i = 0; i < notationDefault.Length; i++){
				temp[i] = notationDefault[i];
			}
			return temp;
		}
		
		private void removeNotation(int whichGroup)
		{
			string[] temp = new string[length];
			int k = 0;
			
			for(int i = 0;i < notationByGroup.GetLength(1); i++)
			{
				for (int j = 0; j < notation.Length; j++) {
					if(notation[j] == notationByGroup[whichGroup,i])
					{
						notation[j] = null;
					}
				}
			}
			
			// Copy temp to notation, skipping null value in notation
			for(int i = 0; i < notation.Length; i++)
			{
				if(notation[i] == null)
				{
					continue;
				}
				
				temp[k] = notation[i];
				k++;
			}
			
			notation = temp;
		}
		
		private void getRawNotation()
		{
			for(int i = 0; i < Scramble.Length;i++)
			{
				// Get the character only, ignoring " ' " and " 2 "
				rawScramble[i] = Scramble[i][0];
			}
		}
		
		private void initScramble(string mode = "DEFAULT", int atIndex = 0)
		{
			int pickNotation;
			switch(mode)
			{
				// Get a new move at specified index in Scramble
				case "INDEX":
					pickNotation = pickRandomNotation.Next(0, (notationDefault.Length - 1));
					Scramble[atIndex] = notationDefault[pickNotation];
					break;
				case "DEFAULT":
					for(int i = 0; i < length; i++)
					{		
						pickNotation = pickRandomNotation.Next(0, (notationDefault.Length - 1));
						Scramble[i] = notationDefault[pickNotation];
					}
					break;
				default:
					break;
			}
			
		}
		
		private int getNotationIndex(int currentRawScrambleIndex)
		{
			int notationIndex = 0;
			switch(rawScramble[currentRawScrambleIndex]){
				case 'U':
					notationIndex = 0;
					break;
				case 'R':
					notationIndex = 1;
					break;
				case 'D':
					notationIndex = 2;
					break;
				case 'L':
					notationIndex = 3;
					break;
				case 'F':
					notationIndex = 4;
					break;
				case 'B':
					notationIndex = 5;
					break;
				default:
					notationIndex = 0;
					break;
			}
			return notationIndex;
		}
		
		public void generateScramble()
		{
			int notationIndex;
			notation = restoreNotation();
			initScramble();
			getRawNotation();
			// Using bubble sort's check check
			for(int i = 0; i < length; i++)
			{
				for(int j = 0; j < length - (i+1); j++)
				{
					notationIndex = getNotationIndex(j);
					if(rawScramble[j] == rawScramble[j+1] || (j > 0 && rawScramble[j] == rawScramble[j-1]))
					{
						removeNotation(notationIndex);
						initScramble("INDEX", j+1);
					}
					notation = restoreNotation();
					getRawNotation();
				}
			}
			return;
		}
	}
}
