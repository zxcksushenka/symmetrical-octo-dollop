using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SysLab2
{
    public static class FileOperation
    {
        // Системные константы
        public static readonly IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);

        public static readonly int FILE_ATTRIBUTE_READONLY = 0x00000001;
        public static readonly int FILE_ATTRIBUTE_HIDDEN = 0x00000002;
        public static readonly int FILE_ATTRIBUTE_SYSTEM = 0x00000004;
        public static readonly int FILE_ATTRIBUTE_DIRECTORY = 0x00000010;
        public static readonly int FILE_ATTRIBUTE_ARCHIVE = 0x00000020;
        public static readonly int FILE_ATTRIBUTE_DEVICE = 0x00000040;
        public static readonly int FILE_ATTRIBUTE_NORMAL = 0x00000080;
        public static readonly int FILE_ATTRIBUTE_TEMPORARY = 0x00000100;
        public static readonly int FILE_ATTRIBUTE_SPARSE_FILE = 0x00000200;
        public static readonly int FILE_ATTRIBUTE_REPARSE_POINT = 0x00000400;
        public static readonly int FILE_ATTRIBUTE_COMPRESSED = 0x00000800;
        public static readonly int FILE_ATTRIBUTE_OFFLINE = 0x00001000;
        public static readonly int FILE_ATTRIBUTE_NOT_CONTENT_INDEXED = 0x00002000;
        public static readonly int FILE_ATTRIBUTE_ENCRYPTED = 0x00004000;
        public static readonly int FILE_ATTRIBUTE_VIRTUAL = 0x00010000;

        public static readonly long MAXDWORD = 0xFFFFFFFF; // 4294967296;

        // Описание структуры WIN32_FIND_DATA
        [Serializable,
            System.Runtime.InteropServices.StructLayout
                (System.Runtime.InteropServices.LayoutKind.Sequential,
                CharSet = System.Runtime.InteropServices.CharSet.Auto
                ),
            System.Runtime.InteropServices.BestFitMapping(false)]
        private struct WIN32_FIND_DATA
        {
            public int dwFileAttributes;
            public int ftCreationTime_dwLowDateTime;
            public int ftCreationTime_dwHighDateTime;
            public int ftLastAccessTime_dwLowDateTime;
            public int ftLastAccessTime_dwHighDateTime;
            public int ftLastWriteTime_dwLowDateTime;
            public int ftLastWriteTime_dwHighDateTime;
            public int nFileSizeHigh;
            public int nFileSizeLow;
            public int dwReserved0;
            public int dwReserved1;
            [System.Runtime.InteropServices.MarshalAs
                 (System.Runtime.InteropServices.UnmanagedType.ByValTStr,
                 SizeConst = 260)]
            public string cFileName;
            [System.Runtime.InteropServices.MarshalAs
                 (System.Runtime.InteropServices.UnmanagedType.ByValTStr,
                 SizeConst = 14)]
            public string cAlternateFileName;
        }

        // Описание системной функции FindFirstFile
        [System.Runtime.InteropServices.DllImport
             ("kernel32.dll",
             CharSet = System.Runtime.InteropServices.CharSet.Auto,
             SetLastError = true)]
        private static extern IntPtr FindFirstFile(string pFileName, ref WIN32_FIND_DATA pFindFileData);

        // Описание системной функции FindNextFile
        [System.Runtime.InteropServices.DllImport
             ("kernel32.dll",
             CharSet = System.Runtime.InteropServices.CharSet.Auto,
             SetLastError = true)]
        private static extern bool FindNextFile(IntPtr hndFindFile, ref WIN32_FIND_DATA lpFindFileData);

        // Описание системной функции FindClose
        [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool FindClose(IntPtr hndFindFile);

        // Базовый метод поиска папок и файлов удовлетворяющих маске внутри указанной папки.
        // Для файлов существует возможность отбора по их минимальному и максимальному размеру
        public static List<String> FindItemsInFolder(string searchFolder, string mask,
            long minSize, long maxSize)
        {
            // Создание списка найденных элементов
            List<string> foundItems = new List<string>();
            // Проверка корректности входных параметров
            ...
            // Формирование полного пути исходной папки с маской для поиска
            ...
            // Инициализация системных структур, используемых в процессе поиска
            WIN32_FIND_DATA findData = new WIN32_FIND_DATA();
            System.IntPtr handle = INVALID_HANDLE_VALUE;
            // Попытка запустить поиск
            handle = FindFirstFile(sourceFolder, ref findData);
            if (handle != INVALID_HANDLE_VALUE)
            {
                // Основной цикл поиска   
                do
                {
                    // Имя найденного элемента
                    string itemName = findData.cFileName;
                    // Проверка атрибута найденного элемента - является ли он файлом
                    if (...)
                    {
                        // Вычисление размера найденного файла 
                        long size = (findData.nFileSizeHigh << 0x20) + findData.nFileSizeLow;
                        // Проверка соответствия размера файла заданным критериям
                        if (minSize == 0 || minSize <= size) && (maxSize == 0 || maxSize >= size)
                        {
                            // Добавление нового подходящего файла в список найденных
                            
                        }
                    }
                    else
                    {
                        // Проверка имени папки с целью устранить системные имена из списка найденных
                        if (!itemName.Equals(".") && !itemName.Equals(".."))
                        {
                            // Добавление новой подходящей папки в список найденных
                            ...
                        }
                    }
                    // Временное прерывание цикла, чтобы отреагировал интерфейс пользователя
                    System.Windows.Forms.Application.DoEvents();
                    // Попытка продолжить процесс поиска
                } while (FindNextFile(handle, ref findData));
                // Завершение процесса поиска
                FindClose(handle);
            }
            // Возвращение сформированного списка
            return foundItems;
        }

    }
}
