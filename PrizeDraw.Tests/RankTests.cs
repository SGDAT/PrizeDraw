using NUnit.Framework;
using PrizeDraw.Logic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
    [TestFixture]
    public static class RankTests
    {

        private static Random rnd = new Random();

        [Test]
        public static void test1()
        {
            string st = "";
            int[] we = new int[] { 4, 2, 1, 4, 3, 1, 2 };
            Assert.AreEqual("No participants", Rank.NthRank(st, we, 4));
            st = "Addison,Jayden,Sofia,Michael,Andrew,Lily,Benjamin";
            we = new int[] { 4, 2, 1, 4, 3, 1, 2 };
            Assert.AreEqual("Not enough participants", Rank.NthRank(st, we, 8));
            st = "Addison,Jayden,Sofia,Michael,Andrew,Lily,Benjamin";
            we = new int[] { 4, 2, 1, 4, 3, 1, 2 };
            Assert.AreEqual("Benjamin", Rank.NthRank(st, we, 4));
            st = "Elijah,Chloe,Elizabeth,Matthew,Natalie,Jayden";
            we = new int[] { 1, 3, 5, 5, 3, 6 };
            Assert.AreEqual("Matthew", Rank.NthRank(st, we, 2));
            st = "Aubrey,Olivai,Abigail,Chloe,Andrew,Elizabeth";
            we = new int[] { 3, 1, 4, 4, 3, 2 };
            Assert.AreEqual("Abigail", Rank.NthRank(st, we, 4));
            st = "Lagon,Lily";
            we = new int[] { 1, 5 };
            Assert.AreEqual("Lagon", Rank.NthRank(st, we, 2));
            st = "Elijah,Michael,Avery,Sophia,Samantha";
            we = new int[] { 2, 1, 5, 2, 2 };
            Assert.AreEqual("Sophia", Rank.NthRank(st, we, 3));
            st = "William,Willaim,Olivia,Olivai,Lily,Lyli";
            we = new int[] { 1, 1, 1, 1, 1, 1 };
            Assert.AreEqual("Willaim", Rank.NthRank(st, we, 1));
            st = "Addison,William,Jayden";
            we = new int[] { 3, 5, 6 };
            Assert.AreEqual("William", Rank.NthRank(st, we, 1));
            st = "Joshua,Grace,Isabella";
            we = new int[] { 1, 5, 4 };
            Assert.AreEqual("Isabella", Rank.NthRank(st, we, 1));
            st = "Elijah,Addison";
            we = new int[] { 3, 6 };
            Assert.AreEqual("Elijah", Rank.NthRank(st, we, 2));
            st = "Willaim,Liam,Daniel,Alexander";
            we = new int[] { 6, 4, 6, 2 };
            Assert.AreEqual("Daniel", Rank.NthRank(st, we, 2));
            st = "Avery,Olivai,Sophia,Michael,Elizabeth,Willaim,Liam";
            we = new int[] { 5, 5, 3, 2, 1, 3, 6 };
            Assert.AreEqual("Sophia", Rank.NthRank(st, we, 5));
            st = "Liam,Madison,Lyli,Jacob,Matthew,Michael";
            we = new int[] { 2, 6, 5, 5, 3, 4 };
            Assert.AreEqual("Liam", Rank.NthRank(st, we, 6));
            st = "Sophia,Robert,Abigail,Grace,Lagon";
            we = new int[] { 2, 6, 5, 5, 3, 4 };
            Assert.AreEqual("Sophia", Rank.NthRank(st, we, 5));
            st = "Samantha,Ella";
            we = new int[] { 5, 6 };
            Assert.AreEqual("Samantha", Rank.NthRank(st, we, 1));
            st = "Aubrey,Jayden";
            we = new int[] { 3, 4 };
            Assert.AreEqual("Aubrey", Rank.NthRank(st, we, 2));
            st = "Jacob,Elijah";
            we = new int[] { 4, 3 };
            Assert.AreEqual("Elijah", Rank.NthRank(st, we, 1));
        }
        private static string combstr(int n)
        {
            string exstr = "Sophia,Jacob,Isabella,Mason,Emma,William,Willaim,Olivia,Olivai,Jayden,Ava,Noah,Naoh,Emily,Michael,Abigail,Ethan,Madison,";
            exstr += "Alexander,Mia,Aiden,Chloe,Daniel,Elizabeth,Robert,Ella,Matthew,Addison,Elijah,Natalie,Joshua,Lily,Lyli,Liam,Grace,Andrew,Samantha,";
            exstr += "James,Avery,David,Sofia,Benjamin,Aubrey,Logan,Lagon,xxxxx,yyyyy,zzzzz";

            string res = "";
            string[] f = exstr.Split(',');
            ArrayList nb = new ArrayList();
            int i = 0;
            while (i < n)
            {
                int h = rnd.Next(1, 40);
                if (nb.IndexOf(h) == -1)
                {
                    nb.Add(h);
                    res += f[h] + ",";
                }
                i++;
            }
            return res.Substring(0, res.Length - 1);
        }
        private static int[] combnbr(int n)
        {
            ArrayList nb = new ArrayList();
            int i = 0;
            while (i < n)
            {
                int h = rnd.Next(1, 5);
                nb.Add(h);
                i++;
            }
            int[] res = nb.ToArray(typeof(int)) as int[];
            return res;
        }
        //-----------------------
        private static int GetValueSol(KeyValuePair<string, int> pair)
        {
            var pk = pair.Key;
            return pair.Value * (pk.Length + pk.ToUpper().Sum(d => d - 64)); ;
        }
        public static IOrderedEnumerable<KeyValuePair<string, int>> NthRankAuxSol(string[] lstr, int[] we, int n)
        {
            Dictionary<string, int> dic = lstr.Zip(we, (s, i) => new { s, i }).ToDictionary(item => item.s, item => item.i);
            Dictionary<string, int> dicW = new Dictionary<string, int>();
            foreach (var p in dic)
                dicW.Add(p.Key, GetValueSol(p));
            IOrderedEnumerable<KeyValuePair<string, int>> sC = dicW
                        .OrderByDescending(x => x.Value)
                        .ThenBy(kvp => kvp.Key);
            return sC;
        }
        private static string NthRankSol(string st, int[] we, int n)
        {
            if (st == "") return "No participants";
            string[] lstr = st.Split(',');
            if (n > lstr.Length) return "Not enough participants";
            IOrderedEnumerable<KeyValuePair<string, int>> sC = NthRankAuxSol(lstr, we, n);
            var i = 1;
            foreach (var p in sC)
            {
                if (i == n)
                    return p.Key;
                i++;
            }
            return null;
        }
        //-----------------------
        [Test]
        public static void RandomTest()
        {
            Console.WriteLine("Random Tests");
            for (int i = 0; i < 50; i++)
            {
                int h = rnd.Next(1, 40);
                string st = combstr(h);
                int l = st.Split(',').Length;
                int[] we = combnbr(l);
                int k = 1;
                if (l > 4)
                    k = rnd.Next(1, l - 2);
                Assert.AreEqual(NthRankSol(st, we, k), Rank.NthRank(st, we, k));
            }
        }

    }

}