using UnityEngine;

public class Layers
{
	public static readonly int Player = 1 << LayerMask.NameToLayer ("Player");
	public static readonly int Enemy = 1 << LayerMask.NameToLayer ("Enemy");
}
