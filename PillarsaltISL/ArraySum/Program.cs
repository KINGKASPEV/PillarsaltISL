// See https://aka.ms/new-console-template for more information
static bool SumableToLargest(int[] numbers)
{
    if (numbers.Length < 2)
    {
        return false;
    }

    int maxNumber = numbers.Max();
    int maxIndex = Array.IndexOf(numbers, maxNumber);
    int[] rest = numbers.Where((n, i) => i != maxIndex).ToArray();

    return SubsetSum(rest, maxNumber);
}

static bool SubsetSum(int[] numbers, int target)
{
    bool[] dp = new bool[target + 1];
    dp[0] = true;

    foreach (int num in numbers)
    {
        for (int i = target; i >= num; i--)
        {
            if (dp[i - num])
            {
                dp[i] = true;
            }
        }
    }

    return dp[target];
}



Console.WriteLine("Enter numbers separated by spaces:");
string input = Console.ReadLine();
int[] numbers = input.Split(' ').Select(int.Parse).ToArray();

bool result = SumableToLargest(numbers);

Console.WriteLine(result ? "Yes, a combination exists." : "No, no combination exists.");
Console.ReadLine();
