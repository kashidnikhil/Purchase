import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SupplierCategoriesComponent } from './supplier-category-list/supplier-categories.component';

const routes: Routes = [
    {
        path: '',
        component: SupplierCategoriesComponent,
        pathMatch: 'full',
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class SupplierCategoriesRoutingModule {}
