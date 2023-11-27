using System.Windows.Navigation;

namespace BinnaryTreeSort.Model
{

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
            else if (add > root)
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
            if (root == null)
            {
                return null;
            }
            if (root.Value == value)
            {
                return root;
            }
            return value>root.Value ? Search(root.Right, value) : Search(root.Left, value);
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
            if (root == null)
            {
                return null;
            }
            if (root.Right == null)
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

        public void Clear()
        {
            Root = null;
        }

        // обход дерева в ширину не модифицированный он возращает сумму
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

}
