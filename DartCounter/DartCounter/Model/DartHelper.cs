using System;
using System.Collections.Generic;
using System.Text;

namespace DartCounter.Model
{
    public static class DartHelper
    {
        public static int GetPoints(Multiplikator multiplikator, int field)
        {
            if (field >= 0)
            {
                switch (multiplikator)
                {
                    case Multiplikator.Single:
                        return field;
                    case Multiplikator.Double:
                        return 2 * field;
                    case Multiplikator.Triple:
                        return 3 * field;
                }
            }

            return 0;
        }
    }
}
