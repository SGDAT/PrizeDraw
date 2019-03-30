﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace PrizeDraw.Logic
{
    public class Rank
    {
        public static string NthRank(string st, int[] we, int n)
        {
            //requirement from notes
            if (string.IsNullOrEmpty(st))
                return "No participants";

            //get list of participants from string
            string[] participants = st.Split(',');

            //requirement from notes
            if (participants.Length < we.Length)
                return "Not enough participants";

            Dictionary<string, int> participantsNumbers = new Dictionary<string, int>();

            for (int i = 0; i < participants.Length; i++)
            {
                string participant = participants[i];
                int sumOfRanks = GetSumOfRanks(participant);
                int participantN = participant.Length + sumOfRanks;
                participantsNumbers[participant] = participantN * we[i];
            }

            //order the list decending by value
            IOrderedEnumerable<KeyValuePair<string, int>> orderedParticipantsNumbers = participantsNumbers.OrderByDescending(p => p.Value);
            KeyValuePair<string, int> winner = orderedParticipantsNumbers.ElementAt(n - 1);

            IOrderedEnumerable<KeyValuePair<string, int>> orderedByNameParticipantsNumbers = orderedParticipantsNumbers.Where(p => p.Value == winner.Value);

            return winner;
        }

        private static int GetSumOfRanks(string participant)
        {
            int sumOfRanks = 0;
            foreach(char character in participant)
            {
                sumOfRanks += GetAlphabetRank(character);
            }

            return sumOfRanks;
        }

        private static int GetAlphabetRank(char r)
        {
            return char.ToUpper(r) - 64;
        }
    }
}
