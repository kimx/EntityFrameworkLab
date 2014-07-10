
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkLab
{
    public enum ProgramTypes
    {
        /// <summary>
        /// 0:目錄
        /// </summary>
        Directory = '0',
        /// <summary>
        /// User not exist (email or userno)
        /// </summary>
        WebMVC = '1',
        /// <summary>
        /// Wrong password
        /// </summary>
        WebForm = '2',

    }

    [Serializable]
    public class UserEvntLogInfo
    {
        public int EVNT_ID { get; set; }

        public string USER_NO { get; set; }
        public string PRG_AREA { get; set; }
        public string MVC_CTRL { get; set; }
        public string MVC_ACT { get; set; }
        public string DESCTXT { get; set; }
        public System.DateTime CRT_UTC { get; set; }
        public string HOST_IPADDR { get; set; }
        public string POST_DATA { get; set; }

        //exten
        public IDictionary<string, object> ActionParameters { get; set; }
    }

    #region Basic Test
    //class Program
    //{
    //    static Entities prudenceERPEntities;
    //    static void Main(string[] args)
    //    {
    //        prudenceERPEntities = new Entities();
    //        //      FirstQty();
    //        //ProcedureTest();
    //        // OrderByTest();
    //        // ThenByTest();
    //        //DynamicOrderByTest();
    //        //DynamicThenByTest();
    //        //DynamicWhereTest();
    //        //InsertRoleRelation();
    //        //   GetLogs();
    //        Console.Read();

    //    }

    //    public static void GetLogs()
    //    {
    //        DateTime beginCrtUtc = DateTime.Now.AddDays(-30);
    //        DateTime endCrtUtc = DateTime.Now.AddDays(1);

    //        var result = from entity in prudenceERPEntities.SYSUSER_EVNTLOG.AsNoTracking()
    //                     where entity.CRT_UTC >= beginCrtUtc && entity.CRT_UTC <= endCrtUtc
    //                     select new UserEvntLogInfo
    //                     {
    //                         EVNT_ID = entity.EVNT_ID,
    //                         USER_NO = entity.USER_NO,
    //                         PRG_AREA = entity.PRG_AREA,
    //                         MVC_CTRL = entity.MVC_CTRL,
    //                         MVC_ACT = entity.MVC_ACT,
    //                         DESCTXT = entity.DESCTXT,
    //                         CRT_UTC = entity.CRT_UTC.Value,
    //                         HOST_IPADDR = entity.HOST_IPADDR,
    //                         POST_DATA = entity.POST_DATA,

    //                     };
    //        var list = result.ToList();
    //    }

    //    private static void InsertRoleRelation()
    //    {
    //        var role = prudenceERPEntities.SYSROLE.Single(o => o.ROLE_NO == "10000001");//

    //        var user = new SYSUSER();
    //        user.COMP_NO = "1000";
    //        user.DEP_NO = "10000000";
    //        user.LOGIN_ID = "tester@gmail.com";
    //        user.USER_CULTURE = "zh-TW";
    //        user.USER_TIMEZONE = "Taipei Standard Time";
    //        user.USER_PWD = "mis123";
    //        user.LOGIN_PROVIDER = 0;
    //        user.USER_NAME = "tester";
    //        user.USER_NO = "99999999";
    //        user.SYSROLE.Add(role);
    //        //重點要從資料庫查回來的加入才可以寫回
    //        //user.SYSROLE.Add(new SYSROLE { ROLE_NO = "10000000", ROLE_NAME = "System Administrators" });
    //        prudenceERPEntities.SYSUSER.Add(user);

    //        //上面的不能這樣加入，要像下方由主從關係加入
    //        //  var user = prudenceERPEntities.SYSUSER.Single(o => o.USER_NO == "10000000");

    //        // role.SYSUSER.Add(user);1.ok

    //        //2.ok
    //        //  user.SYSROLE.Add(role);
    //        prudenceERPEntities.SaveChanges();
    //    }

    //    private static void FirstQty()
    //    {
    //        var count = prudenceERPEntities.GRPREC.First().QTY;
    //        Console.WriteLine("GRPREC" + count.ToString());
    //    }

    //    private static void ProcedureTest()
    //    {
    //        //var query = prudenceERPEntities.SysCore_Auth_GetModuleByUser("10000000").ToList();
    //        //Console.WriteLine(query.Count.ToString());
    //    }

    //    #region Order by
    //    private static void OrderByTest()
    //    {
    //        //var query = prudenceERPEntities.SysCore_Auth_GetMenuByUser("10000000", "SysCore").AsQueryable();
    //        //query = OrderBy(query, "MVC_ACT", false, false);
    //        //Console.WriteLine("OrderByTes");
    //        //Console.WriteLine("======================");
    //        //foreach (var item in query)
    //        //{
    //        //    Console.WriteLine(item.PRG_NAME);
    //        //}
    //    }

    //    private static void ThenByTest()
    //    {
    //        //var query = prudenceERPEntities.SysCore_Auth_GetMenuByUser("10000000", "SysCore").AsQueryable();
    //        //query = OrderBy(query, "MVC_ACT", false, false);
    //        //query = OrderBy(query, "ORDERNUM", false, true);
    //        //Console.WriteLine("ThenByTest");
    //        //Console.WriteLine("======================");
    //        //foreach (var item in query)
    //        //{
    //        //    Console.WriteLine(item.PRG_NAME);
    //        //}

    //    }

    //    public static IQueryable<T> OrderBy<T>(IQueryable<T> source2, string propertyName, bool ascending, bool isThenBy) where T : class
    //    {
    //        Type type = typeof(T);

    //        PropertyInfo property = type.GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
    //        if (property == null)
    //            throw new ArgumentException("propertyName", "Not Exist");
    //        var source = source2.AsQueryable();
    //        ParameterExpression param = Expression.Parameter(type, "p");
    //        Expression propertyAccessExpression = Expression.MakeMemberAccess(param, property);
    //        LambdaExpression orderByExpression = Expression.Lambda(propertyAccessExpression, param);

    //        string methodName = ascending ? "OrderBy" : "OrderByDescending";
    //        if (isThenBy)
    //            methodName = ascending ? "ThenBy" : "ThenByDescending";
    //        MethodCallExpression resultExp = Expression.Call(typeof(Queryable), methodName, new Type[] { type, property.PropertyType }, source.Expression, Expression.Quote(orderByExpression));
    //        return source.Provider.CreateQuery<T>(resultExp);
    //    }
    //    #endregion

    //    #region DynamicQueryExtension
    //    private static void DynamicOrderByTest()
    //    {
    //        var query = prudenceERPEntities.SYSPRG.AsQueryable();
    //        query = query.OrderBy("MVC_ACT desc");
    //        Console.WriteLine("DynamicOrderByTest");
    //        Console.WriteLine("======================");
    //        foreach (var item in query)
    //        {
    //            Console.WriteLine(item.PRG_NAME);
    //        }
    //    }

    //    private static void DynamicThenByTest()
    //    {
    //        var query = prudenceERPEntities.SYSPRG.AsQueryable();
    //        query = query.OrderBy("MVC_ACT asc,ORDERNUM desc");

    //        Console.WriteLine("DynamicThenByTest");
    //        Console.WriteLine("======================");
    //        foreach (var item in query)
    //        {
    //            Console.WriteLine(item.PRG_NAME);
    //        }
    //    }

    //    private static void DynamicWhereTest()
    //    {
    //        var query = prudenceERPEntities.SYSPRG.AsQueryable();
    //        string searchColumn = "PRG_NAME";
    //        string keyword = "儲存";
    //        string queryExpression = string.Format("{0}.Contains(@0) && ORDERNUM>@1", searchColumn);
    //        query = query.Where(queryExpression, keyword, 1);
    //        Console.WriteLine("DynamicWhereTest");
    //        Console.WriteLine("======================");
    //        foreach (var item in query)
    //        {
    //            Console.WriteLine(item.PRG_NAME);
    //        }
    //    }
    //    #endregion
    //}
    #endregion

    #region Log Test
    //class Program
    //{
    //    static Entities prudenceERPEntities;
    //    static void Main(string[] args)
    //    {
    //        prudenceERPEntities = new Entities();
    //        LogDelegate();
    //        Console.Read();

    //    }

    //    private static void LogDelegate()
    //    {
    //        prudenceERPEntities.Database.Log = (log) => Debug.WriteLine(log);
    //        var list = prudenceERPEntities.BOOKS.ToList();
    //    }
    //}
    #endregion

    #region Cache Test
    class Program
    {
        static Entities prudenceERPEntities;
        static void Main(string[] args)
        {
            prudenceERPEntities = new Entities();
            prudenceERPEntities.Configuration.LazyLoadingEnabled = false;
            prudenceERPEntities.Configuration.ProxyCreationEnabled = false;


            //AsNoTracking 可以避免快取，但無法使用SaveChanges
            var code = prudenceERPEntities.SYSCODE.Find("KIMZ0001");
            Console.WriteLine(code.CODE_NO);
            code.CODE_NO = "B";
            int effect = prudenceERPEntities.SaveChanges();
            //effect=0


            Console.ReadLine();
            var code2 = prudenceERPEntities.SYSCODE.SingleOrDefault(o => o.CODE_ID == "KIMZ0001");
            Console.WriteLine(code2.CODE_NO);
            Console.ReadLine();

            var codes = prudenceERPEntities.SYSCODE.Where(o => o.CKIND == code.CKIND).ToList();
            foreach (var item in codes)
            {
                Console.WriteLine(item.CODE_NO);
            }
            Console.ReadLine();

        }


    }
    #endregion


}
