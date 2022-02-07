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

namespace WPF2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        
        }

        private void pathFileButton_Click(object sender, RoutedEventArgs e)
        {
            if (warningMsg())
            {
                if (notFolderMsg())
                {
                    var dialog = new FolderBrowserDialog();
                    dialog.Description = "Выбор папки";
                    dialog.ShowNewFolderButton = true;
                    dialog.RootFolder = Environment.SpecialFolder.Desktop;
                    if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        pathFolderTextBox.Text = dialog.SelectedPath;
                    }
                }
            }
        }
        private bool warningMsg()
        {
            if (pathFolderTextBox.Text.Trim() == "")
            {
                System.Windows.Forms.MessageBox.Show("Строка пуста");
                return false;
            }
            else
            {
                return true;
            }
        }
        private bool notFolderMsg()
        {
            if (!Directory.Exists(pathFolderTextBox.Text))
            {
                System.Windows.Forms.MessageBox.Show("не указана директория папки");
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
