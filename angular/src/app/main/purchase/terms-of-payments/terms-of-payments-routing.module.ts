import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TermsOfPaymentsComponent } from './terms-of-payment-list/terms-of-payments.component';

const routes: Routes = [
    {
        path: '',
        component: TermsOfPaymentsComponent,
        pathMatch: 'full',
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class TermsOfPaymentsRoutingModule {}
