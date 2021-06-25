using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassMessage
{
    // Статический класс Message
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

        // summary>Выводит слова сообщения, которые содержат не более n букв

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
    }
}