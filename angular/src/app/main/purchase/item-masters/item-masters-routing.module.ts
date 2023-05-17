import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ItemMasterListComponent } from './item-master-list/item-master-list.component';

const routes: Routes = [
    {
        path: '',
        component: ItemMasterListComponent,
        pathMatch: 'full',
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class ItemMastersRoutingModule {}
