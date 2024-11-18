using System.Collections;
using System.Text;
using System.Diagnostics;
using System.ComponentModel;


/*******************************************************************************************************************
 * 
 *      IOS CUSTOM BACK BUTTON
 *      
 *      Charles Tatum II
 *      
 *      Version 1.0.0 - Nov 2024
 *      
 *      This technique solves an issue that's especially problematic for iOS apps - when a user needs to back 
 *      out of a screen, and the app needs to confirm the user intended to do that.  The NavigationPage class
 *      includes an OnBackButtonPressed method that can be overridden, but that's not for iOS apps, because 
 *      iOS devices do not have a "hard" or hardware back button. Essentially, this is little other than a 
 *      layout "shell" into which your own content can be placed. 
 *      
 *      The technique makes use of a 3-column grid where a back button PNG (which can be customized, of course)
 *      is in an ImageButton calling a DisplayAlert to show a confirmation message. The middle column in the grid
 *      has space for a title (this is not the "Title" property that's in the ContentPage class.  In addition,
 *      the NavigationPage.HasNavigationBar property is specifically set to FALSE in order to accomodate the
 *      grid-based, simulated toolbar - it hides the navigation bar that would typically be visible. 
 *      
 *      For this sample code, there are dummy form fields (name, address, city, etc.) that are just present to 
 *      show how this technique could look in use.
 *      
 *      .NET MAUI developers would do well to find a way to implement this functionality so that it's baked in
 *      to the code base without the need for gymnastics like this to make it happen.
 * 
 *******************************************************************************************************************/


namespace IOSCustomBackButton
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage, INotifyPropertyChanged
    {

        public MainPage()                                       // null call (default)
        {
            InitializeComponent();
        }


        protected override bool OnBackButtonPressed()
        {

            // NOTE: This method only applies to Android because it has a "hard" Back button (legacy component). In
            // actuality, the "hard" button is the "<" button on the lower edge of modern Android devices, but that is
            // still considered a hardware back button.

            CustomBackButton();

            return true;

        }

        private void btnCustomBackButton_Clicked(object sender, EventArgs e)
        {
            CustomBackButton();
        }

        private void CustomBackButton()
        {
            // Runs on both Android and iOS, but made specifically so that iOS users can have 
            // a chance to confirm their early exit of a screen or page.

            MainThread.InvokeOnMainThreadAsync(async () => {

                var exit_now = await DisplayAlert("Unsaved Data", "You have unsaved data. Are you sure you want to exit?", "Yes", "No");

                if (exit_now == true)
                {
                    Application.Current?.Quit();
                }
                else
                {
                    await DisplayAlert("Exit Canceled", "Action to exit was canceled.", "OK");
                }

            });

        }

    }

}