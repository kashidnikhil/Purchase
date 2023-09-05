using Abp.Application.Editions;
using Abp.Application.Features;
using Abp.Auditing;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.DynamicEntityProperties;
using Abp.EntityHistory;
using Abp.Localization;
using Abp.Notifications;
using Abp.Organizations;
using Abp.UI.Inputs;
using Abp.Webhooks;
using AutoMapper;
using IdentityServer4.Extensions;
using MyTraining1101Demo.Auditing.Dto;
using MyTraining1101Demo.Authorization.Accounts.Dto;
using MyTraining1101Demo.Authorization.Delegation;
using MyTraining1101Demo.Authorization.Permissions.Dto;
using MyTraining1101Demo.Authorization.Roles;
using MyTraining1101Demo.Authorization.Roles.Dto;
using MyTraining1101Demo.Authorization.Users;
using MyTraining1101Demo.Authorization.Users.Delegation.Dto;
using MyTraining1101Demo.Authorization.Users.Dto;
using MyTraining1101Demo.Authorization.Users.Importing.Dto;
using MyTraining1101Demo.Authorization.Users.Profile.Dto;
using MyTraining1101Demo.Chat;
using MyTraining1101Demo.Chat.Dto;
using MyTraining1101Demo.Customer;
using MyTraining1101Demo.Customer.Dto;
using MyTraining1101Demo.Dto;
using MyTraining1101Demo.DynamicEntityProperties.Dto;
using MyTraining1101Demo.Editions;
using MyTraining1101Demo.Editions.Dto;
using MyTraining1101Demo.Friendships;
using MyTraining1101Demo.Friendships.Cache;
using MyTraining1101Demo.Friendships.Dto;
using MyTraining1101Demo.Localization.Dto;
using MyTraining1101Demo.MultiTenancy;
using MyTraining1101Demo.MultiTenancy.Dto;
using MyTraining1101Demo.MultiTenancy.HostDashboard.Dto;
using MyTraining1101Demo.MultiTenancy.Payments;
using MyTraining1101Demo.MultiTenancy.Payments.Dto;
using MyTraining1101Demo.Notifications.Dto;
using MyTraining1101Demo.Organizations.Dto;
using MyTraining1101Demo.PhoneBook;
using MyTraining1101Demo.PhoneBook.Dto;
using MyTraining1101Demo.Purchase.AcceptanceCriterias;
using MyTraining1101Demo.Purchase.AcceptanceCriterias.Dto;
using MyTraining1101Demo.Purchase.Assemblies;
using MyTraining1101Demo.Purchase.Assemblies.Dto;
using MyTraining1101Demo.Purchase.Companies.CompanyAddresses;
using MyTraining1101Demo.Purchase.Companies.CompanyContactPersons;
using MyTraining1101Demo.Purchase.Companies.CompanyMaster;
using MyTraining1101Demo.Purchase.Companies.Dto.CompanyAddresses;
using MyTraining1101Demo.Purchase.Companies.Dto.CompanyContactPersons;
using MyTraining1101Demo.Purchase.Companies.Dto.CompanyMaster;
using MyTraining1101Demo.Purchase.DeliveryTerms;
using MyTraining1101Demo.Purchase.DeliveryTerms.Dto;
using MyTraining1101Demo.Purchase.ItemCategories;
using MyTraining1101Demo.Purchase.ItemCategories.Dto;
using MyTraining1101Demo.Purchase.Items.CalibrationAgenciesMaster;
using MyTraining1101Demo.Purchase.Items.CalibrationTypeMaster;
using MyTraining1101Demo.Purchase.Items.Dto.CalibrationAgenciesMaster;
using MyTraining1101Demo.Purchase.Items.Dto.CalibrationTypeMaster;
using MyTraining1101Demo.Purchase.Items.Dto.ItemAccessoriesMaster;
using MyTraining1101Demo.Purchase.Items.Dto.ItemAttachmentsMaster;
using MyTraining1101Demo.Purchase.Items.Dto.ItemMaster;
using MyTraining1101Demo.Purchase.Items.Dto.ItemRateRevisionMaster;
using MyTraining1101Demo.Purchase.Items.Dto.ItemStorageConditionMaster;
using MyTraining1101Demo.Purchase.Items.Dto.ItemSupplierMaster;
using MyTraining1101Demo.Purchase.Items.Dto.ProcurementMaster;
using MyTraining1101Demo.Purchase.Items.Dto.RequiredItemSparesMaster;
using MyTraining1101Demo.Purchase.Items.ItemAccesoriesMaster;
using MyTraining1101Demo.Purchase.Items.ItemAttachmentsMaster;
using MyTraining1101Demo.Purchase.Items.ItemMaster;
using MyTraining1101Demo.Purchase.Items.ItemRateRevisionMaster;
using MyTraining1101Demo.Purchase.Items.ItemStorageConditionMaster;
using MyTraining1101Demo.Purchase.Items.ItemSupplierMaster;
using MyTraining1101Demo.Purchase.Items.ProcurementMaster;
using MyTraining1101Demo.Purchase.Items.RequiredItemSparesMaster;
using MyTraining1101Demo.Purchase.LegalEntities;
using MyTraining1101Demo.Purchase.LegalEntities.Dto;
using MyTraining1101Demo.Purchase.MaterialGrades;
using MyTraining1101Demo.Purchase.MaterialGrades.Dto;
using MyTraining1101Demo.Purchase.Models;
using MyTraining1101Demo.Purchase.Models.Dto;
using MyTraining1101Demo.Purchase.ModelWiseItems.ModelWiseItemMasters;
using MyTraining1101Demo.Purchase.ModelWiseItems.ModelWiseItemMasters.Dto;
using MyTraining1101Demo.Purchase.ModelWiseItems.ModelWiseItems;
using MyTraining1101Demo.Purchase.ModelWiseItems.ModelWiseItems.Dto;
using MyTraining1101Demo.Purchase.POGeneralTerms;
using MyTraining1101Demo.Purchase.POGeneralTerms.Dto;
using MyTraining1101Demo.Purchase.SubAssemblies;
using MyTraining1101Demo.Purchase.SubAssemblies.Dto;
using MyTraining1101Demo.Purchase.SubAssemblyItems;
using MyTraining1101Demo.Purchase.SubAssemblyItems.Dto;
using MyTraining1101Demo.Purchase.SupplierCategories;
using MyTraining1101Demo.Purchase.SupplierCategories.Dto;
using MyTraining1101Demo.Purchase.Suppliers.Dto.MappedSupplierCategories;
using MyTraining1101Demo.Purchase.Suppliers.Dto.SupplierAddresses;
using MyTraining1101Demo.Purchase.Suppliers.Dto.SupplierBanks;
using MyTraining1101Demo.Purchase.Suppliers.Dto.SupplierContactPersons;
using MyTraining1101Demo.Purchase.Suppliers.Dto.SupplierMaster;
using MyTraining1101Demo.Purchase.Suppliers.MappedSupplierCategories;
using MyTraining1101Demo.Purchase.Suppliers.SupplierAddresses;
using MyTraining1101Demo.Purchase.Suppliers.SupplierBanks;
using MyTraining1101Demo.Purchase.Suppliers.SupplierContactPersons;
using MyTraining1101Demo.Purchase.Suppliers.SupplierMaster;
using MyTraining1101Demo.Purchase.TermsOfPayments;
using MyTraining1101Demo.Purchase.TermsOfPayments.Dto;
using MyTraining1101Demo.Purchase.Transactions.Dto.MaterialRequisitionItem;
using MyTraining1101Demo.Purchase.Transactions.Dto.MaterialRequisitionMaster;
using MyTraining1101Demo.Purchase.Transactions.MaterialRequisitionItemMaster;
using MyTraining1101Demo.Purchase.Transactions.MaterialRequisitionMaster;
using MyTraining1101Demo.Purchase.Units;
using MyTraining1101Demo.Purchase.Units.Dto;
using MyTraining1101Demo.Sessions.Dto;
using MyTraining1101Demo.WebHooks.Dto;

