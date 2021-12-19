using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public readonly struct MovePlays{

	public readonly struct Flag {
		public const int None = 0;
		public const int PromoteToQueen = 1;
		public const int PromoteToKnight = 2;
		public const int PromoteToRook = 3;
		public const int PromoteToBishop = 4;
	}

	readonly ushort moveValue;

	const ushort startSquareMask = 0b0000000000111111;
	const ushort targetSquareMask = 0b000011111111000000;
	const ushort flagMask = 0b1111000000000000;

	public MovePlays (ushort moveValue) {
		this.moveValue = moveValue;
	}

	public MovePlays (int startSquare, int targetSquare) {
		moveValue = (ushort) (startSquare | targetSquare << 6);
	}

	public MovePlays (int startSquare, int targetSquare, int flag) {
		moveValue = (ushort) (startSquare | targetSquare << 6 | flag << 12);
	}

	public int StartSquare {
		get {
			return (moveValue & startSquareMask);
		}
	}

	public int TargetSquare {
		get {
			return ((moveValue & targetSquareMask) >> 6);
		}
	}

	public int MoveFlagPlayer() {
       
		if (Input.GetKeyDown(KeyCode.A) || Input.GetKey(KeyCode.A))
		{
			return 1;
		}
		else if (Input.GetKeyDown(KeyCode.S) || Input.GetKey(KeyCode.S))
		{
			return 2;
		}
		else if (Input.GetKeyDown(KeyCode.D) || Input.GetKey(KeyCode.D))
		{
			return 4;
		}
		else if (Input.GetKeyDown(KeyCode.F) || Input.GetKey(KeyCode.F))
		{
			return 3;
		}
		return 0;
	}

	public int MoveFlagAI()
	{
        return Random.Range(1, 4) switch
        {
            1 => 1,
            2 => 2,
            3 => 3,
            4 => 4,
		    _ => 0
        };
    }

	public static MovePlays InvalidMove {
		get {
			return new MovePlays (0);
		}
	}

	public static bool SameMove (MovePlays a, MovePlays b) {
		return a.moveValue == b.moveValue;
	}

	public ushort Value {
		get {
			return moveValue;
		}
	}

	public bool IsInvalid {
		get {
			return moveValue == 0;
		}
	}
}

