using System;
using System.Collections.Generic;
using System.Text;

namespace Tracker.App.Services
{
    public class MenuService
    {
        private readonly AppShell _shell;

        public MenuService(AppShell shell)
        {
            _shell = shell;
        }


        public void AddMenu(bool isAuthenticated)
        {
            if (isAuthenticated)
            {
                AddAuthorized();
            }
            else
            {
                AddNotAuthorized();
            }
        }

        private void AddNotAuthorized()
        {
            _shell.Items.Clear();

            _shell.Items.Add(new FlyoutItem
            {
                Title = "Home",
                Route = "home",
                Icon = (ImageSource)Application.Current.Resources["IconDashboard"],
                Items =
                {
                    new ShellContent
                    {
                        ContentTemplate = new DataTemplate(typeof(MainPage)),
                    }
                }
            });

            _shell.Items.Add(new FlyoutItem
            {
                Title = "Login",
                Route = "login",
                Icon = (ImageSource)Application.Current.Resources["IconWeather"],
                Items =
                {
                    new ShellContent
                    {
                        ContentTemplate = new DataTemplate(typeof(LoginPage)),
                    }
                }
            });
        }

        private void AddAuthorized()
        {
            _shell.Items.Clear();

            _shell.Items.Add(new FlyoutItem
            {
                Title = "Home",
                Route = "home",
                Icon = (ImageSource)Application.Current.Resources["IconDashboard"],
                Items =
                {
                    new ShellContent
                    {
                        ContentTemplate = new DataTemplate(typeof(MainPage)),
                    }
                }
            });

            _shell.Items.Add(new FlyoutItem
            {
                Title = "Counter",
                Route = "counter",
                Icon = (ImageSource)Application.Current.Resources["IconCounter"],
                Items =
                {
                    new ShellContent
                    {
                        ContentTemplate = new DataTemplate(typeof(CounterPage)),
                    }
                }
            });

            _shell.Items.Add(new FlyoutItem
            {
                Title = "Weather",
                Route = "weather",
                Icon = (ImageSource)Application.Current.Resources["IconWeather"],
                Items =
                {
                    new ShellContent
                    {
                        ContentTemplate = new DataTemplate(typeof(WeatherPage)),
                    }
                }
            });

            _shell.Items.Add(new FlyoutItem
            {
                Title = "Project-Dashboard",
                Route = "project-dashboard",
                Icon = (ImageSource)Application.Current.Resources["IconDashboard"],
                Items =
                {
                    new ShellContent
                    {
                        ContentTemplate = new DataTemplate(typeof(ProjectDashboardPage)),
                    }
                }
            });

            _shell.Items.Add(new FlyoutItem
            {
                Title = "Projects",
                Route = "projects",
                Icon = (ImageSource)Application.Current.Resources["IconProjects"],
                Items =
                {
                    new ShellContent
                    {
                        ContentTemplate = new DataTemplate(typeof(ProjectListPage)),
                    }
                }
            });

            _shell.Items.Add(new FlyoutItem
            {
                Title = "Manage Meta",
                Route = "manage",
                Icon = (ImageSource)Application.Current.Resources["IconMeta"],
                Items =
                {
                    new ShellContent
                    {
                        ContentTemplate = new DataTemplate(typeof(ManageMetaPage)),
                    }
                }
            });

            _shell.Items.Add(new FlyoutItem
            {
                Title = "Logout",
                Route = "logout",
                Icon = (ImageSource)Application.Current.Resources["IconWeather"],
                Items =
                {
                    new ShellContent
                    {
                        ContentTemplate = new DataTemplate(typeof(LogoutPage)),
                    }
                }
            });

        }

        public void UpdateMenu(bool isAuthorized)
        {
            UpdateAuthorized(isAuthorized);
            UpdateNonAuthorized(isAuthorized);
        }

        private void UpdateNonAuthorized(bool isAuthorized)
        {
            string[] routes = [
                "login",
                ];

            foreach (string route in routes)
            {
                var item = _shell.Items.FirstOrDefault(i => i.Route == route);
                if (item != null)
                {
                    item.FlyoutItemIsVisible = !isAuthorized;
                    item.IsVisible = !isAuthorized;
                }
            }

        }

        private void UpdateAuthorized(bool isAuthorized)
        {
            string[] routes = ["counter",
                "weather",
                "project-dashboard",
                "projects",
                "manage",
                "logout",
                ];

            foreach (string route in routes)
            {
                var item = _shell.Items.FirstOrDefault(i => i.Route == route);
                if (item != null)
                {
                    item.FlyoutItemIsVisible = isAuthorized;
                    item.IsVisible = isAuthorized;
                }
            }

        }


        public void RemoveSecureMenu()
        {
            var item = _shell.Items.FirstOrDefault(i => i.Route == "secure");
            if (item != null)
                _shell.Items.Remove(item);
        }
    }

}
