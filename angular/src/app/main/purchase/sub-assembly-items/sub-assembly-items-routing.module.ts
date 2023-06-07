import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SubAssemblyItemListComponent } from './sub-assembly-item-list/sub-assembly-item-list.component';

const routes: Routes = [
    {
        path: '',
        component: SubAssemblyItemListComponent,
        pathMatch: 'full',
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class SubAssemblyItemsRoutingModule {}
