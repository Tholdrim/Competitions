using Microsoft.Z3;
using System.Numerics;

namespace AdventOfCode2025
{
    [TestClass]
    public class Day10
    {
        [TestMethod]
        [DataRow("Sample 10.txt", 7, 33, DisplayName = "Sample")]
        [DataRow("Input 10.txt", 488, 18771, DisplayName = "Input")]
        public void Solve(string fileName, int expectedResult1, int expectedResult2)
        {
            var (result1, result2) = (0, 0);

            foreach (var line in File.ReadLines(fileName))
            {
                var (indicatorLights, buttons, joltage) = ParseMachine(line);

                result1 += FindMinPresses(buttons, indicatorLights);
                result2 += FindMinPressesForJoltage(buttons, joltage);
            }

            Assert.AreEqual(expectedResult1, result1);
            Assert.AreEqual(expectedResult2, result2);
        }

        private static int FindMinPresses(List<int[]> buttons, string indicatorLights)
        {
            var targetPattern = 0;
            var bitWidth = indicatorLights.Length;

            foreach (var indicatorChar in indicatorLights)
            {
                targetPattern = (targetPattern << 1) | (indicatorChar == '#' ? 1 : 0);
            }

            var buttonBitMasks = new int[buttons.Count];

            for (var buttonIndex = 0; buttonIndex < buttons.Count; buttonIndex++)
            {
                var mask = 0;

                foreach (var lightIndex in buttons[buttonIndex])
                {
                    mask |= 1 << (bitWidth - lightIndex - 1);
                }

                buttonBitMasks[buttonIndex] = mask;
            }

            var buttonCount = buttonBitMasks.Length;
            var bestPressCount = int.MaxValue;

            for (var subsetMask = 0; subsetMask < (1 << buttonCount); subsetMask++)
            {
                var currentPattern = 0;

                for (var buttonIndex = 0; buttonIndex < buttonCount; buttonIndex++)
                {
                    if (((subsetMask >> buttonIndex) & 1) != 0)
                    {
                        currentPattern ^= buttonBitMasks[buttonIndex];
                    }
                }

                if (currentPattern == targetPattern)
                {
                    var pressCount = BitOperations.PopCount((uint)subsetMask);

                    if (pressCount < bestPressCount)
                    {
                        bestPressCount = pressCount;
                    }
                }
            }

            return bestPressCount;
        }

        // This breaks my rule about not using external libraries, but I don't have a better idea...
        private static int FindMinPressesForJoltage(List<int[]> buttons, List<int> joltageRequirements)
        {
            var counterCount = joltageRequirements.Count;
            var buttonCount = buttons.Count;

            if (counterCount == 0 || buttonCount == 0)
            {
                return 0;
            }

            using var context = new Context();
            using var optimize = context.MkOptimize();

            var buttonPressVariables = new IntExpr[buttonCount];

            for (var i = 0; i < buttonCount; ++i)
            {
                buttonPressVariables[i] = context.MkIntConst($"button_{i}");
                optimize.Add(context.MkGe(buttonPressVariables[i], context.MkInt(0)));
            }

            for (var i = 0; i < counterCount; ++i)
            {
                var affectingButtonTerms = new List<ArithExpr>();

                for (var buttonIndex = 0; buttonIndex < buttonCount; buttonIndex++)
                {
                    if (buttons[buttonIndex].Contains(i))
                    {
                        affectingButtonTerms.Add(buttonPressVariables[buttonIndex]);
                    }
                }

                var sumForCounter = affectingButtonTerms.Count == 1
                    ? affectingButtonTerms[0]
                    : context.MkAdd([.. affectingButtonTerms]);

                optimize.Add(context.MkEq(sumForCounter, context.MkInt(joltageRequirements[i])));
            }

            var totalPressCount = context.MkAdd(buttonPressVariables);

            optimize.MkMinimize(totalPressCount);

            var status = optimize.Check();
            var evaluatedTotalPressCount = optimize.Model.Evaluate(totalPressCount).Simplify();

            return ((IntNum)evaluatedTotalPressCount).Int;
        }

        private static (string IndicatorLights, List<int[]> Buttons, List<int> Joltage) ParseMachine(string line)
        {
            var data = line.Split(' ');
            var buttons = data[1..^1].Select(d => d[1..^1].Split(',').Select(int.Parse).ToArray()).ToList();
            var joltage = data[^1][1..^1].Split(',').Select(int.Parse).ToList();

            return (data[0][1..^1], buttons, joltage);
        }
    }
}
