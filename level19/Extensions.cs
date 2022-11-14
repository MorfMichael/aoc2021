﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace level19
{
    public static class Extensions
    {
        public static Vector3 Round(this Vector3 vector) => new Vector3(MathF.Round(vector.X), MathF.Round(vector.Y), MathF.Round(vector.Z));
    }
}
