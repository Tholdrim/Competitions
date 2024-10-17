using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Competition
{
    internal class Program
    {
        [SuppressMessage("Style", "IDE0060:Remove unused parameter")]
        public static void Main(string[] arguments)
        {
            var inputs = new List<ulong>();
            var operations = new Dictionary<int, Operation>();

            var @base = int.Parse(Console.ReadLine());
            var expectedValue = Convert.ToUInt64(Console.ReadLine(), 2);

            var outputId = 0;
            var maxValue = @base switch
            {
                16 => ushort.MaxValue,
                32 => uint.MaxValue,
                64 => ulong.MaxValue,
                _  => throw new NotImplementedException()
            };

            var n = int.Parse(Console.ReadLine());

            for (var i = 0; i < n; ++i)
            {
                var operation = Operation.Parse(Console.ReadLine());

                if (operation is InputOperation)
                {
                    inputs.Add(0UL);
                }
                else if (operation is UnaryOperation unaryOperation && unaryOperation.Operator == "OUTPUT")
                {
                    outputId = operation.Id;
                }

                operations.Add(operation.Id, operation);
            }

            FindSolution(operations, inputs, outputId, expectedValue, maxValue);

            foreach (var input in inputs)
            {
                var line = Convert.ToString((long)input, 2).PadLeft(@base, '0');

                Console.WriteLine(line);
            }
        }

        private static void FindSolution(IDictionary<int, Operation> operations, IList<ulong> inputs, int currentIndex, ulong expectedValue, ulong maxValue)
        {
            var currentOperation = operations[currentIndex];

            if (currentOperation is InputOperation inputOperation)
            {
                inputs[inputOperation.Id] = expectedValue;
            }
            else if (currentOperation is UnaryOperation unaryOperation)
            {
                var newExpectedValue = unaryOperation.Operator switch
                {
                    "NOT"         => ~expectedValue & maxValue,
                    "LEFT_SHIFT"  => (expectedValue & 1UL) == 0UL ? (expectedValue >> 1) & maxValue : throw new Exception(),
                    "RIGHT_SHIFT" => (maxValue - (maxValue / 2) & expectedValue) == 0UL ? (expectedValue << 1) & maxValue : throw new Exception(),
                    "OUTPUT"      => expectedValue,
                    _             => throw new NotImplementedException()
                };

                FindSolution(operations, inputs, unaryOperation.InputId, newExpectedValue, maxValue);
            }
            else if (currentOperation is BinaryOperation binaryOperation)
            {
                var (leftExpectedValue, rightExpectedValue) = binaryOperation.Operator switch
                {
                    "AND"         => (expectedValue, expectedValue),
                    "OR"          => (0UL, expectedValue),
                    "XOR"         => ((expectedValue ^ 0UL) & maxValue, 0UL),
                    "SUM"         => (0UL, expectedValue),
                    _             => throw new NotImplementedException()
                };

                try
                {
                    FindSolution(operations, inputs, binaryOperation.LeftInputId, leftExpectedValue, maxValue);
                    FindSolution(operations, inputs, binaryOperation.RightInputId, rightExpectedValue, maxValue);
                }
                catch
                {
                    FindSolution(operations, inputs, binaryOperation.LeftInputId, rightExpectedValue, maxValue);
                    FindSolution(operations, inputs, binaryOperation.RightInputId, leftExpectedValue, maxValue);
                }
            }
        }

        internal abstract class Operation
        {
            public int Id { get; }

            protected Operation(string[] data)
            {
                Id = int.Parse(data[1]);
            }

            public static Operation Parse(string line)
            {
                var data = line.Split();

                return data[0] switch
                {
                    "INPUT"       => new InputOperation(data),
                    "OUTPUT"      => new UnaryOperation(data),
                    "NOT"         => new UnaryOperation(data),
                    "LEFT_SHIFT"  => new UnaryOperation(data),
                    "RIGHT_SHIFT" => new UnaryOperation(data),
                    "AND"         => new BinaryOperation(data),
                    "OR"          => new BinaryOperation(data),
                    "XOR"         => new BinaryOperation(data),
                    "SUM"         => new BinaryOperation(data),
                    _             => throw new NotImplementedException()
                };
            }
        }

        internal class InputOperation : Operation
        {
            internal InputOperation(string[] data)
               : base(data)
            {
            }
        }

        internal class UnaryOperation : Operation
        {
            public string Operator { get; }

            public int InputId { get; }

            internal UnaryOperation(string[] data)
               : base(data)
            {
                (Operator, InputId) = (data[0], int.Parse(data[3]));
            }
        }

        internal class BinaryOperation : Operation
        {
            public string Operator { get; }

            public int LeftInputId { get; }

            public int RightInputId { get; }

            internal BinaryOperation(string[] data)
               : base(data)
            {
                (Operator, LeftInputId, RightInputId) = (data[0], int.Parse(data[3]), int.Parse(data[4]));
            }
        }
    }
}
