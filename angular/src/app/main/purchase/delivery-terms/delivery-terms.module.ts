import { NgModule } from "@angular/core";
import { AppSharedModule } from "@app/shared/app-shared.module";
import { SubheaderModule } from "@app/shared/common/sub-header/subheader.module";
import { DeliveryTermsRoutingModule } from "./delivery-terms-routing.module";

@NgModule({
    declarations: [
    ],
    imports: [
        AppSharedModule,  
        DeliveryTermsRoutingModule,
        SubheaderModule,
    ],
    providers: [],
})

export class DeliveryTermsModule {}