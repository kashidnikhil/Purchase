import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
    // {
    //     path: '',
    //     component: SupplierCategoriesComponent,
    //     pathMatch: 'full',
    // },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class SuppliersRoutingModule {}
