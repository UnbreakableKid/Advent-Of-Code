using System.Text.RegularExpressions;

namespace Advent_Of_Code;

public partial class Problem1
{
    private const string Input1 = "1abc2\npqr3stu8vwx\na1b2c3d4e5f\ntreb7uchet";

    public static void Run_Part1()
    {
        var lines = Input1.Split("\n");
        var sum = lines.Select(line => MyRegex().Matches(line))
            .Aggregate(0, (current, numbers) => Sum(numbers, current));

        Console.WriteLine(sum);
    }

    private static int Sum(MatchCollection numbers, int sum)
    {
        switch (numbers.Count)
        {
            case 0:
                return sum;
            case 1:
                sum += int.Parse(numbers[0].Value + numbers[0].Value);
                break;
            default:
            {
                var first = numbers[0].Value;
                var last = numbers[^1].Value;
                sum += int.Parse(first + last);
                break;
            }
        }

        return sum;
    }

    [GeneratedRegex(@"\d")]
    private static partial Regex MyRegex();

    public static void Run_Part2()
    {
        var lines = File.ReadLines("inputs/p1_2.txt");

        var result = new List<int>();

        Dictionary<string, int> replacements = new()
        {
            { "one", 1 },
            { "two", 2 },
            { "three", 3 },
            { "four", 4 },
            { "five", 5 },
            { "six", 6 },
            { "seven", 7 },
            { "eight", 8 },
            { "nine", 9 }
        };

        foreach (var line in lines)
        {
            var digitsPerLine = new List<int>();

            for (var i = 0; i < line.Length; i++)
            {
                if (char.IsNumber(line[i]))
                {
                    digitsPerLine.Add(int.Parse(line[i].ToString()));
                    continue;
                }

                foreach (var (spelled, digit) in replacements)
                {
                    if (i + spelled.Length - 1 >= line.Length || line[i..(i + spelled.Length)] != spelled)
                        continue;

                    digitsPerLine.Add(digit);
                    break;
                }
            }

            result.Add(digitsPerLine.First() * 10 + digitsPerLine.Last());
        }

        Console.WriteLine(result.Sum().ToString());
    }
}