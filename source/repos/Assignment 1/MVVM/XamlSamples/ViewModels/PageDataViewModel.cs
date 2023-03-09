namespace XamlSamples
{
    public class PageDataViewModel
    {
        public PageDataViewModel(Type type, string title, string description)
        {
            Type = type;
            Title = title;
            Description = description;
        }

        public Type Type { private set; get; }

        public string Title { private set; get; }

        public string Description { private set; get; }

        static PageDataViewModel()
        {
            All = new List<PageDataViewModel>
            {
                

                // Part 5. From Data Bindings to MVVM
                new PageDataViewModel(typeof(OneShotDateTimePage), "One-Shot DateTime",
                                      "Obtain the current DateTime and display it"),

                new PageDataViewModel(typeof(ClockPage), "Clock",
                                      "Dynamically display the current time"),

                new PageDataViewModel(typeof(HslColorScrollPage), "HSL Color Scroll",
                                      "Use a view model to select HSL colors"),

                new PageDataViewModel(typeof(KeypadPage), "Keypad",
                                      "Use a view model for numeric keypad logic"),

                new PageDataViewModel(typeof(Student), "Student",
                                      "Use a view model for Student information")
            };
        }

        public static IList<PageDataViewModel> All { private set; get; } 
    }
}