namespace MyTraining1101Demo
{
    internal static class CustomDtoMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            //Inputs
            configuration.CreateMap<CheckboxInputType, FeatureInputTypeDto>();
            configuration.CreateMap<SingleLineStringInputType, FeatureInputTypeDto>();
            configuration.CreateMap<ComboboxInputType, FeatureInputTypeDto>();
            configuration.CreateMap<IInputType, FeatureInputTypeDto>()
                .Include<CheckboxInputType, FeatureInputTypeDto>()
                .Include<SingleLineStringInputType, FeatureInputTypeDto>()
                .Include<ComboboxInputType, FeatureInputTypeDto>();
            configuration.CreateMap<StaticLocalizableComboboxItemSource, LocalizableComboboxItemSourceDto>();
            configuration.CreateMap<ILocalizableComboboxItemSource, LocalizableComboboxItemSourceDto>()
                .Include<StaticLocalizableComboboxItemSource, LocalizableComboboxItemSourceDto>();
            configuration.CreateMap<LocalizableComboboxItem, LocalizableComboboxItemDto>();
            configuration.CreateMap<ILocalizableComboboxItem, LocalizableComboboxItemDto>()
                .Include<LocalizableComboboxItem, LocalizableComboboxItemDto>();

            // PhoneBook (we will comment out other lines when the new DTOs are added)
            configuration.CreateMap<Person, PersonListDto>();
            configuration.CreateMap<AddPhoneInput, Phone>();
            configuration.CreateMap<CreatePersonInput, Person>();
            configuration.CreateMap<Person, GetPersonForEditOutput>();
            configuration.CreateMap<Phone, PhoneInPersonListDto>();

