using System.Collections.Specialized;

namespace BinnaryTreeConsole
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


    public class DrawingTree
    {
        public Node Root;

        public void Add(double? value)
        {
            Add(Root, new Node(value));
        }

        public void Add(Node root, Node add)
        {
            if (root == null)
            {
                Root = add;
                return;
            }

            if (add < root)
            {
                if (root.Left == null)
                {
                    root.Left = add;
                }
                else
                {
                    Add(root.Left, add);
                }
            }
            else if(add>root)
            {
                if (root.Right == null)
                {
                    root.Right = add;
                }
                else
                {
                    Add(root.Right, add);
                }

            }
            else
            {
                throw new ArgumentException("Данные элемент уже в дереве");
            }
        }

        public void Remove(double? value)
        {
            Root = Remove(Root, new Node(value));
        }

        private Node Remove(Node root, Node removeValue)
        {
            if (root == null)
            {
                return null;
            }

            if (removeValue.Value < root.Value)
            {
                root.Left = Remove(root.Left, removeValue);
            }
            else if (removeValue.Value > root.Value)
            {
                root.Right = Remove(root.Right, removeValue);
            }
            else
            {
                if (root.Left == null || root.Right == null)
                {
                    root = (root.Left == null) ? root.Right : root.Left;
                }
                else
                {
                    Node maxLeft = GetMax(root.Left);

                    root.Value = maxLeft.Value;

                    root.Left = Remove(root.Left, maxLeft);
                }
            }

            return root;
        }

        public Node Search(double? value)
        {
            return Search(Root, value);
        }

        private Node Search(Node root, double? value)
        {
            if(root == null)
            {
                return null;
            }
            if(root.Value == value)
            {
                return root;
            }
            return root.Value < value ? Search(root.Right, value) : Search(root.Left, value);
        }

        public Node GetMin()
        {
            return GetMin(Root);
        }
        private Node GetMin(Node root)
        {
            if (root == null)
            {
                return null;
            }
            if (root.Left == null)
            {
                return root;
            }
            return GetMin(root.Left);
        }

        public Node GetMax()
        {
            return GetMax(Root);
        }
        private Node GetMax(Node root)
        {
            if(root == null)
            {
                return null;
            }
            if(root.Right == null)
            {
                return root;
            }
            return GetMax(root.Right);
        }

        public List<double?> GetSortedList()
        {
            if (Root == null)
            {
                return new List<double?>();
            }

            return GetSortedList(Root, new List<double?>());
        }

        private List<double?> GetSortedList(Node root, List<double?> list)
        {
            if (root != null)
            {
                if (root.Left != null)
                {
                    GetSortedList(root.Left, list);
                }

                list.Add(root.Value);

                if (root.Right != null)
                {
                    GetSortedList(root.Right, list);
                }
            }

            return list;
        }

        public void Wide()
        {
            Wide(Root);
        }

        private void Wide(Node root)
        {
            Queue<Node> queue = new Queue<Node>();
            queue.Enqueue(root);

            while (queue.Count > 0) 
            {   
                Node node = queue.Dequeue();


                Console.WriteLine(node.Value);

                if (node.Left != null)
                {
                    queue.Enqueue(node.Left);
                }

                if(node.Right != null)
                {
                    queue.Enqueue(node.Right);
                }
            }
        }

        public void Print()
        {
            Print(Root);
        }

        private void Print(Node root)
        {
            Queue<Node> queue = new Queue<Node>();
            queue.Enqueue(root);

            int count = 0;
            int numLay = 1;
            int startWidth = 80;
            int countSimbols = startWidth / (numLay + 1);

            string width = new string('*', countSimbols);
            // width = canvas.width/(numLay+1)
            while (queue.Count > 0)
            {
                Node node = queue.Dequeue();

                if (numLay == count)
                {
                    numLay *= 2;
                    countSimbols = startWidth / (numLay + 1);
                    count = 0;
                    width = new string('*', countSimbols);
                    Console.WriteLine();
                }
                count++;

                Console.Write(width + node.Value.ToString());

                if (node.Left != null)
                {
                    queue.Enqueue(node.Left);
                }

                if (node.Right != null)
                {
                    queue.Enqueue(node.Right);
                }
            }
        }
    }

        public class Program
    {
        static void Main(string[] args)
        {
            DrawingTree tree = new DrawingTree();
            tree.Add(4);
            tree.Add(1);
            //tree.Add(5);
            
            tree.Add(-2);
            tree.Add(3);
            //tree.Add(6);

            //tree.Wide();
            tree.Print();
            //Console.WriteLine(tree.Search(23).Value);
            Console.WriteLine();
            /*var list = tree.GetSortedList();

            foreach (var i in list)
            {
                Console.WriteLine(i);
            }*/
        }
    }
}
