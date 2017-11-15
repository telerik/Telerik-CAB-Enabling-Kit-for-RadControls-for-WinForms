//----------------------------------------------------------------------------------------
// patterns & practices - Smart Client Software Factory - Guidance Package
//
// The BaseTranslator class is a base helper implementation of an IEntityTranslator
// 
// For more information see: 
// ms-help://MS.VSCC.v80/MS.VSIPCC.v80/ms.practices.scsf.2007may/SCSF/html/03-01-090-How_to_Translate_Between_Business_Entities_and_Service_Entities.htm
//
// Latest version of this Guidance Package: http://go.microsoft.com/fwlink/?LinkId=62182
//----------------------------------------------------------------------------------------

using System;
using FinanceApplicationCAB.Infrastructure.Interface.Services;

namespace FinanceApplicationCAB.Infrastructure.Library.EntityTranslators
{
	public abstract class BaseTranslator : IEntityTranslator
	{
		public abstract bool CanTranslate(Type targetType, Type sourceType);

		public bool CanTranslate<TTarget, TSource>()
		{
			return CanTranslate(typeof(TTarget), typeof(TSource));
		}

		public TTarget Translate<TTarget>(IEntityTranslatorService service, object source)
		{
			return (TTarget)Translate(service, typeof(TTarget), source);
		}

		public abstract object Translate(IEntityTranslatorService service, Type targetType, object source);
	}
}