            // Customer Dto (we will comment out other lines when the new DTOs are added)
            configuration.CreateMap<customer, CustomerListDto>();
            //configuration.CreateMap<AddPhoneInput, Phone>();
            configuration.CreateMap<CreateCustomerInput, customer>();
            //configuration.CreateMap<Person, GetPersonForEditOutput>();
            // configuration.CreateMap<Phone, PhoneInPersonListDto>();


            //Chat
            configuration.CreateMap<ChatMessage, ChatMessageDto>();
            configuration.CreateMap<ChatMessage, ChatMessageExportDto>();

            //Feature
            configuration.CreateMap<FlatFeatureSelectDto, Feature>().ReverseMap();
            configuration.CreateMap<Feature, FlatFeatureDto>();

            //Role
            configuration.CreateMap<RoleEditDto, Role>().ReverseMap();
            configuration.CreateMap<Role, RoleListDto>();
            configuration.CreateMap<UserRole, UserListRoleDto>();

            

            //Edition
            configuration.CreateMap<EditionEditDto, SubscribableEdition>().ReverseMap();
            configuration.CreateMap<EditionCreateDto, SubscribableEdition>();
            configuration.CreateMap<EditionSelectDto, SubscribableEdition>().ReverseMap();
            configuration.CreateMap<SubscribableEdition, EditionInfoDto>();

            configuration.CreateMap<Edition, EditionInfoDto>().Include<SubscribableEdition, EditionInfoDto>();

            configuration.CreateMap<SubscribableEdition, EditionListDto>();
            configuration.CreateMap<Edition, EditionEditDto>();
            configuration.CreateMap<Edition, SubscribableEdition>();
            configuration.CreateMap<Edition, EditionSelectDto>();


