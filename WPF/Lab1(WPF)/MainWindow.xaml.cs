using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.IO;
using System.Collections.ObjectModel;

namespace Lab1_WPF_
{
    public partial class MainWindow : Window
    {
        private ObservableCollection<string> Items;
        public MainWindow()
        {
            InitializeComponent();
            Items = new ObservableCollection<string>();
            listBox.ItemsSource = Items;
        }
        private void SelectFileButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                FileTextBox.Text = openFileDialog.FileName;
            }
        }
        private void selectedFolder_Click(object sender, RoutedEventArgs e)
        {
            selectFolder1();
        }
        private void reName_Click(object sender, RoutedEventArgs e)
        {
            if (warningMsg())
            {
                if (notFileMsg())
                {
                    RenameWindow renamewindow = new RenameWindow(FileTextBox.Text);
                    renamewindow.Show();
                }
            }
        }
        private void addToList_Click(object sender, RoutedEventArgs e)
        {
            if (warningMsg())
            {
                if (notFileMsg())
                {
                    Items.Add(FileTextBox.Text);
                }
            }
                  
        }
        private bool warningMsg()
        {
            if (FileTextBox.Text.Trim() == "")
            {
                System.Windows.Forms.MessageBox.Show("Строка пуста");
                return false;
            }
            else
            {
                return true;
            }
        }
        private bool warningMsg2()
        {
            if (FolderTextBox.Text.Trim() == "")
            {
                System.Windows.Forms.MessageBox.Show("Строка пуста");
                return false;
            }
            else
            {
                return true;
            }
        }
        private bool warningMsg3()
        {
            if (finalFolderTextBox.Text.Trim() == "")
            {
                System.Windows.Forms.MessageBox.Show("Строка пуста");
                return false;
            }
            else
            {
                return true;
            }
        }
        private bool notFileMsg()
        {
            if (!File.Exists(FileTextBox.Text))
            {
                System.Windows.Forms.MessageBox.Show("Не является файлом");
                return false;
            }
            else
            {
                return true;
            }
        }
        private bool notFolderMsg()
        {
            if (!Directory.Exists(FolderTextBox.Text))
            {
                System.Windows.Forms.MessageBox.Show("не указана директория папки");
                return false;
            }
            else
            {
                return true;
            }
        }
        private bool notFolderMsg2()
        {
            if (!Directory.Exists(finalFolderTextBox.Text))
            {
                System.Windows.Forms.MessageBox.Show("не указана директория папки");
                return false;
            }
            else
            {
                return true;
            }
        }
        private void findButton_Click(object sender, RoutedEventArgs e)
        {
            if (notFolderMsg())
            {
                ShellExecute shellExecute = new ShellExecute();
                shellExecute.Verb = ShellExecute.FindInFolder;
                shellExecute.Path = FolderTextBox.Text;
                shellExecute.Execute();
            }
        }
        private void printButton_Click(object sender, RoutedEventArgs e)
        {
            ShellExecute shellExecute = new ShellExecute();
            shellExecute.Verb = ShellExecute.PrintFile;
            shellExecute.Path = FileTextBox.Text;
            shellExecute.Execute();
        }
        private void removeListItem_Click(object sender, RoutedEventArgs e)
        {
            if(listBox.SelectedIndex > -1)
            {
                Items.RemoveAt(listBox.SelectedIndex);
            }
        }
        private void openFile_Click(object sender, RoutedEventArgs e)
        {
            if (warningMsg())
            {
                if (notFileMsg())
                {
                    ShellExecute shellExecute = new ShellExecute();
                    shellExecute.Verb = ShellExecute.OpenFile;
                    shellExecute.Path = FileTextBox.Text;
                    shellExecute.Execute();
                }
            }
        }
        private void openFolder_Click(object sender, RoutedEventArgs e)
        {
            if (warningMsg2())
            {
                if (notFolderMsg())
                {
                    ShellExecute shellExecute = new ShellExecute();
                    shellExecute.Verb = ShellExecute.ExploreFolder;
                    shellExecute.Path = FolderTextBox.Text;
                    shellExecute.Execute();
                }
            }
        }
        private void clearListItems_Click(object sender, RoutedEventArgs e)
        {
            Items.Clear();
        }
        private void selectFinalFolder_Click(object sender, RoutedEventArgs e)
        {
            selectFolder2();
        }
        private void selectFolder1()
        {
            var dialog = new FolderBrowserDialog();
            dialog.Description = "Выбор папки";
            dialog.ShowNewFolderButton = true;
            dialog.RootFolder = Environment.SpecialFolder.Desktop;
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                FolderTextBox.Text = dialog.SelectedPath;
            }
        }
        private void selectFolder2()
        {
            var dialog = new FolderBrowserDialog();
            dialog.Description = "Выбор папки";
            dialog.ShowNewFolderButton = true;
            dialog.RootFolder = Environment.SpecialFolder.Desktop;
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                finalFolderTextBox.Text = dialog.SelectedPath;
            }
        }
        private void copyFileWithMask_Click(object sender, RoutedEventArgs e)
        {
            selectFolder1();
            selectFolder2();
            //FileOperation.MoveFileWithMasks(FolderTextBox.Text, finalFolderTextBox.Text, mask.Text);
        }
        private void moveFileWithMask_Click(object sender, RoutedEventArgs e)
        {
            selectFolder1();
            selectFolder2();
        }
        private void removeFileWithMask_Click(object sender, RoutedEventArgs e)
        {
            selectFolder1();
        }

        private void copyFileofList_Click(object sender, RoutedEventArgs e)
        {

        }

        private void moveFileofList_Click(object sender, RoutedEventArgs e)
        {

        }

        private void removeFileofList_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}