// Copyright 2004-2007 Castle Project - http://www.castleproject.org/
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace Castle.Windsor.Installer
{
	using System;
	using System.Configuration;

	using Castle.Core.Configuration;
	using Castle.Core.Resource;
	using Castle.MicroKernel;
	using Castle.Windsor.Configuration.Interpreters;

	/// <summary>
	/// Default <see cref="IComponentsInstaller"/> implementation.
	/// </summary>
	public class DefaultComponentInstaller : IComponentsInstaller
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="DefaultComponentInstaller"/> class.
		/// </summary>
		public DefaultComponentInstaller()
		{
		}

		#region IComponentsInstaller Members

		/// <summary>
		/// Perform installation.
		/// </summary>
		/// <param name="container">Target container</param>
		/// <param name="store">Configuration store</param>
		public void SetUp(IWindsorContainer container, IConfigurationStore store)
		{
			SetUpComponents(store.GetBootstrapComponents(), container);
			SetUpFacilities(store.GetFacilities(), container);
			SetUpComponents(store.GetComponents(), container);
			SetUpChildContainers(store.GetConfigurationForChildContainers(), container);
		}

		#endregion

		protected virtual void SetUpFacilities(IConfiguration[] configurations, IWindsorContainer container)
		{
			foreach(IConfiguration facility in configurations)
			{
				String id = facility.Attributes["id"];
				String typeName = facility.Attributes["type"];
				if (typeName == null || typeName.Length == 0) continue;

				Type type = ObtainType(typeName);

				IFacility facilityInstance = InstantiateFacility(type);

				System.Diagnostics.Debug.Assert( id != null );
				System.Diagnostics.Debug.Assert( facilityInstance != null );

				container.AddFacility(id, facilityInstance);
			}
		}

		protected virtual void SetUpComponents(IConfiguration[] configurations, IWindsorContainer container)
		{
			foreach(IConfiguration component in configurations)
			{
				String id = component.Attributes["id"];
				
				String typeName = component.Attributes["type"];
				String serviceTypeName = component.Attributes["service"];
				
				if (typeName == null || typeName.Length == 0) continue;

				Type type = ObtainType(typeName);
				Type service = type;

				if (serviceTypeName != null && serviceTypeName.Length != 0)
				{
					service = ObtainType(serviceTypeName);
				}

				System.Diagnostics.Debug.Assert( id != null );
				System.Diagnostics.Debug.Assert( type != null );
				System.Diagnostics.Debug.Assert( service != null );

				container.AddComponent(id, service, type);
			}
		}

		private void SetUpChildContainers(IConfiguration[] configurations, IWindsorContainer parentContainer)
		{
			foreach(IConfiguration childContainerConfig in configurations)
			{
				String id = childContainerConfig.Attributes["name"];
				
				System.Diagnostics.Debug.Assert( id != null );

				new WindsorContainer(id, parentContainer, 
					new XmlInterpreter(new StaticContentResource(childContainerConfig.Value)));
			}
		}

		private Type ObtainType(String typeName)
		{
			Type type = Type.GetType(typeName, false, false);

			if (type == null)
			{
				String message = String.Format("The type name {0} could not be located", typeName);

				throw new ConfigurationErrorsException(message);
			}

			return type;
		}

		private IFacility InstantiateFacility(Type facilityType)
		{
			if (!typeof(IFacility).IsAssignableFrom( facilityType ))
			{
				String message = String.Format("Type {0} does not implement the interface IFacility", facilityType.FullName);

				throw new ConfigurationErrorsException(message);
			}

			try
			{
				return (IFacility) Activator.CreateInstance(facilityType);
			}
			catch(Exception)
			{
				String message = String.Format("Could not instantiate {0}. Does it have a public default constructor?", facilityType.FullName);

				throw new ConfigurationErrorsException(message);
			}
		}
	}
}
