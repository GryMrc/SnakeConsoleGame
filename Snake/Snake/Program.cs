using System;
using System.Collections.Generic;
using System.Linq;

namespace Snake
{
    class Program
    {
        public enum DIRECTION
        {
            RIGHT = 0,
            DOWN = 1,
            LEFT = 2,
            UP = 3
        }

        public class Snake
        {
            public char symbol { get; set; }
            public int xPosition { get; set; }
            public int yPosition { get; set; }
            public int direction { get; set; }
        }
        public static List<Snake> snake = new List<Snake>();
        public static List<Snake> snakeCopy = new();
        public static ushort _space = 1;
        public static int _xMeal = 50;
        public static int _yMeal = 10;
        public static Random random = new Random();
        public static bool key { get; set; }
        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.CursorVisible = false;
            ushort _beginAxis = 30;
            ushort _beginAyis = 20;
            char headSymbol = '>';
            string quequSymbol = "-";
            System.Timers.Timer timer = new(60);
            timer.Elapsed += Timer_Elapsed;
            timer.Enabled = true;
            snake.Add(new Snake { symbol = headSymbol, direction = (int)DIRECTION.RIGHT, xPosition = _beginAxis, yPosition = _beginAyis });
            while (true)
            {
                if (!key)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.SetCursorPosition(_xMeal, _yMeal);
                    Console.Write("\u263A");

                    for (int i = 0; i < snake.Count; i++)
                    {
                        Console.SetCursorPosition(snake[i].xPosition, snake[i].yPosition);
                        Console.Write(snake[i].symbol);
                    }

                    if (Console.KeyAvailable)
                    {
                        switch (Console.ReadKey(true).Key)
                        {
                            case ConsoleKey.LeftArrow:
                                snake[0].direction = (int)DIRECTION.LEFT;
                                snake[0].symbol = '<';
                                break;
                            case ConsoleKey.RightArrow:
                                snake[0].direction = (int)DIRECTION.RIGHT;
                                snake[0].symbol = '>';
                                break;
                            case ConsoleKey.DownArrow:
                                snake[0].direction = (int)DIRECTION.DOWN;
                                snake[0].symbol = 'v';
                                break;
                            case ConsoleKey.UpArrow:
                                snake[0].direction = (int)DIRECTION.UP;
                                snake[0].symbol = '^';
                                break;

                        }

                    }
                    key = true;
                }
            }
        }

        private static void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {

            if (key)
            {
                Console.ResetColor();
                for (int i = 0; i < snake.Count; i++)
                {
                    Console.SetCursorPosition(snake[i].xPosition, snake[i].yPosition);
                    Console.Write(" ");
                }

                for (int i = 0; i < snake.Count; i++)
                {
                    switch ((DIRECTION)snake[i].direction)
                    {
                        case DIRECTION.LEFT:
                            snake[i].xPosition = snake[i].xPosition >= _space ? (snake[i].xPosition - _space) : 0;
                            break;
                        case DIRECTION.RIGHT:
                            snake[i].xPosition = snake[i].xPosition < (118 - _space) ? (snake[i].xPosition + _space) : 118;
                            break;
                        case DIRECTION.DOWN:
                            snake[i].yPosition = snake[i].yPosition < (40 - _space) ? (snake[i].yPosition + _space) : 40;
                            break;
                        case DIRECTION.UP:
                            snake[i].yPosition = snake[i].yPosition > _space ? (snake[i].yPosition - _space) : 0;
                            break;
                    }
                }
                for (int i = snake.Count - 1; i > 0; i--)
                {
                    snake[i].direction = snake[i - 1].direction;
                }
                if (snake[0].yPosition == _yMeal && snake[0].xPosition == _xMeal)
                {
                    Console.Beep();
                    Console.ResetColor();
                    Console.SetCursorPosition(_xMeal, _yMeal);
                    Console.Write(" ");
                    _xMeal = random.Next(100) + 5;
                    _yMeal = random.Next(10) + 5;
                    var lastSnake = snake.Last();
                    var newSnake = new Snake { direction = lastSnake.direction, symbol = '.' };
                    switch ((DIRECTION)lastSnake.direction)
                    {
                        case DIRECTION.RIGHT:
                            newSnake.xPosition = lastSnake.xPosition - _space;
                            newSnake.yPosition = lastSnake.yPosition;
                            break;
                        case DIRECTION.LEFT:
                            newSnake.xPosition = lastSnake.xPosition + _space;
                            newSnake.yPosition = lastSnake.yPosition;
                            break;
                        case DIRECTION.DOWN:
                            newSnake.xPosition = lastSnake.xPosition;
                            newSnake.yPosition = lastSnake.yPosition - _space;
                            break;
                        case DIRECTION.UP:
                            newSnake.xPosition = lastSnake.xPosition;
                            newSnake.yPosition = lastSnake.yPosition + _space;
                            break;
                    }
                    snake.Add(newSnake);
                }
                key = false;
            }
    }
    }
}
