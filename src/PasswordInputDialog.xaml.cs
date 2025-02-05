using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace eimg
{
    public partial class PasswordInputDialog : Window
    {
        public string Password { get; private set; }

        public PasswordInputDialog()
        {
            InitializeComponent();
        }

        private void ConfirmPassword()
        {
            if (!string.IsNullOrEmpty(PasswordBox.Password))
            {
                Password = PasswordBox.Password;
                DialogResult = true;
                Close();
            }
        }

        private void CancelDialog()
        {
            DialogResult = false;
            Close();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ConfirmPassword();
            }
            else if (e.Key == Key.Escape)
            {
                CancelDialog();
            }
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            ConfirmPassword();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            CancelDialog();
        }
    }
}