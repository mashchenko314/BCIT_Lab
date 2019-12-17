using System;

namespace Lab_5
{
    public class DisDamLev
    {
        public static int Distance(string str1Param, string str2Param)
        {
            if ((str1Param == null) || (str2Param == null)) return -1;
            int str1Len = str1Param.Length;
            int str2Len = str2Param.Length;
            if ((str1Len == 0) && (str2Len == 0)) return 0;            //Если хотя бы одна строка пустая, возвращается длина другой строки
            if (str1Len == 0) return str2Len;
            if (str2Len == 0) return str1Len;
            string str1 = str1Param.ToUpper();     //Приведение строк к верхнему регистру
            string str2 = str2Param.ToUpper();
            int[,] matrix = new int[str1Len + 1, str2Len + 1];      //Объявление матрицы
            for (int i = 0; i <= str1Len; i++) matrix[i, 0] = i;    //Инициализация нулевой строки и нулевого столбца матрицы
            for (int j = 0; j <= str2Len; j++) matrix[0, j] = j;
            for (int i = 1; i <= str1Len; i++)            //Вычисление расстояния Дамерау-Левенштейна
            {
                for (int j = 1; j <= str2Len; j++)
                {
                    int symbEqual = ((str1.Substring(i - 1, 1) == str2.Substring(j - 1, 1)) ? 0 : 1); //Эквивалентность символов, переменная symbEqual соответствует m(s1[i],s2[j])
                    int ins = matrix[i, j - 1] + 1;     //Добавление
                    int del = matrix[i - 1, j] + 1;     //Удаление
                    int subst = matrix[i - 1, j - 1] + symbEqual;   //Замена
                    matrix[i, j] = Math.Min(Math.Min(ins, del), subst);       //Элемент матрицы вычисляется как минимальный из трех случаев
                    if ((i > 1) && (j > 1) && (str1.Substring(i - 1, 1) == str2.Substring(j - 2, 1)) && (str1.Substring(i - 2, 1) == str2.Substring(j - 1, 1)))             //Дополнение Дамерау по перестановке соседних символов
                    {
                        matrix[i, j] = Math.Min(matrix[i, j], matrix[i - 2, j - 2] + symbEqual);
                    }
                }
            }
            return matrix[str1Len, str2Len]; //Возвращается нижний правый элемент матрицы
        }
        
    }
}
