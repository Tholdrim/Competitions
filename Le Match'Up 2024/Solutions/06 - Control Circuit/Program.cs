using System;
using System.Collections.Generic;
using System.Linq;

namespace LeMatchUp.Year2024.Task06
{
    public class Program
    {
        public static void Main(string[] _)
        {
            var bitWidth = int.Parse(Console.ReadLine());
            var targetValue = Convert.ToUInt64(Console.ReadLine(), 2);
            var nodeCount = int.Parse(Console.ReadLine());

            var inputValues = new List<ulong>();
            var circuitNodes = new (int Id, string Operation, int? LeftNodeId, int? RightNodeId)[nodeCount];

            for (var i = 0; i < nodeCount; ++i)
            {
                var node = ParseNode(Console.ReadLine());

                if (node.Operation == "INPUT")
                {
                    inputValues.Add(0UL);
                }

                circuitNodes[node.Id] = node;
            }

            var maxValue = bitWidth == 64 ? ulong.MaxValue : (1UL << bitWidth) - 1;
            var outputId = circuitNodes.Where(o => o.Operation == "OUTPUT").Select(o => o.Id).Single();

            PropagateNode(outputId, targetValue);

            foreach (var input in inputValues.Select(v => Convert.ToString(unchecked((long)v), 2).PadLeft(bitWidth, '0')))
            {
                Console.WriteLine(input);
            }

            ulong PropagateNode(int nodeId, ulong targetValue)
            {
                targetValue &= maxValue;

                var result = circuitNodes[nodeId] switch
                {
                    (_, "INPUT",       null,              null)            => inputValues[nodeId] = targetValue,
                    (_, var operation, int operandNodeId, null)            => PropagateUnaryNode(operation, operandNodeId, targetValue),
                    (_, var operation, int leftNodeId,    int rightNodeId) => PropagateBinaryNode(operation, leftNodeId, rightNodeId, targetValue),
                    _                                                      => throw new NotImplementedException()
                };

                return result & maxValue;
            }

            ulong PropagateUnaryNode(string operation, int operandNodeId, ulong targetValue) => operation switch
            {
                "OUTPUT"      => PropagateNode(operandNodeId, targetValue),
                "NOT"         => ~PropagateNode(operandNodeId, ~targetValue),
                "LEFT_SHIFT"  => PropagateNode(operandNodeId, targetValue >> 1) << 1,
                "RIGHT_SHIFT" => PropagateNode(operandNodeId, targetValue << 1) >> 1,
                _             => throw new NotImplementedException()
            };

            ulong PropagateBinaryNode(string operation, int leftNodeId, int rightNodeId, ulong targetValue)
            {
                var leftValue = PropagateNode(leftNodeId, targetValue);

                return operation switch
                {
                    "AND" => leftValue & PropagateNode(rightNodeId, targetValue | ~leftValue),
                    "OR"  => leftValue | PropagateNode(rightNodeId, targetValue & ~leftValue),
                    "XOR" => leftValue ^ PropagateNode(rightNodeId, targetValue ^ leftValue),
                    "SUM" => leftValue + PropagateNode(rightNodeId, targetValue - leftValue),
                    _     => throw new NotImplementedException()
                };
            }
        }

        private static (int Id, string Operation, int?, int?) ParseNode(string line)
        {
            var data = line.Split();
            var leftNodeId = data.Length >= 4 ? int.Parse(data[3]) : (int?)null;
            var rightNodeId = data.Length >= 5 ? int.Parse(data[4]) : (int?)null;

            return (int.Parse(data[1]), data[0], leftNodeId, rightNodeId);
        }
    }
}
