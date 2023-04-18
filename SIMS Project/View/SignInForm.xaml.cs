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
using System.Windows.Shapes;
using SIMS_Project.Model;
using SIMS_Project.Controller;
using SIMS_Project.View.Owner;
using SIMS_Project.Stores;
using SIMS_Project.Application.UseCases;

namespace SIMS_Project.View
{
    /// <summary>
    /// Interaction logic for SignInForm.xaml
    /// </summary>
    public partial class SignInForm : Window
    {
        private UserController _userController; // remove
        private readonly UserService _userService;
        private UserStore _userStore;
        public string Username { get; set; }
        public string Password { get; set; }

        public SignInForm()
        {
            InitializeComponent();

            _userController = UserController.GetInstance(); //remove
            _userService = UserService.GetInstance();
            _userStore = new UserStore();

            DataContext = this;
        }

        private void Save(User user)
        {
            Properties.Settings.Default.LoggedInUserId = user.Id;
            Properties.Settings.Default.Save();
        }

        private void ShowMain(User user)
        {
            switch (user.Role)
            {
                case UserRole.OWNER:
                    ShowOwnerMain(user);
                    break;
                case UserRole.GUEST_1:
                    ShowGuest1Main(user);
                    break;
                case UserRole.GUIDE:
                    ShowGuideMain(user);
                    break;
                case UserRole.GUEST_2:
                    ShowGuest2Main(user);
                    break;
                default:
                    MessageBox.Show("User role unknown");
                    break;
            }
        }

        private void BtnSignIn_Click(object sender, RoutedEventArgs e)
        {
            User? user = _userController.GetAll().Find(x => x.Username == Username && x.Password == Password);
            if (user == null) return;

            Save(user);
            ShowMain(user);

            ClearInput();
        }

        private void ClearInput()
        {
            TextBoxUsername.Text = String.Empty;
            PasswordBox.Password = String.Empty;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            Password = PasswordBox.Password;
        }

        private void ShowOwnerMain(User owner)
        {
            _userStore.SignedInUser = _userController.GetById(0);
            MainView ownerMain = new MainView(_userStore);
            ownerMain.Owner = this;

            //Hci related issue: Hidden window will show in the same location where it was "closed" 
            this.Hide();
            ownerMain.ShowDialog();
            this.Show();
        }

        private void ShowGuest1Main(User guest11)
        {
            User guest1 = _userController.GetById(1);
            Guest1MainWindow guest1MainWindow = new Guest1MainWindow(guest1);
            guest1MainWindow.ShowDialog();
        }

        private void ShowGuideMain(User guide)
        {
            GuideMain guideMain = new GuideMain();
            guideMain.Owner = this;

            this.Hide();
            guideMain.ShowDialog();
            this.Show();
        }

        private void ShowGuest2Main(User guest2)
        {
            Guest2Main guest2Main = new Guest2Main(guest2);
            guest2Main.Owner = this;

            this.Hide();
            guest2Main.ShowDialog();
            this.Show();
        }

        private void SignInForm_Loaded(object sender, RoutedEventArgs e)
        {
            User? user = _userController.GetById(Properties.Settings.Default.LoggedInUserId);
            if (user == null) return;

            ShowMain(user);
        }
    }
}
