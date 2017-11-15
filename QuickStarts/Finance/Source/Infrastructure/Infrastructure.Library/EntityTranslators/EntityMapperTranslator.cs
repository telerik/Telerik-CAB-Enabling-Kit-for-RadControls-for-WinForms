//----------------------------------------------------------------------------------------
// patterns & practices - Smart Client Software Factory - Guidance Package
//
// The EntityMapperTranslator class is a base helper implementation of an IEntityTranslator
// that provides placeholders to translate from business entities to service and viceversa
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
	public abstract class EntityMapperTranslator<TBusinessEntity, TServiceEntity> : BaseTranslator
	{
		public override bool CanTranslate(Type targetType, Type sourceType)
		{
			return (targetType == typeof(TBusinessEntity) && sourceType == typeof(TServiceEntity)) ||
				(targetType == typeof(TServiceEntity) && sourceType == typeof(TBusinessEntity));
		}

		public override object Translate(IEntityTranslatorService service, Type targetType, object source)
		{
			if (targetType == typeof(TBusinessEntity))
				return ServiceToBusiness(service, (TServiceEntity)source);
			if (targetType == typeof(TServiceEntity))
				return BusinessToService(service, (TBusinessEntity)source);

			throw new EntityTranslatorException();
		}

		protected abstract TServiceEntity BusinessToService(IEntityTranslatorService service, TBusinessEntity value);
		protected abstract TBusinessEntity ServiceToBusiness(IEntityTranslatorService service, TServiceEntity value);
	}
}
