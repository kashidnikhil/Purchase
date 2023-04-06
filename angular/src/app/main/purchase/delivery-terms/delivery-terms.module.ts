import { NgModule } from "@angular/core";
import { AppSharedModule } from "@app/shared/app-shared.module";
import { SubheaderModule } from "@app/shared/common/sub-header/subheader.module";
import { DeliveryTermsRoutingModule } from "./delivery-terms-routing.module";
import { DeliveryTermsComponent } from "./delivery-term-list/delivery-terms.component";
import { CreateOrEditDeliveryTermModalComponent } from "./create-edit-delivery-term/create-or-edit-delivery-term-modal.component";

@NgModule({
    declarations: [
        DeliveryTermsComponent,
        CreateOrEditDeliveryTermModalComponent
    ],
    imports: [
        AppSharedModule,  
        DeliveryTermsRoutingModule,
        SubheaderModule,
    ],
    providers: [],
})

export class DeliveryTermsModule {}