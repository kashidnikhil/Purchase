import { NgModule } from "@angular/core";
import { AppSharedModule } from "@app/shared/app-shared.module";
import { SubheaderModule } from "@app/shared/common/sub-header/subheader.module";
import { TermsOfPaymentsRoutingModule } from "./terms-of-payments-routing.module";

@NgModule({
    declarations: [
    ],
    imports: [
        AppSharedModule,  
        TermsOfPaymentsRoutingModule,
        SubheaderModule,
    ],
    providers: [],
})

export class TermsOfPaymentsModule {}