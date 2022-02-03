/*


        protected MonoGame.Extended.Gui.Controls.Control GetInventoryBar()
        {
            return new MonoGame.Extended.Gui.Controls.StackPanel
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
                            Content = new Label("1") { VerticalTextAlignment = VerticalAlignment.Top },
                            Width = 32,
                            IsPressed = true
                        },
                        new Button
                        {
                            Name = "Button2",
                            BackgroundColor = new Color(30, 30, 30, 128),
                            Content = new Label("2") { VerticalTextAlignment = VerticalAlignment.Top },
                            Width = 32
                            
                        },
                        new Button
                        {
                            Name = "Button3",
                            BackgroundColor = new Color(30, 30, 30, 128),
                            Content = new Label("3")  { VerticalTextAlignment = VerticalAlignment.Top },
                            Width = 32
                            
                        },
                        new Button
                        {
                            Name = "Button4",
                            BackgroundColor = new Color(30, 30, 30, 128),
                            Content = new Label("4") { VerticalTextAlignment = VerticalAlignment.Top },
                            Width = 32
                            
                        },
                        new Button
                        {
                            Name = "Button5",
                            BackgroundColor = new Color(30, 30, 30, 128),
                            Content = new Label("5") { VerticalTextAlignment = VerticalAlignment.Top },
                            Width = 32
                            
                        },
                        new Button
                        {
                            Name = "Button6",
                            BackgroundColor = new Color(30, 30, 30, 128),
                            Content = new Label("6")  { VerticalTextAlignment = VerticalAlignment.Top },
                            Width = 32
                            
                        },
                        new Button
                        {
                            Name = "Button7",
                            BackgroundColor = new Color(30, 30, 30, 128),
                            Content = new Label("7")  { VerticalTextAlignment = VerticalAlignment.Top },
                            Width = 32
                            
                        },
                        new Button
                        {
                            Name = "Button8",
                            BackgroundColor = new Color(30, 30, 30, 128),
                            Content = new Label("8")  { VerticalTextAlignment = VerticalAlignment.Top },
                            Width = 32
                            
                        }
                    }
                };
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

        var guiRenderer = new GuiSpriteBatchRenderer(GraphicsDevice, () => Matrix.Identity);

            var viewportAdapter = new MonoGame.Extended.ViewportAdapters.DefaultViewportAdapter(GraphicsDevice);
            
            var font = Content.Load<MonoGame.Extended.BitmapFonts.BitmapFont>("fonts/sm_arial");
            MonoGame.Extended.BitmapFonts.BitmapFont.UseKernings = false;
            Skin.CreateDefault(font);

            var test = new MonoGame.Extended.Gui.Screen
            {
                Content = GetInventoryBar()
            };

            _guiSystem = new GuiSystem(viewportAdapter, guiRenderer) {
                 ActiveScreen = test 
            };

*/