﻿#region Copyright & License Information
/*
 * Copyright 2007-2011 The OpenRA Developers (see AUTHORS)
 * This file is part of OpenRA, which is free software. It is made
 * available to you under the terms of the GNU General Public License
 * as published by the Free Software Foundation. For more information,
 * see COPYING.
 */
#endregion

using System.Collections.Generic;
using OpenRA.Effects;
using OpenRA.Graphics;
using OpenRA.Traits;

namespace OpenRA.Mods.RA.Effects
{
	class SatelliteLaunch : IEffect
	{
		int frame = 0;
		Animation doors = new Animation("atek");
		WPos pos;

		public SatelliteLaunch(Actor a)
		{
			doors.PlayThen("active",
				() => a.World.AddFrameEndTask(w => w.Remove(this)));

			pos = a.CenterPosition;
		}

		public void Tick( World world )
		{
			doors.Tick();

			if (++frame == 19)
				world.AddFrameEndTask(w => w.Add(new GpsSatellite(pos)));
		}

		public IEnumerable<IRenderable> Render(WorldRenderer wr)
		{
			return doors.Render(pos, wr.Palette("effect"));
		}
	}
}
