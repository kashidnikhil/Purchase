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
                        loadChildren: () => import('./phonebook/phonebook.module').then(m => m.PhoneBookModule),
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
                                      
                    { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
                    { path: '**', redirectTo: 'dashboard' },
                ],
            },
        ]),
    ],
    exports: [RouterModule],
})
export class MainRoutingModule {}
