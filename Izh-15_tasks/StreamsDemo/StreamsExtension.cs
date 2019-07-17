using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace StreamsDemo
{
    // C# 6.0 in a Nutshell. Joseph Albahari, Ben Albahari. O'Reilly Media. 2015
    // Chapter 15: Streams and I/O
    // Chapter 6: Framework Fundamentals - Text Encodings and Unicode
    // https://msdn.microsoft.com/ru-ru/library/system.text.encoding(v=vs.110).aspx

    public static class StreamsExtension
    {

        #region Public members

        #region TODO: Implement by byte copy logic using class FileStream as a backing store stream .

        public static int ByByteCopy(string sourcePath, string destinationPath)
        {
            InputValidation(sourcePath, destinationPath);

            int numberOfByte = 0;
            int buf;

            using (FileStream fsRead = new FileStream(sourcePath, FileMode.Open))
            {
                using (FileStream fsWrite = File.Create(destinationPath))
                {
                    while ((buf = fsRead.ReadByte()) > -1)
                    {
                        fsWrite.WriteByte((byte)buf);
                        numberOfByte++;
                    }
                }
            }

            return numberOfByte;
        }

        #endregion

        #region TODO: Implement by byte copy logic using class MemoryStream as a backing store stream.

        public static int InMemoryByByteCopy(string sourcePath, string destinationPath)
        {
            // TODO: step 1. Use StreamReader to read entire file in string

            // TODO: step 2. Create byte array on base string content - use  System.Text.Encoding class

            // TODO: step 3. Use MemoryStream instance to read from byte array (from step 2)

            // TODO: step 4. Use MemoryStream instance (from step 3) to write it content in new byte array

            // TODO: step 5. Use Encoding class instance (from step 2) to create char array on byte array content

            // TODO: step 6. Use StreamWriter here to write char array content in new file

            InputValidation(sourcePath, destinationPath);

            int numberOfBytes = 0;

            using (StreamReader sReader = new StreamReader(sourcePath, Encoding.Default, true))
            {
                string fileContent = sReader.ReadToEnd();
                Encoding encoding = sReader.CurrentEncoding;
                byte[] buf1 = encoding.GetBytes(fileContent);

                using (MemoryStream ms = new MemoryStream())
                {
                    for (int i = 0; i < buf1.Length; i++)
                    {
                        ms.WriteByte(buf1[i]);
                    }

                    ms.Seek(0, SeekOrigin.Begin);

                    byte[] buf2 = new byte[ms.Length];

                    for (int i = 0; i < ms.Length; i++)
                    {
                        buf2[i] = (byte)ms.ReadByte();
                        numberOfBytes++;
                    }

                    char[] arr = encoding.GetChars(buf2);

                    using (StreamWriter strWriter = new StreamWriter(destinationPath))
                    {
                        strWriter.Write(arr);
                    }
                }
            }

            return numberOfBytes;
        }

        #endregion

        #region TODO: Implement by block copy logic using FileStream buffer.

        public static int ByBlockCopy(string sourcePath, string destinationPath)
        {
            InputValidation(sourcePath, destinationPath);

            int numberOfByte = 0;
            int readBytes;
            byte[] buffer = new byte[1024];

            using (FileStream fsRead = new FileStream(sourcePath, FileMode.Open))
            {
                using (FileStream fsWrite = File.Create(destinationPath))
                {
                    while ((readBytes = fsRead.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        fsWrite.Write(buffer, 0, readBytes);
                        fsWrite.Flush();
                        numberOfByte += readBytes;
                    }
                }
            }

            return numberOfByte;
        }

        #endregion

        #region TODO: Implement by block copy logic using MemoryStream.

        public static int InMemoryByBlockCopy(string sourcePath, string destinationPath)
        {
            // TODO: Use InMemoryByByteCopy method's approach
            InputValidation(sourcePath, destinationPath);

            int numberOfBytes = 0;
            const int block = 1024 * sizeof(Int32);

            using (StreamReader sReader = new StreamReader(sourcePath, Encoding.Default, true))
            {
                string fileContent = sReader.ReadToEnd();
                Encoding encoding = sReader.CurrentEncoding;
                byte[] buf1 = encoding.GetBytes(fileContent);
                int iterations = buf1.Length / block;
                int tail = buf1.Length % block;

                using (MemoryStream ms = new MemoryStream())
                {
                    for (int i = 0; i < iterations; i++)
                    {
                        ms.Write(buf1, i * block, block);
                    }

                    if (tail > 0)
                    {
                        ms.Write(buf1, iterations * block, tail);
                    }

                    ms.Seek(0, SeekOrigin.Begin);

                    byte[] buf2 = new byte[ms.Length];

                    for (int i = 0; i < iterations; i++)
                    {
                        numberOfBytes += ms.Read(buf2, i * block, block);
                        
                    }

                    if (tail > 0)
                    {
                        numberOfBytes += ms.Read(buf2, iterations * block, tail);
                    }

                    char[] arr = encoding.GetChars(buf2);

                    using (StreamWriter strWriter = new StreamWriter(destinationPath))
                    {
                        strWriter.Write(arr);
                    }
                }
            }

            return numberOfBytes;
        }

        #endregion

        #region TODO: Implement by block copy logic using class-decorator BufferedStream.

        public static int BufferedCopy(string sourcePath, string destinationPath)
        {
            InputValidation(sourcePath, destinationPath);

            int numberOfByte = 0;
            int readBytes;
            byte[] buffer = new byte[1000];

            using (BufferedStream bs = new BufferedStream(new FileStream(sourcePath, FileMode.Open), buffer.Length))
            {
                using (FileStream fsWrite = File.Create(destinationPath))
                {
                    while ((readBytes = bs.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        fsWrite.Write(buffer, 0, readBytes);
                        fsWrite.Flush();
                        numberOfByte += readBytes;
                    }
                }
            }

            return numberOfByte;
        }

        #endregion

        #region TODO: Implement by line copy logic using FileStream and classes text-adapters StreamReader/StreamWriter

        public static int ByLineCopy(string sourcePath, string destinationPath)
        {
            InputValidation(sourcePath, destinationPath);

            int numberOfLines = 0;
            int readChar;
            StringBuilder sb = new StringBuilder();

            using (StreamReader sReader = new StreamReader(sourcePath))
            {
                using (StreamWriter fsWriter = new StreamWriter(destinationPath))
                {
                    do
                    {
                        do
                        {
                            readChar = sReader.Read();
                            sb.Append((char)readChar);
                        }
                        while ((char)readChar != '\r' && (char)readChar != '\n' && readChar != -1);

                        if (readChar == -1)
                        {
                            sb.Remove(sb.Length - 1, 1);
                        }

                        fsWriter.Write(sb);
                        sb.Clear();
                        numberOfLines++;
                    }
                    while (readChar != -1);
                }
            }

            return numberOfLines;
        }

        #endregion

        #region TODO: Implement content comparison logic of two files 

        public static bool IsContentEquals(string sourcePath, string destinationPath)
        {
            InputValidation(sourcePath, destinationPath);

            if (!File.Exists(destinationPath))
            {
                throw new FileNotFoundException();
            }

            FileInfo srcFile = new FileInfo(sourcePath);
            FileInfo dstFile = new FileInfo(destinationPath);

            using (TextReader fsSrc = srcFile.OpenText())
            using (TextReader fsDst = dstFile.OpenText())
            {
                string srcLine;
                string dstLine;

                while (true)
                {
                    srcLine = fsSrc.ReadLine();
                    dstLine = fsDst.ReadLine();

                    if (srcLine == null && dstLine == null)
                    {
                        return true;
                    }

                    if (srcLine == null)
                    {
                        return false;
                    }

                    if (!srcLine.Equals(dstLine))
                    {
                        return false;
                    }
                }
            }
        }

        #endregion

        #endregion

        #region Private members

        #region TODO: Implement validation logic

        private static void InputValidation(string sourcePath, string destinationPath)
        {
            if (string.IsNullOrEmpty(sourcePath) || string.IsNullOrEmpty(destinationPath))
            {
                throw new ArgumentNullException();
            }

            if (!File.Exists(sourcePath))
            {
                throw new FileNotFoundException();
            }

        }

        #endregion

        #endregion

    }
}