            //Payment
            configuration.CreateMap<SubscriptionPaymentDto, SubscriptionPayment>().ReverseMap();
            configuration.CreateMap<SubscriptionPaymentListDto, SubscriptionPayment>().ReverseMap();
            configuration.CreateMap<SubscriptionPayment, SubscriptionPaymentInfoDto>();

            //Permission
            configuration.CreateMap<Permission, FlatPermissionDto>();
            configuration.CreateMap<Permission, FlatPermissionWithLevelDto>();

            //Language
            configuration.CreateMap<ApplicationLanguage, ApplicationLanguageEditDto>();
            configuration.CreateMap<ApplicationLanguage, ApplicationLanguageListDto>();
            configuration.CreateMap<NotificationDefinition, NotificationSubscriptionWithDisplayNameDto>();
            configuration.CreateMap<ApplicationLanguage, ApplicationLanguageEditDto>()
                .ForMember(ldto => ldto.IsEnabled, options => options.MapFrom(l => !l.IsDisabled));

            //Tenant
            configuration.CreateMap<Tenant, RecentTenant>();
            configuration.CreateMap<Tenant, TenantLoginInfoDto>();
            configuration.CreateMap<Tenant, TenantListDto>();
            configuration.CreateMap<TenantEditDto, Tenant>().ReverseMap();
            configuration.CreateMap<CurrentTenantInfoDto, Tenant>().ReverseMap();

            //User
            configuration.CreateMap<User, UserEditDto>()
                .ForMember(dto => dto.Password, options => options.Ignore())
                .ReverseMap()
                .ForMember(user => user.Password, options => options.Ignore());
            configuration.CreateMap<User, UserLoginInfoDto>();
            configuration.CreateMap<User, UserListDto>();
            configuration.CreateMap<User, ChatUserDto>();
            configuration.CreateMap<User, OrganizationUnitUserListDto>();
            configuration.CreateMap<Role, OrganizationUnitRoleListDto>();
            configuration.CreateMap<CurrentUserProfileEditDto, User>().ReverseMap();
            configuration.CreateMap<UserLoginAttemptDto, UserLoginAttempt>().ReverseMap();
            configuration.CreateMap<ImportUserDto, User>();

            //AuditLog
            configuration.CreateMap<AuditLog, AuditLogListDto>();
            configuration.CreateMap<EntityChange, EntityChangeListDto>();
            configuration.CreateMap<EntityPropertyChange, EntityPropertyChangeDto>();

            //Friendship
            configuration.CreateMap<Friendship, FriendDto>();
            configuration.CreateMap<FriendCacheItem, FriendDto>();

            //OrganizationUnit
            configuration.CreateMap<OrganizationUnit, OrganizationUnitDto>();

            //Webhooks
            configuration.CreateMap<WebhookSubscription, GetAllSubscriptionsOutput>();
            configuration.CreateMap<WebhookSendAttempt, GetAllSendAttemptsOutput>()
                .ForMember(webhookSendAttemptListDto => webhookSendAttemptListDto.WebhookName,
                    options => options.MapFrom(l => l.WebhookEvent.WebhookName))
                .ForMember(webhookSendAttemptListDto => webhookSendAttemptListDto.Data,
                    options => options.MapFrom(l => l.WebhookEvent.Data));

            configuration.CreateMap<WebhookSendAttempt, GetAllSendAttemptsOfWebhookEventOutput>();

            configuration.CreateMap<DynamicProperty, DynamicPropertyDto>().ReverseMap();
            configuration.CreateMap<DynamicPropertyValue, DynamicPropertyValueDto>().ReverseMap();
            configuration.CreateMap<DynamicEntityProperty, DynamicEntityPropertyDto>()
                .ForMember(dto => dto.DynamicPropertyName,
                    options => options.MapFrom(entity => entity.DynamicProperty.DisplayName.IsNullOrEmpty() ? entity.DynamicProperty.PropertyName : entity.DynamicProperty.DisplayName));
            configuration.CreateMap<DynamicEntityPropertyDto, DynamicEntityProperty>();

