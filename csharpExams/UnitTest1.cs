namespace csharpExams;

public class UnitTest1
{
    [Fact]
    public void Test02()
    {
        var res = CheckPattern("++*{5} jtggggg");
        Assert.Equal(res, true);
    }

    [Fact]
    public void Test01()
    {
        var res = CheckPattern("+++++* abcdehhhhhh");
        Assert.Equal(res, false);
    }

    [Fact]
    public void Test00()
    {
        var res = CheckPattern("+++++$ abcde1");
        Assert.Equal(res, true);
    }

    [Fact]
    public void Test000()
    {
        var res = CheckPattern("+++++* abcdehhhhh");
        Assert.Equal(res, false);
        var res2 = CheckPattern("+++++* abcdehhh");
        Assert.Equal(res2, true);
    }

    [Fact]
    public void Test0000()
    {
        var res = CheckPattern("$**+*{2} 9mmmrrrkbb");
        Assert.Equal(res, true);
    }

    static bool CheckPattern(string str)
    {
        var rawStr = str.Split(' ');
        var pattern = rawStr[0].Replace("{2}", "");
        var word = rawStr[1];

        var skipWordIndex = 0;

        for (int i = 0; i < pattern.Length; i++)
        {
            var p = pattern[i];
            var w = word[i + skipWordIndex];
            var isLetter = (p == '+');
            var isDigit = (p == '$');
            var isAny = (p == '*');

            // check match per char
            if (isLetter && !Char.IsLetter(w))
                return false;

            if (isDigit && !Char.IsDigit(w))
                return false;

            if (isAny)
            {
                // check if next p char is {N}
                // else check if multiple of 3
                int count = 0;
                var sub = word.Substring(i + skipWordIndex);
                foreach (var c in sub)
                {
                    if (w == c)
                        count++;
                    else
                        break;

                    if (count > 3)
                        return false;
                }

                skipWordIndex += count - 1;
            }
        }
        return true;
    }
}

public class UnitTest2
{
    [Fact]
    public void Test1()
    {
        var res = CountAnagram("aa aa odg dog gdo");
        Assert.Equal(res, 2);
    }

    [Fact]
    public void Test2()
    {
        var res = CountAnagram("a c b c run urn urn");
        Assert.Equal(res, 1);
    }

    [Fact]
    public void Test3()
    {
        var res = CountAnagram("mom omm mmo pop opp");
        Assert.Equal(res, 3);
    }


    private static int CountAnagram(string input)
    {
        var rawWords = input.Split(' ');
        var uniqueWords = new List<string>();
        var anagrams = new Dictionary<string, int>();
        var count = 0;

        // get unique words
        foreach (var w in rawWords)
        {
            var word = new string(w.Distinct().ToArray());
            if (word.Length == 1)
                continue;
            uniqueWords.Add(w);
        }
        uniqueWords = uniqueWords.Distinct().ToList();

        // sort word then check if it does exist on dict
        foreach (var w in uniqueWords)
        {
            var sortedWord1 = string.Concat(w.OrderBy(x => x));
            if (anagrams.ContainsKey(sortedWord1))
                anagrams[sortedWord1] = anagrams.GetValueOrDefault(sortedWord1) + 1;
            else
                anagrams.Add(sortedWord1, 1);
        }

        foreach (var d in anagrams)
        {
            if (d.Value > 1)
                count += d.Value - 1;
        }
        return count;
    }
}