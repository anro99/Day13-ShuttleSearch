using System;
using System.Collections.Generic;
using System.Text;

namespace Day13_ShuttleSearch
{
    class Bus
    {
        public Bus(long a_id, long a_position)
        {
            ID = a_id;
            Position = a_position;
            //PrimeFactorsOfId = Primfactors(a_id);
        }

        public long ID { get; private set; }
        public long Position { get; private set; }

        public List<long> PrimeFactorsOfId{ get; private set; }

        public long NextDeparture(long a_time)
        {
            Math.DivRem(a_time, ID, out var reminder);
            if (reminder == 0)
                return 0;

            return ID - reminder;
        }

        private static List<long> Primfactors(long a_number)
        {
            List<long> factors = new List<long>();
            long nextNumber = a_number;
            while(true)
            {
                var factor = LowestPrimfactor(nextNumber, out nextNumber);
                factors.Add(factor);
                if (nextNumber == 1)
                    return factors;
            }
        }

        private static long LowestPrimfactor(long a_number, out long a_reminingNumber)
        {
            foreach(var prime in m_prims)
            {
                a_reminingNumber = Math.DivRem(a_number, prime, out var reminder);
                if (reminder == 0)
                    return prime;
            }
            throw new Exception("Need more primes");
        }

        private static List<long> m_prims = new List<long> { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97,
        101, 103, 107, 109, 113, 127, 131, 137, 139, 149, 151, 157, 163, 167, 173, 179, 181, 191, 193, 197, 199,
        211, 223, 227, 229, 233, 239, 241, 251, 257, 263, 269, 271, 277, 281, 283, 293,
        307, 311, 313, 317, 331, 337, 347, 349, 353, 359, 367, 373, 379, 383, 389, 397,
        401, 409, 419, 421, 431, 433, 439, 443, 449, 457, 461, 463, 467, 479, 487, 491, 499,
        503, 509, 521, 523, 541, 547, 557, 563, 569, 571, 577, 587, 593, 599,
        601, 607, 613, 617, 619, 631, 641, 643, 647, 653, 659, 661, 673, 677, 683, 691,
        701, 709, 719, 727, 733, 739, 743, 751, 757, 761, 769, 773, 787, 797,
        809, 811, 821, 823, 827, 829, 839, 853, 857, 859, 863, 877, 881, 883, 887,
        907, 911, 919, 929, 937, 941, 947, 953, 967, 971, 977, 983, 991, 997};


        /*
        5,7

        5 10 15 20 25 30 35 40 45 50 55 60 65 70 75 80 85 90 95 100
        7 14 21 28 35 42 49 56 63 70 77 84 91

        5 * 7 = 35



         */
    }
}