            configuration.CreateMap<DynamicEntityPropertyValue, DynamicEntityPropertyValueDto>().ReverseMap();
            
            //User Delegations
            configuration.CreateMap<CreateUserDelegationDto, UserDelegation>();

            configuration.CreateMap<AcceptanceCriteria, AcceptanceCriteriaDto>().ReverseMap();
            configuration.CreateMap<AcceptanceCriteria, AcceptanceCriteriaInputDto>().ReverseMap();

            configuration.CreateMap<DeliveryTerm, DeliveryTermDto>().ReverseMap();
            configuration.CreateMap<DeliveryTerm, DeliveryTermInputDto>().ReverseMap();

            configuration.CreateMap<MaterialGrade, MaterialGradeDto>().ReverseMap();
            configuration.CreateMap<MaterialGrade, MaterialGradeInputDto>().ReverseMap();

            configuration.CreateMap<POGeneralTerm, POGeneralTermDto>().ReverseMap();
            configuration.CreateMap<POGeneralTerm, POGeneralTermInputDto>().ReverseMap();

            configuration.CreateMap<SupplierCategory, SupplierCategoryDto>().ReverseMap();
            configuration.CreateMap<SupplierCategory, SupplierCategoryInputDto>().ReverseMap();

            configuration.CreateMap<TermsOfPayment, TermsOfPaymentDto>().ReverseMap();
            configuration.CreateMap<TermsOfPayment, TermsOfPaymentInputDto>().ReverseMap();

            configuration.CreateMap<Unit, UnitDto>().ReverseMap();
            configuration.CreateMap<Unit, UnitInputDto>().ReverseMap();

            configuration.CreateMap<LegalEntity, LegalEntityDto>().ReverseMap();
            configuration.CreateMap<LegalEntity, LegalEntityInputDto>().ReverseMap();

            configuration.CreateMap<Model, ModelDto>().ReverseMap();
            configuration.CreateMap<Model, ModelInputDto>().ReverseMap();

            configuration.CreateMap<Supplier, SupplierDto>()
                 .ForMember(dto => dto.DeliveryBy, options => options.MapFrom(x => x.DeliveryBy))
                 .ForMember(dto => dto.PaymentMode, options => options.MapFrom(x => x.PaymentMode))
                 .ForMember(dto => dto.Category, options => options.MapFrom(x => x.Category))
                 .ReverseMap();
            
            configuration.CreateMap<Supplier, SupplierInputDto>()
                 .ForMember(dto => dto.DeliveryBy, options => options.MapFrom(x => x.DeliveryBy))
                 .ForMember(dto => dto.PaymentMode, options => options.MapFrom(x => x.PaymentMode))
                 .ForMember(dto => dto.Category, options => options.MapFrom(x => x.Category))
                 .ReverseMap();

            configuration.CreateMap<Supplier, SupplierListDto>().ReverseMap();

            configuration.CreateMap<SupplierBank, SupplierBankDto>()
                 .ForMember(dto => dto.PaymentMode, options => options.MapFrom(x => x.PaymentMode))
                 .ReverseMap();
            configuration.CreateMap<SupplierBank, SupplierBankInputDto>()
                 .ForMember(dto => dto.PaymentMode, options => options.MapFrom(x => x.PaymentMode))
                 .ReverseMap();

            configuration.CreateMap<SupplierContactPerson, SupplierContactPersonDto>().ReverseMap();
            configuration.CreateMap<SupplierContactPerson, SupplierContactPersonInputDto>().ReverseMap();

            configuration.CreateMap<SupplierAddress, SupplierAddressDto>().ReverseMap();
            configuration.CreateMap<SupplierAddress, SupplierAddressInputDto>().ReverseMap();

