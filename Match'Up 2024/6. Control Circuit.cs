using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Competition
{
    internal class Program
    {
        private static ulong MaxValue { get; set; }

        private static IList<ulong> Inputs { get; } = new List<ulong>();

        private static IDictionary<int, Operation> Operations { get; } = new Dictionary<int, Operation>();

        [SuppressMessage("Style", "IDE0060:Remove unused parameter")]
        public static void Main(string[] arguments)
        {
            var @base = int.Parse(Console.ReadLine());
            var desiredValue = Convert.ToUInt64(Console.ReadLine(), 2);
            var n = int.Parse(Console.ReadLine());

            for (var i = 0; i < n; ++i)
            {
                var operation = Operation.Parse(Console.ReadLine());

                if (operation is InputOperation)
                {
                    Inputs.Add(0UL);
                }

                Operations.Add(operation.Id, operation);
            }

            MaxValue = GetMaxValueByBase(@base);

            var outputId = Operations.Values.OfType<UnaryOperation>().Where(o => o.Operator == "OUTPUT").Select(o => o.Id).Single();

            VisitOperation(outputId, desiredValue);

            foreach (var input in Inputs)
            {
                var line = Convert.ToString((long)input, 2).PadLeft(@base, '0');

                Console.WriteLine(line);
            }
        }

        private static ulong GetMaxValueByBase(int @base) => @base switch
        {
            16 => ushort.MaxValue,
            32 => uint.MaxValue,
            64 => ulong.MaxValue,
            _  => throw new NotImplementedException()
        };

        private static ulong VisitOperation(int currentIndex, ulong desiredValue)
        {
            var currentOperation = Operations[currentIndex];

            return currentOperation switch
            {
                InputOperation inputOperation   => VisitInputOperation(inputOperation, desiredValue),
                UnaryOperation unaryOperation   => VisitUnaryOperation(unaryOperation, desiredValue),
                BinaryOperation binaryOperation => VisitBinaryOperation(binaryOperation, desiredValue),
                _                               => throw new NotImplementedException()
            };
        }

        private static ulong VisitInputOperation(InputOperation inputOperation, ulong desiredValue)
        {
            return Inputs[inputOperation.Id] = desiredValue;
        }

        private static ulong VisitUnaryOperation(UnaryOperation unaryOperation, ulong desiredValue)
        {
            switch (unaryOperation.Operator)
            {
                case "OUTPUT":
                {
                    var value = VisitOperation(unaryOperation.InputId, desiredValue);
                        
                    return value;
                }

                case "NOT":
                {
                    var value = VisitOperation(unaryOperation.InputId, ~desiredValue & MaxValue);

                    return ~value & MaxValue;
                }

                case "LEFT_SHIFT":
                {
                    var value = VisitOperation(unaryOperation.InputId, desiredValue >> 1);

                    return value << 1 & MaxValue;
                }

                case "RIGHT_SHIFT":
                {
                    var value = VisitOperation(unaryOperation.InputId, desiredValue << 1 & MaxValue);

                    return value >> 1;
                }

                default:
                    throw new NotImplementedException();
            }
        }

        private static ulong VisitBinaryOperation(BinaryOperation binaryOperation, ulong desiredValue)
        {
            switch (binaryOperation.Operator)
            {
                case "AND":
                {
                    var leftValue = VisitOperation(binaryOperation.LeftInputId, desiredValue);
                    var rightValue = VisitOperation(binaryOperation.RightInputId, desiredValue);

                    return leftValue & rightValue;
                }

                case "OR":
                {
                    var leftValue = VisitOperation(binaryOperation.LeftInputId, desiredValue);
                    var rightValue = VisitOperation(binaryOperation.RightInputId, desiredValue);

                    return leftValue | rightValue;
                }

                case "XOR":
                {
                    var leftValue = VisitOperation(binaryOperation.LeftInputId, desiredValue);
                    var rightValue = VisitOperation(binaryOperation.RightInputId, desiredValue ^ leftValue);

                    return leftValue ^ rightValue;
                }

                case "SUM":
                {
                    var leftValue = VisitOperation(binaryOperation.LeftInputId, desiredValue);
                    var rightValue = VisitOperation(binaryOperation.RightInputId, desiredValue - leftValue);

                    return leftValue + rightValue & MaxValue;
                }

                default:
                    throw new NotImplementedException();
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
