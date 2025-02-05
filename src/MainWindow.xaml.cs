using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace eimg
{
    public partial class MainWindow : Window
    {
        private readonly bool startArg;
        private string inputArg;

        public MainWindow()
        {
            InitializeComponent();
            startArg = false;
            inputArg = null;
        }

        public MainWindow(string arg)
        {
            InitializeComponent();
            startArg = true;
            inputArg = arg;
        }

        private void OpenEIMG()
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "Encrypted Image(*.eimg)|*.eimg",
                Multiselect = false
            };

            if (ofd.ShowDialog() == true)
            {
                PasswordInputDialog pid = new PasswordInputDialog
                {
                    Owner = this
                };

                if (pid.ShowDialog() == true)
                {
                    try
                    {
                        DisplayedImage.Source = Core.DecryptImage(ofd.FileName, pid.Password);
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void CreateEIMG()
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "Supported Image Files(*.jpg;*.jpeg;*.png;*.tiff;*.bmp;*.gif)|*.jpg;*.jpeg;*.png;*.tiff;*.bmp;*.gif"
            };

            if (ofd.ShowDialog() == true)
            {
                PasswordInputDialog pid = new PasswordInputDialog
                { 
                    Owner = this
                };

                if (pid.ShowDialog() == true)
                {
                    SaveFileDialog sfd = new SaveFileDialog
                    {
                        Filter = "Encrypted Image(*.eimg)|*.eimg"
                    };

                    if (sfd.ShowDialog() == true)
                    {
                        try
                        {
                            Core.EncryptImage(ofd.FileName, sfd.FileName, pid.Password);
                            MessageBox.Show("Encrypted image saved!", "Encrypted Image Viewer", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        catch (System.Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (!WindowAffinityHelper.EnableMonitorOnlyDisplay(this))
            {
                MessageBox.Show("SetWindowDisplayAffinity API call failed. Operating system may not be compatible.", "Encrypted Image Viewer", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (startArg)
            {
                if (File.Exists(inputArg))
                {
                    PasswordInputDialog pid = new PasswordInputDialog
                    {
                        Owner = this
                    };

                    if (pid.ShowDialog() == true)
                    {
                        try
                        {
                            DisplayedImage.Source = Core.DecryptImage(inputArg, pid.Password);
                        }
                        catch (System.Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
        }

        private void NewCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            CreateEIMG();
        }

        private void OpenCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenEIMG();
        }

        private void ExitCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ForgottenPassword_Click(object sender, RoutedEventArgs e)
        {
            ForgottenPasswordWindow fpw = new ForgottenPasswordWindow()
            {
                Owner = this
            };
            fpw.ShowDialog();
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Copyright © 2025 Koray USTUNDAG.\r\n\r\nThis program is free software: you can redistribute it and/or modify\r\nit under the terms of the GNU General Public License as published by\r\nthe Free Software Foundation, either version 3 of the License, or\r\n(at your option) any later version.\r\n\r\nThis program is distributed in the hope that it will be useful,\r\nbut WITHOUT ANY WARRANTY; without even the implied warranty of\r\nMERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the\r\nGNU General Public License for more details.\r\n\r\nYou should have received a copy of the GNU General Public License\r\nalong with this program.  If not, see <https://www.gnu.org/licenses/>.", "Encrypted Image Viewer", MessageBoxButton.OK);
        }
    }
}