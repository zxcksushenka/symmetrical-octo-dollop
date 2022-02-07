using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_WPF_
{
    public class FileOperation
    {
        [Flags]
        public enum ShellFileOperationFlags
        {
            FOF_MULTIDESTFILES = 0x0001,
            FOF_ALLOWUNDO = 0x0040,
            FOF_NO_CONNECTED_ELEMENTS = 0x2000,
            FOF_WANTNUKEWARNING = 0x4000,
        }
        [Flags]
        public enum ShellChangeNotificationEvents : uint
        {	
            SHCNE_ALLEVENTS = 0x7FFFFFFF
        }
        public enum ShellChangeNotificationFlags
        {
            SHCNF_IDLIST = 0x0000,
            SHCNF_PATHA = 0x0001,
            SHCNF_PRINTERA = 0x0002,
            SHCNF_DWORD = 0x0003,
            SHCNF_PATHW = 0x0005,	
            SHCNF_PRINTERW = 0x0006,	
            SHCNF_TYPE = 0x00FF,
            SHCNF_FLUSH = 0x1000,
            SHCNF_FLUSHNOWAIT = 0x2000
        }
        public IntPtr OwnerWindow;
        public ShellFileOperationFlags OperationFlags;
        public String ProgressTitle;
        public ShellNameMapping[] NameMappings;
        private bool FileOperation(uint operation, string source, string destination = "")
        {
            OwnerWindow = IntPtr.Zero;
            OperationFlags = ShellFileOperationFlags.FOF_ALLOWUNDO | ShellFileOperationFlags.FOF_MULTIDESTFILES | ShellFileOperationFlags.FOF_NO_CONNECTED_ELEMENTS | ShellFileOperationFlags.FOF_WANTNUKEWARNING;
            ProgressTitle = "";
            NameMappings = null;
            ShellApi.SHFILEOPSTRUCT FileOpStruct = new ShellApi.SHFILEOPSTRUCT();
            FileOpStruct.hwnd = OwnerWindow;
            FileOpStruct.wFunc = operation;
            FileOpStruct.pFrom = Marshal.StringToHGlobalUni(source);
            FileOpStruct.pTo = Marshal.StringToHGlobalUni(destination);
            FileOpStruct.fFlags = (ushort)OperationFlags;
            FileOpStruct.lpszProgressTitle = ProgressTitle;
            FileOpStruct.fAnyOperationsAborted = 0;
            FileOpStruct.hNameMappings = IntPtr.Zero;
            NameMappings = new ShellNameMapping[0];
            int failure = ShellApi.SHFileOperation(ref FileOpStruct);
            ShellApi.SHChangeNotify(
                (uint)ShellChangeNotificationEvents.SHCNE_ALLEVENTS,
                (uint)ShellChangeNotificationFlags.SHCNF_DWORD,
                IntPtr.Zero,
                IntPtr.Zero);
            if (failure != 0)
                return false;
            if (FileOpStruct.fAnyOperationsAborted != 0)
                return false;
            return true;
        }
        public static String StringArrayToMultiString(ObservableCollection<string> stringArray)
        {
            String multiString = "";
            if ((strings?.Count ?? 0) == 0)
                return multiString;

            for (int i = 0; i < strings.Count; ++i)
                multiString += strings[i] + '\0';
            multiString += '\0';

            return multiString;
        }
        private static string MasksToMultiMask(string sourceFolder, string mask)
        {
            ObservableCollection<string> temp = new ObservableCollection<string>();
            string[] masks;
            char[] separator = new char[] { '|' };

            masks = mask.Split(separator, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < masks.Length; ++i)
            {
                string[] filesWithMask = Directory.GetFiles(sourceFolder, masks[i]);
                int filesWithMaskCount = filesWithMask.Length;

                for (int j = 0; j < filesWithMaskCount; ++j)
                    if (!(temp.Contains(filesWithMask[j])))
                        temp.Add(filesWithMask[j]);
            }

            return StringArrayToMultiString(temp);
        }
        public bool CopyFileWithMasks(string srcFolder, string dstFolder, string mask)
        {
            FileOperation(0x0001, MasksToMultiMask(srcFolder, mask), dstFolder);
            return true;
        }
        public bool MoveFileWithMasks(string srcFolder, string dstFolder, string mask)
        {
            FileOperation(0x0002, srcFolder, dstFolder);
            return true;
        }
        public bool DeleteFileWithMasks(string srcFolder, string mask)
        {
            FileOperation(0x0003, MasksToMultiMask(srcFolder, mask));
            return true;
        }
        public bool CopyFilesByList(ObservableCollection<string> files)
        {
            FileOperation(0x0001, StringArrayToMultiString(files));
            return true;
        }
        public bool MoveFileByList(ObservableCollection<string> files)
        {
            FileOperation(0x0002, StringArrayToMultiString(files));
            return true;
        }
        public bool DeleteFileByList(ObservableCollection<string> files)
        {
            FileOperation(0x0003, StringArrayToMultiString(files));
            return true;
        }
        public bool RenameFile(string srcFile, string reName)
        {
            FileOperation(0x0004, srcFile, dest);
            return true;
        }
    }
}
