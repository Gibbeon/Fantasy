using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Gui;
using MonoGame.Extended.Gui.Controls;
using Fantasy.Game.Screens;
using SpriteFontPlus;

namespace Fantasy
{  
    public class Main : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager _graphics;
        public ScreenManager _screenManager;
        private GuiSystem _guiSystem;
        public Main()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
            IsMouseVisible = true;
            Window.IsBorderless = true; 
            
            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += WindowOnClientSizeChanged;           
        }

        private void WindowOnClientSizeChanged(object sender, EventArgs eventArgs)
        {
            _guiSystem.ClientSizeChanged();
        }

        protected override void LoadContent()
        {    
            _screenManager = new ScreenManager();
            Components.Add(_screenManager);

            _screenManager.LoadScreen(new LocalAreaScreen(this));
            var guiRenderer = new GuiSpriteBatchRenderer(GraphicsDevice, () => Matrix.Identity);

            var viewportAdapter = new MonoGame.Extended.ViewportAdapters.DefaultViewportAdapter(GraphicsDevice);
            
            var font = Content.Load<MonoGame.Extended.BitmapFonts.BitmapFont>("sm_arial");
            MonoGame.Extended.BitmapFonts.BitmapFont.UseKernings = false;
            Skin.CreateDefault(font);

            var stackTest = new DemoViewModel("Stack Panels",
                    new StackPanel
                    {
                        Items =
                        {
                            new Button { Content = "Press Me", HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top },
                            new Button { Content = "Press Me", HorizontalAlignment = HorizontalAlignment.Right, VerticalAlignment = VerticalAlignment.Bottom  },
                            new Button { Content = "Press Me", HorizontalAlignment = HorizontalAlignment.Centre, VerticalAlignment = VerticalAlignment.Centre  },
                            new Button { Content = "Press Me", HorizontalAlignment = HorizontalAlignment.Stretch, VerticalAlignment = VerticalAlignment.Stretch },
                        }
                    });

            var dockTest = new DemoViewModel("Dock Panels",
                new DockPanel
                {
                    Items =
                    {
                        new Button { Content = "Dock.Top", AttachedProperties = { { DockPanel.DockProperty, Dock.Top } } },
                        new Button { Content = "Dock.Bottom", AttachedProperties = { { DockPanel.DockProperty, Dock.Bottom } } },
                        new Button { Content = "Dock.Left", AttachedProperties = { { DockPanel.DockProperty, Dock.Left } } },
                        new Button { Content = "Dock.Right", AttachedProperties = { { DockPanel.DockProperty, Dock.Right } } },
                        new Button { Content = "Fill" }
                    }
                });

            var controlTest = new DemoViewModel("Basic Controls",
                new StackPanel
                {
                    Margin = 5,
                    Orientation = Orientation.Vertical,
                    Items =
                    {
                        new Label("Buttons") { Margin = 5 },
                        new StackPanel
                        {
                            Orientation = Orientation.Horizontal,
                            Spacing = 5,
                            Items =
                            {
                                new Button { Content = "Enabled" },
                                new Button { Content = "Disabled", IsEnabled = false },
                                new ToggleButton { Content = "ToggleButton" }
                            }
                        },

                        new Label("TextBox") { Margin = 5 },
                        new TextBox {Text = "TextBox" },

                        new Label("CheckBox") { Margin = 5 },
                        new CheckBox {Content = "Check me please!"},

                        new Label("ListBox") { Margin = 5 },
                        new ListBox {Items = {"ListBoxItem1", "ListBoxItem2", "ListBoxItem3"}, SelectedIndex = 0},

                        new Label("ProgressBar") { Margin = 5 },
                        new ProgressBar {Progress = 0.5f, Width = 100},

                        new Label("ComboBox") { Margin = 5 },
                        new ComboBox {Items = {"ComboBoxItemA", "ComboBoxItemB", "ComboBoxItemC"}, SelectedIndex = 0, HorizontalAlignment = HorizontalAlignment.Left}
                    }
                });

            var demoScreen = new MonoGame.Extended.Gui.Screen
            {
                Content = new DockPanel
                {
                    LastChildFill = true,
                    Items =
                    {
                        new ListBox
                        {
                            Name = "DemoList",
                            AttachedProperties = { { DockPanel.DockProperty, Dock.Left} },
                            ItemPadding = new Thickness(5),
                            VerticalAlignment = VerticalAlignment.Stretch,
                            HorizontalAlignment = HorizontalAlignment.Left,
                            SelectedIndex = 0,
                            Items = { controlTest, stackTest, dockTest }
                        },
                        new ContentControl
                        {
                            Name = "Content",
                            BackgroundColor = new Color(30, 30, 30)
                        }
                    }
                }
            };


            var test = new MonoGame.Extended.Gui.Screen
            {

                Content = new MonoGame.Extended.Gui.Controls.StackPanel
                {
                    Height = 32,
                    Spacing = 3,
                    Orientation = Orientation.Horizontal,
                    VerticalAlignment = VerticalAlignment.Bottom,
                    HorizontalAlignment = HorizontalAlignment.Centre,
                    Items =
                    {
                        new Button
                        {
                            Name = "Button1",       
                            BackgroundColor = new Color(30, 30, 30, 128),                     
                            Content = new Label("1") { Margin = 5 },
                            Width = 32
                        },
                        new Button
                        {
                            Name = "Button2",
                            BackgroundColor = new Color(30, 30, 30, 128),
                            Content = new Label("2") { Margin = 5 },
                            Width = 32
                            
                        },
                        new Button
                        {
                            Name = "Button3",
                            BackgroundColor = new Color(30, 30, 30, 128),
                            Content = new Label("3") { Margin = 5 },
                            Width = 32
                            
                        },
                        new Button
                        {
                            Name = "Button4",
                            BackgroundColor = new Color(30, 30, 30, 128),
                            Content = new Label("4") { Margin = 5 },
                            Width = 32
                            
                        },
                        new Button
                        {
                            Name = "Button5",
                            BackgroundColor = new Color(30, 30, 30, 128),
                            Content = new Label("5") { Margin = 5 },
                            Width = 32
                            
                        },
                        new Button
                        {
                            Name = "Button6",
                            BackgroundColor = new Color(30, 30, 30, 128),
                            Content = new Label("6") { Margin = 5 },
                            Width = 32
                            
                        },
                        new Button
                        {
                            Name = "Button7",
                            BackgroundColor = new Color(30, 30, 30, 128),
                            Content = new Label("7") { Margin = 5 },
                            Width = 32
                            
                        },
                        new Button
                        {
                            Name = "Button8",
                            BackgroundColor = new Color(30, 30, 30, 128),
                            Content = new Label("8") { Margin = 5 },
                            Width = 32
                            
                        }
                    }
                }
            };

            _guiSystem = new GuiSystem(viewportAdapter, guiRenderer) {
                 ActiveScreen = test 
            };

            var demoList = demoScreen.FindControl<ListBox>("DemoList");
            var demoContent = demoScreen.FindControl<ContentControl>("Content");

            demoList.SelectedIndexChanged += (sender, args) => demoContent.Content = (demoList.SelectedItem as DemoViewModel)?.Content;
            demoContent.Content = (demoList.SelectedItem as DemoViewModel)?.Content;
        }

        protected override void Update(GameTime gameTime)
        {
            // process input
            base.Update(gameTime);
            
            _guiSystem.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkGray);

            base.Draw(gameTime);
            
            _guiSystem.Draw(gameTime);
        }
    }

    public class DemoViewModel
    {
        public DemoViewModel(string name, object content)
        {
            Name = name;
            Content = content;
        }

        public string Name { get; }
        public object Content { get; }

        public override string ToString()
        {
            return Name;
        }
    }
}
