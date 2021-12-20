using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public int pieceType = 0;
    public bool color = true; //true is white, false is black
    public bool hasMoved = false;
    public int amountMoved = 0;
    public Vector2Int boardPosition;
    public Vector2Int startingBoardPosition;
    const int blackMask = 0b10000;
    const int whiteMask = 0b01000;
    public const int White = 8;
    public const int Black = 16;
    const int colourMask = whiteMask | blackMask;

    public static int PieceTypeIndex(int piece)
    {
        return piece;
    }

    public string getPieceText(int pieceTypeText, bool colorText)
    {
        if (colorText)
        {
            return pieceTypeText switch
            {
                0 => "none",
                1 => "pawn",
                2 => "rook",
                3 => "knight",
                4 => "bishop",
                5 => "king",
                6 => "queen",
                7 => "amazon",
                8 => "wizard",
                9 => "champion",
                10 => "archbishop",
                11 => "chancellor",
                12 => "falcon",
                13 => "hunter",
                14 => "dragon horse",
                15 => "dragon king",
                16 => "gold general",
                17 => "silver general",
                18 => "lance",
                19 => "ultima_pawn",
                20 => "long leaper",
                21 => "cannon",
                22 => "guard",
                23 => "mao",
                24 => "elephant",
                _ => "none",
            };
        }
        else
        {
            return pieceTypeText switch
            {
                0 => "none",
                1 => "pawn",
                2 => "rook",
                3 => "knight",
                4 => "bishop",
                5 => "king",
                6 => "queen",
                7 => "amazon",
                8 => "wizard",
                9 => "champion",
                10 => "archbishop",
                11 => "chancellor",
                12 => "falcon",
                13 => "hunter",
                14 => "dragon horse",
                15 => "dragon king",
                16 => "gold general",
                17 => "silver general",
                18 => "lance",
                19 => "ultima_pawn",
                20 => "long leaper",
                21 => "cannon",
                22 => "guard",
                23 => "mao",
                24 => "elephant",
                _ => "none",
            };
        }
    }

    public static bool IsColour(int piece, int colour)
    {
        return (piece & colourMask) == colour;
    }

    public bool colorMath(String fen, Vector2 xy)
    {
        boardPosition = new Vector2Int((int)xy.x, (int)xy.y);
        startingBoardPosition = boardPosition;
        GameManager gameManager = GameManager.current;
        // int index = (int)((xy.y * 24) + xy.x);
        var loadedPosition = FenUtility.PositionFromFen(fen);
        return color = xy.y switch
        {
            0 => xy.x switch
            {
                0 => loadedPosition.colors[0] == 0 ? true : false,
                1 => loadedPosition.colors[1] == 0 ? true : false,
                2 => loadedPosition.colors[2] == 0 ? true : false,
                3 => loadedPosition.colors[3] == 0 ? true : false,
                4 => loadedPosition.colors[4] == 0 ? true : false,
                5 => loadedPosition.colors[5] == 0 ? true : false,
                6 => loadedPosition.colors[6] == 0 ? true : false,
                7 => loadedPosition.colors[7] == 0 ? true : false,
                8 => loadedPosition.colors[8] == 0 ? true : false,
                9 => loadedPosition.colors[9] == 0 ? true : false,
                10 => loadedPosition.colors[10] == 0 ? true : false,
                11 => loadedPosition.colors[11] == 0 ? true : false,
                12 => loadedPosition.colors[12] == 0 ? true : false,
                13 => loadedPosition.colors[13] == 0 ? true : false,
                14 => loadedPosition.colors[14] == 0 ? true : false,
                15 => loadedPosition.colors[15] == 0 ? true : false,
                16 => loadedPosition.colors[16] == 0 ? true : false,
                17 => loadedPosition.colors[17] == 0 ? true : false,
                18 => loadedPosition.colors[18] == 0 ? true : false,
                19 => loadedPosition.colors[19] == 0 ? true : false,
                20 => loadedPosition.colors[20] == 0 ? true : false,
                21 => loadedPosition.colors[21] == 0 ? true : false,
                22 => loadedPosition.colors[22] == 0 ? true : false,
                23 => loadedPosition.colors[23] == 0 ? true : false,
                _ => false,
            },
            1 => xy.x switch
            {
                0 => loadedPosition.colors[24] == 0 ? true : false,
                1 => loadedPosition.colors[25] == 0 ? true : false,
                2 => loadedPosition.colors[26] == 0 ? true : false,
                3 => loadedPosition.colors[27] == 0 ? true : false,
                4 => loadedPosition.colors[28] == 0 ? true : false,
                5 => loadedPosition.colors[29] == 0 ? true : false,
                6 => loadedPosition.colors[30] == 0 ? true : false,
                7 => loadedPosition.colors[31] == 0 ? true : false,
                8 => loadedPosition.colors[32] == 0 ? true : false,
                9 => loadedPosition.colors[33] == 0 ? true : false,
                10 => loadedPosition.colors[34] == 0 ? true : false,
                11 => loadedPosition.colors[35] == 0 ? true : false,
                12 => loadedPosition.colors[36] == 0 ? true : false,
                13 => loadedPosition.colors[37] == 0 ? true : false,
                14 => loadedPosition.colors[38] == 0 ? true : false,
                15 => loadedPosition.colors[39] == 0 ? true : false,
                16 => loadedPosition.colors[40] == 0 ? true : false,
                17 => loadedPosition.colors[41] == 0 ? true : false,
                18 => loadedPosition.colors[42] == 0 ? true : false,
                19 => loadedPosition.colors[43] == 0 ? true : false,
                20 => loadedPosition.colors[44] == 0 ? true : false,
                21 => loadedPosition.colors[45] == 0 ? true : false,
                22 => loadedPosition.colors[46] == 0 ? true : false,
                23 => loadedPosition.colors[47] == 0 ? true : false,
                _ => false,
            },
            2 => xy.x switch
            {
                0 => loadedPosition.colors[48] == 0 ? true : false,
                1 => loadedPosition.colors[49] == 0 ? true : false,
                2 => loadedPosition.colors[50] == 0 ? true : false,
                3 => loadedPosition.colors[51] == 0 ? true : false,
                4 => loadedPosition.colors[52] == 0 ? true : false,
                5 => loadedPosition.colors[53] == 0 ? true : false,
                6 => loadedPosition.colors[54] == 0 ? true : false,
                7 => loadedPosition.colors[55] == 0 ? true : false,
                8 => loadedPosition.colors[56] == 0 ? true : false,
                9 => loadedPosition.colors[57] == 0 ? true : false,
                10 => loadedPosition.colors[58] == 0 ? true : false,
                11 => loadedPosition.colors[59] == 0 ? true : false,
                12 => loadedPosition.colors[60] == 0 ? true : false,
                13 => loadedPosition.colors[61] == 0 ? true : false,
                14 => loadedPosition.colors[62] == 0 ? true : false,
                15 => loadedPosition.colors[63] == 0 ? true : false,
                16 => loadedPosition.colors[64] == 0 ? true : false,
                17 => loadedPosition.colors[65] == 0 ? true : false,
                18 => loadedPosition.colors[66] == 0 ? true : false,
                19 => loadedPosition.colors[67] == 0 ? true : false,
                20 => loadedPosition.colors[68] == 0 ? true : false,
                21 => loadedPosition.colors[69] == 0 ? true : false,
                22 => loadedPosition.colors[70] == 0 ? true : false,
                23 => loadedPosition.colors[71] == 0 ? true : false,
                _ => false,
            },
            3 => xy.x switch
            {
                0 => loadedPosition.colors[72] == 0 ? true : false,
                1 => loadedPosition.colors[73] == 0 ? true : false,
                2 => loadedPosition.colors[74] == 0 ? true : false,
                3 => loadedPosition.colors[75] == 0 ? true : false,
                4 => loadedPosition.colors[76] == 0 ? true : false,
                5 => loadedPosition.colors[77] == 0 ? true : false,
                6 => loadedPosition.colors[78] == 0 ? true : false,
                7 => loadedPosition.colors[79] == 0 ? true : false,
                8 => loadedPosition.colors[80] == 0 ? true : false,
                9 => loadedPosition.colors[81] == 0 ? true : false,
                10 => loadedPosition.colors[82] == 0 ? true : false,
                11 => loadedPosition.colors[83] == 0 ? true : false,
                12 => loadedPosition.colors[84] == 0 ? true : false,
                13 => loadedPosition.colors[85] == 0 ? true : false,
                14 => loadedPosition.colors[86] == 0 ? true : false,
                15 => loadedPosition.colors[87] == 0 ? true : false,
                16 => loadedPosition.colors[88] == 0 ? true : false,
                17 => loadedPosition.colors[89] == 0 ? true : false,
                18 => loadedPosition.colors[90] == 0 ? true : false,
                19 => loadedPosition.colors[91] == 0 ? true : false,
                20 => loadedPosition.colors[92] == 0 ? true : false,
                21 => loadedPosition.colors[93] == 0 ? true : false,
                22 => loadedPosition.colors[94] == 0 ? true : false,
                23 => loadedPosition.colors[95] == 0 ? true : false,
                _ => false,
            },
            4 => xy.x switch
            {
                0 => loadedPosition.colors[96] == 0 ? true : false,
                1 => loadedPosition.colors[97] == 0 ? true : false,
                2 => loadedPosition.colors[98] == 0 ? true : false,
                3 => loadedPosition.colors[99] == 0 ? true : false,
                4 => loadedPosition.colors[100] == 0 ? true : false,
                5 => loadedPosition.colors[101] == 0 ? true : false,
                6 => loadedPosition.colors[102] == 0 ? true : false,
                7 => loadedPosition.colors[103] == 0 ? true : false,
                8 => loadedPosition.colors[104] == 0 ? true : false,
                9 => loadedPosition.colors[105] == 0 ? true : false,
                10 => loadedPosition.colors[106] == 0 ? true : false,
                11 => loadedPosition.colors[107] == 0 ? true : false,
                12 => loadedPosition.colors[108] == 0 ? true : false,
                13 => loadedPosition.colors[109] == 0 ? true : false,
                14 => loadedPosition.colors[110] == 0 ? true : false,
                15 => loadedPosition.colors[111] == 0 ? true : false,
                16 => loadedPosition.colors[112] == 0 ? true : false,
                17 => loadedPosition.colors[113] == 0 ? true : false,
                18 => loadedPosition.colors[114] == 0 ? true : false,
                19 => loadedPosition.colors[115] == 0 ? true : false,
                20 => loadedPosition.colors[116] == 0 ? true : false,
                21 => loadedPosition.colors[117] == 0 ? true : false,
                22 => loadedPosition.colors[118] == 0 ? true : false,
                23 => loadedPosition.colors[119] == 0 ? true : false,
                _ => false,
            },
            5 => xy.x switch
            {
                0 => loadedPosition.colors[120] == 0 ? true : false,
                1 => loadedPosition.colors[121] == 0 ? true : false,
                2 => loadedPosition.colors[122] == 0 ? true : false,
                3 => loadedPosition.colors[123] == 0 ? true : false,
                4 => loadedPosition.colors[124] == 0 ? true : false,
                5 => loadedPosition.colors[125] == 0 ? true : false,
                6 => loadedPosition.colors[126] == 0 ? true : false,
                7 => loadedPosition.colors[127] == 0 ? true : false,
                8 => loadedPosition.colors[128] == 0 ? true : false,
                9 => loadedPosition.colors[129] == 0 ? true : false,
                10 => loadedPosition.colors[130] == 0 ? true : false,
                11 => loadedPosition.colors[131] == 0 ? true : false,
                12 => loadedPosition.colors[132] == 0 ? true : false,
                13 => loadedPosition.colors[133] == 0 ? true : false,
                14 => loadedPosition.colors[134] == 0 ? true : false,
                15 => loadedPosition.colors[135] == 0 ? true : false,
                16 => loadedPosition.colors[136] == 0 ? true : false,
                17 => loadedPosition.colors[137] == 0 ? true : false,
                18 => loadedPosition.colors[138] == 0 ? true : false,
                19 => loadedPosition.colors[139] == 0 ? true : false,
                20 => loadedPosition.colors[140] == 0 ? true : false,
                21 => loadedPosition.colors[141] == 0 ? true : false,
                22 => loadedPosition.colors[142] == 0 ? true : false,
                23 => loadedPosition.colors[143] == 0 ? true : false,
                _ => false,
            },
            6 => xy.x switch
            {
                0 => loadedPosition.colors[144] == 0 ? true : false,
                1 => loadedPosition.colors[145] == 0 ? true : false,
                2 => loadedPosition.colors[146] == 0 ? true : false,
                3 => loadedPosition.colors[147] == 0 ? true : false,
                4 => loadedPosition.colors[148] == 0 ? true : false,
                5 => loadedPosition.colors[149] == 0 ? true : false,
                6 => loadedPosition.colors[150] == 0 ? true : false,
                7 => loadedPosition.colors[151] == 0 ? true : false,
                8 => loadedPosition.colors[152] == 0 ? true : false,
                9 => loadedPosition.colors[153] == 0 ? true : false,
                10 => loadedPosition.colors[154] == 0 ? true : false,
                11 => loadedPosition.colors[155] == 0 ? true : false,
                12 => loadedPosition.colors[156] == 0 ? true : false,
                13 => loadedPosition.colors[157] == 0 ? true : false,
                14 => loadedPosition.colors[158] == 0 ? true : false,
                15 => loadedPosition.colors[159] == 0 ? true : false,
                16 => loadedPosition.colors[160] == 0 ? true : false,
                17 => loadedPosition.colors[161] == 0 ? true : false,
                18 => loadedPosition.colors[162] == 0 ? true : false,
                19 => loadedPosition.colors[163] == 0 ? true : false,
                20 => loadedPosition.colors[164] == 0 ? true : false,
                21 => loadedPosition.colors[165] == 0 ? true : false,
                22 => loadedPosition.colors[166] == 0 ? true : false,
                23 => loadedPosition.colors[167] == 0 ? true : false,
                _ => false,
            },
            7 => xy.x switch
            {
                0 => loadedPosition.colors[168] == 0 ? true : false,
                1 => loadedPosition.colors[169] == 0 ? true : false,
                2 => loadedPosition.colors[170] == 0 ? true : false,
                3 => loadedPosition.colors[171] == 0 ? true : false,
                4 => loadedPosition.colors[172] == 0 ? true : false,
                5 => loadedPosition.colors[173] == 0 ? true : false,
                6 => loadedPosition.colors[174] == 0 ? true : false,
                7 => loadedPosition.colors[175] == 0 ? true : false,
                8 => loadedPosition.colors[176] == 0 ? true : false,
                9 => loadedPosition.colors[177] == 0 ? true : false,
                10 => loadedPosition.colors[178] == 0 ? true : false,
                11 => loadedPosition.colors[179] == 0 ? true : false,
                12 => loadedPosition.colors[180] == 0 ? true : false,
                13 => loadedPosition.colors[181] == 0 ? true : false,
                14 => loadedPosition.colors[182] == 0 ? true : false,
                15 => loadedPosition.colors[183] == 0 ? true : false,
                16 => loadedPosition.colors[184] == 0 ? true : false,
                17 => loadedPosition.colors[185] == 0 ? true : false,
                18 => loadedPosition.colors[186] == 0 ? true : false,
                19 => loadedPosition.colors[187] == 0 ? true : false,
                20 => loadedPosition.colors[188] == 0 ? true : false,
                21 => loadedPosition.colors[189] == 0 ? true : false,
                22 => loadedPosition.colors[190] == 0 ? true : false,
                23 => loadedPosition.colors[191] == 0 ? true : false,
                _ => false,
            },
            8 => xy.x switch
            {
                0 => loadedPosition.colors[192] == 0 ? true : false,
                1 => loadedPosition.colors[193] == 0 ? true : false,
                2 => loadedPosition.colors[194] == 0 ? true : false,
                3 => loadedPosition.colors[195] == 0 ? true : false,
                4 => loadedPosition.colors[196] == 0 ? true : false,
                5 => loadedPosition.colors[197] == 0 ? true : false,
                6 => loadedPosition.colors[198] == 0 ? true : false,
                7 => loadedPosition.colors[199] == 0 ? true : false,
                8 => loadedPosition.colors[200] == 0 ? true : false,
                9 => loadedPosition.colors[201] == 0 ? true : false,
                10 => loadedPosition.colors[202] == 0 ? true : false,
                11 => loadedPosition.colors[203] == 0 ? true : false,
                12 => loadedPosition.colors[204] == 0 ? true : false,
                13 => loadedPosition.colors[205] == 0 ? true : false,
                14 => loadedPosition.colors[206] == 0 ? true : false,
                15 => loadedPosition.colors[207] == 0 ? true : false,
                16 => loadedPosition.colors[208] == 0 ? true : false,
                17 => loadedPosition.colors[209] == 0 ? true : false,
                18 => loadedPosition.colors[210] == 0 ? true : false,
                19 => loadedPosition.colors[211] == 0 ? true : false,
                20 => loadedPosition.colors[212] == 0 ? true : false,
                21 => loadedPosition.colors[213] == 0 ? true : false,
                22 => loadedPosition.colors[214] == 0 ? true : false,
                23 => loadedPosition.colors[215] == 0 ? true : false,
                _ => false,
            },
            9 => xy.x switch
            {
                0 => loadedPosition.colors[216] == 0 ? true : false,
                1 => loadedPosition.colors[217] == 0 ? true : false,
                2 => loadedPosition.colors[218] == 0 ? true : false,
                3 => loadedPosition.colors[219] == 0 ? true : false,
                4 => loadedPosition.colors[220] == 0 ? true : false,
                5 => loadedPosition.colors[221] == 0 ? true : false,
                6 => loadedPosition.colors[222] == 0 ? true : false,
                7 => loadedPosition.colors[223] == 0 ? true : false,
                8 => loadedPosition.colors[224] == 0 ? true : false,
                9 => loadedPosition.colors[225] == 0 ? true : false,
                10 => loadedPosition.colors[226] == 0 ? true : false,
                11 => loadedPosition.colors[227] == 0 ? true : false,
                12 => loadedPosition.colors[228] == 0 ? true : false,
                13 => loadedPosition.colors[229] == 0 ? true : false,
                14 => loadedPosition.colors[230] == 0 ? true : false,
                15 => loadedPosition.colors[231] == 0 ? true : false,
                16 => loadedPosition.colors[232] == 0 ? true : false,
                17 => loadedPosition.colors[233] == 0 ? true : false,
                18 => loadedPosition.colors[234] == 0 ? true : false,
                19 => loadedPosition.colors[235] == 0 ? true : false,
                20 => loadedPosition.colors[236] == 0 ? true : false,
                21 => loadedPosition.colors[237] == 0 ? true : false,
                22 => loadedPosition.colors[238] == 0 ? true : false,
                23 => loadedPosition.colors[239] == 0 ? true : false,
                _ => false,
            },
            10 => xy.x switch
            {
                0 => loadedPosition.colors[240] == 0 ? true : false,
                1 => loadedPosition.colors[241] == 0 ? true : false,
                2 => loadedPosition.colors[242] == 0 ? true : false,
                3 => loadedPosition.colors[243] == 0 ? true : false,
                4 => loadedPosition.colors[244] == 0 ? true : false,
                5 => loadedPosition.colors[245] == 0 ? true : false,
                6 => loadedPosition.colors[246] == 0 ? true : false,
                7 => loadedPosition.colors[247] == 0 ? true : false,
                8 => loadedPosition.colors[248] == 0 ? true : false,
                9 => loadedPosition.colors[249] == 0 ? true : false,
                10 => loadedPosition.colors[250] == 0 ? true : false,
                11 => loadedPosition.colors[251] == 0 ? true : false,
                12 => loadedPosition.colors[252] == 0 ? true : false,
                13 => loadedPosition.colors[253] == 0 ? true : false,
                14 => loadedPosition.colors[254] == 0 ? true : false,
                15 => loadedPosition.colors[255] == 0 ? true : false,
                16 => loadedPosition.colors[256] == 0 ? true : false,
                17 => loadedPosition.colors[257] == 0 ? true : false,
                18 => loadedPosition.colors[258] == 0 ? true : false,
                19 => loadedPosition.colors[259] == 0 ? true : false,
                20 => loadedPosition.colors[260] == 0 ? true : false,
                21 => loadedPosition.colors[261] == 0 ? true : false,
                22 => loadedPosition.colors[262] == 0 ? true : false,
                23 => loadedPosition.colors[263] == 0 ? true : false,
                _ => false,
            },
            11 => xy.x switch
            {
                0 => loadedPosition.colors[264] == 0 ? true : false,
                1 => loadedPosition.colors[265] == 0 ? true : false,
                2 => loadedPosition.colors[266] == 0 ? true : false,
                3 => loadedPosition.colors[267] == 0 ? true : false,
                4 => loadedPosition.colors[268] == 0 ? true : false,
                5 => loadedPosition.colors[269] == 0 ? true : false,
                6 => loadedPosition.colors[270] == 0 ? true : false,
                7 => loadedPosition.colors[271] == 0 ? true : false,
                8 => loadedPosition.colors[272] == 0 ? true : false,
                9 => loadedPosition.colors[273] == 0 ? true : false,
                10 => loadedPosition.colors[274] == 0 ? true : false,
                11 => loadedPosition.colors[275] == 0 ? true : false,
                12 => loadedPosition.colors[276] == 0 ? true : false,
                13 => loadedPosition.colors[277] == 0 ? true : false,
                14 => loadedPosition.colors[278] == 0 ? true : false,
                15 => loadedPosition.colors[279] == 0 ? true : false,
                16 => loadedPosition.colors[280] == 0 ? true : false,
                17 => loadedPosition.colors[281] == 0 ? true : false,
                18 => loadedPosition.colors[282] == 0 ? true : false,
                19 => loadedPosition.colors[283] == 0 ? true : false,
                20 => loadedPosition.colors[284] == 0 ? true : false,
                21 => loadedPosition.colors[285] == 0 ? true : false,
                22 => loadedPosition.colors[286] == 0 ? true : false,
                23 => loadedPosition.colors[287] == 0 ? true : false,
                _ => false,
            },
            12 => xy.x switch
            {
                0 => loadedPosition.colors[288] == 0 ? true : false,
                1 => loadedPosition.colors[289] == 0 ? true : false,
                2 => loadedPosition.colors[290] == 0 ? true : false,
                3 => loadedPosition.colors[291] == 0 ? true : false,
                4 => loadedPosition.colors[292] == 0 ? true : false,
                5 => loadedPosition.colors[293] == 0 ? true : false,
                6 => loadedPosition.colors[294] == 0 ? true : false,
                7 => loadedPosition.colors[295] == 0 ? true : false,
                8 => loadedPosition.colors[296] == 0 ? true : false,
                9 => loadedPosition.colors[297] == 0 ? true : false,
                10 => loadedPosition.colors[298] == 0 ? true : false,
                11 => loadedPosition.colors[299] == 0 ? true : false,
                12 => loadedPosition.colors[300] == 0 ? true : false,
                13 => loadedPosition.colors[301] == 0 ? true : false,
                14 => loadedPosition.colors[302] == 0 ? true : false,
                15 => loadedPosition.colors[303] == 0 ? true : false,
                16 => loadedPosition.colors[304] == 0 ? true : false,
                17 => loadedPosition.colors[305] == 0 ? true : false,
                18 => loadedPosition.colors[306] == 0 ? true : false,
                19 => loadedPosition.colors[307] == 0 ? true : false,
                20 => loadedPosition.colors[308] == 0 ? true : false,
                21 => loadedPosition.colors[309] == 0 ? true : false,
                22 => loadedPosition.colors[310] == 0 ? true : false,
                23 => loadedPosition.colors[311] == 0 ? true : false,
                _ => false,
            },
            13 => xy.x switch
            {
                0 => loadedPosition.colors[312] == 0 ? true : false,
                1 => loadedPosition.colors[313] == 0 ? true : false,
                2 => loadedPosition.colors[314] == 0 ? true : false,
                3 => loadedPosition.colors[315] == 0 ? true : false,
                4 => loadedPosition.colors[316] == 0 ? true : false,
                5 => loadedPosition.colors[317] == 0 ? true : false,
                6 => loadedPosition.colors[318] == 0 ? true : false,
                7 => loadedPosition.colors[319] == 0 ? true : false,
                8 => loadedPosition.colors[320] == 0 ? true : false,
                9 => loadedPosition.colors[321] == 0 ? true : false,
                10 => loadedPosition.colors[322] == 0 ? true : false,
                11 => loadedPosition.colors[323] == 0 ? true : false,
                12 => loadedPosition.colors[324] == 0 ? true : false,
                13 => loadedPosition.colors[325] == 0 ? true : false,
                14 => loadedPosition.colors[326] == 0 ? true : false,
                15 => loadedPosition.colors[327] == 0 ? true : false,
                16 => loadedPosition.colors[328] == 0 ? true : false,
                17 => loadedPosition.colors[329] == 0 ? true : false,
                18 => loadedPosition.colors[330] == 0 ? true : false,
                19 => loadedPosition.colors[331] == 0 ? true : false,
                20 => loadedPosition.colors[332] == 0 ? true : false,
                21 => loadedPosition.colors[333] == 0 ? true : false,
                22 => loadedPosition.colors[334] == 0 ? true : false,
                23 => loadedPosition.colors[335] == 0 ? true : false,
                _ => false,
            },
            14 => xy.x switch
            {
                0 => loadedPosition.colors[336] == 0 ? true : false,
                1 => loadedPosition.colors[337] == 0 ? true : false,
                2 => loadedPosition.colors[338] == 0 ? true : false,
                3 => loadedPosition.colors[339] == 0 ? true : false,
                4 => loadedPosition.colors[340] == 0 ? true : false,
                5 => loadedPosition.colors[341] == 0 ? true : false,
                6 => loadedPosition.colors[342] == 0 ? true : false,
                7 => loadedPosition.colors[343] == 0 ? true : false,
                8 => loadedPosition.colors[344] == 0 ? true : false,
                9 => loadedPosition.colors[345] == 0 ? true : false,
                10 => loadedPosition.colors[346] == 0 ? true : false,
                11 => loadedPosition.colors[347] == 0 ? true : false,
                12 => loadedPosition.colors[348] == 0 ? true : false,
                13 => loadedPosition.colors[349] == 0 ? true : false,
                14 => loadedPosition.colors[350] == 0 ? true : false,
                15 => loadedPosition.colors[351] == 0 ? true : false,
                16 => loadedPosition.colors[352] == 0 ? true : false,
                17 => loadedPosition.colors[353] == 0 ? true : false,
                18 => loadedPosition.colors[354] == 0 ? true : false,
                19 => loadedPosition.colors[355] == 0 ? true : false,
                20 => loadedPosition.colors[356] == 0 ? true : false,
                21 => loadedPosition.colors[357] == 0 ? true : false,
                22 => loadedPosition.colors[358] == 0 ? true : false,
                23 => loadedPosition.colors[359] == 0 ? true : false,
                _ => false,
            },
            15 => xy.x switch
            {
                0 => loadedPosition.colors[360] == 0 ? true : false,
                1 => loadedPosition.colors[361] == 0 ? true : false,
                2 => loadedPosition.colors[362] == 0 ? true : false,
                3 => loadedPosition.colors[363] == 0 ? true : false,
                4 => loadedPosition.colors[364] == 0 ? true : false,
                5 => loadedPosition.colors[365] == 0 ? true : false,
                6 => loadedPosition.colors[366] == 0 ? true : false,
                7 => loadedPosition.colors[367] == 0 ? true : false,
                8 => loadedPosition.colors[368] == 0 ? true : false,
                9 => loadedPosition.colors[369] == 0 ? true : false,
                10 => loadedPosition.colors[370] == 0 ? true : false,
                11 => loadedPosition.colors[371] == 0 ? true : false,
                12 => loadedPosition.colors[372] == 0 ? true : false,
                13 => loadedPosition.colors[373] == 0 ? true : false,
                14 => loadedPosition.colors[374] == 0 ? true : false,
                15 => loadedPosition.colors[375] == 0 ? true : false,
                16 => loadedPosition.colors[376] == 0 ? true : false,
                17 => loadedPosition.colors[377] == 0 ? true : false,
                18 => loadedPosition.colors[378] == 0 ? true : false,
                19 => loadedPosition.colors[379] == 0 ? true : false,
                20 => loadedPosition.colors[380] == 0 ? true : false,
                21 => loadedPosition.colors[381] == 0 ? true : false,
                22 => loadedPosition.colors[382] == 0 ? true : false,
                23 => loadedPosition.colors[383] == 0 ? true : false,
                _ => false,
            },
            16 => xy.x switch
            {
                0 => loadedPosition.colors[384] == 0 ? true : false,
                1 => loadedPosition.colors[385] == 0 ? true : false,
                2 => loadedPosition.colors[386] == 0 ? true : false,
                3 => loadedPosition.colors[387] == 0 ? true : false,
                4 => loadedPosition.colors[388] == 0 ? true : false,
                5 => loadedPosition.colors[389] == 0 ? true : false,
                6 => loadedPosition.colors[390] == 0 ? true : false,
                7 => loadedPosition.colors[391] == 0 ? true : false,
                8 => loadedPosition.colors[392] == 0 ? true : false,
                9 => loadedPosition.colors[393] == 0 ? true : false,
                10 => loadedPosition.colors[394] == 0 ? true : false,
                11 => loadedPosition.colors[395] == 0 ? true : false,
                12 => loadedPosition.colors[396] == 0 ? true : false,
                13 => loadedPosition.colors[397] == 0 ? true : false,
                14 => loadedPosition.colors[398] == 0 ? true : false,
                15 => loadedPosition.colors[399] == 0 ? true : false,
                16 => loadedPosition.colors[400] == 0 ? true : false,
                17 => loadedPosition.colors[401] == 0 ? true : false,
                18 => loadedPosition.colors[402] == 0 ? true : false,
                19 => loadedPosition.colors[403] == 0 ? true : false,
                20 => loadedPosition.colors[404] == 0 ? true : false,
                21 => loadedPosition.colors[405] == 0 ? true : false,
                22 => loadedPosition.colors[406] == 0 ? true : false,
                23 => loadedPosition.colors[407] == 0 ? true : false,
                _ => false,
            },
            17 => xy.x switch
            {
                0 => loadedPosition.colors[408] == 0 ? true : false,
                1 => loadedPosition.colors[409] == 0 ? true : false,
                2 => loadedPosition.colors[410] == 0 ? true : false,
                3 => loadedPosition.colors[411] == 0 ? true : false,
                4 => loadedPosition.colors[412] == 0 ? true : false,
                5 => loadedPosition.colors[413] == 0 ? true : false,
                6 => loadedPosition.colors[414] == 0 ? true : false,
                7 => loadedPosition.colors[415] == 0 ? true : false,
                8 => loadedPosition.colors[416] == 0 ? true : false,
                9 => loadedPosition.colors[417] == 0 ? true : false,
                10 => loadedPosition.colors[418] == 0 ? true : false,
                11 => loadedPosition.colors[419] == 0 ? true : false,
                12 => loadedPosition.colors[420] == 0 ? true : false,
                13 => loadedPosition.colors[421] == 0 ? true : false,
                14 => loadedPosition.colors[422] == 0 ? true : false,
                15 => loadedPosition.colors[423] == 0 ? true : false,
                16 => loadedPosition.colors[424] == 0 ? true : false,
                17 => loadedPosition.colors[425] == 0 ? true : false,
                18 => loadedPosition.colors[426] == 0 ? true : false,
                19 => loadedPosition.colors[427] == 0 ? true : false,
                20 => loadedPosition.colors[428] == 0 ? true : false,
                21 => loadedPosition.colors[429] == 0 ? true : false,
                22 => loadedPosition.colors[430] == 0 ? true : false,
                23 => loadedPosition.colors[431] == 0 ? true : false,
                _ => false,
            },
            18 => xy.x switch
            {
                0 => loadedPosition.colors[432] == 0 ? true : false,
                1 => loadedPosition.colors[433] == 0 ? true : false,
                2 => loadedPosition.colors[434] == 0 ? true : false,
                3 => loadedPosition.colors[435] == 0 ? true : false,
                4 => loadedPosition.colors[436] == 0 ? true : false,
                5 => loadedPosition.colors[437] == 0 ? true : false,
                6 => loadedPosition.colors[438] == 0 ? true : false,
                7 => loadedPosition.colors[439] == 0 ? true : false,
                8 => loadedPosition.colors[440] == 0 ? true : false,
                9 => loadedPosition.colors[441] == 0 ? true : false,
                10 => loadedPosition.colors[442] == 0 ? true : false,
                11 => loadedPosition.colors[443] == 0 ? true : false,
                12 => loadedPosition.colors[444] == 0 ? true : false,
                13 => loadedPosition.colors[445] == 0 ? true : false,
                14 => loadedPosition.colors[446] == 0 ? true : false,
                15 => loadedPosition.colors[447] == 0 ? true : false,
                16 => loadedPosition.colors[448] == 0 ? true : false,
                17 => loadedPosition.colors[449] == 0 ? true : false,
                18 => loadedPosition.colors[450] == 0 ? true : false,
                19 => loadedPosition.colors[451] == 0 ? true : false,
                20 => loadedPosition.colors[452] == 0 ? true : false,
                21 => loadedPosition.colors[453] == 0 ? true : false,
                22 => loadedPosition.colors[454] == 0 ? true : false,
                23 => loadedPosition.colors[455] == 0 ? true : false,
                _ => false,
            },
            19 => xy.x switch
            {
                0 => loadedPosition.colors[456] == 0 ? true : false,
                1 => loadedPosition.colors[457] == 0 ? true : false,
                2 => loadedPosition.colors[458] == 0 ? true : false,
                3 => loadedPosition.colors[459] == 0 ? true : false,
                4 => loadedPosition.colors[460] == 0 ? true : false,
                5 => loadedPosition.colors[461] == 0 ? true : false,
                6 => loadedPosition.colors[462] == 0 ? true : false,
                7 => loadedPosition.colors[463] == 0 ? true : false,
                8 => loadedPosition.colors[464] == 0 ? true : false,
                9 => loadedPosition.colors[465] == 0 ? true : false,
                10 => loadedPosition.colors[466] == 0 ? true : false,
                11 => loadedPosition.colors[467] == 0 ? true : false,
                12 => loadedPosition.colors[468] == 0 ? true : false,
                13 => loadedPosition.colors[469] == 0 ? true : false,
                14 => loadedPosition.colors[470] == 0 ? true : false,
                15 => loadedPosition.colors[471] == 0 ? true : false,
                16 => loadedPosition.colors[472] == 0 ? true : false,
                17 => loadedPosition.colors[473] == 0 ? true : false,
                18 => loadedPosition.colors[474] == 0 ? true : false,
                19 => loadedPosition.colors[475] == 0 ? true : false,
                20 => loadedPosition.colors[476] == 0 ? true : false,
                21 => loadedPosition.colors[477] == 0 ? true : false,
                22 => loadedPosition.colors[478] == 0 ? true : false,
                23 => loadedPosition.colors[479] == 0 ? true : false,
                _ => false,
            },
            20 => xy.x switch
            {
                0 => loadedPosition.colors[480] == 0 ? true : false,
                1 => loadedPosition.colors[481] == 0 ? true : false,
                2 => loadedPosition.colors[482] == 0 ? true : false,
                3 => loadedPosition.colors[483] == 0 ? true : false,
                4 => loadedPosition.colors[484] == 0 ? true : false,
                5 => loadedPosition.colors[485] == 0 ? true : false,
                6 => loadedPosition.colors[486] == 0 ? true : false,
                7 => loadedPosition.colors[487] == 0 ? true : false,
                8 => loadedPosition.colors[488] == 0 ? true : false,
                9 => loadedPosition.colors[489] == 0 ? true : false,
                10 => loadedPosition.colors[490] == 0 ? true : false,
                11 => loadedPosition.colors[491] == 0 ? true : false,
                12 => loadedPosition.colors[492] == 0 ? true : false,
                13 => loadedPosition.colors[493] == 0 ? true : false,
                14 => loadedPosition.colors[494] == 0 ? true : false,
                15 => loadedPosition.colors[495] == 0 ? true : false,
                16 => loadedPosition.colors[496] == 0 ? true : false,
                17 => loadedPosition.colors[497] == 0 ? true : false,
                18 => loadedPosition.colors[498] == 0 ? true : false,
                19 => loadedPosition.colors[499] == 0 ? true : false,
                20 => loadedPosition.colors[500] == 0 ? true : false,
                21 => loadedPosition.colors[501] == 0 ? true : false,
                22 => loadedPosition.colors[502] == 0 ? true : false,
                23 => loadedPosition.colors[503] == 0 ? true : false,
                _ => false,
            },
            21 => xy.x switch
            {
                0 => loadedPosition.colors[504] == 0 ? true : false,
                1 => loadedPosition.colors[505] == 0 ? true : false,
                2 => loadedPosition.colors[506] == 0 ? true : false,
                3 => loadedPosition.colors[507] == 0 ? true : false,
                4 => loadedPosition.colors[508] == 0 ? true : false,
                5 => loadedPosition.colors[509] == 0 ? true : false,
                6 => loadedPosition.colors[510] == 0 ? true : false,
                7 => loadedPosition.colors[511] == 0 ? true : false,
                8 => loadedPosition.colors[512] == 0 ? true : false,
                9 => loadedPosition.colors[513] == 0 ? true : false,
                10 => loadedPosition.colors[514] == 0 ? true : false,
                11 => loadedPosition.colors[515] == 0 ? true : false,
                12 => loadedPosition.colors[516] == 0 ? true : false,
                13 => loadedPosition.colors[517] == 0 ? true : false,
                14 => loadedPosition.colors[518] == 0 ? true : false,
                15 => loadedPosition.colors[519] == 0 ? true : false,
                16 => loadedPosition.colors[520] == 0 ? true : false,
                17 => loadedPosition.colors[521] == 0 ? true : false,
                18 => loadedPosition.colors[522] == 0 ? true : false,
                19 => loadedPosition.colors[523] == 0 ? true : false,
                20 => loadedPosition.colors[524] == 0 ? true : false,
                21 => loadedPosition.colors[525] == 0 ? true : false,
                22 => loadedPosition.colors[526] == 0 ? true : false,
                23 => loadedPosition.colors[527] == 0 ? true : false,
                _ => false,
            },
            22 => xy.x switch
            {
                0 => loadedPosition.colors[528] == 0 ? true : false,
                1 => loadedPosition.colors[529] == 0 ? true : false,
                2 => loadedPosition.colors[530] == 0 ? true : false,
                3 => loadedPosition.colors[531] == 0 ? true : false,
                4 => loadedPosition.colors[532] == 0 ? true : false,
                5 => loadedPosition.colors[533] == 0 ? true : false,
                6 => loadedPosition.colors[534] == 0 ? true : false,
                7 => loadedPosition.colors[535] == 0 ? true : false,
                8 => loadedPosition.colors[536] == 0 ? true : false,
                9 => loadedPosition.colors[537] == 0 ? true : false,
                10 => loadedPosition.colors[538] == 0 ? true : false,
                11 => loadedPosition.colors[539] == 0 ? true : false,
                12 => loadedPosition.colors[540] == 0 ? true : false,
                13 => loadedPosition.colors[541] == 0 ? true : false,
                14 => loadedPosition.colors[542] == 0 ? true : false,
                15 => loadedPosition.colors[543] == 0 ? true : false,
                16 => loadedPosition.colors[544] == 0 ? true : false,
                17 => loadedPosition.colors[545] == 0 ? true : false,
                18 => loadedPosition.colors[546] == 0 ? true : false,
                19 => loadedPosition.colors[547] == 0 ? true : false,
                20 => loadedPosition.colors[548] == 0 ? true : false,
                21 => loadedPosition.colors[549] == 0 ? true : false,
                22 => loadedPosition.colors[550] == 0 ? true : false,
                23 => loadedPosition.colors[551] == 0 ? true : false,
                _ => false,
            },
            23 => xy.x switch
            {
                0 => loadedPosition.colors[552] == 0 ? true : false,
                1 => loadedPosition.colors[553] == 0 ? true : false,
                2 => loadedPosition.colors[554] == 0 ? true : false,
                3 => loadedPosition.colors[555] == 0 ? true : false,
                4 => loadedPosition.colors[556] == 0 ? true : false,
                5 => loadedPosition.colors[557] == 0 ? true : false,
                6 => loadedPosition.colors[558] == 0 ? true : false,
                7 => loadedPosition.colors[559] == 0 ? true : false,
                8 => loadedPosition.colors[560] == 0 ? true : false,
                9 => loadedPosition.colors[561] == 0 ? true : false,
                10 => loadedPosition.colors[562] == 0 ? true : false,
                11 => loadedPosition.colors[563] == 0 ? true : false,
                12 => loadedPosition.colors[564] == 0 ? true : false,
                13 => loadedPosition.colors[565] == 0 ? true : false,
                14 => loadedPosition.colors[566] == 0 ? true : false,
                15 => loadedPosition.colors[567] == 0 ? true : false,
                16 => loadedPosition.colors[568] == 0 ? true : false,
                17 => loadedPosition.colors[569] == 0 ? true : false,
                18 => loadedPosition.colors[570] == 0 ? true : false,
                19 => loadedPosition.colors[571] == 0 ? true : false,
                20 => loadedPosition.colors[572] == 0 ? true : false,
                21 => loadedPosition.colors[573] == 0 ? true : false,
                22 => loadedPosition.colors[574] == 0 ? true : false,
                23 => loadedPosition.colors[575] == 0 ? true : false,
                _ => false,
            },
            _ => false,
        };
    }

    public void pieceTypeInit(String fen, Vector2 xy)
    {
        boardPosition = new Vector2Int((int)xy.x, (int)xy.y);
        startingBoardPosition = boardPosition;
        GameManager gameManager = GameManager.current;
        // int index = (int)((xy.y * 24) + xy.x);
        var loadedPosition = FenUtility.PositionFromFen(fen);
        pieceType = xy.y switch
        {
            0 => xy.x switch
            {
                0 => loadedPosition.squares[0],
                1 => loadedPosition.squares[1],
                2 => loadedPosition.squares[2],
                3 => loadedPosition.squares[3],
                4 => loadedPosition.squares[4],
                5 => loadedPosition.squares[5],
                6 => loadedPosition.squares[6],
                7 => loadedPosition.squares[7],
                8 => loadedPosition.squares[8],
                9 => loadedPosition.squares[9],
                10 => loadedPosition.squares[10],
                11 => loadedPosition.squares[11],
                12 => loadedPosition.squares[12],
                13 => loadedPosition.squares[13],
                14 => loadedPosition.squares[14],
                15 => loadedPosition.squares[15],
                16 => loadedPosition.squares[16],
                17 => loadedPosition.squares[17],
                18 => loadedPosition.squares[18],
                19 => loadedPosition.squares[19],
                20 => loadedPosition.squares[20],
                21 => loadedPosition.squares[21],
                22 => loadedPosition.squares[22],
                23 => loadedPosition.squares[23],
                _ => 0,
            },
            1 => xy.x switch
            {
                0 => loadedPosition.squares[24],
                1 => loadedPosition.squares[25],
                2 => loadedPosition.squares[26],
                3 => loadedPosition.squares[27],
                4 => loadedPosition.squares[28],
                5 => loadedPosition.squares[29],
                6 => loadedPosition.squares[30],
                7 => loadedPosition.squares[31],
                8 => loadedPosition.squares[32],
                9 => loadedPosition.squares[33],
                10 => loadedPosition.squares[34],
                11 => loadedPosition.squares[35],
                12 => loadedPosition.squares[36],
                13 => loadedPosition.squares[37],
                14 => loadedPosition.squares[38],
                15 => loadedPosition.squares[39],
                16 => loadedPosition.squares[40],
                17 => loadedPosition.squares[41],
                18 => loadedPosition.squares[42],
                19 => loadedPosition.squares[43],
                20 => loadedPosition.squares[44],
                21 => loadedPosition.squares[45],
                22 => loadedPosition.squares[46],
                23 => loadedPosition.squares[47],
                _ => 0,
            },
            2 => xy.x switch
            {
                0 => loadedPosition.squares[48],
                1 => loadedPosition.squares[49],
                2 => loadedPosition.squares[50],
                3 => loadedPosition.squares[51],
                4 => loadedPosition.squares[52],
                5 => loadedPosition.squares[53],
                6 => loadedPosition.squares[54],
                7 => loadedPosition.squares[55],
                8 => loadedPosition.squares[56],
                9 => loadedPosition.squares[57],
                10 => loadedPosition.squares[58],
                11 => loadedPosition.squares[59],
                12 => loadedPosition.squares[60],
                13 => loadedPosition.squares[61],
                14 => loadedPosition.squares[62],
                15 => loadedPosition.squares[63],
                16 => loadedPosition.squares[64],
                17 => loadedPosition.squares[65],
                18 => loadedPosition.squares[66],
                19 => loadedPosition.squares[67],
                20 => loadedPosition.squares[68],
                21 => loadedPosition.squares[69],
                22 => loadedPosition.squares[70],
                23 => loadedPosition.squares[71],
                _ => 0,
            },
            3 => xy.x switch
            {
                0 => loadedPosition.squares[72],
                1 => loadedPosition.squares[73],
                2 => loadedPosition.squares[74],
                3 => loadedPosition.squares[75],
                4 => loadedPosition.squares[76],
                5 => loadedPosition.squares[77],
                6 => loadedPosition.squares[78],
                7 => loadedPosition.squares[79],
                8 => loadedPosition.squares[80],
                9 => loadedPosition.squares[81],
                10 => loadedPosition.squares[82],
                11 => loadedPosition.squares[83],
                12 => loadedPosition.squares[84],
                13 => loadedPosition.squares[85],
                14 => loadedPosition.squares[86],
                15 => loadedPosition.squares[87],
                16 => loadedPosition.squares[88],
                17 => loadedPosition.squares[89],
                18 => loadedPosition.squares[90],
                19 => loadedPosition.squares[91],
                20 => loadedPosition.squares[92],
                21 => loadedPosition.squares[93],
                22 => loadedPosition.squares[94],
                23 => loadedPosition.squares[95],
                _ => 0,
            },
            4 => xy.x switch
            {
                0 => loadedPosition.squares[96],
                1 => loadedPosition.squares[97],
                2 => loadedPosition.squares[98],
                3 => loadedPosition.squares[99],
                4 => loadedPosition.squares[100],
                5 => loadedPosition.squares[101],
                6 => loadedPosition.squares[102],
                7 => loadedPosition.squares[103],
                8 => loadedPosition.squares[104],
                9 => loadedPosition.squares[105],
                10 => loadedPosition.squares[106],
                11 => loadedPosition.squares[107],
                12 => loadedPosition.squares[108],
                13 => loadedPosition.squares[109],
                14 => loadedPosition.squares[110],
                15 => loadedPosition.squares[111],
                16 => loadedPosition.squares[112],
                17 => loadedPosition.squares[113],
                18 => loadedPosition.squares[114],
                19 => loadedPosition.squares[115],
                20 => loadedPosition.squares[116],
                21 => loadedPosition.squares[117],
                22 => loadedPosition.squares[118],
                23 => loadedPosition.squares[119],
                _ => 0,
            },
            5 => xy.x switch
            {
                0 => loadedPosition.squares[120],
                1 => loadedPosition.squares[121],
                2 => loadedPosition.squares[122],
                3 => loadedPosition.squares[123],
                4 => loadedPosition.squares[124],
                5 => loadedPosition.squares[125],
                6 => loadedPosition.squares[126],
                7 => loadedPosition.squares[127],
                8 => loadedPosition.squares[128],
                9 => loadedPosition.squares[129],
                10 => loadedPosition.squares[130],
                11 => loadedPosition.squares[131],
                12 => loadedPosition.squares[132],
                13 => loadedPosition.squares[133],
                14 => loadedPosition.squares[134],
                15 => loadedPosition.squares[135],
                16 => loadedPosition.squares[136],
                17 => loadedPosition.squares[137],
                18 => loadedPosition.squares[138],
                19 => loadedPosition.squares[139],
                20 => loadedPosition.squares[140],
                21 => loadedPosition.squares[141],
                22 => loadedPosition.squares[142],
                23 => loadedPosition.squares[143],
                _ => 0,
            },
            6 => xy.x switch
            {
                0 => loadedPosition.squares[144],
                1 => loadedPosition.squares[145],
                2 => loadedPosition.squares[146],
                3 => loadedPosition.squares[147],
                4 => loadedPosition.squares[148],
                5 => loadedPosition.squares[149],
                6 => loadedPosition.squares[150],
                7 => loadedPosition.squares[151],
                8 => loadedPosition.squares[152],
                9 => loadedPosition.squares[153],
                10 => loadedPosition.squares[154],
                11 => loadedPosition.squares[155],
                12 => loadedPosition.squares[156],
                13 => loadedPosition.squares[157],
                14 => loadedPosition.squares[158],
                15 => loadedPosition.squares[159],
                16 => loadedPosition.squares[160],
                17 => loadedPosition.squares[161],
                18 => loadedPosition.squares[162],
                19 => loadedPosition.squares[163],
                20 => loadedPosition.squares[164],
                21 => loadedPosition.squares[165],
                22 => loadedPosition.squares[166],
                23 => loadedPosition.squares[167],
                _ => 0,
            },
            7 => xy.x switch
            {
                0 => loadedPosition.squares[168],
                1 => loadedPosition.squares[169],
                2 => loadedPosition.squares[170],
                3 => loadedPosition.squares[171],
                4 => loadedPosition.squares[172],
                5 => loadedPosition.squares[173],
                6 => loadedPosition.squares[174],
                7 => loadedPosition.squares[175],
                8 => loadedPosition.squares[176],
                9 => loadedPosition.squares[177],
                10 => loadedPosition.squares[178],
                11 => loadedPosition.squares[179],
                12 => loadedPosition.squares[180],
                13 => loadedPosition.squares[181],
                14 => loadedPosition.squares[182],
                15 => loadedPosition.squares[183],
                16 => loadedPosition.squares[184],
                17 => loadedPosition.squares[185],
                18 => loadedPosition.squares[186],
                19 => loadedPosition.squares[187],
                20 => loadedPosition.squares[188],
                21 => loadedPosition.squares[189],
                22 => loadedPosition.squares[190],
                23 => loadedPosition.squares[191],
                _ => 0,
            },
            8 => xy.x switch
            {
                0 => loadedPosition.squares[192],
                1 => loadedPosition.squares[193],
                2 => loadedPosition.squares[194],
                3 => loadedPosition.squares[195],
                4 => loadedPosition.squares[196],
                5 => loadedPosition.squares[197],
                6 => loadedPosition.squares[198],
                7 => loadedPosition.squares[199],
                8 => loadedPosition.squares[200],
                9 => loadedPosition.squares[201],
                10 => loadedPosition.squares[202],
                11 => loadedPosition.squares[203],
                12 => loadedPosition.squares[204],
                13 => loadedPosition.squares[205],
                14 => loadedPosition.squares[206],
                15 => loadedPosition.squares[207],
                16 => loadedPosition.squares[208],
                17 => loadedPosition.squares[209],
                18 => loadedPosition.squares[210],
                19 => loadedPosition.squares[211],
                20 => loadedPosition.squares[212],
                21 => loadedPosition.squares[213],
                22 => loadedPosition.squares[214],
                23 => loadedPosition.squares[215],
                _ => 0,
            },
            9 => xy.x switch
            {
                0 => loadedPosition.squares[216],
                1 => loadedPosition.squares[217],
                2 => loadedPosition.squares[218],
                3 => loadedPosition.squares[219],
                4 => loadedPosition.squares[220],
                5 => loadedPosition.squares[221],
                6 => loadedPosition.squares[222],
                7 => loadedPosition.squares[223],
                8 => loadedPosition.squares[224],
                9 => loadedPosition.squares[225],
                10 => loadedPosition.squares[226],
                11 => loadedPosition.squares[227],
                12 => loadedPosition.squares[228],
                13 => loadedPosition.squares[229],
                14 => loadedPosition.squares[230],
                15 => loadedPosition.squares[231],
                16 => loadedPosition.squares[232],
                17 => loadedPosition.squares[233],
                18 => loadedPosition.squares[234],
                19 => loadedPosition.squares[235],
                20 => loadedPosition.squares[236],
                21 => loadedPosition.squares[237],
                22 => loadedPosition.squares[238],
                23 => loadedPosition.squares[239],
                _ => 0,
            },
            10 => xy.x switch
            {
                0 => loadedPosition.squares[240],
                1 => loadedPosition.squares[241],
                2 => loadedPosition.squares[242],
                3 => loadedPosition.squares[243],
                4 => loadedPosition.squares[244],
                5 => loadedPosition.squares[245],
                6 => loadedPosition.squares[246],
                7 => loadedPosition.squares[247],
                8 => loadedPosition.squares[248],
                9 => loadedPosition.squares[249],
                10 => loadedPosition.squares[250],
                11 => loadedPosition.squares[251],
                12 => loadedPosition.squares[252],
                13 => loadedPosition.squares[253],
                14 => loadedPosition.squares[254],
                15 => loadedPosition.squares[255],
                16 => loadedPosition.squares[256],
                17 => loadedPosition.squares[257],
                18 => loadedPosition.squares[258],
                19 => loadedPosition.squares[259],
                20 => loadedPosition.squares[260],
                21 => loadedPosition.squares[261],
                22 => loadedPosition.squares[262],
                23 => loadedPosition.squares[263],
                _ => 0,
            },
            11 => xy.x switch
            {
                0 => loadedPosition.squares[264],
                1 => loadedPosition.squares[265],
                2 => loadedPosition.squares[266],
                3 => loadedPosition.squares[267],
                4 => loadedPosition.squares[268],
                5 => loadedPosition.squares[269],
                6 => loadedPosition.squares[270],
                7 => loadedPosition.squares[271],
                8 => loadedPosition.squares[272],
                9 => loadedPosition.squares[273],
                10 => loadedPosition.squares[274],
                11 => loadedPosition.squares[275],
                12 => loadedPosition.squares[276],
                13 => loadedPosition.squares[277],
                14 => loadedPosition.squares[278],
                15 => loadedPosition.squares[279],
                16 => loadedPosition.squares[280],
                17 => loadedPosition.squares[281],
                18 => loadedPosition.squares[282],
                19 => loadedPosition.squares[283],
                20 => loadedPosition.squares[284],
                21 => loadedPosition.squares[285],
                22 => loadedPosition.squares[286],
                23 => loadedPosition.squares[287],
                _ => 0,
            },
            12 => xy.x switch
            {
                0 => loadedPosition.squares[288],
                1 => loadedPosition.squares[289],
                2 => loadedPosition.squares[290],
                3 => loadedPosition.squares[291],
                4 => loadedPosition.squares[292],
                5 => loadedPosition.squares[293],
                6 => loadedPosition.squares[294],
                7 => loadedPosition.squares[295],
                8 => loadedPosition.squares[296],
                9 => loadedPosition.squares[297],
                10 => loadedPosition.squares[298],
                11 => loadedPosition.squares[299],
                12 => loadedPosition.squares[300],
                13 => loadedPosition.squares[301],
                14 => loadedPosition.squares[302],
                15 => loadedPosition.squares[303],
                16 => loadedPosition.squares[304],
                17 => loadedPosition.squares[305],
                18 => loadedPosition.squares[306],
                19 => loadedPosition.squares[307],
                20 => loadedPosition.squares[308],
                21 => loadedPosition.squares[309],
                22 => loadedPosition.squares[310],
                23 => loadedPosition.squares[311],
                _ => 0,
            },
            13 => xy.x switch
            {
                0 => loadedPosition.squares[312],
                1 => loadedPosition.squares[313],
                2 => loadedPosition.squares[314],
                3 => loadedPosition.squares[315],
                4 => loadedPosition.squares[316],
                5 => loadedPosition.squares[317],
                6 => loadedPosition.squares[318],
                7 => loadedPosition.squares[319],
                8 => loadedPosition.squares[320],
                9 => loadedPosition.squares[321],
                10 => loadedPosition.squares[322],
                11 => loadedPosition.squares[323],
                12 => loadedPosition.squares[324],
                13 => loadedPosition.squares[325],
                14 => loadedPosition.squares[326],
                15 => loadedPosition.squares[327],
                16 => loadedPosition.squares[328],
                17 => loadedPosition.squares[329],
                18 => loadedPosition.squares[330],
                19 => loadedPosition.squares[331],
                20 => loadedPosition.squares[332],
                21 => loadedPosition.squares[333],
                22 => loadedPosition.squares[334],
                23 => loadedPosition.squares[335],
                _ => 0,
            },
            14 => xy.x switch
            {
                0 => loadedPosition.squares[336],
                1 => loadedPosition.squares[337],
                2 => loadedPosition.squares[338],
                3 => loadedPosition.squares[339],
                4 => loadedPosition.squares[340],
                5 => loadedPosition.squares[341],
                6 => loadedPosition.squares[342],
                7 => loadedPosition.squares[343],
                8 => loadedPosition.squares[344],
                9 => loadedPosition.squares[345],
                10 => loadedPosition.squares[346],
                11 => loadedPosition.squares[347],
                12 => loadedPosition.squares[348],
                13 => loadedPosition.squares[349],
                14 => loadedPosition.squares[350],
                15 => loadedPosition.squares[351],
                16 => loadedPosition.squares[352],
                17 => loadedPosition.squares[353],
                18 => loadedPosition.squares[354],
                19 => loadedPosition.squares[355],
                20 => loadedPosition.squares[356],
                21 => loadedPosition.squares[357],
                22 => loadedPosition.squares[358],
                23 => loadedPosition.squares[359],
                _ => 0,
            },
            15 => xy.x switch
            {
                0 => loadedPosition.squares[360],
                1 => loadedPosition.squares[361],
                2 => loadedPosition.squares[362],
                3 => loadedPosition.squares[363],
                4 => loadedPosition.squares[364],
                5 => loadedPosition.squares[365],
                6 => loadedPosition.squares[366],
                7 => loadedPosition.squares[367],
                8 => loadedPosition.squares[368],
                9 => loadedPosition.squares[369],
                10 => loadedPosition.squares[370],
                11 => loadedPosition.squares[371],
                12 => loadedPosition.squares[372],
                13 => loadedPosition.squares[373],
                14 => loadedPosition.squares[374],
                15 => loadedPosition.squares[375],
                16 => loadedPosition.squares[376],
                17 => loadedPosition.squares[377],
                18 => loadedPosition.squares[378],
                19 => loadedPosition.squares[379],
                20 => loadedPosition.squares[380],
                21 => loadedPosition.squares[381],
                22 => loadedPosition.squares[382],
                23 => loadedPosition.squares[383],
                _ => 0,
            },
            16 => xy.x switch
            {
                0 => loadedPosition.squares[384],
                1 => loadedPosition.squares[385],
                2 => loadedPosition.squares[386],
                3 => loadedPosition.squares[387],
                4 => loadedPosition.squares[388],
                5 => loadedPosition.squares[389],
                6 => loadedPosition.squares[390],
                7 => loadedPosition.squares[391],
                8 => loadedPosition.squares[392],
                9 => loadedPosition.squares[393],
                10 => loadedPosition.squares[394],
                11 => loadedPosition.squares[395],
                12 => loadedPosition.squares[396],
                13 => loadedPosition.squares[397],
                14 => loadedPosition.squares[398],
                15 => loadedPosition.squares[399],
                16 => loadedPosition.squares[400],
                17 => loadedPosition.squares[401],
                18 => loadedPosition.squares[402],
                19 => loadedPosition.squares[403],
                20 => loadedPosition.squares[404],
                21 => loadedPosition.squares[405],
                22 => loadedPosition.squares[406],
                23 => loadedPosition.squares[407],
                _ => 0,
            },
            17 => xy.x switch
            {
                0 => loadedPosition.squares[408],
                1 => loadedPosition.squares[409],
                2 => loadedPosition.squares[410],
                3 => loadedPosition.squares[411],
                4 => loadedPosition.squares[412],
                5 => loadedPosition.squares[413],
                6 => loadedPosition.squares[414],
                7 => loadedPosition.squares[415],
                8 => loadedPosition.squares[416],
                9 => loadedPosition.squares[417],
                10 => loadedPosition.squares[418],
                11 => loadedPosition.squares[419],
                12 => loadedPosition.squares[420],
                13 => loadedPosition.squares[421],
                14 => loadedPosition.squares[422],
                15 => loadedPosition.squares[423],
                16 => loadedPosition.squares[424],
                17 => loadedPosition.squares[425],
                18 => loadedPosition.squares[426],
                19 => loadedPosition.squares[427],
                20 => loadedPosition.squares[428],
                21 => loadedPosition.squares[429],
                22 => loadedPosition.squares[430],
                23 => loadedPosition.squares[431],
                _ => 0,
            },
            18 => xy.x switch
            {
                0 => loadedPosition.squares[432],
                1 => loadedPosition.squares[433],
                2 => loadedPosition.squares[434],
                3 => loadedPosition.squares[435],
                4 => loadedPosition.squares[436],
                5 => loadedPosition.squares[437],
                6 => loadedPosition.squares[438],
                7 => loadedPosition.squares[439],
                8 => loadedPosition.squares[440],
                9 => loadedPosition.squares[441],
                10 => loadedPosition.squares[442],
                11 => loadedPosition.squares[443],
                12 => loadedPosition.squares[444],
                13 => loadedPosition.squares[445],
                14 => loadedPosition.squares[446],
                15 => loadedPosition.squares[447],
                16 => loadedPosition.squares[448],
                17 => loadedPosition.squares[449],
                18 => loadedPosition.squares[450],
                19 => loadedPosition.squares[451],
                20 => loadedPosition.squares[452],
                21 => loadedPosition.squares[453],
                22 => loadedPosition.squares[454],
                23 => loadedPosition.squares[455],
                _ => 0,
            },
            19 => xy.x switch
            {
                0 => loadedPosition.squares[456],
                1 => loadedPosition.squares[457],
                2 => loadedPosition.squares[458],
                3 => loadedPosition.squares[459],
                4 => loadedPosition.squares[460],
                5 => loadedPosition.squares[461],
                6 => loadedPosition.squares[462],
                7 => loadedPosition.squares[463],
                8 => loadedPosition.squares[464],
                9 => loadedPosition.squares[465],
                10 => loadedPosition.squares[466],
                11 => loadedPosition.squares[467],
                12 => loadedPosition.squares[468],
                13 => loadedPosition.squares[469],
                14 => loadedPosition.squares[470],
                15 => loadedPosition.squares[471],
                16 => loadedPosition.squares[472],
                17 => loadedPosition.squares[473],
                18 => loadedPosition.squares[474],
                19 => loadedPosition.squares[475],
                20 => loadedPosition.squares[476],
                21 => loadedPosition.squares[477],
                22 => loadedPosition.squares[478],
                23 => loadedPosition.squares[479],
                _ => 0,
            },
            20 => xy.x switch
            {
                0 => loadedPosition.squares[480],
                1 => loadedPosition.squares[481],
                2 => loadedPosition.squares[482],
                3 => loadedPosition.squares[483],
                4 => loadedPosition.squares[484],
                5 => loadedPosition.squares[485],
                6 => loadedPosition.squares[486],
                7 => loadedPosition.squares[487],
                8 => loadedPosition.squares[488],
                9 => loadedPosition.squares[489],
                10 => loadedPosition.squares[490],
                11 => loadedPosition.squares[491],
                12 => loadedPosition.squares[492],
                13 => loadedPosition.squares[493],
                14 => loadedPosition.squares[494],
                15 => loadedPosition.squares[495],
                16 => loadedPosition.squares[496],
                17 => loadedPosition.squares[497],
                18 => loadedPosition.squares[498],
                19 => loadedPosition.squares[499],
                20 => loadedPosition.squares[500],
                21 => loadedPosition.squares[501],
                22 => loadedPosition.squares[502],
                23 => loadedPosition.squares[503],
                _ => 0,
            },
            21 => xy.x switch
            {
                0 => loadedPosition.squares[504],
                1 => loadedPosition.squares[505],
                2 => loadedPosition.squares[506],
                3 => loadedPosition.squares[507],
                4 => loadedPosition.squares[508],
                5 => loadedPosition.squares[509],
                6 => loadedPosition.squares[510],
                7 => loadedPosition.squares[511],
                8 => loadedPosition.squares[512],
                9 => loadedPosition.squares[513],
                10 => loadedPosition.squares[514],
                11 => loadedPosition.squares[515],
                12 => loadedPosition.squares[516],
                13 => loadedPosition.squares[517],
                14 => loadedPosition.squares[518],
                15 => loadedPosition.squares[519],
                16 => loadedPosition.squares[520],
                17 => loadedPosition.squares[521],
                18 => loadedPosition.squares[522],
                19 => loadedPosition.squares[523],
                20 => loadedPosition.squares[524],
                21 => loadedPosition.squares[525],
                22 => loadedPosition.squares[526],
                23 => loadedPosition.squares[527],
                _ => 0,
            },
            22 => xy.x switch
            {
                0 => loadedPosition.squares[528],
                1 => loadedPosition.squares[529],
                2 => loadedPosition.squares[530],
                3 => loadedPosition.squares[531],
                4 => loadedPosition.squares[532],
                5 => loadedPosition.squares[533],
                6 => loadedPosition.squares[534],
                7 => loadedPosition.squares[535],
                8 => loadedPosition.squares[536],
                9 => loadedPosition.squares[537],
                10 => loadedPosition.squares[538],
                11 => loadedPosition.squares[539],
                12 => loadedPosition.squares[540],
                13 => loadedPosition.squares[541],
                14 => loadedPosition.squares[542],
                15 => loadedPosition.squares[543],
                16 => loadedPosition.squares[544],
                17 => loadedPosition.squares[545],
                18 => loadedPosition.squares[546],
                19 => loadedPosition.squares[547],
                20 => loadedPosition.squares[548],
                21 => loadedPosition.squares[549],
                22 => loadedPosition.squares[550],
                23 => loadedPosition.squares[551],
                _ => 0,
            },
            23 => xy.x switch
            {
                0 => loadedPosition.squares[552],
                1 => loadedPosition.squares[553],
                2 => loadedPosition.squares[554],
                3 => loadedPosition.squares[555],
                4 => loadedPosition.squares[556],
                5 => loadedPosition.squares[557],
                6 => loadedPosition.squares[558],
                7 => loadedPosition.squares[559],
                8 => loadedPosition.squares[560],
                9 => loadedPosition.squares[561],
                10 => loadedPosition.squares[562],
                11 => loadedPosition.squares[563],
                12 => loadedPosition.squares[564],
                13 => loadedPosition.squares[565],
                14 => loadedPosition.squares[566],
                15 => loadedPosition.squares[567],
                16 => loadedPosition.squares[568],
                17 => loadedPosition.squares[569],
                18 => loadedPosition.squares[570],
                19 => loadedPosition.squares[571],
                20 => loadedPosition.squares[572],
                21 => loadedPosition.squares[573],
                22 => loadedPosition.squares[574],
                23 => loadedPosition.squares[575],
                _ => 0,
            },
            _ => 0,
        };
    }

    public void getPieceSprites()
    {
        SpriteRenderer sprRenderer = gameObject.AddComponent<SpriteRenderer>();
        if (color)
        {
            switch (pieceType)
            {
                case 0:
                    return;
                case 1:
                    sprRenderer.sprite = Resources.Load<Sprite>("pawn 1");
                    return;
                case 2:
                    sprRenderer.sprite = Resources.Load<Sprite>("rook 1");
                    return;
                case 3:
                    sprRenderer.sprite = Resources.Load<Sprite>("knight 1");
                    return;
                case 4:
                    sprRenderer.sprite = Resources.Load<Sprite>("bishop 1");
                    return;
                case 5:
                    sprRenderer.sprite = Resources.Load<Sprite>("king 1");
                    return;
                case 6:
                    sprRenderer.sprite = Resources.Load<Sprite>("queen 1");
                    return;
                case 7:
                    sprRenderer.sprite = Resources.Load<Sprite>("amazon 1");
                    return;
                case 8:
                    sprRenderer.sprite = Resources.Load<Sprite>("wizard 1");
                    return;
                case 9:
                    sprRenderer.sprite = Resources.Load<Sprite>("champion 1");
                    return;
                case 10:
                    sprRenderer.sprite = Resources.Load<Sprite>("archbishop 1");
                    return;
                case 11:
                    sprRenderer.sprite = Resources.Load<Sprite>("chancellor 1");
                    return;
                case 12:
                    sprRenderer.sprite = Resources.Load<Sprite>("falcon 1");
                    return;
                case 13:
                    sprRenderer.sprite = Resources.Load<Sprite>("hunter 1");
                    return;
                case 14:
                    sprRenderer.sprite = Resources.Load<Sprite>("dragon_horse 1");
                    return;
                case 15:
                    sprRenderer.sprite = Resources.Load<Sprite>("dragon_king 1");
                    return;
                case 16:
                    sprRenderer.sprite = Resources.Load<Sprite>("gold_general 1");
                    return;
                case 17:
                    sprRenderer.sprite = Resources.Load<Sprite>("silver_general 1");
                    return;
                case 18:
                    sprRenderer.sprite = Resources.Load<Sprite>("lance 1");
                    return;
                case 19:
                    sprRenderer.sprite = Resources.Load<Sprite>("ultima_pawn 1");
                    return;
                case 20:
                    sprRenderer.sprite = Resources.Load<Sprite>("long_leaper 1");
                    return;
                case 21:
                    sprRenderer.sprite = Resources.Load<Sprite>("cannon 1");
                    return;
                case 22:
                    sprRenderer.sprite = Resources.Load<Sprite>("guard 1");
                    return;
                case 23:
                    sprRenderer.sprite = Resources.Load<Sprite>("mao 1");
                    return;
                case 24:
                    sprRenderer.sprite = Resources.Load<Sprite>("elephant 1");
                    return;
            }
        }
        else
        {
            switch (pieceType)
            {
                case 0:
                    return;
                case 1:
                    sprRenderer.sprite = Resources.Load<Sprite>("pawn");
                    return;
                case 2:
                    sprRenderer.sprite = Resources.Load<Sprite>("rook");
                    return;
                case 3:
                    sprRenderer.sprite = Resources.Load<Sprite>("knight");
                    return;
                case 4:
                    sprRenderer.sprite = Resources.Load<Sprite>("bishop");
                    return;
                case 5:
                    sprRenderer.sprite = Resources.Load<Sprite>("king");
                    return;
                case 6:
                    sprRenderer.sprite = Resources.Load<Sprite>("queen");
                    return;
                case 7:
                    sprRenderer.sprite = Resources.Load<Sprite>("amazon");
                    return;
                case 8:
                    sprRenderer.sprite = Resources.Load<Sprite>("wizard");
                    return;
                case 9:
                    sprRenderer.sprite = Resources.Load<Sprite>("champion");
                    return;
                case 10:
                    sprRenderer.sprite = Resources.Load<Sprite>("archbishop");
                    return;
                case 11:
                    sprRenderer.sprite = Resources.Load<Sprite>("chancellor");
                    return;
                case 12:
                    sprRenderer.sprite = Resources.Load<Sprite>("falcon");
                    return;
                case 13:
                    sprRenderer.sprite = Resources.Load<Sprite>("hunter");
                    return;
                case 14:
                    sprRenderer.sprite = Resources.Load<Sprite>("dragon_horse");
                    return;
                case 15:
                    sprRenderer.sprite = Resources.Load<Sprite>("dragon_king");
                    return;
                case 16:
                    sprRenderer.sprite = Resources.Load<Sprite>("gold_general");
                    return;
                case 17:
                    sprRenderer.sprite = Resources.Load<Sprite>("silver_general");
                    return;
                case 18:
                    sprRenderer.sprite = Resources.Load<Sprite>("lance");
                    return;
                case 19:
                    sprRenderer.sprite = Resources.Load<Sprite>("ultima_pawn");
                    return;
                case 20:
                    sprRenderer.sprite = Resources.Load<Sprite>("long_leaper");
                    return;
                case 21:
                    sprRenderer.sprite = Resources.Load<Sprite>("cannon");
                    return;
                case 22:
                    sprRenderer.sprite = Resources.Load<Sprite>("guard");
                    return;
                case 23:
                    sprRenderer.sprite = Resources.Load<Sprite>("mao");
                    return;
                case 24:
                    sprRenderer.sprite = Resources.Load<Sprite>("elephant");
                    return;
            }
        }
    }

    public Sprite getPieceSprite(int pieceTypeSprite, bool colorSprite, SpriteRenderer sprRenderer)
    {
        if (colorSprite)
        {
            return pieceTypeSprite switch
            {
                0 => Resources.Load<Sprite>("none"),
                1 => Resources.Load<Sprite>("pawn 1"),
                2 => Resources.Load<Sprite>("rook 1"),
                3 => Resources.Load<Sprite>("knight 1"),
                4 => Resources.Load<Sprite>("bishop 1"),
                5 => Resources.Load<Sprite>("king 1"),
                6 => Resources.Load<Sprite>("queen 1"),
                7 => Resources.Load<Sprite>("amazon 1"),
                8 => Resources.Load<Sprite>("wizard 1"),
                9 => Resources.Load<Sprite>("champion 1"),
                10 => Resources.Load<Sprite>("archbishop 1"),
                11 => Resources.Load<Sprite>("chancellor 1"),
                12 => Resources.Load<Sprite>("falcon 1"),
                13 => Resources.Load<Sprite>("hunter 1"),
                14 => Resources.Load<Sprite>("dragon_horse 1"),
                15 => Resources.Load<Sprite>("dragon_king 1"),
                16 => Resources.Load<Sprite>("gold_general 1"),
                17 => Resources.Load<Sprite>("silver_general 1"),
                18 => Resources.Load<Sprite>("lance 1"),
                19 => Resources.Load<Sprite>("ultima_pawn 1"),
                20 => Resources.Load<Sprite>("long_leaper 1"),
                21 => Resources.Load<Sprite>("cannon 1"),
                22 => Resources.Load<Sprite>("guard 1"),
                23 => Resources.Load<Sprite>("mao 1"),
                24 => Resources.Load<Sprite>("elephant 1"),
                _ => Resources.Load<Sprite>("none"),
            };
        }
        else
        {
            return pieceTypeSprite switch
            {
                0 => Resources.Load<Sprite>("none"),
                1 => Resources.Load<Sprite>("pawn"),
                2 => Resources.Load<Sprite>("rook"),
                3 => Resources.Load<Sprite>("knight"),
                4 => Resources.Load<Sprite>("bishop"),
                5 => Resources.Load<Sprite>("king"),
                6 => Resources.Load<Sprite>("queen"),
                7 => Resources.Load<Sprite>("amazon"),
                8 => Resources.Load<Sprite>("wizard"),
                9 => Resources.Load<Sprite>("champion"),
                10 => Resources.Load<Sprite>("archbishop"),
                11 => Resources.Load<Sprite>("chancellor"),
                12 => Resources.Load<Sprite>("falcon"),
                13 => Resources.Load<Sprite>("hunter"),
                14 => Resources.Load<Sprite>("dragon_horse"),
                15 => Resources.Load<Sprite>("dragon_king"),
                16 => Resources.Load<Sprite>("gold_general"),
                17 => Resources.Load<Sprite>("silver_general"),
                18 => Resources.Load<Sprite>("lance"),
                19 => Resources.Load<Sprite>("ultima_pawn"),
                20 => Resources.Load<Sprite>("long_leaper"),
                21 => Resources.Load<Sprite>("cannon"),
                22 => Resources.Load<Sprite>("guard"),
                23 => Resources.Load<Sprite>("mao"),
                24 => Resources.Load<Sprite>("elephant"),
                _ => Resources.Load<Sprite>("none"),
            };
        }
    }

    public List<Vector2Int> GenerateMoves(Piece[] board)
    {
        List<Vector2Int> moves = new List<Vector2Int>();
        if (pieceType == 1) //pawn
        {
            foreach (Vector2Int move in PawnMoves(board))
            {
                moves.Add(move);
            }
        }
        else if (pieceType == 2) //rook
        {
            foreach (Vector2Int move in RookMoves(board))
            {
                moves.Add(move);
            }
        }
        else if (pieceType == 3) //knight
        {
            foreach (Vector2Int move in KnightMoves(board))
            {
                moves.Add(move);
            }
        }
        else if (pieceType == 4) //bishop
        {
            foreach (Vector2Int move in BishopMoves(board))
            {
                moves.Add(move);
            }
        }
        else if (pieceType == 5) //king
        {
            foreach (Vector2Int move in KingMoves(board))
            {
                moves.Add(move);
            }
        }
        else if (pieceType == 6) //queen
        {
            foreach (Vector2Int move in BishopMoves(board))
            {
                moves.Add(move);
            }
            foreach (Vector2Int move in RookMoves(board))
            {
                moves.Add(move);
            }
        }
        else if (pieceType == 7) //amazon
        {
            foreach (Vector2Int move in BishopMoves(board))
            {
                moves.Add(move);
            }
            foreach (Vector2Int move in RookMoves(board))
            {
                moves.Add(move);
            }
            foreach (Vector2Int move in KnightMoves(board))
            {
                moves.Add(move);
            }
        }
        else if (pieceType == 8) //wizard
        {
            foreach (Vector2Int move in KnightMoves(board))
            {
                moves.Add(move);
            }
            foreach (Vector2Int move in WizardMoves(board))
            {
                moves.Add(move);
            }
        }
        else if (pieceType == 9) //Champion
        {
            foreach (Vector2Int move in ChampionMoves(board))
            {
                moves.Add(move);
            }
        }
        else if (pieceType == 10) //Archbishop
        {
            foreach (Vector2Int move in KnightMoves(board))
            {
                moves.Add(move);
            }
            foreach (Vector2Int move in BishopMoves(board))
            {
                moves.Add(move);
            }
        }
        else if (pieceType == 11) //Chancellor
        {
            foreach (Vector2Int move in KnightMoves(board))
            {
                moves.Add(move);
            }
            foreach (Vector2Int move in RookMoves(board))
            {
                moves.Add(move);
            }
        }
        else if (pieceType == 12) //Falcon
        {
            foreach (Vector2Int move in FalconMoves(board))
            {
                moves.Add(move);
            }
        }
        else if (pieceType == 13) //Hunter
        {
            foreach (Vector2Int move in HunterMoves(board))
            {
                moves.Add(move);
            }
        }
        else if (pieceType == 14) //Dragon Horse
        {
            foreach (Vector2Int move in SingleRookMoves(board))
            {
                moves.Add(move);
            }
            foreach (Vector2Int move in BishopMoves(board))
            {
                moves.Add(move);
            }
        }
        else if (pieceType == 15) //Dragon King
        {
            foreach (Vector2Int move in SingleBishopMoves(board))
            {
                moves.Add(move);
            }
            foreach (Vector2Int move in RookMoves(board))
            {
                moves.Add(move);
            }
        }
        else if (pieceType == 16) //Gold General
        {
            foreach (Vector2Int move in SingleRookMoves(board))
            {
                moves.Add(move);
            }
            foreach (Vector2Int move in GoldMoves(board))
            {
                moves.Add(move);
            }
        }
        else if (pieceType == 17) //Silver General
        {
            foreach (Vector2Int move in SingleBishopMoves(board))
            {
                moves.Add(move);
            }
            foreach (Vector2Int move in SilverMoves(board))
            {
                moves.Add(move);
            }
        }
        else if (pieceType == 18) // Lance
        {
            foreach (Vector2Int move in LanceMoves(board))
            {
                moves.Add(move);
            }
        }
        else if (pieceType == 19) //Ultima Pawn
        {
            foreach (Vector2Int move in SingleRookMoves(board))
            {
                moves.Add(move);
            }
        }
        else if (pieceType == 20) // Long Leaper
        {
            foreach (Vector2Int move in BishopMoves(board))
            {
                moves.Add(move);
            }
            foreach (Vector2Int move in LongMoves(board))
            {
                moves.Add(move);
            }
            foreach (Vector2Int move in SingleLongMoves(board))
            {
                moves.Add(move);
            }
        }
        else if (pieceType == 21) //Cannon
        {
            foreach (Vector2Int move in LongMoves(board))
            {
                moves.Add(move);
            }
            foreach (Vector2Int move in CannonMoves(board))
            {
                moves.Add(move);
            }
        }
        else if (pieceType == 22) //Guard
        {
            foreach (Vector2Int move in SingleBishopMoves(board))
            {
                moves.Add(move);
            }
        }
        else if (pieceType == 23) //Mao
        {
            foreach (Vector2Int move in SingleBishopMoves(board))
            {
                moves.Add(move);
            }
            foreach (Vector2Int move in SingleRookMoves(board))
            {
                moves.Add(move);
            }
            foreach (Vector2Int move in KnightMoves(board))
            {
                moves.Add(move);
            }
        }
        else if (pieceType == 24) //Elephant
        {
            foreach (Vector2Int move in ElephantMoves(board))
            {
                moves.Add(move);
            }
        }
        return moves;
    }

    List<Vector2Int> ElephantMoves(Piece[] board)
    {
        if (board[boardPosition.y * 24 + boardPosition.x].color) {
            Vector2Int[] offsets = new Vector2Int[]{new Vector2Int(1, 2), new Vector2Int(-1, 2),
            new Vector2Int(1, -2), new Vector2Int(-1, -2)};
            List<Vector2Int> moves = new List<Vector2Int>();
            foreach (Vector2Int offset in offsets)
            {
                Vector2Int index = boardPosition + offset;
                if (index.x > 23 || index.y > 23 || index.x < 0 || index.y < 0)
                {
                    continue;
                }
                else if (board[index.y * 24 + index.x] != null && board[index.y * 24 + index.x].pieceType != 0 && board[index.y * 24 + index.x].color != IsColour(24, White))
                {
                    continue;
                }
                moves.Add(index);
            }
            return moves;
        } else
        {
            Vector2Int[] offsets = new Vector2Int[]{new Vector2Int(1, 2), new Vector2Int(-1, 2),
            new Vector2Int(1, -2), new Vector2Int(-1, -2)};
            List<Vector2Int> moves = new List<Vector2Int>();
            foreach (Vector2Int offset in offsets)
            {
                Vector2Int index = boardPosition + offset;
                if (index.x > 23 || index.y > 23 || index.x < 0 || index.y < 0)
                {
                    continue;
                }
                else if (board[index.y * 24 + index.x] != null && board[index.y * 24 + index.x].pieceType != 0 && board[index.y * 24 + index.x].color == IsColour(24, Black))
                {
                    continue;
                }
                moves.Add(index);
            }
            return moves;
        }
    }

    List<Vector2Int> CannonMoves(Piece[] board)
    {
        if (board[boardPosition.y * 24 + boardPosition.x].color)
        {
            Vector2Int[] offsets = new Vector2Int[] { new Vector2Int(0, 1), new Vector2Int(0, 3) };
            List<Vector2Int> moves = new List<Vector2Int>();
            foreach (Vector2Int offset in offsets)
            {
                Vector2Int index = boardPosition + offset;
                if (index.x > 23 || index.y > 23 || index.x < 0 || index.y < 0)
                {
                    continue;
                }
                else if (board[index.y * 24 + index.x] != null && board[index.y * 24 + index.x].pieceType != 0 && board[index.y * 24 + index.x].color != IsColour(21, White))
                {
                    continue;
                }
                moves.Add(index);
            }
            return moves;
        }
        else
        {
            Vector2Int[] offsets = new Vector2Int[] { new Vector2Int(0, -1), new Vector2Int(0, -3) };
            List<Vector2Int> moves = new List<Vector2Int>();
            foreach (Vector2Int offset in offsets)
            {
                Vector2Int index = boardPosition + offset;
                if (index.x > 23 || index.y > 23 || index.x < 0 || index.y < 0)
                {
                    continue;
                }
                else if (board[index.y * 24 + index.x] != null && board[index.y * 24 + index.x].pieceType != 0 && board[index.y * 24 + index.x].color != IsColour(21, Black))
                {
                    continue;
                }
                moves.Add(index);
            }
            return moves;
        }
    }

    List<Vector2Int> SingleLongMoves(Piece[] board)
    {
        if (board[boardPosition.y * 24 + boardPosition.x].color)
        {
            Vector2Int[] offsets = new Vector2Int[] { new Vector2Int(0, 1), new Vector2Int(0, 2) };
            List<Vector2Int> moves = new List<Vector2Int>();
            foreach (Vector2Int offset in offsets)
            {
                Vector2Int index = boardPosition + offset;
                if (index.x > 23 || index.y > 23 || index.x < 0 || index.y < 0)
                {
                    continue;
                }
                else if (board[index.y * 24 + index.x] != null && board[index.y * 24 + index.x].pieceType != 0 && board[index.y * 24 + index.x].color != IsColour(20, White))
                {
                    continue;
                }
                moves.Add(index);
            }
            return moves;
        } else
        {
            Vector2Int[] offsets = new Vector2Int[] { new Vector2Int(0, -1), new Vector2Int(0, -2) };
            List<Vector2Int> moves = new List<Vector2Int>();
            foreach (Vector2Int offset in offsets)
            {
                Vector2Int index = boardPosition + offset;
                if (index.x > 23 || index.y > 23 || index.x < 0 || index.y < 0)
                {
                    continue;
                }
                else if (board[index.y * 24 + index.x] != null && board[index.y * 24 + index.x].pieceType != 0 && board[index.y * 24 + index.x].color != IsColour(20, Black))
                {
                    continue;
                }
                moves.Add(index);
            }
            return moves;
        }
    }

    List<Vector2Int> LongMoves(Piece[] board)
    {
        if (board[boardPosition.y * 24 + boardPosition.x].color)
        {
            Vector2Int[] offsets = new Vector2Int[]{new Vector2Int(0, -1),
            new Vector2Int(1, 0), new Vector2Int(-1, 0)};
            List<Vector2Int> moves = new List<Vector2Int>();
            foreach (Vector2Int offset in offsets)
            {
                for (int i = 1; i < 24; i++)
                {
                    Vector2Int index = boardPosition + offset * i;
                    if (index.x > 23 || index.y > 23 || index.x < 0 || index.y < 0)
                    {
                        break;
                    }
                    else if (board[index.y * 24 + index.x] != null)
                    {
                        if (board[index.y * 24 + index.x].pieceType == 0 || board[index.y * 24 + index.x].color != IsColour(20, Black))
                        {
                            if (board[index.y * 24 + index.x].pieceType != 0)
                            {
                                moves.Add(index);
                                break;
                            }
                            else
                            {
                                moves.Add(index);
                            }
                        } else
                        {
                            break;
                        }
                    }
                    moves.Add(index);
                }
            }
            return moves;
        }
        else
        {
            Vector2Int[] offsets = new Vector2Int[]{new Vector2Int(0, 1),
            new Vector2Int(1, 0), new Vector2Int(-1, 0)};
            List<Vector2Int> moves = new List<Vector2Int>();
            foreach (Vector2Int offset in offsets)
            {
                for (int i = 1; i < 24; i++)
                {
                    Vector2Int index = boardPosition + offset * i;
                    if (index.x > 23 || index.y > 23 || index.x < 0 || index.y < 0)
                    {
                        break;
                    }
                    else if (board[index.y * 24 + index.x] != null)
                    {
                        if (board[index.y * 24 + index.x].pieceType == 0 || board[index.y * 24 + index.x].color != IsColour(20, White))
                        {
                            if (board[index.y * 24 + index.x].pieceType != 0)
                            {
                                moves.Add(index);
                                break;
                            }
                            else
                            {
                                moves.Add(index);
                            }
                        } else
                        {
                            break;
                        }
                    }
                    moves.Add(index);
                }
            }
            return moves;
        }
    }

    public void PromotionPieceType()
    {
        MovePlays mp = new MovePlays();
        GameManager gm = GameManager.current;
        if (!gm.isAiVsMode)
        {
            if (!gm.isPlayerIsBlackMode)
            {
                if (gm.isPlayerAI)
                {
                    if (!gm.isPlayerWhite)
                    {
                        switch (mp.MoveFlagAI())
                        {
                            case MovePlays.Flag.PromoteToRook:
                                gm.promotePawnToRookAI();
                                break;
                            case MovePlays.Flag.PromoteToKnight:
                                gm.promotePawnToKnightAI();
                                break;
                            case MovePlays.Flag.PromoteToBishop:
                                gm.promotePawnToBishopAI();
                                break;
                            case MovePlays.Flag.PromoteToQueen:
                                gm.promotePawnToQueenAI();
                                break;
                        }
                    }
                    else
                    {
                        switch (mp.MoveFlagPlayer())
                        {
                            case MovePlays.Flag.PromoteToRook:
                                gm.promotePawnToRook();
                                break;
                            case MovePlays.Flag.PromoteToKnight:
                                gm.promotePawnToKnight();
                                break;
                            case MovePlays.Flag.PromoteToBishop:
                                gm.promotePawnToBishop();
                                break;
                            case MovePlays.Flag.PromoteToQueen:
                                gm.promotePawnToQueen();
                                break;
                        }
                    }
                }
                else
                {

                    switch (mp.MoveFlagPlayer())
                    {
                        case MovePlays.Flag.PromoteToRook:
                            gm.promotePawnToRook();
                            break;
                        case MovePlays.Flag.PromoteToKnight:
                            gm.promotePawnToKnight();
                            break;
                        case MovePlays.Flag.PromoteToBishop:
                            gm.promotePawnToBishop();
                            break;
                        case MovePlays.Flag.PromoteToQueen:
                            gm.promotePawnToQueen();
                            break;
                    }
                }
            } else
            {
                if (gm.isPlayerAI)
                {
                    if (gm.isPlayerWhite)
                    {
                        switch (mp.MoveFlagAI())
                        {
                            case MovePlays.Flag.PromoteToRook:
                                gm.promotePawnToRookAI();
                                break;
                            case MovePlays.Flag.PromoteToKnight:
                                gm.promotePawnToKnightAI();
                                break;
                            case MovePlays.Flag.PromoteToBishop:
                                gm.promotePawnToBishopAI();
                                break;
                            case MovePlays.Flag.PromoteToQueen:
                                gm.promotePawnToQueenAI();
                                break;

                        }
                    }
                    else
                    {
                        switch (mp.MoveFlagPlayer())
                        {
                            case MovePlays.Flag.PromoteToRook:
                                gm.promotePawnToRook();
                                break;
                            case MovePlays.Flag.PromoteToKnight:
                                gm.promotePawnToKnight();
                                break;
                            case MovePlays.Flag.PromoteToBishop:
                                gm.promotePawnToBishop();
                                break;
                            case MovePlays.Flag.PromoteToQueen:
                                gm.promotePawnToQueen();
                                break;
                        }
                    }
                }
                else
                {

                    switch (mp.MoveFlagPlayer())
                    {
                        case MovePlays.Flag.PromoteToRook:
                            gm.promotePawnToRook();
                            break;
                        case MovePlays.Flag.PromoteToKnight:
                            gm.promotePawnToKnight();
                            break;
                        case MovePlays.Flag.PromoteToBishop:
                            gm.promotePawnToBishop();
                            break;
                        case MovePlays.Flag.PromoteToQueen:
                            gm.promotePawnToQueen();
                            break;
                    }
                }
            }
        } else
        {
            if (gm.isPlayerAI)
            {
                switch (mp.MoveFlagAI())
                {
                    case MovePlays.Flag.PromoteToRook:
                        gm.promotePawnToRookAI();
                        break;
                    case MovePlays.Flag.PromoteToKnight:
                        gm.promotePawnToKnightAI();
                        break;
                    case MovePlays.Flag.PromoteToBishop:
                        gm.promotePawnToBishopAI();
                        break;
                    case MovePlays.Flag.PromoteToQueen:
                        gm.promotePawnToQueenAI();
                        break;
                }
            }
            else
            {
                switch (mp.MoveFlagAI())
                {
                    case MovePlays.Flag.PromoteToRook:
                        gm.promotePawnToRookAI();
                        break;
                    case MovePlays.Flag.PromoteToKnight:
                        gm.promotePawnToKnightAI();
                        break;
                    case MovePlays.Flag.PromoteToBishop:
                        gm.promotePawnToBishopAI();
                        break;
                    case MovePlays.Flag.PromoteToQueen:
                        gm.promotePawnToQueenAI();
                        break;
                }
            }
        }
    }

    List<Vector2Int> PawnMoves(Piece[] board)
    { if (board[boardPosition.y * 24 + boardPosition.x].color)
        {
            Vector2Int[] offsets = new Vector2Int[] { new Vector2Int(-1, 0), new Vector2Int(1, 0) };
            List<Vector2Int> moves = new List<Vector2Int>();
            if (boardPosition.y == 23 || boardPosition.y == 0)
            {
                PromotionPieceType();
                return moves;
            }
            int colorType = 1;
            Vector2Int pawnMove = new Vector2Int(0, 1);
            Vector2Int[] Moons = new Vector2Int[] { new Vector2Int(0, 1) };
            Vector2Int index;
            Vector2Int index2;
            Vector2Int index3;
            foreach (Vector2Int offset in offsets)
            {
                index = boardPosition + pawnMove * colorType + offset;
                index2 = boardPosition + pawnMove * colorType;
                if (index.x > 23 || index.y > 23 || index.x < 0 || index.y < 0)
                {
                    continue;
                }

                if (index2.x > 23 || index2.y > 23 || index2.x < 0 || index2.y < 0)
                {
                    continue;
                }

                if (boardPosition.x != 0 && board[(index.y * 24) + boardPosition.x - 1] != null)
                {
                    if (boardPosition.x - 1 <= 23 && boardPosition.x - 1 >= 0)
                    {
                        if (board[(index.y * 24) + boardPosition.x - 1].color != this.color)
                        {
                            if (board[(index.y * 24) + boardPosition.x - 1].pieceType != 0)
                            {
                                moves.Add(index2 + new Vector2Int(-1, 0));
                            }
                        }
                    }
                }

                if (boardPosition.x != 23 && board[(index.y * 24) + boardPosition.x + 1] != null)
                {
                    if (boardPosition.x + 1 <= 23 && boardPosition.x + 1 >= 0)
                    {
                        if (board[(index.y * 24) + boardPosition.x + 1].color != this.color)
                        {
                            if (board[(index.y * 24) + boardPosition.x + 1].pieceType != 0)
                            {
                                moves.Add(index2 + new Vector2Int(1, 0));
                            }
                        }
                    }
                }   
            }

            foreach (Vector2Int Moon in Moons)
            {
                index = boardPosition + Moon * colorType;
                index3 = boardPosition + Moon * colorType;
                if (index.x > 23 || index.y > 23 || index.x < 0 || index.y < 0 || index3.x > 23 || index3.y > 23 || index3.x < 0 || index3.y < 0)
                {
                    continue;
                }
                else
                {
                    if (amountMoved == 0)
                    {
                        if (board[index3.y * 24 + index3.x] != null && board[index3.y * 24 + index3.x].pieceType != 0)
                        {
                            return moves;
                        }
                        else
                        {
                            moves.Add(index3);
                            index = boardPosition + 2 * pawnMove * colorType;
                            if (board[index.y * 24 + index.x] != null && board[index.y * 24 + index.x].pieceType != 0)
                            {
                                return moves;
                            } 
                            moves.Add(index);
                        }
                    }

                    if (board[index.y * 24 + index.x] != null && board[index.y * 24 + index.x].pieceType != 0)
                    {
                        return moves;
                    }
                    moves.Add(index);
                }
            }
            return moves;
        } else
        {
            Vector2Int[] offsets = new Vector2Int[] { new Vector2Int(-1, 0), new Vector2Int(1, 0) };
            List<Vector2Int> moves = new List<Vector2Int>();
            if (boardPosition.y == 23 || boardPosition.y == 0)
            {
                PromotionPieceType();
                return moves;
            }
            int colorType = -1;
            Vector2Int pawnMove = new Vector2Int(0, 1);
            Vector2Int[] Moons = new Vector2Int[] { new Vector2Int(0, 1) };
            Vector2Int index;
            Vector2Int index2;
            Vector2Int index3;
            foreach (Vector2Int offset in offsets)
            {
                GameManager gm = GameManager.current;
                index = boardPosition + pawnMove * colorType + offset;
                index2 = boardPosition + pawnMove * colorType;
                if (index.x > 23 || index.y > 23 || index.x < 0 || index.y < 0)
                {
                    continue;
                }

                if (index2.x > 23 || index2.y > 23 || index2.x < 0 || index2.y < 0)
                {
                    continue;
                }

                if (boardPosition.x != 0  && board[(index.y * 24) + boardPosition.x - 1] != null)
                {
                    if (boardPosition.x - 1 <= 23 && boardPosition.x - 1 >= 0)
                    {
                        if (board[(index.y * 24) + boardPosition.x - 1].color != IsColour(1, Black))
                        {
                            if (board[(index.y * 24) + boardPosition.x - 1].pieceType != 0)
                            {
                                moves.Add(index2 + new Vector2Int(-1, 0));
                            }
                        }
                    }
                }

                if (boardPosition.x != 23 && board[(index.y * 24) + boardPosition.x + 1] != null)
                {
                    if (boardPosition.x + 1 <= 23 && boardPosition.x + 1 >= 0)
                    {
                        if (board[(index.y * 24) + boardPosition.x + 1].color != IsColour(1, White))
                        {
                            if (board[(index.y * 24) + boardPosition.x + 1].pieceType != 0)
                            {
                                moves.Add(index2 + new Vector2Int(1, 0));
                            }
                        }
                    }
                }
            }

            foreach (Vector2Int Moon in Moons)
            {
                index = boardPosition + Moon * colorType;
                index3 = boardPosition + Moon * colorType;
                if (index.x > 23 || index.y > 23 || index.x < 0 || index.y < 0 || index3.x > 23 || index3.y > 23 || index3.x < 0 || index3.y < 0)
                {
                    continue;
                }
                else
                {
                    if (amountMoved == 0)
                    {
                        if (board[index3.y * 24 + index3.x] != null && board[index3.y * 24 + index3.x].pieceType != 0)
                        {
                            return moves;
                        }
                        else
                        {
                            moves.Add(index3);
                            index = boardPosition + 2 * pawnMove * colorType;
                            if (board[index.y * 24 + index.x] != null && board[index.y * 24 + index.x].pieceType != 0)
                            {
                                return moves;
                            }
                            moves.Add(index);  
                        }

                        if (board[index3.y * 24 + index3.x] != null && board[index3.y * 24 + index3.x].pieceType != 0)
                        {
                            return moves;
                        }
                        moves.Add(index3);
                    }

                    if (board[index.y * 24 + index.x] != null && board[index.y * 24 + index.x].pieceType != 0)
                    {
                        return moves;
                    }
                    moves.Add(index);
                }
            }
            return moves;
        }
    }

    List<Vector2Int> RookMoves(Piece[] board)
    {
        if (board[boardPosition.y * 24 + boardPosition.x].color)
        {
            Vector2Int[] offsets = new Vector2Int[]{new Vector2Int(0, 1), new Vector2Int(0, -1),
            new Vector2Int(1, 0), new Vector2Int(-1, 0)};
            List<Vector2Int> moves = new List<Vector2Int>();
            foreach (Vector2Int offset in offsets)
            {
                for (int i = 1; i < 24; i++)
                {
                    Vector2Int index = boardPosition + offset * i;
                    if (index.x > 23 || index.y > 23 || index.x < 0 || index.y < 0)
                    {
                        break;
                    }
                    else if (board[index.y * 24 + index.x] != null)
                    {
                        if (board[index.y * 24 + index.x].pieceType == 0 || board[index.y * 24 + index.x].color == IsColour(2, Black))
                        {
                            if (board[index.y * 24 + index.x].pieceType != 0)
                            {
                                moves.Add(index);
                                break;
                            } else
                            {
                                moves.Add(index);
                            }
                        } else
                        {
                            break;
                        }
                    }
                    moves.Add(index);
                }
            }
            return moves;
        } else
        {
            Vector2Int[] offsets = new Vector2Int[]{new Vector2Int(0, 1), new Vector2Int(0, -1),
            new Vector2Int(1, 0), new Vector2Int(-1, 0)};
            List<Vector2Int> moves = new List<Vector2Int>();
            foreach (Vector2Int offset in offsets)
            {
                for (int i = 1; i < 24; i++)
                {
                    Vector2Int index = boardPosition + offset * i;
                    if (index.x > 23 || index.y > 23 || index.x < 0 || index.y < 0)
                    {
                        break;
                    }
                    else if (board[index.y * 24 + index.x] != null)
                    {
                        if (board[index.y * 24 + index.x].pieceType == 0 || board[index.y * 24 + index.x].color != IsColour(2, White))
                        {
                            if (board[index.y * 24 + index.x].pieceType != 0)
                            {
                                moves.Add(index);
                                break;
                            }
                            else
                            {
                                moves.Add(index);
                            }
                        } else
                        {
                            break;
                        }
                    }
                    moves.Add(index);
                }
            }
            return moves;
        }
    }
    List<Vector2Int> SingleBishopMoves(Piece[] board)
    {
        if (board[boardPosition.y * 24 + boardPosition.x].color)
        {
            Vector2Int[] offsets = new Vector2Int[]{new Vector2Int(-1, 1), new Vector2Int(1, -1),
            new Vector2Int(1, 1), new Vector2Int(-1, -1) };
            List<Vector2Int> moves = new List<Vector2Int>();
            foreach (Vector2Int offset in offsets)
            {
                Vector2Int index = boardPosition + offset;
                if (index.x > 23 || index.y > 23 || index.x < 0 || index.y < 0)
                {
                    continue;
                }
                else if (board[index.y * 24 + index.x] != null && board[index.y * 24 + index.x].pieceType != 0 && board[index.y * 24 + index.x].color != IsColour(4, White))
                {
                    continue;
                }
                moves.Add(index);
            }
            return moves;
        } else
        {
            Vector2Int[] offsets = new Vector2Int[]{new Vector2Int(-1, 1), new Vector2Int(1, -1),
            new Vector2Int(1, 1), new Vector2Int(-1, -1) };
            List<Vector2Int> moves = new List<Vector2Int>();
            foreach (Vector2Int offset in offsets)
            {
                Vector2Int index = boardPosition + offset;
                if (index.x > 23 || index.y > 23 || index.x < 0 || index.y < 0)
                {
                    continue;
                }
                else if (board[index.y * 24 + index.x] != null && board[index.y * 24 + index.x].pieceType != 0 && board[index.y * 24 + index.x].color == IsColour(4, Black))
                {
                    continue;
                }
                moves.Add(index);
            }
            return moves;
        }
    }

    List<Vector2Int> SingleRookMoves(Piece[] board)
    {
        if (board[boardPosition.y * 24 + boardPosition.x].color)
        {
            Vector2Int[] offsets = new Vector2Int[]{new Vector2Int(-1, 0), new Vector2Int(0, -1),
            new Vector2Int(0, 1), new Vector2Int(1, 0) };
            List<Vector2Int> moves = new List<Vector2Int>();
            foreach (Vector2Int offset in offsets)
            {
                Vector2Int index = boardPosition + offset;
                if (index.x > 23 || index.y > 23 || index.x < 0 || index.y < 0)
                {
                    continue;
                }
                else if (board[index.y * 24 + index.x] != null && board[index.y * 24 + index.x].pieceType != 0 && board[index.y * 24 + index.x].color != IsColour(2, White))
                {
                    continue;
                }
                moves.Add(index);
            }
            return moves;
        }
        else
        {
            Vector2Int[] offsets = new Vector2Int[]{new Vector2Int(-1, 0), new Vector2Int(0, -1),
            new Vector2Int(0, 1), new Vector2Int(1, 0) };
            List<Vector2Int> moves = new List<Vector2Int>();
            foreach (Vector2Int offset in offsets)
            {
                Vector2Int index = boardPosition + offset;
                if (index.x > 23 || index.y > 23 || index.x < 0 || index.y < 0)
                {
                    continue;
                }
                else if (board[index.y * 24 + index.x] != null && board[index.y * 24 + index.x].pieceType != 0 && board[index.y * 24 + index.x].color == IsColour(2, Black))
                {
                    continue;
                }
                moves.Add(index);
            }
            return moves;
        }
    }

    List<Vector2Int> WizardMoves(Piece[] board)
    {
        if (board[boardPosition.y * 24 + boardPosition.x].color)
        {
            Vector2Int[] offsets = new Vector2Int[]{new Vector2Int(1, 5), new Vector2Int(-1, 5),
            new Vector2Int(5, 1), new Vector2Int(5, -1),
            new Vector2Int(-5, 1), new Vector2Int(-5, -1),
            new Vector2Int(1, -5), new Vector2Int(-1, -5)};
            List<Vector2Int> moves = new List<Vector2Int>();
            foreach (Vector2Int offset in offsets)
            {
                Vector2Int index = boardPosition + offset;
                if (index.x > 23 || index.y > 23 || index.x < 0 || index.y < 0)
                {
                    continue;
                }
                else if (board[index.y * 24 + index.x] != null && (board[index.y * 24 + index.x].pieceType != 0 || board[index.y * 24 + index.x].color != IsColour(8, White)))
                {
                    continue;
                }
                moves.Add(index);
            }
            return moves;
        }
        else
        {
            Vector2Int[] offsets = new Vector2Int[]{new Vector2Int(1, 5), new Vector2Int(-1, 5),
            new Vector2Int(5, 1), new Vector2Int(5, -1),
            new Vector2Int(-5, 1), new Vector2Int(-5, -1),
            new Vector2Int(1, -5), new Vector2Int(-1, -5)};
            List<Vector2Int> moves = new List<Vector2Int>();
            foreach (Vector2Int offset in offsets)
            {
                Vector2Int index = boardPosition + offset;
                if (index.x > 23 || index.y > 23 || index.x < 0 || index.y < 0)
                {
                    continue;
                }
                else if (board[index.y * 24 + index.x] != null && (board[index.y * 24 + index.x].pieceType != 0 || board[index.y * 24 + index.x].color != IsColour(8, Black)))
                {
                    continue;
                }
                moves.Add(index);
            }
            return moves;
        }
    }

    List<Vector2Int> ChampionMoves(Piece[] board)
    {
        if (board[boardPosition.y * 24 + boardPosition.x].color)
        {
            Vector2Int[] offsets = new Vector2Int[]{new Vector2Int(0, 1), new Vector2Int(0, 2),
            new Vector2Int(2, 0), new Vector2Int(1, 0),
            new Vector2Int(0, -1), new Vector2Int(0, -2),
            new Vector2Int(-1, 0), new Vector2Int(-2, 0),
            new Vector2Int(-2, 2), new Vector2Int(-2, -2),
            new Vector2Int(2, 2), new Vector2Int(2, -2)
        };
            List<Vector2Int> moves = new List<Vector2Int>();
            foreach (Vector2Int offset in offsets)
            {
                Vector2Int index = boardPosition + offset;
                if (index.x > 23 || index.y > 23 || index.x < 0 || index.y < 0)
                {
                    continue;
                }
                else if (board[index.y * 24 + index.x] != null && (board[index.y * 24 + index.x].pieceType != 0 || board[index.y * 24 + index.x].color != IsColour(9, White)))
                {
                    continue;
                }
                moves.Add(index);
            }
            return moves;
        } else
        {
            Vector2Int[] offsets = new Vector2Int[]{new Vector2Int(0, 1), new Vector2Int(0, 2),
            new Vector2Int(2, 0), new Vector2Int(1, 0),
            new Vector2Int(0, -1), new Vector2Int(0, -2),
            new Vector2Int(-1, 0), new Vector2Int(-2, 0),
            new Vector2Int(-2, 2), new Vector2Int(-2, -2),
            new Vector2Int(2, 2), new Vector2Int(2, -2)
        };
            List<Vector2Int> moves = new List<Vector2Int>();
            foreach (Vector2Int offset in offsets)
            {
                Vector2Int index = boardPosition + offset;
                if (index.x > 23 || index.y > 23 || index.x < 0 || index.y < 0)
                {
                    continue;
                }
                else if (board[index.y * 24 + index.x] != null && (board[index.y * 24 + index.x].pieceType != 0 || board[index.y * 24 + index.x].color != IsColour(9, White)))
                {
                    continue;
                }
                moves.Add(index);
            }
            return moves;
        }
    }

    List<Vector2Int> SilverMoves(Piece[] board)
    {
        if (board[boardPosition.y * 24 + boardPosition.x].color)
        {
            Vector2Int[] offsets = new Vector2Int[] { new Vector2Int(0, 1) };
            List<Vector2Int> moves = new List<Vector2Int>();
            foreach (Vector2Int offset in offsets)
            {
                Vector2Int index = boardPosition + offset;
                if (index.x > 23 || index.y > 23 || index.x < 0 || index.y < 0)
                {
                    continue;
                }
                else if (board[index.y * 24 + index.x] != null && board[index.y * 24 + index.x].pieceType != 0 && board[index.y * 24 + index.x].color != IsColour(17, White))
                {
                    continue;
                }
                moves.Add(index);
            }
            return moves;
        } else
        {
            Vector2Int[] offsets = new Vector2Int[] { new Vector2Int(0, -1) };
            List<Vector2Int> moves = new List<Vector2Int>();
            foreach (Vector2Int offset in offsets)
            {
                Vector2Int index = boardPosition + offset;
                if (index.x > 23 || index.y > 23 || index.x < 0 || index.y < 0)
                {
                    continue;
                }
                else if (board[index.y * 24 + index.x] != null && board[index.y * 24 + index.x].pieceType != 0 && board[index.y * 24 + index.x].color != IsColour(17, Black))
                {
                    continue;
                }
                moves.Add(index);
            }
            return moves;
        }
    }

    List<Vector2Int> GoldMoves(Piece[] board)
    {
        if (board[boardPosition.y * 24 + boardPosition.x].color)
        {
            Vector2Int[] offsets = new Vector2Int[] { new Vector2Int(1, 1), new Vector2Int(-1, 1) };
            List<Vector2Int> moves = new List<Vector2Int>();
            foreach (Vector2Int offset in offsets)
            {
                Vector2Int index = boardPosition + offset;
                if (index.x > 23 || index.y > 23 || index.x < 0 || index.y < 0)
                {
                    continue;
                }
                else if (board[index.y * 24 + index.x] != null && board[index.y * 24 + index.x].pieceType != 0 && board[index.y * 24 + index.x].color != IsColour(16, White))
                {
                    continue;
                }
                moves.Add(index);
            }
            return moves;
        } else
        {
            Vector2Int[] offsets = new Vector2Int[] { new Vector2Int(-1,-1), new Vector2Int(1, -1) };
            List<Vector2Int> moves = new List<Vector2Int>();
            foreach (Vector2Int offset in offsets)
            {
                Vector2Int index = boardPosition + offset;
                if (index.x > 23 || index.y > 23 || index.x < 0 || index.y < 0)
                {
                    continue;
                }
                else if (board[index.y * 24 + index.x] != null && board[index.y * 24 + index.x].pieceType != 0 && board[index.y * 24 + index.x].color != IsColour(16, Black))
                {
                    continue;
                }
                moves.Add(index);
            }
            return moves;
        }
    }

    List<Vector2Int> LanceMoves(Piece[] board)
    {
        if (board[boardPosition.y * 24 + boardPosition.x].color)
        {
            Vector2Int[] offsets = new Vector2Int[]{new Vector2Int(0, 1), new Vector2Int(0, 2),
            new Vector2Int(0, 3), new Vector2Int(0, 4)};
            List<Vector2Int> moves = new List<Vector2Int>();
            foreach (Vector2Int offset in offsets)
            {
                Vector2Int index = boardPosition + offset;
                if (index.x > 23 || index.y > 23 || index.x < 0 || index.y < 0)
                {
                    continue;
                }
                else if (board[index.y * 24 + index.x] != null && board[index.y * 24 + index.x].pieceType != 0 && board[index.y * 24 + index.x].color != IsColour(18, White))
                {
                    continue;
                }
                moves.Add(index);
            }
            return moves;
        } else
        {
            Vector2Int[] offsets = new Vector2Int[]{new Vector2Int(0, -1), new Vector2Int(0, -2),
            new Vector2Int(0, -3), new Vector2Int(0, -4)};
            List<Vector2Int> moves = new List<Vector2Int>();
            foreach (Vector2Int offset in offsets)
            {
                Vector2Int index = boardPosition + offset;
                if (index.x > 23 || index.y > 23 || index.x < 0 || index.y < 0)
                {
                    continue;
                }
                else if (board[index.y * 24 + index.x] != null && board[index.y * 24 + index.x].pieceType != 0 && board[index.y * 24 + index.x].color != IsColour(18, Black))
                {
                    continue;
                }
                moves.Add(index);
            }
            return moves;
        }
    }

    List<Vector2Int> KnightMoves(Piece[] board)
    {
        if (board[boardPosition.y * 24 + boardPosition.x].color)
        {
            Vector2Int[] offsets = new Vector2Int[]{new Vector2Int(1, 2), new Vector2Int(-1, 2),
            new Vector2Int(2, 1), new Vector2Int(2, -1),
            new Vector2Int(-2, 1), new Vector2Int(-2, -1),
            new Vector2Int(1, -2), new Vector2Int(-1, -2)};
            List<Vector2Int> moves = new List<Vector2Int>();
            foreach (Vector2Int offset in offsets)
            {
                Vector2Int index = boardPosition + offset;
                if (index.x > 23 || index.y > 23 || index.x < 0 || index.y < 0)
                {
                    continue;
                }
                else if (board[index.y * 24 + index.x] != null && board[index.y * 24 + index.x].pieceType != 0 && board[index.y * 24 + index.x].color != IsColour(3, White))
                {
                    continue;
                }
                moves.Add(index);
            }
            return moves;
        } else
        {
            Vector2Int[] offsets = new Vector2Int[]{new Vector2Int(1, 2), new Vector2Int(-1, 2),
            new Vector2Int(2, 1), new Vector2Int(2, -1),
            new Vector2Int(-2, 1), new Vector2Int(-2, -1),
            new Vector2Int(1, -2), new Vector2Int(-1, -2)};
            List<Vector2Int> moves = new List<Vector2Int>();
            foreach (Vector2Int offset in offsets)
            {
                Vector2Int index = boardPosition + offset;
                if (index.x > 23 || index.y > 23 || index.x < 0 || index.y < 0)
                {
                    continue;
                }
                else if (board[index.y * 24 + index.x] != null && board[index.y * 24 + index.x].pieceType != 0 && board[index.y * 24 + index.x].color == IsColour(3, Black))
                {
                    continue;
                }
                moves.Add(index);
            }
            return moves;
        }
    }

    List<Vector2Int> HunterMoves(Piece[] board)
    {
        if (!board[boardPosition.y * 24 + boardPosition.x].color)
        {
            Vector2Int[] offsets = new Vector2Int[] { new Vector2Int(1, 1), new Vector2Int(-1, 1), new Vector2Int(0, -1) };
            List<Vector2Int> moves = new List<Vector2Int>();
            foreach (Vector2Int offset in offsets)
            {
                for (int i = 1; i < 24; i++)
                {
                    Vector2Int index = boardPosition + offset * i;
                    if (index.x > 23 || index.y > 23 || index.x < 0 || index.y < 0)
                    {
                        break;
                    }
                    else if (board[index.y * 24 + index.x] != null)
                    {
                        if (board[index.y * 24 + index.x].pieceType == 0 || board[index.y * 24 + index.x].color != IsColour(13, Black))
                        {
                            if (board[index.y * 24 + index.x].pieceType != 0)
                            {
                                moves.Add(index);
                                break;
                            }
                            else
                            {
                                moves.Add(index);
                            }
                        }
                        else 
                        {
                            break;
                        }
                    }
                    moves.Add(index);
                }    
            }
            return moves;
        }
        else
        {
            Vector2Int[] offsets = new Vector2Int[] { new Vector2Int(-1, -1), new Vector2Int(1, -1), new Vector2Int(0, 1) };
            List<Vector2Int> moves = new List<Vector2Int>();
            foreach (Vector2Int offset in offsets)
            {
                for (int i = 1; i < 24; i++)
                {
                    Vector2Int index = boardPosition + offset * i;
                    if (index.x > 23 || index.y > 23 || index.x < 0 || index.y < 0)
                    {
                        break;
                    }
                    else if (board[index.y * 24 + index.x] != null)
                    {
                        if (board[index.y * 24 + index.x].pieceType == 0 || board[index.y * 24 + index.x].color != IsColour(13, White))
                        {
                            if (board[index.y * 24 + index.x].pieceType != 0)
                            {
                                moves.Add(index);
                                break;
                            }
                            else
                            {
                                moves.Add(index);
                            }
                        } else
                        {
                            break;
                        }
                    }
                    moves.Add(index);
                }
            }
            return moves;
        }
    }

    List<Vector2Int> FalconMoves(Piece[] board)
    {
        if (board[boardPosition.y * 24 + boardPosition.x].color)
        {
            Vector2Int[] offsets = new Vector2Int[] { new Vector2Int(1, 1), new Vector2Int(-1, 1), new Vector2Int(0, -1)};
            List<Vector2Int> moves = new List<Vector2Int>();
            foreach (Vector2Int offset in offsets)
            {
                for (int i = 1; i < 24; i++)
                {
                    Vector2Int index = boardPosition + offset * i;
                    if (index.x > 23 || index.y > 23 || index.x < 0 || index.y < 0)
                    {
                        break;
                    }
                    else if (board[index.y * 24 + index.x] != null)
                    {
                        if (board[index.y * 24 + index.x].pieceType == 0 || board[index.y * 24 + index.x].color == IsColour(12, Black))
                        {
                            if (board[index.y * 24 + index.x].pieceType != 0)
                            {
                                moves.Add(index);
                                break;
                            }
                            else
                            {
                                moves.Add(index);
                            }
                        } else
                        {
                            break;
                        }
                    }
                    moves.Add(index);
                }
            }
            return moves;
        } else
        {
            Vector2Int[] offsets = new Vector2Int[] { new Vector2Int(-1, -1), new Vector2Int(1, -1), new Vector2Int(0, 1)};
            List<Vector2Int> moves = new List<Vector2Int>();
            foreach (Vector2Int offset in offsets)
            {
                for (int i = 1; i < 24; i++)
                {
                    Vector2Int index = boardPosition + offset * i;
                    if (index.x > 23 || index.y > 23 || index.x < 0 || index.y < 0)
                    {
                        break;
                    }
                    else if (board[index.y * 24 + index.x] != null)
                    {
                        if (board[index.y * 24 + index.x].pieceType == 0 || board[index.y * 24 + index.x].color == IsColour(12, White))
                        {
                            if (board[index.y * 24 + index.x].pieceType != 0)
                            {
                                moves.Add(index);
                                break;
                            }
                            else
                            {
                                moves.Add(index);
                            }
                        } else
                        {
                            break;
                        }
                    }
                    moves.Add(index);
                }
            }
            return moves;
        }
    }

    List<Vector2Int> BishopMoves(Piece[] board)
    {
        if (board[boardPosition.y * 24 + boardPosition.x].color)
        {
            Vector2Int[] offsets = new Vector2Int[]{new Vector2Int(1, 1), new Vector2Int(1, -1),
            new Vector2Int(-1, -1), new Vector2Int(-1, 1)};
            List<Vector2Int> moves = new List<Vector2Int>();
            foreach (Vector2Int offset in offsets)
            {
                for (int i = 1; i < 24; i++)
                {
                    Vector2Int index = boardPosition + offset * i;
                    if (index.x > 23 || index.y > 23 || index.x < 0 || index.y < 0)
                    {
                        break;
                    }
                    else if (board[index.y * 24 + index.x] != null)
                    {
                        if (board[index.y * 24 + index.x].pieceType == 0 || board[index.y * 24 + index.x].color == IsColour(4, Black))
                        {
                            if (board[index.y * 24 + index.x].pieceType != 0)
                            {
                                moves.Add(index);
                                break;
                            }
                            else
                            {
                                moves.Add(index);
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    moves.Add(index);
                }
            }
        return moves;
        } else
        {
            Vector2Int[] offsets = new Vector2Int[]{new Vector2Int(1, 1), new Vector2Int(1, -1),
            new Vector2Int(-1, -1), new Vector2Int(-1, 1)};
            List<Vector2Int> moves = new List<Vector2Int>();
            foreach (Vector2Int offset in offsets)
            {
                for (int i = 1; i < 24; i++)
                {
                    Vector2Int index = boardPosition + offset * i;
                    if (index.x > 23 || index.y > 23 || index.x < 0 || index.y < 0)
                    {
                        break;
                    }
                    else if (board[index.y * 24 + index.x] != null)
                    {
                        if (board[index.y * 24 + index.x].pieceType == 0 || board[index.y * 24 + index.x].color != IsColour(4, White))
                        {
                            if (board[index.y * 24 + index.x].pieceType != 0)
                            {
                                moves.Add(index);
                                break;
                            }
                            else
                            {
                                moves.Add(index);
                            }
                        } else
                        {
                            break;
                        }
                    }
                    moves.Add(index);
                }
            }
            return moves;
        }
    }

    List<Vector2Int> KingMoves(Piece[] board)
    {
        if (board[boardPosition.y * 24 + boardPosition.x].color)
        {
            Vector2Int[] offsets = new Vector2Int[]{
            new Vector2Int(-1, 1), new Vector2Int(0, 1), new Vector2Int(1, 1),
            new Vector2Int(-1, 0), new Vector2Int(1, 0),
            new Vector2Int(1, -1), new Vector2Int(0, -1), new Vector2Int(-1, -1)};
            List<Vector2Int> moves = new List<Vector2Int>();
            foreach (Vector2Int offset in offsets)
            {
                Vector2Int index = boardPosition + offset;
                if (index.x > 23 || index.y > 23 || index.x < 0 || index.y < 0)
                {
                    continue;
                }
                else if (board[index.y * 24 + index.x] != null)
                {
                    if (board[index.y * 24 + index.x].pieceType != 0 && board[index.y * 24 + index.x].color != IsColour(5, White))
                    {
                        continue;
                    }
                }
                moves.Add(index);
            }
            if (amountMoved == 0)
            {
                int index = boardPosition.y * 24 + boardPosition.x;
                if (board[index - 1] == null && board[index - 2] == null && board[index - 3] == null && board[index - 4] != null)
                {
                    if (board[index - 4].amountMoved == 0)
                    {
                        moves.Add(new Vector2Int(boardPosition.x - 2, boardPosition.y));
                    }
                }
                if (board[index + 1] == null && board[index + 2] == null && board[index + 3] != null)
                {
                    if (board[index + 3].amountMoved == 0)
                    {
                        moves.Add(new Vector2Int(boardPosition.x + 2, boardPosition.y));
                    }
                }
            }
            return moves;
        } else
        {
            Vector2Int[] offsets = new Vector2Int[]{
            new Vector2Int(-1, 1), new Vector2Int(0, 1), new Vector2Int(1, 1),
            new Vector2Int(-1, 0), new Vector2Int(1, 0),
            new Vector2Int(1, -1), new Vector2Int(0, -1), new Vector2Int(-1, -1)};
            List<Vector2Int> moves = new List<Vector2Int>();
            foreach (Vector2Int offset in offsets)
            {
                Vector2Int index = boardPosition + offset;
                if (index.x > 23 || index.y > 23 || index.x < 0 || index.y < 0)
                {
                    continue;
                }
                else if (board[index.y * 24 + index.x] != null)
                {
                    if (board[index.y * 24 + index.x].pieceType != 0 && board[index.y * 24 + index.x].color == IsColour(5, Black))
                    {
                        continue;
                    }
                }
                moves.Add(index);
            }
            if (amountMoved == 0)
            {
                int index = boardPosition.y * 24 + boardPosition.x;
                if (board[index - 1] == null && board[index - 2] == null && board[index - 3] == null && board[index - 4] != null)
                {
                    if (board[index - 4].amountMoved == 0)
                    {
                        moves.Add(new Vector2Int(boardPosition.x - 2, boardPosition.y));
                    }
                }
                if (board[index + 1] == null && board[index + 2] == null && board[index + 3] != null)
                {
                    if (board[index + 3].amountMoved == 0)
                    {
                        moves.Add(new Vector2Int(boardPosition.x + 2, boardPosition.y));
                    }
                }
            }
            return moves;
        }
    }
}


