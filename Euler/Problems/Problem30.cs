using Euler.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Euler.Problems
{
    class Problem30 : BaseProblem
    {
        public override string ProblemDescription
        {
            get
            {
                return @"Surprisingly there are only three numbers that can be written as the sum of fourth powers of their digits:

1634 = 1^4 + 6^4 + 3^4 + 4^4
8208 = 8^4 + 2^4 + 0^4 + 8^4
9474 = 9^4 + 4^4 + 7^4 + 4^4
As 1 = 1^4 is not a sum it is not included.

The sum of these numbers is 1634 + 8208 + 9474 = 19316.

Find the sum of all the numbers that can be written as the sum of fifth powers of their digits.";
            }
        }

        public override string ProblemName
        {
            get
            {
                return "Digit fifth powers";
            }
        }

        public override int ProblemNumber
        {
            get
            {
                return 30;
            }
        }

        protected override string Solve()
        {
            int result = 0;

            Parallel.For(2, 1000000, i =>
              {
                  IEnumerable<int> digits = NumberHelper.GetDigits(i);

                  int sumOfPowers = 0;

                  foreach (int digit in digits)
                  {
                      sumOfPowers += (int)Math.Pow(digit, 5);
                  }

                  if (sumOfPowers == i)
                  {
                      Interlocked.Add(ref result, i);
                  }
              });

            //for (int i = 2; i < 1000000; i++)
            //{
            //    IEnumerable<int> digits = NumberHelper.GetDigits(i);

            //    int sumOfPowers = 0;

            //    foreach (int digit in digits)
            //    {
            //        sumOfPowers += (int)Math.Pow(digit, 5);
            //    }

            //    if (sumOfPowers == i) result += i;
            //}

            return result.ToString("0");
        }
    }
}
