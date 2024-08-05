using System.Reflection;

namespace SNAKE_Game;
class Point
{
    public int X { set; get; }
    public int Y { set; get; }
    static int row = 20;
    static int col = 40;
    static string direction = "Right";

    // static int[]_head=new int[2]{4,5};
    static Point _head = new Point(4, 5);
    static string[,] board = new string[row, col];
    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }
}

class Program
{
    static Point food = new Point(-1, -1);
    static bool foodExist = false;
    static int speed = 500;
    static int row = 20;
    static int col = 40;
    static string direction = "Right";
    static int score;
    static Point[] body = new Point[1]
    {
       new Point(4,4)

    };
    static bool Lose = false;
    static int[] recordScore = new int[0];
    static int timesPlayed;

    // static int[]_head=new int[2]{4,5};
    static Point _head = new Point(4, 5);
    static string[,] board = new string[row, col];
    static void Main(string[] args)
    {
        Thread _game = new Thread(ListenKey);
        _game.Start();
        do
        {
            Console.Clear();
            MoveSnakeHead();
            SpawnBody();
            Drawboard();
            setUpBoard();
            EatFood();
            PopUpfood();
            ShowPoint();
            CheckLose();
            if (Lose)
            {
                timesPlayed++;
                Array.Resize(ref recordScore, timesPlayed);
                recordScore[timesPlayed - 1] = score;
                RecordofScore();
                break;
            }
            Task.Delay(speed).Wait();

        }
        while (!Lose);
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Game Over!");
        Console.WriteLine($"Final Score: {score}");
        Console.WriteLine($"Highest score:{MaxScore(recordScore)}");
        Console.ResetColor();
        Console.WriteLine("Press any key to restart...");
        Console.ReadKey(true);
        ResetGame();
        Main(args);
    }

    static void Drawboard()
    {
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                if (i == 0 || i == row - 1 || j == 0 || j == col - 1)
                {
                    board[i, j] = "#";
                }
                else if (i == _head.X && j == _head.Y)
                {
                    board[i, j] = "*";
                }
                else
                {
                    bool isBodyPart = false;
                    for (int count = 0; count < body.Length; count++)
                    {
                        if (i == body[count].X && j == body[count].Y)
                        {
                            board[i, j] = "+";
                            isBodyPart = true;
                            break;
                        }
                    }
                    if (!isBodyPart)
                    {
                        if (i == food.X && j == food.Y)
                        {
                            board[i, j] = "@";
                        }
                        else
                        {
                            board[i, j] = " ";
                        }
                    }
                }
            }
        }
    }

    static void MoveSnakeHead()
    {
        switch (direction)
        {
            case "Right":
                _head.Y += 1;
                if (_head.Y == col - 1)
                {
                    _head.Y = 1;
                }
                break;
            case "Left":
                _head.Y -= 1;
                if (_head.Y == 0)
                {
                    _head.Y = col - 2;
                }
                break;
            case "Up":
                _head.X -= 1;
                if (_head.X == 0)
                {
                    _head.X = row - 2;
                }
                break;
            case "Down":
                _head.X += 1;
                if (_head.X == row - 1)
                {
                    _head.X = 1;
                }
                break;
        }


    }
    static void ListenKey()
    {
        while (true)
        {
            ConsoleKeyInfo keyinfo = Console.ReadKey();
            switch (keyinfo.Key)
            {
                case ConsoleKey.RightArrow:
                    if (direction == "Up" || direction == "Down")
                    {
                        direction = "Right";
                    }
                    break;
                case ConsoleKey.LeftArrow:
                    if (direction == "Up" || direction == "Down")
                    {
                        direction = "Left";
                    }
                    break;
                case ConsoleKey.UpArrow:
                    if (direction == "Left" || direction == "Right")
                    {
                        direction = "Up";
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (direction == "Left" || direction == "Right")
                    {
                        direction = "Down";
                    }
                    break;
            }
        }
    }
    static void setUpBoard()
    {
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                Console.Write(board[i, j]);
            }
            Console.WriteLine();
        }
    }

    static void PopUpfood()
    {
        Random random = new Random();
        int x = random.Next(1, row - 1);
        int y = random.Next(1, col - 1);
        if (x != _head.X && y != _head.Y)
        {
            if (!foodExist)
            {
                food.X = x;
                food.Y = y;
                foodExist = true;
            }
        }
    }
    static void EatFood()
    {
        if (_head.X == food.X && _head.Y == food.Y)
        {
            score += 1;
            Array.Resize(ref body, body.Length + 1);
            body[body.Length - 1] = new Point(body[body.Length - 2].X, body[body.Length - 2].Y);
            speed-=20;
            foodExist = false;
        }
    }
    static void SpawnBody()
    {
        for (int i = body.Length - 1; i > 0; i--)
        {
            body[i].X = body[i - 1].X;
            body[i].Y = body[i - 1].Y;
        }
        if (body.Length > 0)
        {
            body[0].X = _head.X;
            body[0].Y = _head.Y;
        }

    }
    static void ShowPoint()
    {
        Console.WriteLine($"Scorce: {score}");
    }

    static void CheckLose()
    {
        for (int i = 3; i < body.Length; i++)
        {
            if (_head.X == body[i].X && _head.Y == body[i].Y)
            {
                Lose = true;
                break;
            }
        }
    }
    static void ResetGame()
    {

        score = 0;
        Lose = false;
        direction = "Right";
        _head = new Point(4, 5);
        speed=500;
        body = new Point[1] { new Point(4, 4) };
        food = new Point(-1, -1);
        foodExist = false;
    }
    static int MaxScore(int []array)
    {
        int temp=array[0];
        for(int i=1;i<array.Length;i++)
        {
            if(array[i]>temp)
            {
                temp=array[i];
            }
        }
        return temp;
    }
    static string path=@"C:\Users\Admin\OneDrive\Máy tính\CODING\2024-Y1\C#\SNAKE-Game";
    static string file="Score.txt";
    static string url=Path.Combine(path,file);
    static void RecordofScore()
    {
        FileStream fileStream= new FileStream(url,FileMode.Create);
        using(StreamWriter streamWriter=new StreamWriter(fileStream))
        {
            for(int i=0;i<recordScore.Length;i++)
            {
            streamWriter.Write(recordScore[i] );
            }
        }
    }
}
