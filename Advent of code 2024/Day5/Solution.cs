namespace Advent_of_code_2024;

public class SolutionDay5() : SolutionBase(5)
{
    public override string Part1Solver()
    {
        var spanInput = Input.AsSpan();
        var sectionSeparatorIdx = spanInput.IndexOf($"{Environment.NewLine}{Environment.NewLine}");
        var firstSection = spanInput[..sectionSeparatorIdx];
        var secondSection = spanInput[(sectionSeparatorIdx + Environment.NewLine.Length * 2)..];

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
        var pageIndices = new Dictionary<int, int>();
        foreach (var update in secondSection.EnumerateLines())
        {
            pageIndices.Clear();
            var pageCount = update.Count(',') + 1;
            
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
    
    public string Part1SolverHashSet()
    {
        var spanInput = Input.AsSpan();
        var sectionSeparatorIdx = spanInput.IndexOf($"{Environment.NewLine}{Environment.NewLine}");
        var firstSection = spanInput[..sectionSeparatorIdx];
        var secondSection = spanInput[(sectionSeparatorIdx + Environment.NewLine.Length * 2)..];

        var rules = new HashSet<(int, int)>();
        
        foreach (var rule in firstSection.EnumerateLines())
        {
            var separatorIndex = rule.IndexOf('|');

            var firstNumber = int.Parse(rule[..separatorIndex]);
            var secondNumber = int.Parse(rule[(separatorIndex + 1)..]);
            rules.Add((firstNumber, secondNumber));
        }
        
        var sumOfMiddlePages = 0;
        
        foreach (var update in secondSection.EnumerateLines())
        {
            var pageCount = update.Count(',') + 1;
            var i = 0;
            var middlePage = 0;
            var valid = true;
            var prevPage = -1;
            foreach (var pageRange in update.Split(','))
            {
                var page = int.Parse(update[pageRange]);
                
                if (i == pageCount / 2)
                    middlePage = page;

                if (rules.Contains((page, prevPage)))
                {
                    valid = false;
                    break;
                    
                };
                prevPage = page;
                i++;

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
        var spanInput = Input.AsSpan();
        var sectionSeparatorIdx = spanInput.IndexOf($"{Environment.NewLine}{Environment.NewLine}");
        var firstSection = spanInput[..sectionSeparatorIdx];
        var secondSection = spanInput[(sectionSeparatorIdx + Environment.NewLine.Length * 2)..];

        var rules = new HashSet<(int, int)>();
        
        foreach (var rule in firstSection.EnumerateLines())
        {
            var separatorIndex = rule.IndexOf('|');

            var firstNumber = int.Parse(rule[..separatorIndex]);
            var secondNumber = int.Parse(rule[(separatorIndex + 1)..]);
            rules.Add((firstNumber, secondNumber));
        }
        
        var sumOfMiddlePages = 0;
        
        foreach (var update in secondSection.EnumerateLines())
        {
            var pageCount = update.Count(',') + 1;
            var pages = new int[pageCount];

            var i = 0;
            var prevPage = -1;
            
            foreach (var pageRange in update.Split(','))
            {
                var page = int.Parse(update[pageRange]);
                pages[i] = page;
                i++;
            }

            i = 0;
            foreach (var pageRange in update.Split(','))
            {
                var page = int.Parse(update[pageRange]);
                
                if (rules.Contains((page, prevPage)))
                {
                    Array.Sort(pages, (a, b) =>
                    {
                        if (rules.Contains((a, b)))
                            return -1;
                        if (rules.Contains((b, a)))
                            return 1;
                        return 0;
                    });
                    
                    var middlePageIndex = pages.Length / 2;
                    sumOfMiddlePages += pages[middlePageIndex];
                    break;
                    
                };
                
                prevPage = page;
                pages[i] = page;
                i++;
            }
        }
        
        return sumOfMiddlePages.ToString();
    }
    
    public string Part2SolverHashSet()
    {
        var spanInput = Input.AsSpan();
        var sectionSeparatorIdx = spanInput.IndexOf($"{Environment.NewLine}{Environment.NewLine}");
        var firstSection = spanInput[..sectionSeparatorIdx];
        var secondSection = spanInput[(sectionSeparatorIdx + Environment.NewLine.Length * 2)..];

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
            
            var pages = new int[pageCount];

            var i = 0;
            foreach (var pageRange in update.Split(','))
            {
                var page = int.Parse(update[pageRange]);
                pages[i] = page;
                i++;
            }

            if (!IsValidOrder(pages))
            {
                Array.Sort(pages, (a, b) =>
                {
                    if (rules.TryGetValue(a, out var dependentPagesA) && dependentPagesA.Contains(b))
                        return -1;
                    if (rules.TryGetValue(b, out var dependentPagesB) && dependentPagesB.Contains(a))
                        return 1;
                    return a.CompareTo(b);
                });
                
                var middlePageIndex = pages.Length / 2;
                sumOfMiddlePages += pages[middlePageIndex];
            }

            continue;

            bool IsValidOrder(int[] pagesToCheck)
            {
                for (var currentIndex = 0; currentIndex < pagesToCheck.Length; currentIndex++)
                {
                    foreach (var rule in  rules.Where(rule => rule.Key == pagesToCheck[currentIndex] || rule.Value.Contains(pagesToCheck[currentIndex])))
                    {
                        foreach (var afterPage in rule.Value)
                        {
                            var beforeIndex = Array.IndexOf(pagesToCheck, rule.Key);
                            var afterIndex = Array.IndexOf(pagesToCheck, afterPage);

                            if (beforeIndex >= 0 && afterIndex >= 0 && beforeIndex >= afterIndex)
                            {
                                return false;
                            }
                        }
                    }
                }
                return true;
            }
        }
        
        return sumOfMiddlePages.ToString();
    }
}