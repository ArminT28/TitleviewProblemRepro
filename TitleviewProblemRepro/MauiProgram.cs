using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;

namespace TitleviewProblemRepro;
  public static class MauiProgram {
    public static MauiApp CreateMauiApp() {
      var builder = MauiApp.CreateBuilder();
      builder
          .UseMauiApp<App>()
          //.UseMauiCommunityToolkit()
          //.RegisterAppServices()
          .RegisterViewModels()
          .RegisterLifecycleEvents();
      return builder.Build();
    }


    //public static MauiAppBuilder RegisterAppServices(this MauiAppBuilder mauiAppBuilder) {
    //  mauiAppBuilder.Services.AddSingleton<IDataManager, DataManager>();
    //  mauiAppBuilder.Services.AddSingleton<Core.Services.INavigationService, Core.Services.NavigationService>();

    //  return mauiAppBuilder;
    //}

    public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder) {
      //mauiAppBuilder.Services.AddTransient<HomeViewModel>();
      //mauiAppBuilder.Services.AddTransient<SectionsViewModel>();
      //mauiAppBuilder.Services.AddTransient<ArticleViewModel>();
      ////mauiAppBuilder.Services.AddTransient<BookmarksViewModel>();

      //mauiAppBuilder.Services.AddTransient<ViewModels.PhotosHomeVM>();
      //mauiAppBuilder.Services.AddTransient<ViewModels.PhotoEditVM>();

      mauiAppBuilder.Services.AddTransient<Pages.PhotosHomePage>();
      //mauiAppBuilder.Services.AddTransient<Pages.PhotoEditPage>();
      //mauiAppBuilder.Services.AddTransient<SectionsPage>();
      //mauiAppBuilder.Services.AddTransient<ArticlePage>();
      //mauiAppBuilder.Services.AddTransient<BookmarksPage>();

      return mauiAppBuilder;
    }



    public static MauiAppBuilder RegisterLifecycleEvents(this MauiAppBuilder mauiAppBuilder) {
      mauiAppBuilder.ConfigureLifecycleEvents(events => {
#if WINDOWS
      events.AddWindows(windows => windows
              .OnActivated((window, args) => LogEvent(nameof(WindowsLifecycle.OnActivated)))
              .OnClosed((window, args) => LogEvent(nameof(WindowsLifecycle.OnClosed)))
              .OnLaunched((window, args) => LogEvent(nameof(WindowsLifecycle.OnLaunched)))
              .OnLaunching((window, args) => LogEvent(nameof(WindowsLifecycle.OnLaunching))));
#endif

#if ANDROID
      events.AddAndroid(android => android
                  .OnActivityResult((activity, requestCode, resultCode, data) => LogEvent(nameof(AndroidLifecycle.OnActivityResult), requestCode.ToString()))
                  .OnStart(activity => Starting())
                  //.OnStart((activity) => LogEvent(nameof(AndroidLifecycle.OnStart)))
                  .OnCreate((activity, bundle) => LogEvent(nameof(AndroidLifecycle.OnCreate)))
                  //.OnBackPressed((activity) => LogEvent(nameof(AndroidLifecycle.OnBackPressed)) && false)  // this method must return a bool
                  .OnStop((activity) => LogEvent(nameof(AndroidLifecycle.OnStop))));
#endif
      });

      return mauiAppBuilder;
    }
    private static void Starting() {
      LogEvent("Generic Starting Method");
    }

    static void LogEvent(string eventName, string type = null) => System.Diagnostics.Debug.WriteLine($"Lifecycle event: {eventName}{(type == null ? string.Empty : $" ({type})")}");
  }
