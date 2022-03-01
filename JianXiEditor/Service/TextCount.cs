namespace JianXiEditor.Service
{
    class TextCount
    {
        /// <summary>
        /// 返回数字（0~9）字数数量
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public int GetNumberLength(string text)
        {
            int lx = 0;
            if (text != null)
            {
                char[] q = text.ToCharArray();
                for (int i = 0; i < q.Length; i++)
                {
                    if (q[i] >= 48 && q[i] <= 57)
                    {
                        lx += 1;
                    }
                }
            }
            return lx;
        }
        /// <summary>
        /// 返回字母（A~Z-a~z）字数数量
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public int GetLetterLength(string text)
        {
            int lz = 0;
            if (text != null)
            {
                char[] q = text.ToLower().ToCharArray();//大写字母转换成小写字母
                for (int i = 0; i < q.Length; i++)
                {
                    if (q[i] >= 97 && q[i] <= 122)//小写字母
                    {
                        lz += 1;
                    }
                }
            }
            return lz;
        }
        /// <summary>
        /// 返回字节数
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public int GetByteLength(string text)
        {
            int lh = 0;
            if (text != null)
            {
                char[] q = text.ToCharArray();
                for (int i = 0; i < q.Length; i++)
                {
                    if (q[i] >= 0x4E00 && q[i] <= 0x9FA5)//汉字
                    {
                        lh += 2;
                    }
                    else
                    {
                        lh += 1;
                    }
                }
            }
            return lh;
        }
    }
}
