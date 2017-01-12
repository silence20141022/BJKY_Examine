using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.ActiveRecord;
using Aim.Portal.Model;
using NHibernate;

namespace Aim.Examining.Model
{
    [Serializable]
    public abstract class ExamModelBase<T> : ModelBase<T> where T : ExamModelBase<T>,new()
    {
        public IList<T> GetOtherMap(string tableName,string withwhereString)
        {

            string query = string.Format("select * from {0} {1}",tableName, withwhereString);
            return GetOtherMapBySql(query);

        }
        public IList<T> GetOtherMapBySql(string sql)
        {

            return (IList<T>)ActiveRecordMediator<T>.Execute(
                delegate(ISession session, object instance)
                {
                    return session.CreateSQLQuery(sql).AddEntity("synonym", typeof(T)).List<T>();
                }, new T());

        }   
    }
}
