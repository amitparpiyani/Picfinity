﻿using Picfinity.DataModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ApplicationSettings;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Item Detail Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234232

namespace Picfinity
{
    /// <summary>
    /// A page that displays details for a single item within a group while allowing gestures to
    /// flip through other items belonging to the same group.
    /// </summary>
    public sealed partial class AccountSettings : Picfinity.Common.LayoutAwarePage
    {
        public AccountSettings()
        {
            this.InitializeComponent();
            ShowHideLoginDetails();
        }

        private void ShowHideLoginDetails()
        {
            if (AppSettings.Oauth500Px != null && AppSettings.Oauth500Px.IsAuthenticated)
            {
                login.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                loggedIn.Visibility = Windows.UI.Xaml.Visibility.Visible;
                loggedIn.DataContext = AppSettings.CurrentUser;
            }
            else
            {
                login.Visibility = Windows.UI.Xaml.Visibility.Visible;
                loggedIn.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
            // Allow saved page state to override the initial item to display
            if (pageState != null && pageState.ContainsKey("SelectedItem"))
            {
                navigationParameter = pageState["SelectedItem"];
            }

            // TODO: Assign a bindable group to this.DefaultViewModel["Group"]
            // TODO: Assign a collection of bindable items to this.DefaultViewModel["Items"]
            // TODO: Assign the selected item to this.flipView.SelectedItem
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
          
            // TODO: Derive a serializable navigation parameter and assign it to pageState["SelectedItem"]
        }

        private void LoginClicked(object sender, RoutedEventArgs e)
        {
            AppSettings.AuthenticateNew();
            ShowHideLoginDetails();
        }

        private void userImage_ImageFailed_1(object sender, ExceptionRoutedEventArgs e)
        {
            defaultUserImage.Visibility = Windows.UI.Xaml.Visibility.Visible;
            userImage.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }

        private void userImage_ImageOpened_1(object sender, RoutedEventArgs e)
        {
            defaultUserImage.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            userImage.Visibility = Windows.UI.Xaml.Visibility.Visible;
        }

        private void LogoutClicked(object sender, RoutedEventArgs e)
        {
            AppSettings.LogoutUser();
            ShowHideLoginDetails();
        }

        /// <summary>
        /// This is the click handler for the back button on the Flyout.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void GoBack(object sender, RoutedEventArgs e)
        {
            // First close our Flyout.
            Popup parent = this.Parent as Popup;
            if (parent != null)
            {
                parent.IsOpen = false;
            }

            // If the app is not snapped, then the back button shows the Settings pane again.
            if (Windows.UI.ViewManagement.ApplicationView.Value != Windows.UI.ViewManagement.ApplicationViewState.Snapped)
            {
                SettingsPane.Show();
            }
        }
    }
}
