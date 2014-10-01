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
using Microsoft.Win32;
using AppConfigEncryptor.BLL;

namespace AppConfigEncriptor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Constructor

        public MainWindow()
        {
            InitializeComponent();
            ExtraInitialize();
        }

        #endregion

        #region Methods

        protected void ExtraInitialize()
        {
            _fileDialog = new OpenFileDialog();
            _fileDialog.Filter = ".Net Executables|*.exe";
            _fileDialog.FileName = "";
        }

        private void OpenFile()
        {
            if (_fileDialog == null) return;

            _fileDialog.ShowDialog();

            _filePath = _fileDialog.FileName;

            txtFileName.Text = _filePath;
        }

        #endregion

        #region Events

        private void txtFileName_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenFile();
        }

        private void btn_selectFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFile();
        }

        private void btn_encrypt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var encrypter = new AppConfigEncryptorBlo();
                encrypter.EncryptFile(_filePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btn_decrypt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var encrypter = new AppConfigEncryptorBlo();
                encrypter.DecryptFile(_filePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region Properties

        #endregion

        #region Attributes

        private OpenFileDialog _fileDialog;

        private string _filePath;

        #endregion

    }
}
