namespace TitleviewProblemRepro.Pages;

public partial class PhotosHomePage : ContentPage
{
    public PhotosHomePage()
    {
        InitializeComponent();
        AdjustTitleViewWidthOnWindows();
    }

    private void AdjustTitleViewWidthOnWindows()
    {
#if WINDOWS
        Dispatcher.Dispatch(() =>
        {
            var window = this.GetParentWindow();
            if (window != null)
            {
                TitleGrid.WidthRequest = window.Width;

                window.SizeChanged += (s, e) =>
                {
                    TitleGrid.WidthRequest = window.Width;
                };
            }
        });
#endif
    }
}