            configuration.CreateMap<Company, CompanyInputDto>().ReverseMap();
            configuration.CreateMap<Company, CompanyDto>().ReverseMap();
            configuration.CreateMap<Company, CompanyListDto>()
                .ForMember(dto => dto.Id, options => options.MapFrom(x => x.Id))
                .ForMember(dto => dto.Name, options => options.MapFrom(x => x.Name))
                .ReverseMap();

            configuration.CreateMap<CompanyContactPerson, CompanyContactPersonDto>().ReverseMap();
            configuration.CreateMap<CompanyContactPerson, CompanyContactPersonInputDto>().ReverseMap();

            configuration.CreateMap<CompanyAddress, CompanyAddressDto>().ReverseMap();
            configuration.CreateMap<CompanyAddress, CompanyAddressInputDto>().ReverseMap();


            configuration.CreateMap<MappedSupplierCategory, MappedSupplierCategoryDto>()
                .ForMember(dto => dto.Name, options => options.MapFrom(x => x.SupplierCategory.Name))
                .ForMember(dto => dto.Description, options => options.MapFrom(x => x.SupplierCategory.Description))
                .ReverseMap();
            configuration.CreateMap<MappedSupplierCategory, MappedSupplierCategoryInputDto>().ReverseMap();


            configuration.CreateMap<Item, ItemMasterDto>().ReverseMap();
            configuration.CreateMap<Item, ItemMasterInputDto>().ReverseMap();
            configuration.CreateMap<Item, ItemMasterListDto>()
                .ForMember(dto => dto.UnitName, options => options.MapFrom(x => x.OrderingUOM.Name))
                .ForMember(dto => dto.CategoryName, options => options.MapFrom(x => x.ItemCategory.Name))
                .ReverseMap();
            configuration.CreateMap<Item, ItemListDto>().ReverseMap(); 


            configuration.CreateMap<CalibrationAgency, CalibrationAgencyDto>().ReverseMap();
            configuration.CreateMap<CalibrationAgency, CalibrationAgencyInputDto>().ReverseMap();

            configuration.CreateMap<CalibrationType, CalibrationTypeDto>().ReverseMap();
            configuration.CreateMap<CalibrationType, CalibrationTypeInputDto>().ReverseMap();

            configuration.CreateMap<ItemAttachment, ItemAttachmentDto>().ReverseMap();
            configuration.CreateMap<ItemAttachment, ItemAttachmentInputDto>().ReverseMap();

            configuration.CreateMap<ItemRateRevision, ItemRateRevisionDto>().ReverseMap();
            //configuration.CreateMap<ItemRateRevision, ItemRateRevisionInputDto>().ReverseMap();
            
            configuration.CreateMap<ItemMasterInputDto,ItemRateRevision> ()
                .ForMember(dto => dto.Make, options => options.MapFrom(x => x.Make))
                .ForMember(dto => dto.RatePerOrderingQuantity, options => options.MapFrom(x => x.RatePerQuantity))
                .ForMember(dto => dto.StockUOMId, options => options.MapFrom(x => x.StockUOMId))
                .ForMember(dto => dto.OrderingUOMId, options => options.MapFrom(x => x.OrderingUOMId))
                .ForMember(dto => dto.RatePerOrderingQuantity, options => options.MapFrom(x => x.RatePerQuantity))
                .ForMember(dto => dto.StockQuantityPerOrderingUOM, options => options.MapFrom(x => x.QuantityPerOrderingUOM))
                .ForMember(x => x.ItemId, opt => opt.Ignore())
                .ReverseMap();

            configuration.CreateMap<ItemStorageCondition, ItemStorageConditionDto>().ReverseMap();
            configuration.CreateMap<ItemStorageCondition, ItemStorageConditionInputDto>().ReverseMap();
            
            configuration.CreateMap<ItemSupplier, ItemSupplierDto>().ReverseMap();
            configuration.CreateMap<ItemSupplier, ItemSupplierInputDto>().ReverseMap();

