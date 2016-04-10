using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Euler.Helpers
{
    class PrimeHelper
    {
        public static IEnumerable<long> GeneratePrimes(long upperBound)
        {
            IList<long> primes = new List<long>();

            bool[] primeCandidates = SieveOfEratosthenes.RunSieve(upperBound);

            for (int i = 0; i < primeCandidates.Length - 1; i++)
            {
                if (primeCandidates[i]) primes.Add(i);
            }

            return primes;
        }

        public static bool IsPrime(long candidate)
        {
            if (candidate <= 1) return false;

            for (long i = (long)Math.Sqrt(candidate); i > 1; i--)
            {
                if (candidate % i == 0)
                {
                    return false;
                }
            }

            return true;
        }

        public static IEnumerable<long> Factorise(long number)
        {
            return Factorise(number, false);
        }

        public static IEnumerable<long> Factorise(long number, bool onlyPrimes)
        {
            IList<long> factors = new List<long>();

            long max = (long)Math.Sqrt(number);

            for (long factor = 2; factor <= max; factor++)
            { 
                if (number % factor == 0)
                {
                    factors.Add(factor);
                    if (factor != number / factor)
                    { 
                        factors.Add(number / factor);
                    }
                }
            }

            if (onlyPrimes)
            {
                return factors.Where(f => IsPrime(f));
            }
            return factors;
        }

        public static IEnumerable<long> GetProperDivisors(long number)
        {
            IList<long> factors = Factorise(number).ToList();

            factors.Remove(number);

            return factors;
        }
    }
}
