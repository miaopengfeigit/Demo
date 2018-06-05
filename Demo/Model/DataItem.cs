namespace MvvmLight1.Model
{
    public class DataItem
    {
        public string Title
        {
            get;
            private set;
        }

        public DemoItem[] DemoItems
        {
            get;
            private set;
        }

        public DataItem(DemoItem[] demoItems)
        {
            //Title = title;
            DemoItems = demoItems;
        }
    }
}