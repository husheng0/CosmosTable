﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CosmosConfigurator
{
    public class TabRow
    {
        public virtual bool IsAutoParse
        {
            get { return false; }
        }
        public int RowNumber;
        protected TabRow()
        {
        }

        public virtual void Parse(string[] cellStrs)
        {
        }

        public virtual object PrimaryKey
        {
            get
            {
                return null;
            }
        }

        protected string Get_string(string value, string defaultValue)
        {
            if (string.IsNullOrEmpty(value))
                return defaultValue;
            return value;
        }
        protected int Get_int(string value, string defaultValue)
        {
            var str = Get_string(value, defaultValue);
            return string.IsNullOrEmpty(str) ? default(int) : int.Parse(str);
        }

        protected uint Get_uint(string value, string defaultValue)
        {
            var str = Get_string(value, defaultValue);
            return string.IsNullOrEmpty(str) ? default(int) : uint.Parse(str);
        }

        protected string[] Get_string_array(string value, string defaultValue)
        {
            var str = Get_string(value, defaultValue);
            return str.Split('|');
        }
    }

    /// <summary>
    /// Default Tab Row
    /// Store All column Values
    /// </summary>
    public class DefaultTabRow : TabRow
    {
        public string[] Values;

        public override void Parse(string[] cellStrs)
        {
            Values = cellStrs;
        }
    }

}
