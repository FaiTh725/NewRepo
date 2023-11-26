using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinnaryTreeSort.Model
{
    public class Node
    {
        public double? Value { get; set; }

        public Node Left { get; set; }
        public Node Right { get; set; }

        public Node(double? value)
        {
            Value = value;
        }

        public static bool operator <(Node leftValue, Node rightValue)
        {
            return leftValue.Value < rightValue.Value;
        }

        public static bool operator >(Node leftValue, Node rightValue)
        {
            return leftValue.Value > rightValue.Value;
        }

    }
}
