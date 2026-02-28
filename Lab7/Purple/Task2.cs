namespace Lab7.Purple
{
    public class Task2
    {
        public struct Participant
        {
            private string _name;
            private string _surname;
            private int _distance;
            private int[] _marks;
            public string Name => _name;
            public string Surname => _surname;
            public int Distance => _distance;

            public int[] Marks
            {
                get
                {
                    if (_marks == null) return new int[5];
                    int[] Marks = new int[_marks.Length];
                    Array.Copy(_marks, Marks, _marks.Length);
                    return Marks;
                }
            }

            public int Result
            {
                get
                {
                    int result = 0;
                    int max = Int32.MinValue, min = Int32.MaxValue;
                    for (int i = 0; i < _marks.Length; i++)
                    {
                        result += _marks[i];
                        if (_marks[i] > max)
                            max = _marks[i];
                        if (_marks[i] < min)
                            min = _marks[i];
                    }

                    result -= max + min;
                    if (_distance >= 120)
                        result += (_distance - 120) * 2 + 60;
                    else 
                        result -= (120 - _distance) * 2 - 60;
                    if (result < 0)
                        result = 0;
                    return result;
                }
            }

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new int[5];
            }

            public void Jump(int distance, int[] marks)
            { 
                for (int i = 0; i < _marks.Length; i++)
                    _marks[i] = marks[i];
                _distance = distance;
            }

            public static void Sort(Participant[] array)
            {
                Participant[] sortedArray = new Participant[array.Length];
                sortedArray = array.OrderByDescending(x => x.Result).ToArray();
                for (int i = 0; i < array.Length; i++)
                    array[i] = sortedArray[i];
            }

            
            public void Print()
            {
                Console.WriteLine($"Name: {_name}, Surname: {_surname}");
                Console.WriteLine($"Distance: {_distance}");
                Console.WriteLine($"Marks: {string.Join(", ", _marks)}");
                Console.WriteLine($"Result: {Result}");
            }
        
        }
    }
}
