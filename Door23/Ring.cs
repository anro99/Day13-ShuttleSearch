using System;
using System.Collections.Generic;
using System.Text;

namespace Day13_ShuttleSearch.Door23
{
    internal class Ring
    {
        private Dictionary<int, Cup> m_cups = new Dictionary<int, Cup>();
        private int m_maxNummer = 0;

        public static Ring Create(string a_line, int a_fillUpTo)
        {
            var ring = new Ring();
            int maxNummer = 0;
            var cups = new List<Cup>();
            foreach(var strNum in a_line.ToCharArray())
            {
                if (!int.TryParse($"{strNum}", out var nummer))
                    return null;
                if (nummer > maxNummer)
                    maxNummer = nummer;

                var newCup = new Cup(nummer);
                cups.Add(newCup);
                ring.m_cups.Add(nummer, newCup);
            }

            for (int additional = maxNummer + 1; additional <= a_fillUpTo; additional++)
            {
                var newCup = new Cup(additional);
                cups.Add(newCup);
                ring.m_cups.Add(additional, newCup);
            }

            if (a_fillUpTo > maxNummer)
                ring.m_maxNummer = a_fillUpTo;
            else
                ring.m_maxNummer = maxNummer;

            for(int i = 0; i < cups.Count; i++)
            {
                if (i != cups.Count - 1)
                    cups[i].Next = cups[i + 1];
                else
                    cups[i].Next = cups[0];
            }

            ring.CurrentCup = cups[0];
            return ring;
        }

        public string RepresentationAsString()
        {
            var sb = new StringBuilder();
            var actCup = CurrentCup;
            while(actCup.Next != CurrentCup)
            {
                sb.Append($"{actCup.Number} ");
                actCup = actCup.Next;
            }
            return sb.ToString();
        }

        internal long MultiplayFollowersOfOne()
        {
            var cupOne = m_cups[1];
            long result = cupOne.Next.Number;
            return result * cupOne.Next.Next.Number;
        }

        public string ResultString()
        {
            var sb = new StringBuilder();
            var cupNo1 = CurrentCup;
            while (cupNo1.Next != CurrentCup)
            {
                if (cupNo1.Number == 1)
                    break;
                cupNo1 = cupNo1.Next;
            }

            if (cupNo1.Number != 1)
                return "cup 1 not found";

            var actCup = cupNo1.Next;
            while (actCup != cupNo1)
            {
                sb.Append($"{actCup.Number}");
                actCup = actCup.Next;
            }

            return sb.ToString();
        }


        public Cup CurrentCup { get; private set; }

        public Cup RemoveCups(int a_numberOfCups)
        {
            if (a_numberOfCups <= 0 || a_numberOfCups >= m_cups.Count - 1)
                return null;

            var firstCup = CurrentCup.Next;
            var lastCup = firstCup;

            for (int i = 0; i < a_numberOfCups - 1; i++)
                lastCup = lastCup.Next;

            CurrentCup.Next = lastCup.Next;
            lastCup.Next = null;
            return firstCup;
        }

        public Cup FindNextLowerOrGreatest(int a_cupNumber, Cup a_removedCups)
        {
            Dictionary<int, Cup> removed = new Dictionary<int, Cup>();
            while(a_removedCups != null)
            {
                removed.Add(a_removedCups.Number, a_removedCups);
                a_removedCups = a_removedCups.Next;
            }

            int nextLower = a_cupNumber - 1;
            while (nextLower >= 1 && removed.ContainsKey(nextLower))
                nextLower = nextLower - 1;

            if (nextLower != 0)
                return m_cups[nextLower];

            int greatest = m_maxNummer;
            while (greatest >= 1 && removed.ContainsKey(greatest))
                greatest = greatest - 1;

            if (greatest != 0)
                return m_cups[greatest];
            return null;
        }

        public void InsertCups(Cup a_insertAfter, Cup a_cupsToInsert)
        {
            var lastCup = a_cupsToInsert;
            while (lastCup.Next != null)
                lastCup = lastCup.Next;

            lastCup.Next = a_insertAfter.Next;
            a_insertAfter.Next = a_cupsToInsert;
        }

        public void StepToNext()
        {
            CurrentCup = CurrentCup.Next;
        }


        public void DoTheCrab(int a_turns, Action<string> a_trace)
        {
            string turnResult;
            for (var turns = 0; turns < a_turns; turns++)
            {
                var pickupCups = RemoveCups(3);
                InsertCups(FindNextLowerOrGreatest(CurrentCup.Number, pickupCups), pickupCups);
                StepToNext();
                if (a_trace != null)
                {
                    turnResult = RepresentationAsString();
                    a_trace(turnResult);
                }
            }
        }
    }
}
