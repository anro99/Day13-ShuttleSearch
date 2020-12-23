using System;
using System.Collections.Generic;
using System.Text;

namespace Day13_ShuttleSearch.Door23
{
    internal class Ring
    {
        private List<Cup> m_cups = new List<Cup>();

        public static Ring Create( string a_line)
        {
            var ring = new Ring();

            foreach(var strNum in a_line.ToCharArray())
            {
                if (!int.TryParse($"{strNum}", out var nummer))
                    return null;

                ring.m_cups.Add(new Cup(nummer));
            }

            for(int i = 0; i < ring.m_cups.Count; i++)
            {
                if (i != ring.m_cups.Count - 1)
                    ring.m_cups[i].Next = ring.m_cups[i + 1];
                else
                    ring.m_cups[i].Next = ring.m_cups[0];
            }

            ring.CurrentCup = ring.m_cups[0];
            return ring;
        }

        public string RepresentationAsString()
        {
            var sb = new StringBuilder();
            var actCup = CurrentCup;
            while(actCup.Next != CurrentCup)
            {
                sb.Append($"{actCup.Number}");
                actCup = actCup.Next;
            }
            return sb.ToString();
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

        public Cup FindNextLowerOrGreatest(int a_cupNumber)
        {
            var greatest = CurrentCup;
            Cup greatestLower = null;
            var actual = CurrentCup.Next;
            while(actual != CurrentCup)
            {
                if (actual.Number > greatest.Number)
                    greatest = actual;
                if (actual.Number < a_cupNumber)
                {
                    if ( greatestLower == null || actual.Number > greatestLower.Number)
                        greatestLower = actual;
                }
                actual = actual.Next;
            }

            if (greatestLower != null)
                return greatestLower;

            return greatest;
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
    }
}
