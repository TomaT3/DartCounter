using System;
using System.Collections.Generic;
using System.Text;

namespace DartCounter.Model
{
    public class Dart
    {
        public Dart()
        {
            Field = -1;
        }

        public Dart(Multiplikator multiplikator, int field)
        {
            Multiplikator = multiplikator;
            Field = field;
        }

        public int Points
        {
            get => DartHelper.GetPoints(Multiplikator, Field);
        }

        public Multiplikator Multiplikator { get; private set; }
        public int Field { get; private set; }
    }
}
