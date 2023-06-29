import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { TermsOfPaymentsComponent } from "../terms-of-payments/terms-of-payment-list/terms-of-payments.component";
import { MaterialRequisitionsComponent } from "./material-requisitions/material-requisition-list/material-requisitions.component";

const routes: Routes = [
    {
        path: '',
        component: TermsOfPaymentsComponent,
        pathMatch: 'full',
    },
    {
        path: 'material-requisitions',
        component: MaterialRequisitionsComponent,
        pathMatch: 'full',
    },
    {
        path: 'material-indentations',
        component: TermsOfPaymentsComponent,
        pathMatch: 'full',
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class TransactionsRoutingModule {}