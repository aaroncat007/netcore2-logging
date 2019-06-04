using Dapper;
using DapperExtensions;
using Microsoft.AspNetCore.Http;
using MySql.Data.MySqlClient;
using netcore2_logging.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace netcore2_logging.Services
{
    public interface ILogServices
    {
        void WriteLog(string ReferenceNumber, string Message, [CallerMemberName] string FunctionName = "");
        void WriteLog<TModel>(string ReferenceNumber, string Message, TModel BeforeData, TModel AfterData, [CallerMemberName] string FunctionName = "") where TModel : class;
    }

    public interface ILogServices<out TService> : ILogServices
    {

    }

    public class LogServices<TService> : ILogServices<TService>
    {
        /// <summary>
        /// 異動者 IP 位置
        /// </summary>
        private readonly string _Address;

        /// <summary>
        /// 異動者瀏覽器資訊
        /// </summary>
        private readonly string _BrowserInformation;

        /// <summary>
        /// 異動者名稱
        /// </summary>
        private readonly string _UserName;

        /// <summary>
        /// 異動資料模組名稱
        /// </summary>
        private readonly string _ModuleName;

        /// <summary>
        /// DB連線
        /// </summary>
        private MySqlConnection _connection;

        public LogServices(IHttpContextAccessor _HttpContextAccessor, IConnectionFactory connectionFactory)
        {
            var ServiceModuleType = typeof(TService);

            var HttpContext = _HttpContextAccessor.HttpContext;

            this._Address = HttpContext.Connection.RemoteIpAddress.ToString();

            this._BrowserInformation = HttpContext.Request.Headers["User-Agent"];

            this._ModuleName = ServiceModuleType.Name;
            this._UserName = HttpContext.User.Identity.Name;

            _connection = (MySqlConnection)connectionFactory.CreateConnection();
        }

        ~LogServices()
        {
            if (_connection != null)
            {
                _connection.Close();
                _connection = null;
            }
        }

        /// <summary>
        ///【紀錄】寫入無資料訊息日誌
        /// </summary>
        /// <param name="ReferenceNumber">追蹤參考碼</param>
        /// <param name="Message">日誌訊息</param>
        /// <param name="FunctionName">執行服務函式名稱</param>
        /// <returns></returns>
        public void WriteLog(string ReferenceNumber, string Message, [CallerMemberName] string FunctionName = "")
        {
            var InsertData = new ApplicationLog
            {
                ReferenceNumber = ReferenceNumber,
                ModuleName = this._ModuleName,
                OperatingFunctionName = FunctionName,
                OperatingType = LogOperatingType.RECORD,
                Address = this._Address,
                Operator = this._UserName,
                VersionNumber = VersionNumberExtensions.GetVersion,
                BrowserInformation = this._BrowserInformation,
                Message = Message,
                Timestamp = DateTime.Now,
            };

            //Write to DB
            int success = _connection.Insert(InsertData);
        }

        /// <summary>
        /// 【更新】批次寫入訊息日誌
        /// </summary>
        /// <typeparam name="TModel">日誌資料模型</typeparam>
        /// <param name="ReferenceNumberCode">追蹤參考碼代碼</param>
        /// <param name="Message">日誌訊息</param>
        /// <param name="BeforeDatas">批次更新前日誌資料</param>
        /// <param name="AfterDatas">批次更新後日誌資料</param>
        /// <param name="FunctionName">執行服務函式名稱</param>
        /// <returns></returns>
        public void WriteLog<TModel>(string ReferenceNumber, string Message, TModel BeforeData, TModel AfterData, [CallerMemberName] string FunctionName = "") where TModel : class
        {
            var InsertData = new ApplicationLog
            {
                ReferenceNumber = ReferenceNumber,
                ModuleName = this._ModuleName,
                OperatingFunctionName = FunctionName,
                OperatingType = LogOperatingType.RECORD,
                Address = this._Address,
                Operator = this._UserName,
                VersionNumber = VersionNumberExtensions.GetVersion,
                BrowserInformation = this._BrowserInformation,
                Message = Message,
                Timestamp = DateTime.Now,
                BeforeData = this.ChangeTransactionLogModelData(BeforeData),
                AfterData = this.ChangeTransactionLogModelData(AfterData),
            };

            //Write to DB
            var success = _connection.Insert(InsertData);
        }


        /// <summary>
        /// 【模型】轉換異動日誌資料訊息
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public string ChangeTransactionLogModelData<TModel>(TModel model) where TModel : class
        {
            return JsonConvert.SerializeObject(model);
        }
    }

    /// <summary>
    /// 異動日誌操作類型列舉
    /// </summary>
    public class LogOperatingType
    {
        /// <summary>
        /// 新增
        /// </summary>
        public static string INSERT = "INSERT";

        /// <summary>
        /// 更新
        /// </summary>
        public static string UPDATE = "UPDATE";

        /// <summary>
        /// 刪除
        /// </summary>
        public static string DELETE = "DELETE";

        /// <summary>
        /// 紀錄
        /// </summary>
        public static string RECORD = "RECORD";
    }

    public class ApplicationLog
    {
        public string Id { set; get; } = Guid.NewGuid().ToString();

        /// <summary>
        /// 追蹤參考碼
        /// </summary>
        public string ReferenceNumber { get; set; }

        /// <summary>
        /// 操作模組名稱
        /// </summary>
        public string ModuleName { get; set; }

        /// <summary>
        /// 操作模組方法名稱
        /// </summary>
        public string OperatingFunctionName { get; set; }

        /// <summary>
        /// 異動操作類型
        /// </summary>
        public string OperatingType { get; set; }

        /// <summary>
        /// 操作者 IP 位置
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 操作者
        /// </summary>
        public string Operator { get; set; }

        /// <summary>
        /// 版本號
        /// </summary>
        public string VersionNumber { get; set; }

        /// <summary>
        /// 使用瀏覽器資訊
        /// </summary>
        public string BrowserInformation { get; set; }

        /// <summary>
        /// 日誌訊息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 異動前資料【更新前資料】
        /// </summary>
        public string BeforeData { get; set; }

        /// <summary>
        /// 異動後資料
        /// </summary>
        public string AfterData { get; set; }

        /// <summary>
        /// 紀錄時間
        /// </summary>
        public DateTime Timestamp { set; get; }
    }
}

