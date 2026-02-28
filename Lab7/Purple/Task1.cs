
namespace Lab7.Purple
{
    public class Task1
    {
        public struct Participant
        {
            private string _name;
            private string _surname;
            private double[] _coefs;
            private int[,] _marks;
            private int _jumpNumber;
            public string Name => _name;
            public string Surname => _surname;

            public double[] Coefs
            {
                get
                {
                    if (_coefs == null) return new double[0];
                    double[] copy = new double[_coefs.Length];
                    Array.Copy(_coefs, copy, _coefs.Length);
                    return copy;
                }
            }

            public int[,] Marks
            {
                get
                {
                    if (_marks == null) return new int[0, 0];
                    int rows =  _marks.GetLength(0);
                    int cols = _marks.GetLength(1);
                    int[,] copy = new int[rows, cols];
                    Array.Copy(_marks, copy, _marks.Length);
                    return copy;
                }
            }

            public double TotalScore
            {
                get
                {
                    double res = 0;
                    int max, min, sum;
                    for (int i = 0; i < 4; i++)
                    {
                        sum = 0;
                        max = int.MinValue;
                        min = int.MaxValue;
                        for (int j = 0; j < 7; j++)
                        {
                            sum += _marks[i, j];
                            max = Math.Max(_marks[i, j], max);
                            min = Math.Min(_marks[i, j], min);
                        }
                        res += _coefs[i] * (sum - min - max);
                    }
                    return res;
                }
            }

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _coefs = new double[4];
                _jumpNumber = 0;
                for (int i = 0; i < 4; i++)
                {
                    _coefs[i] = 2.5;
                }
                _marks = new int[4, 7];
            }

            public void SetCriterias(double[] coefs)
            {
                if (coefs == null || coefs.Length != 4)
                    return;
                for (int i = 0; i < 4; i++)
                {
                    _coefs[i] = coefs[i];
                }
            }

            public void Jump(int[] marks)
            {
                if (marks == null || marks.Length != 7 || _jumpNumber > 3) return;
                for (int j = 0; j < 7; j++)
                {
                    _marks[_jumpNumber, j] = marks[j];
                }
                _jumpNumber++;
            }

            public static void Sort(Participant[] array)
            {
                Participant[] sortedArray = new Participant[array.Length];
                sortedArray = array.OrderByDescending(x => x.TotalScore).ToArray();
                for (int i = 0; i < array.Length; i++)
                    array[i] = sortedArray[i];
            }
            public void Print()
            {
                Console.WriteLine($"Name: {_name}, Surname: {_surname} , Coefficients: {string.Join(',',_coefs)}");
                for (int jump = 0; jump < 4; jump++) 
                {
                    Console.WriteLine($"Jump {jump} ");
                    for (int judge = 0; judge < 7; judge++)
                    {
                        Console.WriteLine($"{_marks[jump, judge]} ");
                    }
                    Console.WriteLine($"Score: {TotalScore}");
                }
                
            }
        }
    }
}
