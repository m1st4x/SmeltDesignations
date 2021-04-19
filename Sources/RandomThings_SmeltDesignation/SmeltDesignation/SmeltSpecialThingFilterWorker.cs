using System;
using RimWorld;
using Verse;
using UnityEngine;

namespace SmeltDesignation
{
	public class SpecialThingFilterWorker_SmeltThing : SpecialThingFilterWorker
	{
		public override bool Matches(Thing t)
		{
			if (t.Map.designationManager.DesignationOn(t, SmeltDefOf.SmeltDesignation) != null)
			{
				return false;
			}
			return true;
		}

		public override bool AlwaysMatches(ThingDef def)
		{
			return false;
		}

		public override bool CanEverMatch(ThingDef def)
		{
			return false;
		}
	}
}