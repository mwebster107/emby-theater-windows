﻿using MediaBrowser.Model.ApiClient;
using MediaBrowser.Model.Querying;
using MediaBrowser.Theater.Interfaces.Navigation;
using MediaBrowser.Theater.Interfaces.Presentation;
using MediaBrowser.Theater.Interfaces.Session;
using MediaBrowser.Theater.Presentation.Controls;
using MediaBrowser.Theater.Presentation.ViewModels;
using System;
using System.Threading.Tasks;

namespace MediaBrowser.Plugins.DefaultTheme.Home.TV
{
    /// <summary>
    /// Interaction logic for NextUp.xaml
    /// </summary>
    public partial class NextUp : BaseItemsControl
    {
        public NextUp(Model.Entities.DisplayPreferences displayPreferences, IApiClient apiClient, IImageManager imageManager, ISessionManager sessionManager, INavigationService navigationManager, IPresentationManager appWindow)
            : base(displayPreferences, apiClient, imageManager, sessionManager, navigationManager, appWindow)
        {
            InitializeComponent();
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.FrameworkElement.Initialized" /> event. This method is invoked whenever <see cref="P:System.Windows.FrameworkElement.IsInitialized" /> is set to true internally.
        /// </summary>
        /// <param name="e">The <see cref="T:System.Windows.RoutedEventArgs" /> that contains the event data.</param>
        protected override void OnInitialized(EventArgs e)
        {
            ItemsList.ItemInvoked += ItemsList_ItemInvoked;

            base.OnInitialized(e);
        }

        protected override bool AutoSelectFirstItemOnFirstLoad
        {
            get
            {
                return false;
            }
        }
        
        /// <summary>
        /// Itemses the list_ item invoked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        void ItemsList_ItemInvoked(object sender, ItemEventArgs<object> e)
        {
            var model = e.Argument as BaseItemDtoViewModel;

            if (model != null)
            {
                var item = model.Item;

                NavigationManager.NavigateToItem(item, string.Empty);
            }
        }

        protected override ExtendedListBox ItemsList
        {
            get { return LstItems; }
        }

        protected override Task<ItemsResult> GetItemsAsync()
        {
            var query = new NextUpQuery
            {
                Fields = new[]
                        {
                            ItemFields.UserData,
                            ItemFields.PrimaryImageAspectRatio,
                            ItemFields.DateCreated,
                            ItemFields.SeriesInfo,
                            ItemFields.DisplayPreferencesId
                        },

                UserId = SessionManager.CurrentUser.Id,

                Limit = 15
            };

            return ApiClient.GetNextUpAsync(query);
        }
    }
}