            configuration.CreateMap<Procurement, ProcurementDto>().ReverseMap();
            configuration.CreateMap<Procurement, ProcurementInputDto>().ReverseMap();

            configuration.CreateMap<ItemSpare, ItemSpareDto>().ReverseMap();
            configuration.CreateMap<ItemSpare, ItemSpareInputDto>().ReverseMap();

            configuration.CreateMap<ItemAccessory, ItemAccessoryDto>().ReverseMap();
            configuration.CreateMap<ItemAccessory, ItemAccessoryInputDto>().ReverseMap();

            configuration.CreateMap<ItemAccessory, ItemAccessoryDto>().ReverseMap();
            configuration.CreateMap<ItemAccessory, ItemAccessoryInputDto>().ReverseMap();

            configuration.CreateMap<Assembly, AssemblyInputDto>().ReverseMap();
            configuration.CreateMap<Assembly, AssemblyDto>().ReverseMap();
            configuration.CreateMap<Assembly, AssemblyListDto>()
                .ForMember(dto => dto.ModelName, options => options.MapFrom(x => x.Model.Name))
                .ReverseMap();

            configuration.CreateMap<SubAssembly, SubAssemblyInputDto>().ReverseMap();
            configuration.CreateMap<SubAssembly, SubAssemblyDto>().ReverseMap();
            configuration.CreateMap<SubAssembly, SubAssemblyListDto>()
                .ForMember(dto => dto.ModelName, options => options.MapFrom(x => x.Model.Name))
                .ForMember(dto => dto.AssemblyName, options => options.MapFrom(x => x.Assembly.Name))
                .ReverseMap();


            configuration.CreateMap<SubAssemblyItem, SubAssemblyItemInputDto>().ReverseMap();
            configuration.CreateMap<SubAssemblyItem, SubAssemblyItemDto>()
                .ForMember(dto => dto.SubAssemblyName, options => options.MapFrom(x => x.SubAssembly.Name))
                .ForMember(dto => dto.ItemName, options => options.MapFrom(x => x.Item.ItemName))
                .ReverseMap();

            configuration.CreateMap<ItemCategory, ItemCategoryDto>().ReverseMap();
            configuration.CreateMap<ItemCategory, ItemCategoryInputDto>().ReverseMap();

            configuration.CreateMap<ModelWiseItemMaster, ModelWiseItemMasterListDto>()
                .ForMember(dto => dto.ModelName, options => options.MapFrom(x => x.Model.Name))
                .ReverseMap();

            configuration.CreateMap<ModelWiseItemMaster, ModelWiseItemMasterDto>().ReverseMap();
            configuration.CreateMap<ModelWiseItemMaster, ModelWiseItemMasterInputDto>().ReverseMap();
            configuration.CreateMap<ModelWiseItemMaster, ModelWiseItemMasterListDto>()
                .ForMember(dto => dto.ModelName, options => options.MapFrom(x => x.Model.Name))
                .ForMember(dto => dto.Id, options => options.MapFrom(x => x.Id))
                .ReverseMap();


            configuration.CreateMap<ModelWiseItem, ModelWiseItemDto>().ReverseMap();
            configuration.CreateMap<ModelWiseItem, ModelWiseItemInputDto>().ReverseMap();

            configuration.CreateMap<MaterialRequisition, MaterialRequisitionMasterListDto>().ReverseMap();
            configuration.CreateMap<MaterialRequisition, MaterialRequisitionInputDto>().ReverseMap();
            configuration.CreateMap<MaterialRequisition, MaterialRequisitionDto>().ReverseMap();

            configuration.CreateMap<MaterialRequisitionItem, MaterialRequisitionItemDto>().ReverseMap();
            configuration.CreateMap<MaterialRequisitionItem, MaterialRequisitionItemInputDto>().ReverseMap();

            /* ADD YOUR OWN CUSTOM AUTOMAPPER MAPPINGS HERE */
        }
    }
}
