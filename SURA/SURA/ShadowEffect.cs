﻿using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SURA
{
	public class ShadowEffect : RoutingEffect
	{
		public float Radius { get; set; }

		public Color Color { get; set; }

		public float DistanceX { get; set; }

		public float DistanceY { get; set; }

		public ShadowEffect() : base("efectoPersonalizado.ItemShadowEffect")
		{
		}
	}
}
