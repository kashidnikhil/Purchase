import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                children: [
                    {
                        path: 'dashboard',
                        loadChildren: () => import('./dashboard/dashboard.module').then((m) => m.DashboardModule),
                        data: { permission: 'Pages.Tenant.Dashboard' },
                    },
                    {
                        path: 'customer',
                        loadChildren: () => import('./customer/customer.module').then(m => m.customerModule)
                    },
                    {
                        path: 'units',
                        loadChildren: () => import('./purchase/units/units.module').then(m => m.UnitsModule)
                    }, 
                    {
                        path: 'legal-entities',
                        loadChildren: () => import('./purchase/legal-entities/legal-entities.module').then(m => m.LegalEntitiesModule)
                    }, 
                    {
                        path: 'acceptance-criterias',
                        loadChildren: () => import('./purchase/acceptance-criterias/acceptance-criterias.module').then(m => m.AcceptanceCriteriasModule)
                    }, 
                    {
                        path: 'delivery-terms',
                        loadChildren: () => import('./purchase/delivery-terms/delivery-terms.module').then(m => m.DeliveryTermsModule)
                    }, 
                    {
                        path: 'material-grades',
                        loadChildren: () => import('./purchase/material-grades/material-grades.module').then(m => m.MaterialGradesModule)
                    }, 
                    {
                        path: 'po-general-terms',
                        loadChildren: () => import('./purchase/po-general-terms/po-general-terms.module').then(m => m.POGeneralTermsModule)
                    }, 
                    {
                        path: 'supplier-categories',
                        loadChildren: () => import('./purchase/supplier-categories/supplier-categories.module').then(m => m.SupplierCategoriesModule)
                    }, 
                    {
                        path: 'terms-of-payments',
                        loadChildren: () => import('./purchase/terms-of-payments/terms-of-payments.module').then(m => m.TermsOfPaymentsModule)
                    },
                    {
                        path: 'suppliers',
                        loadChildren: () => import('./purchase/suppliers/suppliers.module').then(m => m.SuppliersModule)
                    },
                    {
                        path: 'models',
                        loadChildren: () => import('./purchase/models/models.module').then(m => m.ModelsModule)
                    },
                    {
                        path: 'companies',
                        loadChildren: () => import('./purchase/companies/companies.module').then(m => m.CompaniesModule)
                    },
                    {
                        path: 'item-masters',
                        loadChildren: () => import('./purchase/item-masters/item-masters.module').then(m => m.ItemMastersModule)
                    },
                    {
                        path: 'assembly-masters',
                        loadChildren: () => import('./purchase/assembly-masters/assembly-masters.module').then(m => m.AssemblyMastersModule)
                    },
                    {
                        path: 'sub-assembly-items',
                        loadChildren: () => import('./purchase/sub-assembly-items/sub-assembly-items.module').then(m => m.SubAssemblyItemsModule)
                    },
                    {
                        path: 'item-categories',
                        loadChildren: () => import('./purchase/item-categories/item-categories.module').then(m => m.ItemCategoriesModule)
                    },
                    {
                        path: 'model-wise-items',
                        loadChildren: () => import('./purchase/model-wise-items/model-wise-items.module').then(m => m.ModelWiseItemsModule )
                    },
                    {
                        path: 'transactions',
                        loadChildren: () => import('./purchase/transactions/transactions.module').then(m => m.TransactionsModule ) 
                    },
                       
                                      
                    { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
                    { path: '**', redirectTo: 'dashboard' },
                ],
            },
        ]),
    ],
    exports: [RouterModule],
})
export class MainRoutingModule {}
