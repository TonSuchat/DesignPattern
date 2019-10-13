namespace ConsoleApp.DesignPatterns
{

    public class AbstractFactory
    {
        // products
        public interface IButton
        {
            string Clicked();
        }

        public interface ICheckbox
        {
            string Checked();
        }

        public class Button : IButton
        {
            public string OSName { get; set; }

            public string Clicked()
            {
                return $"{OSName} button has been clicked";
            }
        }
        public class Checkbox : ICheckbox
        {
            public string OSName { get; set; }

            public string Checked()
            {
                return $"{OSName} checkbox has been checked";
            }
        }

        // factory
        public interface GUIFactory
        {
            Button createButton();
            Checkbox createCheckbox();
        }

        // window factory (windows os implementation version of GUIFactory)
        public class WinFactory : GUIFactory
        {
            private readonly string osName = "Windows";

            public Button createButton()
            {
                return new Button() { OSName = osName };
            }

            public Checkbox createCheckbox()
            {
                return new Checkbox() { OSName = osName };
            }
        }

        // mac factory (mac os implementation version of GUIFactory)
        public class MacFactory : GUIFactory
        {
            private readonly string osName = "Mac";
            public Button createButton()
            {
                return new Button() { OSName = osName };
            }

            public Checkbox createCheckbox()
            {
                return new Checkbox() { OSName = osName };
            }
        }
    }

}
