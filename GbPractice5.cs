using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
struct Element // Для 4 задания
{
    public string FI;
    public double avgNote;
}
namespace CorrectLogin
{
    class Program
    {

        #region Задание 1 (Доп)

        // Метод отображения слова "попытка" в правильной форме

        static string RightTryWord(int x) //Задание 1
        {
            string s = "";
            // Попытка, когда заканчивается на один, кроме 11.
            if (x % 10 == 1 && x != 11) s += " попытка";
            else
                // Попытки
                if ((x >= 2 && x <= 4) || (x >= 22 && x <= 24) || (x >= 32 && x <= 34) || (x > 41 && x < 45)) s += " попытки";
            else
                    // Попыток
                    if ((x == 11) || (x >= 5 && x <= 20) || (x >= 25 && x <= 30) || (x >= 35 && x < 41) || (x > 44 && x < 51)) s += " попыток";
            return s;
        }

        // Метод проверки на соответствие логина требованиям

        static bool CheckLogin(string login)
        {
            int length = login.Length;
            if (length >= 2 && length <= 10)
            {
                bool check = true;
                char letter = login[0];
                if (Char.IsDigit(letter))
                    return false;
                for (int i = 1; i < length; i++)
                {
                    letter = login[i];
                    if (!(Char.IsDigit(letter) || IsLatinLetter(letter)))
                    {
                        check = false;
                        break;
                    }
                }
                if (check)
                    return true;
            }
            return false;
        }

        // Метод проверки на соответствие логина требованиям через регулярные выражения

        static bool CheckLoginReg(string login)
        {
            char letter = login[0];
            if (Char.IsDigit(letter))
                return false;
            if (!Regex.IsMatch(login, @"^[a-zA-Z0-9]+${2,10}"))
                return false;
            return true;
        }

        // Метод проверяет, что символ - латинская буква

        private static bool IsLatinLetter(char letter)
        {
            int num = letter;
            if ((num >= 65 && num <= 90) || (num >= 97 && num <= 122))
                return true;
            else
                return false;
        }

        #endregion

        #region Задание 3

        static class Message
        {
            static public string text;

            static Message()
            {
                text = "Лейтенант шел по желтому строительному песку, нагретому дневным палящим солнцем. " +
                    "Он был мокрым от кончиков пальцев до кончиков волос, все его тело было усеяно царапинами " +
                    "от острой колючей проволоки и ныло от сводящей с ума боли, но он был жив и направлялся к командному штабу, " +
                    "который виднелся на горизонте метрах в пятистах. Повторим несколько слов для частотного анализа: шел, его, его, тело, жив, он, ";
            }

            // Выводит слова сообщения, которые содержат не более n букв

            static public void GetWordsByLength(int len)
            {
                string[] words = text.Split(new Char[] { ' ', ',', '.', '-', '\n', '\t' });
                //Console.WriteLine("Вывод слов, длинной не более " + len + ": " );
                foreach (string word in words)
                {
                    if (word == "")
                        continue;
                    if (word.Length <= len)
                        Console.Write(word + " ");
                }
            }

            // Удаляет из сообщения все слова, которые заканчиваются на заданный символ

            static public void DeleteWordByEndChar(char ch)
            {
                string[] words = text.Split(new Char[] { ' ', ',', '.', '-', '\n', '\t' });
                //Console.WriteLine("Будут удалены слова, оканчивающиеся на символ '" + ch + "': ");
                foreach (string word in words)
                {
                    if (word == "")
                        continue;
                    if (word[word.Length - 1] == ch)
                    {
                        Console.Write(word + " ");
                        text.Replace(word, "");
                    }
                }
                //Console.WriteLine("В результате работы метода, исходный текст изменился на: " + text);
            }

            // Находит самое длинное слово сообщения

            static public string FindMaxLengthWord()
            {
                string[] words = text.Split(new Char[] { ' ', ',', '.', '-', '\n', '\t' });
                string maxWord = words[0];
                int max = words[0].Length;

                foreach (string word in words)
                {
                    if (word.Length > max)
                    {
                        max = word.Length;
                        maxWord = word;
                    }
                }
                //Console.WriteLine("Слово с самой большой длинной: " + maxWord);
                return maxWord;
            }

            // Формирует строку StringBuilder из самых длинных слов сообщения

            static public StringBuilder GetLongWordsString()
            {
                string[] words = text.Split(new Char[] { ' ', ',', '.', '-', '\n', '\t' });
                StringBuilder result = new StringBuilder();
                int max = Message.FindMaxLengthWord().Length;
                foreach (string word in words)
                {
                    if (word.Length == max)
                    {
                        result.Append(word.ToLower() + " ");
                    }
                }
                //Console.WriteLine("Полученная строка самых длинных слов: " + result);
                return result;
            }

            // Производит частотный анализ текста

