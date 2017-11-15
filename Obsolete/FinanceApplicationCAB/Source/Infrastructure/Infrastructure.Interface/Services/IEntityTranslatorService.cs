//----------------------------------------------------------------------------------------
// patterns & practices - Smart Client Software Factory - Guidance Package
//
// The EntityTranslatorService class is a service that provides a registry of
// translators and translation services between two classes. The user must implement
// the translators and register them with the service.
// 
// For more information see: 
// ms-help://MS.VSCC.v80/MS.VSIPCC.v80/ms.practices.scsf.2007may/SCSF/html/03-01-090-How_to_Translate_Between_Business_Entities_and_Service_Entities.htm
//
// Latest version of this Guidance Package: http://go.microsoft.com/fwlink/?LinkId=62182
//----------------------------------------------------------------------------------------

using System;

namespace FinanceApplicationCAB.Infrastructure.Interface.Services
{
	public interface IEntityTranslatorService
	{
		bool CanTranslate(Type targetType, Type sourceType);
		bool CanTranslate<TTarget, TSource>();
		object Translate(Type targetType, object source);
		TTarget Translate<TTarget>(object source);
		void RegisterEntityTranslator(IEntityTranslator translator);
		void RemoveEntityTranslator(IEntityTranslator translator);
	}
}
