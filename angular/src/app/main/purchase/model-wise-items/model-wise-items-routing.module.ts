import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ModelWiseItemsComponent } from './model-wise-item-list/model-wise-items.component';
const routes: Routes = [
    {
        path: '',
        component: ModelWiseItemsComponent,
        pathMatch: 'full',
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class ModelWiseItemsRoutingModule {}
