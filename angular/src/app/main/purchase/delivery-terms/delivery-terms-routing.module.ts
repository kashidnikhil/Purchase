import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DeliveryTermsComponent } from './delivery-term-list/delivery-terms.component';

const routes: Routes = [
    {
        path: '',
        component: DeliveryTermsComponent,
        pathMatch: 'full',
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class DeliveryTermsRoutingModule {}
