using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace MatchUp2024.Task06
{
    public class Program
    {
        [SuppressMessage("Style", "IDE0060:Remove unused parameter")]
        public static void Main(string[] arguments)
        {
            var @base = int.Parse(Console.ReadLine());
            var desiredValue = Convert.ToUInt64(Console.ReadLine(), 2);
            var n = int.Parse(Console.ReadLine());

            var inputs = new List<ulong>();
            var operations = new Dictionary<int, (int Id, string Operator, int? LeftInputId, int? RightInputId)>();

            for (var i = 0; i < n; ++i)
            {
                var operation = ParseOperation(Console.ReadLine());

                if (operation.Operator == "INPUT")
                {
                    inputs.Add(0UL);
                }

                operations.Add(operation.Id, operation);
            }

            var maxValue = GetMaxValue(@base);
            var outputId = operations.Values.Where(o => o.Operator == "OUTPUT").Select(o => o.Id).Single();

            VisitOperation(outputId, desiredValue);

            foreach (var input in inputs.Select(i => Convert.ToString((long)i, 2).PadLeft(@base, '0')))
            {
                Console.WriteLine(input);
            }

            ulong VisitOperation(int currentIndex, ulong desiredValue) => operations[currentIndex] switch
            {
                (int id, "INPUT", null, null)                         => inputs[id] = desiredValue,
                (_, var @operator, int leftInputId, null)             => VisitUnaryOperation(@operator, leftInputId, desiredValue),
                (_, var @operator, int leftInputId, int rightInputId) => VisitBinaryOperation(@operator, leftInputId, rightInputId, desiredValue),
                _                                                     => throw new NotImplementedException()
            };

            ulong VisitUnaryOperation(string @operator, int inputId, ulong desiredValue) => @operator switch
            {
                "OUTPUT"      => VisitOperation(inputId, desiredValue),
                "NOT"         => ~VisitOperation(inputId, ~desiredValue & maxValue) & maxValue,
                "LEFT_SHIFT"  => VisitOperation(inputId, desiredValue >> 1) << 1 & maxValue,
                "RIGHT_SHIFT" => VisitOperation(inputId, desiredValue << 1 & maxValue) >> 1,
                _             => throw new NotImplementedException()
            };

            ulong VisitBinaryOperation(string @operator, int leftInputId, int rightInputId, ulong desiredValue)
            {
                if (@operator == "AND")
                {
                    var leftValue = VisitOperation(leftInputId, desiredValue);
                    var rightValue = VisitOperation(rightInputId, desiredValue);

                    return leftValue & rightValue;
                }
                else if (@operator == "OR")
                {
                    var leftValue = VisitOperation(leftInputId, desiredValue);
                    var rightValue = VisitOperation(rightInputId, desiredValue & ~leftValue);

                    return leftValue | rightValue;
                }
                else if (@operator == "XOR")
                {
                    var leftValue = VisitOperation(leftInputId, desiredValue);
                    var rightValue = VisitOperation(rightInputId, desiredValue ^ leftValue);

                    return leftValue ^ rightValue;
                }
                else if (@operator == "SUM")
                {
                    var leftValue = VisitOperation(leftInputId, desiredValue);
                    var rightValue = VisitOperation(rightInputId, desiredValue - leftValue);

                    return leftValue + rightValue & maxValue;
                }

                throw new NotImplementedException();
            }
        }

        private static ulong GetMaxValue(int @base) => @base switch
        {
            16 => ushort.MaxValue,
            32 => uint.MaxValue,
            64 => ulong.MaxValue,
            _  => throw new NotImplementedException()
        };

        private static (int Id, string Operator, int? LeftInputId, int? RightInputId) ParseOperation(string line)
        {
            var data = line.Split();
            var leftInputId = data.Length >= 4 ? int.Parse(data[3]) : (int?)null;
            var rightInputId = data.Length >= 5 ? int.Parse(data[4]) : (int?)null;

            return (int.Parse(data[1]), data[0], leftInputId, rightInputId);
        }
    }
}
