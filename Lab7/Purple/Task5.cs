using System.Runtime;
using System.Xml.Linq;

namespace Lab7.Purple
{
    public class Task5
    {
        public struct Response
        {
            private string _animal;
            private string _characterTrait;
            private string _concept;
            public string Animal => _animal;
            public string CharacterTrait => _characterTrait;
            public string Concept => _concept;

            public Response(string animal, string characterTrait, string concept)
            {
                _animal = animal;
                _characterTrait = characterTrait;
                _concept = concept;
            }

            public int CountVotes(Response[] responses, int questionNumber)
            {
                int count = 0;
                switch (questionNumber)
                {
                    case 1:
                        foreach (var response in responses)
                        {
                            if (response._animal == _animal)
                                count++;
                        }

                        break;
                   case 2:
                       foreach (var response in responses)
                       {
                            if (response._concept == _concept)
                                count++;
                       }
                       break;
                  case 3:
                        foreach (var response in responses)
                        {
                            if (response._characterTrait == _characterTrait)
                                count++;
                        }

                        break;
                }
                return count;
            }

            public void Print()
            {
                Console.WriteLine($"{Animal}, {CharacterTrait}, {Concept}");
            }
        }

        public struct Research
        {
            private string _name;
            private Response[] _responses;
            public string Name => _name;
            public Response[] Responses => _responses;

            public Research(string name)
            {
                _name = name;
                _responses = new Response[0];
            }

            public void Add(string[] answers)
            {
                Array.Resize(ref _responses, _responses.Length + 1);
                _responses[_responses.Length - 1] = new Response(answers[0], answers[1], answers[2]);
            }

            private static string GetAnswer(Response responce, int questionNumber) => questionNumber switch
            {
                1 => responce.Animal,
                2 => responce.CharacterTrait,
                3 => responce.Concept,
                _ => string.Empty
            };
            
            public string[] GetTopResponses(int question)
            {
                return _responses.Select(r => GetAnswer(r, question))// берем все ответы, получаем ответы на конкретный вопрос
                    .Where(a => a != null)// 2️⃣ убираем пустые ответы
                    .GroupBy(answer => answer)// 3️⃣ группируем одинаковые ответы
                    .Select(g => new// 4️⃣ создаем объекты с ответом и счетчиком
                    {
                        Answer = g.Key,// сам ответ
                        Count = g.Count()// сколько раз встретился
                    })
                    .OrderByDescending(x => x.Count)// 5️⃣ сортируем по убыванию популярности
                    .Select(x => x.Answer)// 6️⃣ берем только ответы (без счетчиков)
                    .Take(5)// 7️⃣ берем первые 5 (топ-5)
                    .ToArray();// 8️⃣ превращаем в массив
            }
            
            public void Print()
            {
                Console.WriteLine($"Исследование: {Name}");
                Console.WriteLine("Ответы:");
                for (int i = 0; i < _responses.Length; i++)
                {
                    _responses[i].Print();
                }
            }
        }
    }
}
