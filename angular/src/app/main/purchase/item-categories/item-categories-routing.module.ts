import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ItemCategoriesComponent } from './item-category-list/item-categories.component';

const routes: Routes = [
    {
        path: '',
        component: ItemCategoriesComponent,
        pathMatch: 'full',
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class ItemCategoriesRoutingModule {}
