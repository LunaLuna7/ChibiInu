using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatanBossMovementController: KnightBossMovementController{
	public Transform[] possibleLocations1;

	public Transform[] possibleLocations2;
	public Transform[] possibleLocations3;
	public Transform[][] possibleLocationList = new Transform[4][];

	public override void Awake()
	{
		base.Awake();
		possibleLocationList[0] = possibleLocations;
		possibleLocationList[1] = possibleLocations1;
		possibleLocationList[2] = possibleLocations2;
		possibleLocationList[3] = possibleLocations3;
	}
}
