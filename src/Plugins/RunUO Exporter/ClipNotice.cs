namespace RunUOExporter
{
    public partial class ClipNotice
    {
        public bool DontShow
        {
            get => dontshow.Checked;
            set => dontshow.Checked = value;
        }

        public ClipNotice()
        {
            InitializeComponent();
        }
    }
}
