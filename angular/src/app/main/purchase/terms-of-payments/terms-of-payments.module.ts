import { NgModule } from "@angular/core";
import { AppSharedModule } from "@app/shared/app-shared.module";
import { SubheaderModule } from "@app/shared/common/sub-header/subheader.module";
import { TermsOfPaymentsRoutingModule } from "./terms-of-payments-routing.module";
import { CreateOrEditTermsOfPaymentModalComponent } from "./create-edit-terms-of-payment/create-or-edit-terms-of-payment-modal.component";
import { TermsOfPaymentsComponent } from "./terms-of-payment-list/terms-of-payments.component";

@NgModule({
    declarations: [
        TermsOfPaymentsComponent,
        CreateOrEditTermsOfPaymentModalComponent
    ],
    imports: [
        AppSharedModule,  
        TermsOfPaymentsRoutingModule,
        SubheaderModule,
    ],
    providers: [],
})

export class TermsOfPaymentsModule {}