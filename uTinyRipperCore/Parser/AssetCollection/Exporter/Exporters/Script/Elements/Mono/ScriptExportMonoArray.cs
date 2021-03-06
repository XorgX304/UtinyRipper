﻿using Mono.Cecil;
using System;

namespace uTinyRipper.Exporters.Scripts.Mono
{
	public sealed class ScriptExportMonoArray : ScriptExportArray
	{
		public ScriptExportMonoArray(TypeReference type)
		{
			if (type == null)
			{
				throw new ArgumentNullException(nameof(type));
			}
			if (!type.IsArray)
			{
				throw new Exception("Type isn't an array");
			}

			Type = type;

			TypeName = ScriptExportMonoType.GetTypeName(Type);
			Name = ScriptExportMonoType.GetName(Type, TypeName);
			Module = ScriptExportMonoType.GetModule(Type);
			FullName = ScriptExportMonoType.GetFullName(Type, Module);
		}
		
		public override void Init(IScriptExportManager manager)
		{
			TypeSpecification specification = (TypeSpecification)Type;
			m_element = manager.RetrieveType(specification.ElementType);
		}

		public sealed override bool HasMember(string name)
		{
			throw new NotSupportedException();
		}

		public override ScriptExportType Element => m_element;

		public override string FullName { get; }
		public override string Name { get; }
		public override string TypeName { get; }
		public override string Namespace => Element.Namespace;
		public override string Module { get; }

		private TypeReference Type { get; }
		
		private ScriptExportType m_element;
	}
}