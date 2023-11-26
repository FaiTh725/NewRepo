using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;
using BinnaryTreeSort.Model;
using BinnaryTreeSort.Resourses;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using System.Diagnostics;
using LanguageExt;

namespace BinnaryTreeSort.Extension
{
    public static class ExtentionDwaingTree
    {

        // ничего не работает
        public static void DrawTree(this DrawingTree tree, Canvas canvas)
        {

            Draw(tree.Root, canvas);

            async void Draw(Node root, Canvas canvas)
            {

                Queue<Node> queue = new Queue<Node>();
                queue.Enqueue(root);

                Queue<DrawingNode> drawingNodes = new Queue<DrawingNode>();
                drawingNodes.Enqueue(GetTemplateNode(canvas.ActualWidth/2, 30, root.Value.ToString()));
                Queue<Line> lines = new Queue<Line>();
                lines.Enqueue(GetTemaplteLine(canvas.ActualWidth/2, 27, canvas.ActualWidth / 2, 27));

                double widthParent = canvas.ActualWidth / 2;
                int offsetY = 30, offsetX = 18;

                canvas.Children.Clear();

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
                    DrawingNode drawingNode = drawingNodes.Dequeue();
                    DrawingNode nodeWithAnim = GetTemplateNode(line.X1, line.Y1, drawingNode.Text);
                    canvas.Children.Add(nodeWithAnim);

                    DrawingNode drawingNode1 = nodeWithAnim;

                    double currentX = Canvas.GetLeft(drawingNode1);

                    // Создание анимации
                    DoubleAnimation animation1 = new DoubleAnimation
                    {
                        From = 0,
                        To = line.X2 - line.X1 - 15 ,
                        Duration = TimeSpan.FromSeconds(1), // Замените на желаемую длительность анимации
                        FillBehavior = FillBehavior.Stop
                    };

                    animation1.Completed += (s, e) =>
                    {
                        Canvas.SetLeft(drawingNode1,line.X2 - line.X1 - 15) ;
                    };

                    drawingNode1.BeginAnimation(Canvas.LeftProperty, animation1);

                    // -------------------------------

                    storyboard.Children.Add(xAnimation);
                    storyboard.Children.Add(yAnimation);
                    storyboard.Begin();
                    
                    //--------------------------------

                    await Task.Delay(1000);
                    
                    double copyWidthParent = widthParent;
                    if(node.Left != null)
                    {
                        queue.Enqueue(node.Left);

                        double x;

                        x = drawingNode.Margin.Left - widthParent/2;
                        widthParent = Math.Abs(x - drawingNode.Margin.Left); 

                        double y = drawingNode.Margin.Top + 30;

                        drawingNodes.Enqueue(GetTemplateNode(x,y, node.Left.Value.ToString()));
                        lines.Enqueue(GetTemaplteLine(drawingNode.Margin.Left+offsetX, drawingNode.Margin.Top+offsetY, 
                                                      x + offsetX, y));
                    }

                    if(node.Right != null)
                    {
                        queue.Enqueue(node.Right);

                        double x;

                        x = drawingNode.Margin.Left + copyWidthParent / 2;
                        widthParent = Math.Abs(x - drawingNode.Margin.Left);

                        double y = drawingNode.Margin.Top + 30;

                        drawingNodes.Enqueue(GetTemplateNode(x, y, node.Right.Value.ToString()));
                        lines.Enqueue(GetTemaplteLine(drawingNode.Margin.Left+offsetX, drawingNode.Margin.Top+offsetY,
                                                      x + offsetX, y));
                    }
                }
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
                node.Margin = new Thickness(x,y,0,0);
                return node;
            }
            #endregion
        }
    }
}
