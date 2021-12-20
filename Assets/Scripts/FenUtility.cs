using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FenUtility {

	static Dictionary<char, int> pieceTypeFromSymbol = new Dictionary<char, int>()
	{
		['k'] = 5,
		['p'] = 1,
		['n'] = 3,
		['b'] = 4,
		['r'] = 2,
		['q'] = 6,
		['a'] = 7,
		['c'] = 10,
		['d'] = 21,
		['e'] = 9,
		['f'] = 11,
		['h'] = 14,
		['i'] = 15,
		['j'] = 24,
		['l'] = 12,
		['m'] = 16,
		['o'] = 22,
		['s'] = 13,
		['t'] = 17,
		['u'] = 18,
		['v'] = 20,
		['w'] = 23,
		['x'] = 19,
		['z'] = 8
	};

	public const string startFen = "rmxxxxcfnnhikqnnfcxxxxtr/uljvwobzaadeedaazbowvjsu/pppppppppppppppppppppppp/888/888/888/888/888/888/888/888/888/888/888/888/888/888/888/888/888/888/PPPPPPPPPPPPPPPPPPPPPPPP/USJVWOBZAADEEDAAZBOWVJLU/RTXXXXCFNNHIKQNNFCXXXXMR w - - 0 1";

	// Load position from fen string
	public static LoadedPositionInfo PositionFromFen (string fen) {

		LoadedPositionInfo loadedPositionInfo = new LoadedPositionInfo ();
		string[] sections = fen.Split (' ');
		
		int file = 0;
		int rank = 23;
		int file2 = 0;
		int rank2 = 23;

		foreach (char symbol in sections[0])
		{
			if (symbol == '/')
			{
				file2 = 0;
				rank2--;
			}
			else
			{
				if (char.IsDigit(symbol))
				{
					file2 += (int)char.GetNumericValue(symbol);
				}
				else
				{
					int pieceColourInt = char.IsUpper(symbol) ? 0 : 1;
					loadedPositionInfo.colors[rank2 * 24 + file2] = pieceColourInt;
					file2++;
				}
			}
		}

		foreach (char symbol in sections[0])
		{
			if (symbol == '/')
			{
				file = 0;
				rank--;
			}
			else
			{
				if (char.IsDigit(symbol))
				{
					file += (int)char.GetNumericValue(symbol);
				}
				else
				{
					int pieceType = pieceTypeFromSymbol[char.ToLower(symbol)];
					loadedPositionInfo.squares[rank * 24 + file] = pieceType;
					file++;
				} 
			}
		}

		loadedPositionInfo.whiteToMove = (sections[1] == "w");

		// Half-move clock
		if (sections.Length > 4) {
			int.TryParse (sections[4], out loadedPositionInfo.plyCount);
		}
		return loadedPositionInfo;
	}

	// Get the fen string of the current position
	public static string CurrentFen () {
		GameManager gameManager = GameManager.current;
		string fen = "";
		for (int rank = 23; rank >= 0; rank--) {
			int numEmptyFiles = 0;
			for (int file = 0; file < 24; file++)
			{
				int i = rank * 24 + file;

				if(gameManager.pieces[i] == null)
                {
					numEmptyFiles++;
					if (numEmptyFiles != 0)
					{
						switch (numEmptyFiles)
						{
							case 24:
								fen += "888";
								break;
							case 23:
								fen += "887";
								break;
							case 22:
								fen += "886";
								break;
							case 21:
								fen += "885";
								break;
							case 20:
								fen += "884";
								break;
							case 19:
								fen += "883";
								break;
							case 18:
								fen += "882";
								break;
							case 17:
								fen += "881";
								break;
							case 16:
								fen += "88";
								break;
							case 15:
								fen += "87";
								break;
							case 14:
								fen += "86";
								break;
							case 13:
								fen += "85";
								break;
							case 12:
								fen += "84";
								break;
							case 11:
								fen += "83";
								break;
							case 10:
								fen += "82";
								break;
							case 9:
								fen += "81";
								break;
							case 8:
							case 7:
							case 6:
							case 5:
							case 4:
							case 3:
							case 2:
							case 1:
								fen += numEmptyFiles;
								break;
						}
						numEmptyFiles = 0;
					}
					continue;
                }

				int piece = gameManager.pieces[i].pieceType;
				bool isBlack = !gameManager.pieces[i].color;
				if (piece != 0)
				{
					if (numEmptyFiles != 0)
					{
						switch (numEmptyFiles)
						{
							case 24:
								fen += "888";
								break;
							case 23:
								fen += "887";
								break;
							case 22:
								fen += "886";
								break;
							case 21:
								fen += "885";
								break;
							case 20:
								fen += "884";
								break;
							case 19:
								fen += "883";
								break;
							case 18:
								fen += "882";
								break;
							case 17:
								fen += "881";
								break;
							case 16:
								fen += "88";
								break;
							case 15:
								fen += "87";
								break;
							case 14:
								fen += "86";
								break;
							case 13:
								fen += "85";
								break;
							case 12:
								fen += "84";
								break;
							case 11:
								fen += "83";
								break;
							case 10:
								fen += "82";
								break;
							case 9:
								fen += "81";
								break;
							case 8:
							case 7:
							case 6:
							case 5:
							case 4:
							case 3:
							case 2:
							case 1:
								fen += numEmptyFiles;
								break;
						}
						numEmptyFiles = 0;
					}

					char pieceChar = ' ';
					switch (piece)
					{
						case 2:
							pieceChar = 'R';
							break;
						case 3:
							pieceChar = 'N';
							break;
						case 4:
							pieceChar = 'B';
							break;
						case 6:
							pieceChar = 'Q';
							break;
						case 5:
							pieceChar = 'K';
							break;
						case 1:
							pieceChar = 'P';
							break;
						case 7:
							pieceChar = 'A';
							break;
						case 10:
							pieceChar = 'C';
							break;
						case 21:
							pieceChar = 'D';
							break;
						case 9:
							pieceChar = 'E';
							break;
						case 11:
							pieceChar = 'F';
							break;
						case 14:
							pieceChar = 'H';
							break;
						case 15:
							pieceChar = 'I';
							break;
						case 24:
							pieceChar = 'J';
							break;
						case 12:
							pieceChar = 'L';
							break;
						case 16:
							pieceChar = 'M';
							break;
						case 22:
							pieceChar = 'O';
							break;
						case 13:
							pieceChar = 'S';
							break;
						case 17:
							pieceChar = 'T';
							break;
						case 18:
							pieceChar = 'U';
							break;
						case 20:
							pieceChar = 'V';
							break;
						case 23:
							pieceChar = 'W';
							break;
						case 19:
							pieceChar = 'X';
							break;
						case 8:
							pieceChar = 'Z';
							break;
					}
					fen += isBlack ? pieceChar.ToString().ToLower() : pieceChar.ToString();

				}
				else
				{
					numEmptyFiles++;
				}

			}
				if (numEmptyFiles != 0)
				{
					switch (numEmptyFiles)
					{
						case 24:
							fen += "888";
							break;
						case 23:
							fen += "887";
							break;
						case 22:
							fen += "886";
							break;
						case 21:
							fen += "885";
							break;
						case 20:
							fen += "884";
							break;
						case 19:
							fen += "883";
							break;
						case 18:
							fen += "882";
							break;
						case 17:
							fen += "881";
							break;
						case 16:
							fen += "88";
							break;
						case 15:
							fen += "87";
							break;
						case 14:
							fen += "86";
							break;
						case 13:
							fen += "85";
							break;
						case 12:
							fen += "84";
							break;
						case 11:
							fen += "83";
							break;
						case 10:
							fen += "82";
							break;
						case 9:
							fen += "81";
							break;
						case 8:
						case 7:
						case 6:
						case 5:
						case 4:
						case 3:
						case 2:
						case 1:
							fen += numEmptyFiles;
							break;
					}
				}

				if (rank != 0)
				{
					fen += '/';
				}
			}

		// Side to move
		fen += ' ';
		fen += (gameManager.isPlayerTurn) ? 'w' : 'b';

		// 50 move counter
		fen += ' ';
		fen += gameManager.fiftyMoveCounter;

		// Full-move count (should be one at start, and increase after each move by black)
		fen += ' ';
		fen += (gameManager.plyCount / 2) + 1;

		return fen;
	}

	public class LoadedPositionInfo {
		public int[] squares;
		public bool whiteToMove;
		public int plyCount;
		public int[] colors;

		public LoadedPositionInfo () {
			squares = new int[576];
			colors = new int[576];
		}
	}
}
