﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SketchDailyAPI.DAO.References
{
    public static class RandomGenerator
    {
        private static Random _global = new Random();
        [ThreadStatic]
        private static Random? _local;

        public static int Next(int minInclusive, int maxExclusive)
        {
            var instance = _local;
            if (instance == null)
            {
                int seed;
                lock (_global) seed = _global.Next(minInclusive, maxExclusive);
                _local = instance = new Random(seed);
            }
            return instance.Next(minInclusive, maxExclusive);
        }
    }
}
