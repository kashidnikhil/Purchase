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
                        path: 'phonebook',
                        loadChildren: () => import('./Phonebook/phonebook.module').then(m => m.PhoneBookModule),
                        data: { permission: 'Pages.Tenant.PhoneBook' }
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
                       
                                      
                    { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
                    { path: '**', redirectTo: 'dashboard' },
                ],
            },
        ]),
    ],
    exports: [RouterModule],
})
export class MainRoutingModule {}
