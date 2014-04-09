using EntityFrameworkLab.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
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
    class Program
    {
        static PrudenceERPEntities3 prudenceERPEntities;
        static void Main(string[] args)
        {
            prudenceERPEntities = new PrudenceERPEntities3();
            //FirstQty();
            //ProcedureTest();
            // OrderByTest();
            // ThenByTest();
            DynamicOrderByTest();
            DynamicThenByTest();
            DynamicWhereTest();
            Console.Read();

        }

        private static void FirstQty()
        {
            var count = prudenceERPEntities.GRPREC.First().QTY;
            Console.WriteLine("GRPREC" + count.ToString());
        }

        private static void ProcedureTest()
        {
            var query = prudenceERPEntities.SysCore_Auth_GetModuleByUser("10000000").ToList();
            Console.WriteLine(query.Count.ToString());
        }

        #region Order by
        private static void OrderByTest()
        {
            var query = prudenceERPEntities.SysCore_Auth_GetMenuByUser("10000000", "SysCore").AsQueryable();
            query = OrderBy(query, "MVC_ACT", false, false);
            Console.WriteLine("OrderByTes");
            Console.WriteLine("======================");
            foreach (var item in query)
            {
                Console.WriteLine(item.PRG_NAME);
            }
        }

        private static void ThenByTest()
        {
            var query = prudenceERPEntities.SysCore_Auth_GetMenuByUser("10000000", "SysCore").AsQueryable();
            query = OrderBy(query, "MVC_ACT", false, false);
            query = OrderBy(query, "ORDERNUM", false, true);
            Console.WriteLine("ThenByTest");
            Console.WriteLine("======================");
            foreach (var item in query)
            {
                Console.WriteLine(item.PRG_NAME);
            }

        }

        public static IQueryable<T> OrderBy<T>(IQueryable<T> source2, string propertyName, bool ascending, bool isThenBy) where T : class
        {
            Type type = typeof(T);

            PropertyInfo property = type.GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
            if (property == null)
                throw new ArgumentException("propertyName", "Not Exist");
            var source = source2.AsQueryable();
            ParameterExpression param = Expression.Parameter(type, "p");
            Expression propertyAccessExpression = Expression.MakeMemberAccess(param, property);
            LambdaExpression orderByExpression = Expression.Lambda(propertyAccessExpression, param);

            string methodName = ascending ? "OrderBy" : "OrderByDescending";
            if (isThenBy)
                methodName = ascending ? "ThenBy" : "ThenByDescending";
            MethodCallExpression resultExp = Expression.Call(typeof(Queryable), methodName, new Type[] { type, property.PropertyType }, source.Expression, Expression.Quote(orderByExpression));
            return source.Provider.CreateQuery<T>(resultExp);
        }
        #endregion

        #region DynamicQueryExtension
        private static void DynamicOrderByTest()
        {
            var query = prudenceERPEntities.SYSPRG.AsQueryable();
            query = query.OrderBy("MVC_ACT desc");
            Console.WriteLine("DynamicOrderByTest");
            Console.WriteLine("======================");
            foreach (var item in query)
            {
                Console.WriteLine(item.PRG_NAME);
            }
        }

        private static void DynamicThenByTest()
        {
            var query = prudenceERPEntities.SYSPRG.AsQueryable();
            query = query.OrderBy("MVC_ACT asc,ORDERNUM desc");

            Console.WriteLine("DynamicThenByTest");
            Console.WriteLine("======================");
            foreach (var item in query)
            {
                Console.WriteLine(item.PRG_NAME);
            }
        }

        private static void DynamicWhereTest()
        {
            var query = prudenceERPEntities.SYSPRG.AsQueryable();
            string searchColumn = "PRG_NAME";
            string keyword = "儲存";
            string queryExpression = string.Format("{0}.Contains(@0) && ORDERNUM>@1", searchColumn);
            query = query.Where(queryExpression, keyword, 1);
            Console.WriteLine("DynamicWhereTest");
            Console.WriteLine("======================");
            foreach (var item in query)
            {
                Console.WriteLine(item.PRG_NAME);
            }
        }
        #endregion
    }
}