            static public void FrequencyAnalysis(string[] words, string text)
            {
                Dictionary<string, int> wordFrequency = new Dictionary<string, int>();

                string[] textWords = text.Split(new Char[] { ' ', ',', '.', '-', '\n', '\t' });
                foreach (string word in words)
                {
                    foreach (string wordInText in textWords)
                    {
                        if (word == "")
                            continue;
                        if (wordInText == word)
                        {
                            if (word == "")
                                continue;
                            if (wordFrequency.ContainsKey(word))
                                wordFrequency[word] += 1;
                            else
                                wordFrequency.Add(word, 1);
                        }
                    }
                }
                //Console.WriteLine("Частотный анализ текста дал следующий результат: ");
                ICollection<string> keys = wordFrequency.Keys;

                String result = String.Format("{0,-10} {1,-10}\n\n", "Слово", "Частота появления");

                foreach (string key in keys)
                    result += String.Format("{0,-10} {1,-10:N0}\n",
                                       key, wordFrequency[key]);
                Console.WriteLine($"\n{result}");
            }

            #endregion

            #region Задание 4 (Доп)

            class Program
            {
                // Метод сортировки массива учеников

                static void SortPupils(ref Element[] pupils)
                {
                    for (int i = 0; i < pupils.Length; i++)
                    {
                        for (int j = 0; j < pupils.Length - i - 1; j++)
                        {
                            if (pupils[j].avgNote > pupils[j + 1].avgNote)
                            {
                                Element tmp = pupils[j + 1];
                                pupils[j + 1] = pupils[j];
                                pupils[j] = tmp;
                            }
                        }
                    }
                }

                #endregion

                static void Main(string[] args)
                {
                    #region Задание 1

                    Console.WriteLine("Вас приветствует программа проверки корректности логина.");
                    int AmountOfTries = 3;

                    do
                    {
                        Console.Write("Введите логин: ");
                        string login = Console.ReadLine();

                        if (CheckLogin(login) && CheckLoginReg(login))
                        {
                            Console.WriteLine();
                            break;
                        }
                        else
                        {
                            AmountOfTries--;
                            Console.WriteLine("Неверный ввод логина. \nДолжны быть соблюдены следующие условия:"
                                + "\nдлина строки 2 до 10 символов;"
                                + "\nбуквы только латинского алфавита или цифры;"
                                + "\nцифра не может быть первой."
                                + Environment.NewLine + "У Вас осталось " + AmountOfTries + RightTryWord(AmountOfTries));
                        }

                    } while (AmountOfTries > 0);

                    Console.WriteLine("Логин корректен!");

                    Console.ReadKey();

                    #endregion

                    #region Задание 2

                    Console.WriteLine("Вас приветствует программа демонстрации возможностей статического класса Message.");

                    Console.WriteLine("\nИмеется следующий текст: \n" + Message.text);

                    Console.WriteLine("\nВыведем слова текста, которые содержат не более 5 букв:");
                    Message.GetWordsByLength(5);

                    Console.WriteLine();
                    Console.Write("\nУдалим из текста слова, заканчивающиеся на 'я': ");
                    Message.DeleteWordByEndChar('я');

                    Console.WriteLine();
                    Console.WriteLine("\nСамое длинное слово в тексте: " + Message.FindMaxLengthWord());


                    Console.WriteLine("\nСформированная строка StringBuilder из самых длинных слов сообщения: \n" + Message.GetLongWordsString());

                    Console.WriteLine("\nПроизведём частотный анализ текста: ");
                    string[] arr = { "шел", "его", "тело", "жив", "он" };
                    Message.FrequencyAnalysis(arr, Message.text);

                    Console.ReadKey();

                    #endregion

                    #region Задание 4

                    int amountOfWorstPupils = 3;
                    StreamReader sr = new StreamReader("..\\..\\data.txt", encoding: System.Text.Encoding.GetEncoding(1251));
                    int N = int.Parse(sr.ReadLine());
                    Element[] a = new Element[N];
                    for (int i = 0; i < N; i++)
                    {
                        string[] s = sr.ReadLine().Split(' ');
                        a[i].FI = s[0] + " " + s[1];
                        a[i].avgNote = (double.Parse(s[2]) + double.Parse(s[3]) + double.Parse(s[4])) / 3;
                    }
                    sr.Close();

                    SortPupils(ref a);

                    String result = String.Format("{0,-20} {1,-10}\n\n", "Ученик", "Средний балл");

                    Element prev = a[0];

                    for (int i = 0; i < amountOfWorstPupils; i++)
                    {
                        if (i > 0)
                        {
                            if (prev.avgNote == a[i].avgNote)
                                amountOfWorstPupils++;
                            prev = a[i];
                        }

                        result += String.Format("{0,-20} {1,-10:F2}\n",
                                               a[i].FI, a[i].avgNote);

                    }

                    Console.WriteLine("Вас приветствует программа вывода на экран учеников с тремя худшими средними баллами.");

                    Console.WriteLine($"\n{result}");

                    Console.ReadKey();

                    #endregion

                }
            }
        }
    }
}