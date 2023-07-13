namespace TitleviewProblemRepro;
public partial class App : Application {
  public App(IServiceProvider services) {
    InitializeComponent();
    MainPage = new NavigationPage(services.GetService<Pages.PhotosHomePage>());
  }
}
