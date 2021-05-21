using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;

namespace zmeika
{
    class Game
    {
        static readonly int x = 80;
        static readonly int y = 26;
        static Walls walls;
        static FoodFactory foodFactory;
        static void Main()
        {
            foodFactory = new FoodFactory(x, y, '@');
            foodFactory.CreateFood();
            walls = new Walls(x, y, '#');
            Console.SetWindowSize(x + 1, y + 1);
            Console.SetBufferSize(x + 1, y + 1);
            Console.CursorVisible = false;
        }
        struct Point
        {
            public int x { get; set; }
            public int y { get; set; }
            public char ch { get; set; }

            public static implicit operator Point((int, int, char) value) =>
                /*=> называется лямбда-оператор, он используется в качестве 
                 * определения анонимных лямбда выражений, и в качестве тела, 
                 * состоящего из одного выражения*/
                new Point { x = value.Item1, y = value.Item2, ch = value.Item3 };

            public void Draw()
            {
                DrawPoint(ch);
            }
            public void Clear()
            {
                DrawPoint(' ');
            }
            private void DrawPoint(char _ch)
            {
                Console.SetCursorPosition(x, y);
                Console.Write(_ch);
            }
        }

        class Walls
        {
            private char ch;
            private List<Point> wall = new List<Point>();

            public Walls(int x, int y, char ch)
            {
                this.ch = ch;

                DrawHorizontal(x, 0);
                DrawHorizontal(x, y);
                DrawVertical(0, y);
                DrawVertical(x, y);
            }

            private void DrawHorizontal(int x, int y)
            {
                for (int i = 0; i < x; i++)
                {
                    Point p = (i, y, ch);
                    p.Draw();
                    wall.Add(p);
                }

            }
            private void DrawVertical(int x, int y)
            {
                for (int i = 0; i < y; i++)
                {
                    Point p = (x, i, ch);
                    p.Draw();
                    wall.Add(p);
                }
            }
        }

        class FoodFactory
        {
            int x;
            int y;
            char ch;
            public Point food { get; private set; }

            Random random = new Random();

            public FoodFactory(int x, int y, char ch)
            {
                this.x = x;
                this.y = y;
                this.ch = ch;
            }
            
            public void CreateFood()
            {
                food = (random.Next(2, x - 2), random.Next(2, y - 2), ch);
                food.Draw();
            }
        }

        enum Direction
        {
            LEFT,
            RIGHT,
            UP,
            DOWN
        }
        class Snake
        {
            private List<Point> snake;
            private Direction direction;
            private int step = 1;
            private Point tail;
            private Point head;
            bool rotate = true;
            public Snake(int x, int y, int lenght)
            {
                direction = Direction.RIGHT;
                snake = new List<Point>();
                for (int i = x - lenght; i < x; i++)
                {
                    Point p = (i, y, '*');
                    snake.Add(p);
                    p.Draw();
                }
            }
            public Point GetNextPoint()
            {
                Point p = GetHead();
                switch (direction)
                {
                    case Direction.LEFT:
                        p.x -= step;
                        break;
                    case Direction.RIGHT:
                        p.x += step;
                        break;
                    case Direction.UP:
                        p.y -= step;
                        break;
                    case Direction.DOWN:
                        p.y += step;
                        break;
                }
                return p;
            }
            public void Rotation(ConsoleKey key)
            {

            }
        }

    }
}
