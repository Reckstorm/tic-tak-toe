using System.Drawing;

namespace Classwork_19._11
{
    internal class Game
    {
        public delegate void Move(string m);
        public event Move PlayerMove;

        public delegate void Result(string m, Game game);
        public event Result GameOver;
        public char[,] Field { get; set; }
        private List<Coordinate> Coordinates;
        private const int FieldSize = 5;
        private const int CoordSize = 3;
        public Player PlayerOne { get; set; }
        public Player PlayerTwo { get; set; }
        public Game(string playerOne, string playerTwo)
        {
            Field = new char[FieldSize, FieldSize] { { ' ', '|', ' ', '|', ' ' }, { '-', '-', '-', '-', '-' }, { ' ', '|', ' ', '|', ' ' }, { '-', '-', '-', '-', '-' }, { ' ', '|', ' ', '|', ' ' } };
            Coordinates = new List<Coordinate>();
            for (int i = 0, x = 0, num = 0; i < CoordSize; i++, x += 2)
            {
                for (int j = 0, y = 0; j < CoordSize; j++, y += 2)
                {
                    num++;
                    Coordinates.Add(new Coordinate() { Num = num, x = x, y = y });
                }
            }
            PlayerOne = new Player() { Name = playerOne, Mark = 'o' };
            PlayerTwo = new Player() { Name = playerTwo, Mark = 'x' };
        }
        public bool MakeMove(int x, Player player)
        {
            Coordinate temp = Coordinates.Find(c => c.Num == x);
            if (Field[temp.x, temp.y] != PlayerOne.Mark && Field[temp.x, temp.y] != PlayerTwo.Mark)
            {
                Field[temp.x, temp.y] = player.Mark;
                string next = player.Mark == PlayerOne.Mark ? PlayerTwo.Name : PlayerOne.Name;
                PlayerMove?.Invoke($"{player.Name} puts '{player.Mark}' at [{temp.Num}]\n{next}'s move next");
                return true;
            }
            PlayerMove?.Invoke($"{player.Name} didn't make a move");
            return false;
        }
        bool HorCheck()
        {
            for (int i = 0; i < CoordSize*CoordSize; i+= CoordSize)
            {
                if (Field[Coordinates[i].x, Coordinates[i].y] == Field[Coordinates[i + 1].x, Coordinates[i + 1].y] &&
                   Field[Coordinates[i + 1].x, Coordinates[i + 1].y] == Field[Coordinates[i + 2].x, Coordinates[i + 2].y] &&
                   Field[Coordinates[i].x, Coordinates[i].y] != ' ')
                {
                    return true;
                }
            }
            return false;
        }
        bool VertCheck()
        {
            for (int i = 0; i < CoordSize * CoordSize; i+= CoordSize)
            {
                if (Field[Coordinates[i].y, Coordinates[i].x] == Field[Coordinates[i + 1].y, Coordinates[i + 1].x] &&
                   Field[Coordinates[i + 1].y, Coordinates[i + 1].x] == Field[Coordinates[i + 2].y, Coordinates[i + 2].x] &&
                   Field[Coordinates[i].y, Coordinates[i].x] != ' ')
                {
                    return true;
                }
            }
            return false;
        }
        bool DiagCheck()
        {
            int i = 0;
            if ((Field[Coordinates[i].x, Coordinates[i].y] == Field[Coordinates[i + 4].x, Coordinates[i + 4].y] &&
               Field[Coordinates[i + 4].x, Coordinates[i + 4].y] == Field[Coordinates[i + 8].x, Coordinates[i + 8].y] &&
               Field[Coordinates[i].x, Coordinates[i].y] != ' ') ||
               (Field[Coordinates[i + 2].x, Coordinates[i + 2].y] == Field[Coordinates[i + 4].x, Coordinates[i + 4].y] &&
               Field[Coordinates[i + 4].x, Coordinates[i + 4].y] == Field[Coordinates[i + 6].x, Coordinates[i + 6].y] &&
               Field[Coordinates[i + 2].x, Coordinates[i + 2].y] != ' '))
            {
                return true;
            }
            return false;
        }
        public bool WinCheck()
        {
            if (HorCheck() || VertCheck() || DiagCheck())
            {
                GameOver?.Invoke("Game over", this);
                return true;        
            }
            return false;
        }
        public string PrintCoords()
        {
            string temp = "";
            foreach (Coordinate coord in Coordinates)
            {
                temp += coord.Num + " ";
                if (coord.Num % 3 == 0)
                {
                    temp += '\n';
                }
            }
            return temp;
        }
        public override string ToString()
        {
            string temp = "";
            for (int i = 0; i < FieldSize; i++)
            {
                for (int j = 0; j < FieldSize; j++)
                {
                    temp += Field[i, j];
                }
                temp += '\n';
            }
            return temp;
        }
    }
}
