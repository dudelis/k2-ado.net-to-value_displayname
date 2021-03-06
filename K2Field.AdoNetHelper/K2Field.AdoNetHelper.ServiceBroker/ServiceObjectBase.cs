﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using K2Field.AdoNetHelper.ServiceBroker.Constants;
using K2Field.AdoNetHelper.ServiceBroker.ServiceObjects;
using SourceCode.SmartObjects.Services.ServiceSDK.Objects;

namespace K2Field.AdoNetHelper.ServiceBroker
{
    public abstract class ServiceObjectBase
    {
        public virtual string ServiceFolder => string.Empty;
        
        

        protected ServiceBroker ServiceBroker
        {
            get;
            private set;
        }

        protected SoDefinitionCollection SoDefCollection { get; private set; }

        protected ServiceObjectBase(ServiceBroker sb)
        {
            ServiceBroker = sb;
        }
        protected ServiceObjectBase(ServiceBroker sb, SoDefinitionCollection soCollection)
        {
            ServiceBroker = sb;
            SoDefCollection = soCollection;
        }

        public abstract List<ServiceObject> DescribeServiceObjects();
        public abstract void Execute();

        protected string GetStringProperty(string name, bool isRequired = false)
        {
            var p = ServiceBroker.Service.ServiceObjects[0].Properties[name];
            if (p == null)
            {
                if (isRequired)
                    throw new ArgumentException(string.Format(Errors.RequiredPropertyNotFound, name));
                return string.Empty;
            }
            var val = p.Value as string;
            if (isRequired && string.IsNullOrEmpty(val))
                throw new ArgumentException(string.Format(Errors.RequiredPropertyIsEmpty, name));

            return val;
        }
        protected string GetStringParameter(string name, bool isRequired = false)
        {
            MethodParameter p = ServiceBroker.Service.ServiceObjects[0].Methods[0].MethodParameters[name];
            if (p == null)
            {
                if (isRequired)
                    throw new ArgumentException(string.Format(Errors.RequiredParameterNotFound, name));
                return string.Empty;
            }
            string val = p.Value as string;
            if (isRequired && string.IsNullOrEmpty(val))
                throw new ArgumentException(string.Format(Errors.RequiredParameterIsEmpty, name));

            return val;
        }
    }
}
