using HandyControl.Tools;

namespace JianXiEditor.Model
{
    class UpdateModel : BindablePropertyBase
    {
        private string currentVersion;
        /// <summary>
        /// 当前版本
        /// </summary>
        public string CurrentVersion
        {
            get => currentVersion;
            set => Set(ref currentVersion, value);
        }
        private string latestVersion;
        /// <summary>
        /// 最新版本
        /// </summary>
        public string LatestVersion
        {
            get => latestVersion;
            set => Set(ref latestVersion, value);
        }
        private string updateContent;
        /// <summary>
        /// 更新内容
        /// </summary>
        public string UpdateContent
        {
            get => updateContent;
            set => Set(ref updateContent, value);
        }
        private double updateProgress;
        /// <summary>
        /// 更新进度
        /// </summary>
        public double UpdateProgress
        {
            get => updateProgress;
            set => Set(ref updateProgress, value);
        }
        private double progressMaximum;
        /// <summary>
        /// 最大进度
        /// </summary>
        public double ProgressMaximum
        {
            get => progressMaximum;
            set => Set(ref progressMaximum, value);
        }
    }
}
