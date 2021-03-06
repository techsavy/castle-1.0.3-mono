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

namespace AspectSharp.Lang.AST
{
	using System;

	/// <summary>
	/// Summary description for ImportDirective.
	/// </summary>
	public class ImportDirective : NodeBase
	{
		private String _namespace;
		private AssemblyReference _assembly;

		public ImportDirective(LexicalInfo lexInfo, String ns) : base(lexInfo)
		{
			Namespace = ns;
		}

		public String Namespace
		{
			get { return _namespace; }
			set { _namespace = value; }
		}

		public AssemblyReference AssemblyReference
		{
			get { return _assembly; }
			set { _assembly = value; }
		}

		public override void Accept(IVisitor visitor)
		{
			visitor.OnImportDirective(this);
		}
	}
}
