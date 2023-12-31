﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SketchDailyAPI.Models.References.Animals
{
    public class AnimalClassifications : BaseClassifications
    {
        public Species? Species { get; set; }
        public Category? Category { get; set; }
        public ViewAngle? ViewAngle { get; set; }
    }
}
