namespace Izh_04_tasks
{
    public static class Extentions
    {
        /// <summary>
        /// Добавляет в массив элемент.
        /// </summary>
        /// <param name="srcArr"></param>
        /// <param name="insert"></param>
        /// <returns></returns>
        public static int[] Add(this int[] srcArr, int insert)
        {
            int[] resArr = new int[srcArr.Length + 1];
            for (int i = 0; i < srcArr.Length; i++)
            {
                resArr[i] = srcArr[i];
            }

            resArr[resArr.Length - 1] = insert;
            srcArr = resArr;
            return srcArr;
        }
    }
}
