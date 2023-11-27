using BinnaryTreeSort.Model;
using BinnaryTreeSort.Resourses;
using System.Collections;
using System.Collections.Immutable;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace BinnaryTreeSort.Extension
{
    public static class ExtentionDwaingTree
    {
        public static string SorteArray(this DrawingTree tree, Canvas canvas)
        {
            
            string res = "";
            Sorte();

            async void Sorte()
            {
                List<double?> sortedList = tree.GetSortedList();

                while (sortedList.Count >0)
                {
                    DrawingNode drawingNode = FindNodeInCanvas(canvas, sortedList[0]);
                    drawingNode.BackGroundEllipse = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#c844e3"));
                    
                    await Task.Delay(500);
                    
                    res += drawingNode.Text + " ";
                    
                    sortedList.RemoveAt(0);

                }
            }

            return res;

/*            async void Sorted(string res)
            {
                List<double?> sortedList = tree.GetSortedList();

                int countSelectedNodes = 0;

                var xAnimation = new DoubleAnimationUsingKeyFrames();
                var yAnimation = new DoubleAnimationUsingKeyFrames();

                var storyBoard = new Storyboard();

                storyBoard.Children.Add(xAnimation);
                storyBoard.Children.Add(yAnimation);
                while (countSelectedNodes < sortedList.Count)
                {
                    DrawingNode drawingNode = FindNodeInCanvas(canvas, sortedList[countSelectedNodes]);
                    //drawingNode.BackGroundEllipse = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#c844e3"));

                    Line line = null;
                    int indexDeleteNode = canvas.Children.IndexOf(drawingNode);
                    //MessageBox.Show(indexDeleteNode.ToString());
                    if (indexDeleteNode != canvas.Children.Count)
                    {
                        line = (Line)canvas.Children[indexDeleteNode+1];
                        //ine.Stroke = new SolidColorBrush(Colors.Red);
                        //MessageBox.Show((indexDeleteNode-1).ToString());
                    }
                    

                    // создание анимации

                    
                    if (line != null)
                    {
                        // анимация линии
                        double startAnimX = line.X2;
                        double endAnimX = line.X1;
                        bool isSwapX = false;

                        double startAnimY = line.Y2;
                        double endAnimY = line.Y1;

                        if (startAnimX > endAnimX)
                        {
                            (startAnimX, endAnimX) = (endAnimX, startAnimX);
                            (startAnimY, endAnimY) = (endAnimY, startAnimY);
                            isSwapX = true;
                        }


                        storyBoard.Children.Remove(xAnimation);
                        storyBoard.Children.Remove(yAnimation);

                        xAnimation = new DoubleAnimationUsingKeyFrames();
                        xAnimation.KeyFrames.Add(new LinearDoubleKeyFrame(startAnimX, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0))));
                        xAnimation.KeyFrames.Add(new LinearDoubleKeyFrame(endAnimX, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1))));

                        Storyboard.SetTarget(xAnimation, line);
                        if (isSwapX)
                        {
                            Storyboard.SetTargetProperty(xAnimation, new PropertyPath(Line.X1Property));
                        }
                        else
                        {
                            Storyboard.SetTargetProperty(xAnimation, new PropertyPath(Line.X2Property));
                        }


                        yAnimation = new DoubleAnimationUsingKeyFrames();
                        yAnimation.KeyFrames.Add(new LinearDoubleKeyFrame(startAnimY, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0))));
                        yAnimation.KeyFrames.Add(new LinearDoubleKeyFrame(endAnimY, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1))));

                        Storyboard.SetTarget(yAnimation, line);
                        if (isSwapX)
                        {
                            Storyboard.SetTargetProperty(yAnimation, new PropertyPath(Line.Y1Property));
                        }
                        else
                        {
                            Storyboard.SetTargetProperty(yAnimation, new PropertyPath(Line.Y2Property));
                        }
                        // ----------------------

                        // анимация узла

                        DrawingNode drawingNode1 = drawingNode;

                        double toX = line.X2 - line.X1 - 15, toY = 20;

                        if(line.X2<line.X1)
                        {
                            toX *= -1;
                        }

                        if (line.Y2<line.Y1)
                        {
                            toY *= -1;
                        }
                        DoubleAnimation animation1 = new DoubleAnimation
                        {
                            From = -15,
                            To = toX,
                            Duration = TimeSpan.FromSeconds(1), // Замените на желаемую длительность анимации
                            FillBehavior = FillBehavior.Stop
                        };

                        animation1.Completed += (s, e) =>
                        {
                            Canvas.SetLeft(drawingNode1, toX);
                        };

                        //drawingNode1.BeginAnimation(Canvas.LeftProperty, animation1);

                        DoubleAnimation animation2 = new DoubleAnimation()
                        {
                            From = 0,
                            To = toY,
                            Duration = TimeSpan.FromSeconds(1),
                            FillBehavior = FillBehavior.Stop
                        };

                        animation2.Completed += (s, e) =>
                        {
                            Canvas.SetTop(drawingNode1, toY);
                        };

                        //drawingNode1.BeginAnimation(Canvas.TopProperty, animation2);

                        //---------------------

                        storyBoard.Children.Add(xAnimation);
                        storyBoard.Children.Add(yAnimation);
                        storyBoard.Begin();
                    }
                    

                    //------------------------

                    res += sortedList[countSelectedNodes].ToString();

                    await Task.Delay(1000);

                    countSelectedNodes++;
                }
            }
*/        
        }

        public static void SearchNode(this DrawingTree tree, Canvas canvas, double? searchValue)
        {
            Search(tree.Root, canvas, searchValue);


            async void Search(Node root, Canvas canvas, double? searchValue)
            {
                if (root == null)
                {
                    return;
                }
                DrawingNode drawingNode = FindNodeInCanvas(canvas, root.Value);
                drawingNode.BackGroundEllipse = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#c844e3"));
                if (root.Value == searchValue)
                {
                    return;
                }

                await Task.Delay(800);
                drawingNode.BackGroundEllipse = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#09521c"));
                if (searchValue < root.Value)
                {
                    Search(root.Left, canvas, searchValue);
                }
                else
                {
                    Search(root.Right, canvas, searchValue);
                }
            }
        }

        public static DrawingNode FindNodeInCanvas(Canvas canvas, double? searchValue)
        {
            foreach (var item in canvas.Children)
            {
                if (item is DrawingNode drawingNode)
                {
                    if (drawingNode.Text == searchValue.ToString())
                    {
                        return item as DrawingNode;
                    }
                }
            }

            return null;
        }

        public static void DrawTree(this DrawingTree tree, Canvas canvas)
        {

            Draw(tree.Root, canvas);

            async void Draw(Node root, Canvas canvas)
            {

                Queue<Node> queue = new Queue<Node>();
                queue.Enqueue(root);

                Queue<DrawingNode> drawingNodes = new Queue<DrawingNode>();
                drawingNodes.Enqueue(GetTemplateNode(canvas.ActualWidth / 2, 30, root.Value.ToString()));
                Queue<Line> lines = new Queue<Line>();
                lines.Enqueue(GetTemaplteLine(canvas.ActualWidth / 2, 27, canvas.ActualWidth / 2, 27));

                double widthParent = canvas.ActualWidth / 2;
                int offsetY = 30, offsetX = 18;

                DoubleAnimationUsingKeyFrames xAnimation = new DoubleAnimationUsingKeyFrames();
                DoubleAnimationUsingKeyFrames yAnimation = new DoubleAnimationUsingKeyFrames();

                Storyboard storyboard = new Storyboard();
                storyboard.Children.Add(xAnimation);
                storyboard.Children.Add(yAnimation);

                while (queue.Count > 0)
                {
                    Node node = queue.Dequeue();
                    Line line = lines.Dequeue();


                    // создание анимации линии

                    Line lineWithAnim = GetTemaplteLine(line.X1, line.Y1, line.X1, line.Y1);
                    DrawingNode drawingNode = drawingNodes.Dequeue();
                    DrawingNode nodeWithAnim = GetTemplateNode(line.X1, line.Y1, drawingNode.Text);

                    bool isDrawNode = NodeInCanvas(nodeWithAnim);

                    if (!isDrawNode)
                    {
                        canvas.Children.Add(nodeWithAnim);
                        canvas.Children.Add(lineWithAnim);

                        storyboard.Children.Remove(xAnimation);
                        storyboard.Children.Remove(yAnimation);

                        xAnimation = new DoubleAnimationUsingKeyFrames();
                        xAnimation.KeyFrames.Add(new LinearDoubleKeyFrame(line.X1, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0))));
                        xAnimation.KeyFrames.Add(new LinearDoubleKeyFrame(line.X2, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1))));

                        Storyboard.SetTarget(xAnimation, lineWithAnim);
                        Storyboard.SetTargetProperty(xAnimation, new PropertyPath(Line.X2Property));

                        yAnimation = new DoubleAnimationUsingKeyFrames();
                        yAnimation.KeyFrames.Add(new LinearDoubleKeyFrame(line.Y1, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0))));
                        yAnimation.KeyFrames.Add(new LinearDoubleKeyFrame(line.Y2, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1))));

                        Storyboard.SetTarget(yAnimation, lineWithAnim);
                        Storyboard.SetTargetProperty(yAnimation, new PropertyPath(Line.Y2Property));

                        // --------------------------

                        // Создание анимации


                        DrawingNode drawingNode1 = nodeWithAnim;

                        // Создание анимации узла
                        DoubleAnimation animation1 = new DoubleAnimation
                        {
                            From = 0,
                            To = line.X2 - line.X1 - 15,
                            Duration = TimeSpan.FromSeconds(1), // Замените на желаемую длительность анимации
                            FillBehavior = FillBehavior.Stop
                        };

                        animation1.Completed += (s, e) =>
                        {
                            Canvas.SetLeft(drawingNode1, line.X2 - line.X1 - 15);
                        };

                        drawingNode1.BeginAnimation(Canvas.LeftProperty, animation1);

                        DoubleAnimation animation2 = new DoubleAnimation()
                        {
                            From = 0,
                            To = 20,
                            Duration = TimeSpan.FromSeconds(1),
                            FillBehavior = FillBehavior.Stop
                        };

                        animation2.Completed += (s, e) =>
                        {
                            Canvas.SetTop(drawingNode1, 20);
                        };

                        drawingNode1.BeginAnimation(Canvas.TopProperty, animation2);

                        // -------------------------------

                        storyboard.Children.Add(xAnimation);
                        storyboard.Children.Add(yAnimation);
                        storyboard.Begin();

                        //--------------------------------

                        await Task.Delay(1000);
                    }

                    double copyWidthParent = widthParent;
                    if (node.Left != null)
                    {
                        queue.Enqueue(node.Left);

                        double x;

                        x = drawingNode.Margin.Left - widthParent / 2;
                        widthParent = Math.Abs(x - drawingNode.Margin.Left);

                        double y = drawingNode.Margin.Top + 50;

                        drawingNodes.Enqueue(GetTemplateNode(x, y, node.Left.Value.ToString()));
                        lines.Enqueue(GetTemaplteLine(drawingNode.Margin.Left + offsetX, drawingNode.Margin.Top + offsetY,
                                                      x + offsetX, y));
                    }

                    if (node.Right != null)
                    {
                        queue.Enqueue(node.Right);

                        double x;

                        x = drawingNode.Margin.Left + copyWidthParent / 2;
                        widthParent = Math.Abs(x - drawingNode.Margin.Left);

                        double y = drawingNode.Margin.Top + 50;

                        drawingNodes.Enqueue(GetTemplateNode(x, y, node.Right.Value.ToString()));
                        lines.Enqueue(GetTemaplteLine(drawingNode.Margin.Left + offsetX, drawingNode.Margin.Top + offsetY,
                                                      x + offsetX, y));
                    }
                }
            }

            bool NodeInCanvas(DrawingNode nodeWithAnim)
            {
                bool isDrawNode = false;

                foreach (var item in canvas.Children)
                {
                    if (item is DrawingNode nodeInCanvas)
                    {
                        if (nodeWithAnim.Text == nodeInCanvas.Text)
                        {
                            isDrawNode = true;
                            break;
                        }
                    }
                }
                return isDrawNode;
            }

            #region LocalFuncForTemplates
            Line GetTemaplteLine(double x1, double y1, double x2, double y2)
            {
                Line line = new Line();

                line.X1 = x1;
                line.Y1 = y1;
                line.X2 = x2;
                line.Y2 = y2;

                line.StrokeThickness = 2;

                line.Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#09521c"));

                return line;
            }

            DrawingNode GetTemplateNode(double x, double y, string value)
            {
                DrawingNode node = new DrawingNode();
                node.Width = 30;
                node.Height = 30;
                node.Text = value;
                node.Margin = new Thickness(x, y, 0, 0);
                return node;
            }
            #endregion
        }
    }
}
