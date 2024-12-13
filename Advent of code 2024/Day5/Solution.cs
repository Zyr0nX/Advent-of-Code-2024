namespace Advent_of_code_2024;

public class SolutionDay5() : SolutionBase(5)
{
    public override string Part1Solver()
    {
        var spanInput = Input.AsSpan();
        ReadOnlySpan<char> firstSection = default;
        ReadOnlySpan<char> secondSection = default;
        var i = 0;
        foreach (var sectionRange in spanInput.Split($"{Environment.NewLine}{Environment.NewLine}"))
        {
            switch (i)
            {
                case 0:
                    firstSection = spanInput[sectionRange];
                    break;
                case 1:
                    secondSection = spanInput[sectionRange];
                    break;
            }

            i++;
        }

        var rules = new Dictionary<int, HashSet<int>>();
        
        foreach (var rule in firstSection.EnumerateLines())
        {
            var separatorIndex = rule.IndexOf('|');

            var firstNumber = int.Parse(rule[..separatorIndex]);
            var secondNumber = int.Parse(rule[(separatorIndex + 1)..]);
            if (!rules.TryAdd(firstNumber, [secondNumber]))
            {
                rules[firstNumber].Add(secondNumber);
            }
        }
        
        var sumOfMiddlePages = 0;
        
        foreach (var update in secondSection.EnumerateLines())
        {
            var pageCount = 0;
            foreach (var _ in update.Split(','))
                pageCount++;

            var pageIndices = new Dictionary<int, int>(pageCount);
            var index = 0;
            var middlePage = 0;
            var valid = true;

            foreach (var pageRange in update.Split(','))
            {
                var page = int.Parse(update[pageRange]);
                
                pageIndices[page] = index;
                
                if (index == pageCount / 2)
                    middlePage = page;
                
                foreach (var rule in rules.Where(rule => rule.Key == page || rule.Value.Contains(page)))
                {
                    foreach (var afterPage in rule.Value)
                    {
                        if (!pageIndices.TryGetValue(rule.Key, out var beforeIndex) ||
                            !pageIndices.TryGetValue(afterPage, out var afterIndex) ||
                            beforeIndex < afterIndex) continue;
                        valid = false;
                        break;
                    }
                }

                if (!valid)
                    break;

                index++;
            }

            if (valid)
            {
                sumOfMiddlePages += middlePage;
            }

        }
        
        return sumOfMiddlePages.ToString();
    }

    public override string Part2Solver()
    {
        throw new NotImplementedException();
    }
}