using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyNUnitTest
{
    public class LogAnalyzer
    {
        private IExtensionManager _manager;
        private ILogger _logger;

        public LogAnalyzer()
        {
        }

        public LogAnalyzer(IExtensionManager manager)
        {
            this._manager = manager;
        }

        public LogAnalyzer(ILogger logger)
        {
            this._logger = logger;
        }

        public bool WasLastFileNameValid { get; set; }

        public int MinNameLength { get; set; }

        public bool IsValidLogFileName(string fileName)
        {
            // 改变系统状态
            WasLastFileNameValid = false;

            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentException("filename has to be provided");
            }

            if (!fileName.EndsWith(".SLF", StringComparison.CurrentCultureIgnoreCase))
            {
                return false;
            }

            // 改变系统状态
            WasLastFileNameValid = true;

            return true;
        }

        public bool IsValidLogFileExtension(string fileName)
        {
            return _manager.IsValid(fileName);
        }

        public void Analyze(string fileName)
        {
            MinNameLength = fileName.Length;
            if (MinNameLength < 8)
            {
                // 在产品代码中写错误日志
                _logger.LogError(string.Format("Filename too short : {0}", fileName));
            }
        }
    }
}
