using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReverseSentence
{
    public class ReverseHelper
    {
        public static string ReverseWords(string words)
        {
            char[] array = words.ToArray();
            int wordLength = array.Length;

            ReverseCharArray(array, 0, wordLength - 1);

            int start = -1;

            for (int i = 0; i < wordLength; i++)
            {
                if (!((array[i] >= 'a' && array[i] <= 'z') || (array[i] >= 'A' && array[i] <= 'Z')))
                {
                    if (start < i)
                    {
                        ReverseCharArray(array, start + 1, i - 1);
                        start = i;
                    }
                }

                if (i == wordLength - 1)
                {
                    ReverseCharArray(array, start+1, i);
                }
            }
            return new string(array);
        }

        public static void ReverseCharArray(char[] array, int start, int end)
        {
            if (array == null || start >= array.Length || end >= array.Length)
            {
                return;
            }
            while (start < end)
            {
                char temp = array[start];
                array[start] = array[end];
                array[end] = temp;
                start++;
                end--;
            }
        }
    }
}
