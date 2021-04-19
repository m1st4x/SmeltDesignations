using UnityEngine;
using Verse;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SmeltDesignation
{
	public class Designator_SmeltItem : Designator
	{
		public override int DraggableDimensions
		{
			get
			{
				return 2;
			}
		}

		public Designator_SmeltItem()
		{
			this.defaultLabel = "SmeltDesignation.DesignatorSmeltItem".Translate();
			this.defaultDesc = "SmeltDesignation.DesignatorSmeltItemDesc".Translate();
			this.icon = ContentFinder<Texture2D>.Get("SmeltDesignator", true);
			this.soundDragSustain = SoundDefOf.Designate_DragStandard;
			this.soundDragChanged = SoundDefOf.Designate_DragStandard_Changed;
			this.useMouseIcon = true;
			this.soundSucceeded = SoundDefOf.Designate_Claim;
		}

		public override AcceptanceReport CanDesignateCell(IntVec3 loc)
		{
			if (!loc.InBounds(base.Map) || loc.Fogged(base.Map))
			{
				return false;
			}

			if (!(from t in loc.GetThingList(base.Map) where this.CanDesignateThing(t).Accepted select t).Any<Thing>())
			{
				return "SmeltDesignation.MessageMustDesignateSmeltable".Translate();
			}
			return true;
		}

		public override void DesignateSingleCell(IntVec3 c)
		{
			List<Thing> items = c.GetThingList(base.Map);
			for (int i = 0; i < items.Count; i++)
			{
				if (this.CanDesignateThing(items[i]).Accepted)
				{
					this.DesignateThing(items[i]);
				}
			}
		}

		public override AcceptanceReport CanDesignateThing(Thing t)
		{
			if (base.Map.designationManager.DesignationOn(t, SmeltDefOf.SmeltDesignation) != null)
			{
				return false;
			}
			if (t.def.IsWithinCategory(ThingCategoryDefOf.Weapons) || t.def.IsWithinCategory(ThingCategoryDefOf.Apparel))
			{
				return true;
			}
			return false;
		}

		public override void DesignateThing(Thing t)
		{
			base.Map.designationManager.AddDesignation(new Designation(t, SmeltDefOf.SmeltDesignation));
		}
	}
}