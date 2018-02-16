﻿using NHibernate;
using NHibernate.Engine;
using NHibernate.SqlTypes;
using NHibernate.UserTypes;
using NodaTime;
using System;
using System.Data.Common;

namespace Dematt.Airy.Nhibernate.NodaTime
{
    public class OffsetDateTimeType : ImmutableUserType, IUserType
    {
        public SqlType[] SqlTypes
        {
            get { return new[] { SqlTypeFactory.DateTimeOffSet }; }
        }

        public Type ReturnedType
        {
            get { return typeof(OffsetDateTime); }
        }

        public new bool Equals(object x, object y)
        {
            if (x == null && y == null) return true;
            if (x == null || y == null) return false;
            return ((OffsetDateTime)x).ToInstant() == ((OffsetDateTime)y).ToInstant();
        }

        public int GetHashCode(object x)
        {
            return x == null ? 0 : ((OffsetDateTime)x).ToInstant().GetHashCode();
        }

        public object NullSafeGet(DbDataReader rs, string[] names, ISessionImplementor session, object owner)
        {
            var value = NHibernateUtil.DateTimeOffset.NullSafeGet(rs, names, session, owner);
            if (value == null)
            {
                return null;
            }
            return OffsetDateTime.FromDateTimeOffset((DateTimeOffset)value);
        }

        public void NullSafeSet(DbCommand cmd, object value, int index, ISessionImplementor session)
        {
            if (value == null)
            {
                NHibernateUtil.DateTimeOffset.NullSafeSet(cmd, null, index, session);
            }
            else
            {
                NHibernateUtil.DateTimeOffset.NullSafeSet(cmd, ((OffsetDateTime)value).ToDateTimeOffset(), index, session);
            }
        }
    }
}
