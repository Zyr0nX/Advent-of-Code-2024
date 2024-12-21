namespace AdventOfCode;

public partial class SolutionDay14() : SolutionBase(14)
{
    public override string Part1Solver()
    {
        const int m = 103;
        const int n = 101;
        var inputSpan = Input.AsSpan();
        var topLeft = 0;
        var topRight = 0;
        var bottomLeft = 0;
        var bottomRight = 0;
        foreach (var robot in inputSpan.EnumerateLines())
        {
            var i = 0;
            var positionX = 0;
            var positionY = 0;
            var velocityX = 0;
            var velocityY = 0;
            foreach (var partRange in robot.Split(' '))
            {
                var part = robot[partRange];
                var comma = part.IndexOf(',');
                switch (i)
                {
                    case 0:
                        positionX = int.Parse(part[2..comma]);
                        positionY = int.Parse(part[(comma + 1)..]);
                        break;
                    case 1:
                        velocityX = int.Parse(part[2..comma]);
                        velocityY = int.Parse(part[(comma + 1)..]);
                        break;
                }

                i++;
            }

            var positionXAfter100 = Mod(positionX + velocityX * 100, n);
            var positionYAfter100 = Mod(positionY + velocityY * 100, m);

            if (positionXAfter100 / ((n - 1) / 2F) == 1 || positionYAfter100 / ((m - 1) / 2F) == 1)
            {
                continue;
            }
            var isLeft = positionXAfter100 / ((n - 1) / 2F) < 1;
            var isTop = positionYAfter100 / ((m - 1) / 2F) < 1;

            if (isTop && isLeft)
            {
                topLeft += 1;
            }

            if (!isTop && isLeft)
            {
                bottomLeft += 1;
            }

            if (isTop && !isLeft)
            {
                topRight += 1;
            }

            if (!isTop &&!isLeft)
            {
                bottomRight++;
            }
        }

        var res = (long)topLeft * topRight * bottomLeft * bottomRight;
        return res.ToString();
    }

    private static int Mod(int x, int m) {
        var r = x%m;
        return r<0 ? r+m : r;
    }

    public override string Part2Solver()
    {
        const int m = 103;
        const int n = 101;
        var inputSpan = Input.AsSpan();
        
        var seconds = 0;
        var set = new HashSet<(int, int)>();
        while (true)
        {
            set.Clear();
            var overlap = false;
            foreach (var robot in inputSpan.EnumerateLines())
            {
                var i = 0;
                var positionX = 0;
                var positionY = 0;
                var velocityX = 0;
                var velocityY = 0;
                foreach (var partRange in robot.Split(' '))
                {
                    var part = robot[partRange];
                    var comma = part.IndexOf(',');
                    switch (i)
                    {
                        case 0:
                            positionX = int.Parse(part[2..comma]);
                            positionY = int.Parse(part[(comma + 1)..]);
                            break;
                        case 1:
                            velocityX = int.Parse(part[2..comma]);
                            velocityY = int.Parse(part[(comma + 1)..]);
                            break;
                    }

                    i++;
                }

                var newPositionX = Mod(positionX + velocityX * seconds, n);
                var newPositionY = Mod(positionY + velocityY * seconds, m);
                
                if (set.Add((newPositionX, newPositionY))) continue;
                overlap = true;
                break;
            }

            if (!overlap)
            {
                break;
            }

            seconds++;
        }
        return seconds.ToString();
    }
}